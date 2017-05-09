using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

namespace GGL.Examples
{
    [System.Serializable]
    public class CircleSettings
    {
        public Vector3 position = new Vector3(-5, 0, 0);
        public Vector3 up = Vector3.forward;
        public float radius = 0.5f;
        public int segments = 16;
        public Color color = Color.red;
    }

    [System.Serializable]
    public class SquareSettings
    {
        public Vector3 position = new Vector3(0, 0, 0);
        public Vector3 up = Vector3.forward;
        public Vector2 scale = Vector3.one;
        public Color color = Color.green;
    }

    [System.Serializable]
    public class CubeSettings
    {
        public Vector3 position = new Vector3(5, 0, 0);
        public Vector3 scale = Vector3.one * 0.5f;
        public Color color = Color.blue;
    }

    [System.Serializable]
    public class SphereSettings
    {
        public Vector3 position = new Vector3(-5, 5, 0);
        public float radius = 0.5f;
        public int rows = 16;
        public int columns = 16;
        public Color color = Color.cyan;
        public float longMin = 0f;
        public float longMax = 360f;
        public float latMin = -90f;
        public float latMax = 90f;
    }

    [System.Serializable]
    public class CylinderSettings
    {
        public Vector3 position = new Vector3(0, 5, 0);
        public Vector3 up = Vector3.forward;
        public float radius = 0.5f;
        public float halfLength = 1f;
        public int segments = 16;
        public Color color = Color.magenta;
    }

    [System.Serializable]
    public class RingSettings
    {
        public Vector3 position = new Vector3(5, 5, 0);
        public Vector3 up = Vector3.up;
        public float innerRadius = 0.5f;
        public float outerRadius = 1;
        public int segments = 16;
        public Color color = Color.yellow;
    }

    [System.Serializable]
    public class ArcSettings
    {
        public Vector3 position = new Vector3(-2.5f, 10, 0);
        public Vector3 up = Vector3.up;
        public float radius = 1f;
        public float rotation = 1f;
        public float halfAngle = 90f;
        public int segments = 16;
        public Color color = Color.gray;
    }


    [System.Serializable]
    public class LineSettings
    {
        public Vector3 start = new Vector3(2.5f, 10, 0);
        public Vector3 end = new Vector3(0, 15, 0);
        public float startWidth = 0.5f;
        public float endWidth = 0.5f;
        public Color startColor = Color.red;
        public Color endColor = Color.blue;
    }
    
    public class ExampleScript : MonoBehaviour
    {
        public CircleSettings circle;
        public SquareSettings square;
        public CubeSettings cube;
        public SphereSettings sphere;
        public CylinderSettings cylinder;
        public RingSettings ring;
        public ArcSettings arc;
        public LineSettings line;
        
        // Update is called once per frame
        void Update()
        {
            GizmosGL.AddCircle(circle.position, circle.up, circle.radius, circle.segments, circle.color);
            GizmosGL.AddSquare(square.position, square.up, square.scale, square.color);
            GizmosGL.AddCube(cube.position, cube.scale, cube.color);
            GizmosGL.AddSphere(sphere.position, sphere.radius, sphere.rows, sphere.columns, sphere.color, sphere.longMin, sphere.longMax, sphere.latMin, sphere.latMax);
            GizmosGL.AddCylinder(cylinder.position, cylinder.up, cylinder.radius, cylinder.halfLength, cylinder.segments, cylinder.color);
            GizmosGL.AddRing(ring.position, ring.up, ring.innerRadius, ring.outerRadius, ring.segments, ring.color);
            GizmosGL.AddArc(arc.position, arc.up, arc.radius, arc.rotation, arc.halfAngle, arc.segments, arc.color);
            GizmosGL.AddLine(line.start, line.end, line.startWidth, line.endWidth, line.startColor, line.endColor);


            for (int i = 0; i < 50; i++)
            {
                GizmosGL.AddSphere(new Vector3(Mathf.Sin(i) * 10, i * 10, Mathf.Cos(i) * 10), 1, 16, 16, Color.red);
            }
        }
    }
}