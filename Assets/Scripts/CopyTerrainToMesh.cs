using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CopyTerrainToMesh : MonoBehaviour
{
    public enum Axis{ X, Y, Z};

    public Terrain sourceTerrain;
    public MeshFilter targetMesh;
    public Axis meshUp = Axis.Y;

    // Start is called before the first frame update
    void Start()
    {
        CopyTerrain();
    }
    public void PrintHeightAtSamplePoint(Vector3 pos)
    {
        float terrainSampleHeight = sourceTerrain.SampleHeight(pos);
        Debug.Log("Height: "+ terrainSampleHeight);
    }

    public void CopyTerrain()
    {
        Mesh mesh = targetMesh.mesh;
        List<Vector3> newVertices = new List<Vector3>();
        foreach (Vector3 vertex in mesh.vertices)
        {
            Vector3 vertexWorldPosition = targetMesh.transform.localToWorldMatrix * vertex;
            Vector3 newVertex = vertex;
            float terrainSampleHeight = sourceTerrain.SampleHeight(vertexWorldPosition);
            switch (meshUp)
            {
                case Axis.X:
                    newVertex.x = terrainSampleHeight;
                    break;
                case Axis.Y:
                    newVertex.y = terrainSampleHeight;
                    break;
                case Axis.Z:
                    newVertex.z = terrainSampleHeight;
                    break;

            }
            newVertices.Add(newVertex);
        }

        int[] newTraingles = mesh.triangles;
        Vector2[] newUV = mesh.uv;

        mesh.Clear();
        mesh.SetVertices(newVertices);
        mesh.SetTriangles(newTraingles, 0);
        mesh.SetUVs(0, newUV);
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();
    }

}
