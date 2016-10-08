using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UsefulThings
{
    public class BezierSpline : MonoBehaviour
    {

        public List<Vector3> points;

        // Used for uniform curve evaluation
        private const int approximationResolution = 1000;
        [SerializeField]
        private List<Vector3> curveApproximation;
        [SerializeField]
        private List<float> cumulativeDistances;
        [SerializeField]
        private float totalDistance;

        public Vector3 Evaluate(float t, bool debugLog = false)
        {
            int segment = (int)t;
            int index = 3 * segment;

            float t0 = t - segment;
            float t1 = 1 - t0;

            Vector3 p0 = points[index++];
            Vector3 p1 = points[index++];
            Vector3 p2 = points[index++];
            Vector3 p3 = points[index++];

            if (debugLog)
            {
                Debug.Log(p0 + " " + p1 + " " + p2 + " " + p3 + " " + t + " " + t0 + " " + t1);
            }

            float a = t1 * t1 * t1;
            float b = t1 * t1 * t0 * 3;
            float c = t1 * t0 * t0 * 3;
            float d = t0 * t0 * t0;

            return a * p0 + b * p1 + c * p2 + d * p3;
        }

        public Vector3 Derivative(float t)
        {
            int segment = (int)t;
            int index = 3 * segment;

            float t0 = t - segment;
            float t1 = 1 - t0;

            Vector3 p0 = points[index++];
            Vector3 p1 = points[index++];
            Vector3 p2 = points[index++];
            Vector3 p3 = points[index++];

            float a = t1 * t1;
            float b = t1 * t0;
            float c = t0 * t0;

            return a * (p1 - p0) + b * (p2 - p1) + c * (p3 - p2);
        }

        /// <summary>
        /// Recomputes the curve approximation used for uniform sampling. Avoid calling this often, since this is slow.
        /// </summary>
        public void RecomputeApproximateCurve()
        {
            curveApproximation = new List<Vector3>();
            cumulativeDistances = new List<float>();

            int numSegments = points.Count / 3;
            Vector3 prevPoint = Vector3.zero;
            float cumulativeDistance = 0;

            for (int i = 0; i < approximationResolution; i++)
            {
                float t = i * ((float)numSegments / approximationResolution);
                Vector3 p = Evaluate(t);
                curveApproximation.Add(p);

                if (i > 0)
                {
                    cumulativeDistance += (p - prevPoint).magnitude;
                }
                cumulativeDistances.Add(cumulativeDistance);

                prevPoint = p;
            }

            totalDistance = cumulativeDistance + (prevPoint - points[points.Count - 1]).magnitude;
        }

        public Vector3 EvaluateUniform(float t)
        {
            int numSegments = points.Count / 3;
            float distance = t / numSegments * totalDistance;
            int index = cumulativeDistances.BinarySearch(distance);

            if (index < 0)
            {
                index = ~index - 1;

                if (index < 0)
                {
                    index = 0;
                }


                if (index < cumulativeDistances.Count - 1)
                {
                    float distancePercent = (distance - cumulativeDistances[index]) / (cumulativeDistances[index + 1] - cumulativeDistances[index]);
                    return Vector3.Lerp(curveApproximation[index], curveApproximation[index + 1], distancePercent);
                }
                else
                {
                    float distancePercent = (distance - cumulativeDistances[index]) / (totalDistance - cumulativeDistances[index]);
                    return Vector3.Lerp(curveApproximation[index], points[points.Count - 1], distancePercent);
                }
            }
            else
            {
                return curveApproximation[index];
            }
        }
        
        public Vector3 DerivativeUniform(float t)
        {
            int numSegments = points.Count / 3;
            float distance = t / numSegments * totalDistance;
            int index = cumulativeDistances.BinarySearch(distance);

            if (index < 0)
            {
                index = ~index - 1;
            }

            if (index < 0)
            {
                index = 0;
            }

            if (index < cumulativeDistances.Count - 1)
            {
                return curveApproximation[index + 1] - curveApproximation[index];
            }
            else
            {
                return points[points.Count - 1] - curveApproximation[index];
            }
        }

        // EDITOR ONLY
#if UNITY_EDITOR

        public enum ContinuityType
        {
            NONE, TANGENTS, TANGENT_MAGNITUDES
        }
        public Color curveColor = Color.green;
        public Color tangentColor = Color.white;
        public Color uniformSamplingColor = Color.blue;
        public List<ContinuityType> continuity;

        void Reset()
        {
            points = new List<Vector3>();
            points.Add(new Vector3(0, 0, 1));
            points.Add(new Vector3(0, 0, 2));
            points.Add(new Vector3(0, 0, 3));
            points.Add(new Vector3(0, 0, 4));

            continuity = new List<ContinuityType>();

            continuity.Add(ContinuityType.NONE);
            continuity.Add(ContinuityType.NONE);
            continuity.Add(ContinuityType.NONE);
            continuity.Add(ContinuityType.NONE);

            RecomputeApproximateCurve();
        }

        public void AddSegment()
        {
            Vector3 lastPoint = points[points.Count - 1];
            points.Add(lastPoint + new Vector3(0, 0, 1));
            points.Add(lastPoint + new Vector3(0, 0, 2));
            points.Add(lastPoint + new Vector3(0, 0, 3));

            continuity.Add(ContinuityType.NONE);
            continuity.Add(ContinuityType.NONE);
            continuity.Add(ContinuityType.NONE);

            RecomputeApproximateCurve();
        }

        public void RemoveSegment()
        {
            points.RemoveRange(points.Count - 3, 3);

            RecomputeApproximateCurve();
        }

        public void SetControlPoint(int index, Vector3 value)
        {
            Vector3 oldPosition = points[index];
            Vector3 offset = value - oldPosition;

            if (index % 3 == 0)
            {
                points[index] = value;
                if (index + 1 < points.Count)
                {
                    points[index + 1] += offset;
                }
                if (index - 1 >= 0)
                {
                    points[index - 1] += offset;
                }
            }
            else
            {
                int cornerIndex = index % 3 == 1 ? index - 1 : index + 1;
                int oppositeIndex = index % 3 == 1 ? index - 2 : index + 2;

                Vector3 fromCorner = value - points[cornerIndex];

                points[index] = value;
                switch (continuity[cornerIndex])
                {
                    case ContinuityType.NONE:
                        break; // Do Nothing
                    case ContinuityType.TANGENTS:
                        if (oppositeIndex >= 0 && oppositeIndex < points.Count)
                        {
                            float magnitude = (points[oppositeIndex] - points[cornerIndex]).magnitude;
                            points[oppositeIndex] = points[cornerIndex] - fromCorner.normalized * magnitude;
                        }
                        break;
                    case ContinuityType.TANGENT_MAGNITUDES:
                        if (oppositeIndex >= 0 && oppositeIndex < points.Count)
                        {
                            points[oppositeIndex] = points[cornerIndex] - fromCorner;
                        }
                        break;
                }
            }
        }
#endif
    }
}