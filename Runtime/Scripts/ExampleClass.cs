/* ExampleClass.cs
 *
 * Copyright (C) 2021, University of Minnesota
 * Authors: Bridger Herman <herma582@umn.edu>, 
 *   Dan Keefe <dfk@umn.edu> -- updated 7/2022 to include rotation and use in an example scene
 *
 */

using UnityEngine;


// Good practice to use a namespace to avoid naming conflicts with other packages.
namespace IVLab.Template
{
    /// <summary>
    /// Simple example of what can be done in C#. This text is an XML comment,
    /// and will show up nicely formatted in the documentation associated with
    /// this class.
    /// </summary>
    public class ExampleClass : MonoBehaviour
    {
        /// <summary>
        /// Sets the speed of rotation in degrees per second.
        /// </summary>
        public float RotationSpeed {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        /// <summary>
        /// Read-only access to the current rotation angle in degrees.
        /// </summary>
        public float RotationAngle {
            get { return m_Angle; }
        }


        /// <summary>
        /// Returns the amount of rotation that occurs over a specified unit of time given the
        /// current rotation speed.
        /// </summary>
        /// <param name="deltaT">The length in seconds of the time interval.</param>
        /// <returns>The change in rotation in degrees over the period deltaT.</returns>
        /// <example><code>
        /// float t0 = 0.0f;  // time t0
        /// float a0 = 0.0f;  // angle at time t0
        /// float t1 = 10.0f; // time t1
        /// float a1 = RotationForDeltaTime(t1 - t0); // angle at time t1
        /// </code></example>
        public float RotationForDeltaTime(float deltaT)
        {
            return deltaT * m_Speed;
        }


        // Standard MonoBehaviour Update() method - will not get auto-documented because it's protected, not public.
        void Update()
        {
            m_Angle += RotationForDeltaTime(Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0.0f, m_Angle, 0.0f);
        }


        // private member variables -- do not appear in the documentation.

        // inital setting, exposed in the Unity editor
        [Tooltip("Speed of rotation in degrees per second.")]
        [SerializeField] private float m_Speed = 10.0f;

        // runtime only variable, not exposed in the editor
        private float m_Angle = 0.0f;
    }
}
