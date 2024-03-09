using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MyScript : MonoBehaviour
{
    public string filePath;

    private void Start()
    {
        LoadObj(filePath);
    }

    private void LoadObj(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("File not found: " + path);
            return;
        }

        var vertices = new List<Vector3>();
        var triangles = new List<int>();

        using (var sr = new StreamReader(path))
        {
            while (sr.ReadLine() is { } line)
            {
                if (line.StartsWith("v ")) // vertex
                {
                    var vertexData = line.Substring(2).Split(' ');
                    if (vertexData.Length < 3)
                    {
                        Debug.LogError("Invalid vertex data: " + line);
                        return;
                    }

                    var vertex = new Vector3(
                        float.Parse(vertexData[0]),
                        float.Parse(vertexData[1]),
                        float.Parse(vertexData[2]));
                    vertices.Add(vertex);
                }
                else if (line.StartsWith("f ")) // face
                {
                    var faceData = line.Substring(2).Split(' ');
                    if (faceData.Length < 3)
                    {
                        Debug.LogError("Invalid face data: " + line);
                        return;
                    }

                    // Create a fan of triangles for faces with more than 3 vertices (very inefficient)
                    for (var i = 1; i < faceData.Length - 1; i++)
                    {
                        triangles.Add(int.Parse(faceData[0].Split('/')[0]) - 1);
                        triangles.Add(int.Parse(faceData[i].Split('/')[0]) - 1);
                        triangles.Add(int.Parse(faceData[i + 1].Split('/')[0]) - 1);
                    }
                }
            }
        }

        var mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray()
        };
        mesh.RecalculateNormals();

        var obj = new GameObject("LoadedObj", typeof(MeshFilter), typeof(MeshRenderer));
        obj.GetComponent<MeshFilter>().mesh = mesh;

        // Assign a default material
        var material = new Material(Shader.Find("Standard"));
        obj.GetComponent<MeshRenderer>().material = material;
        
        
        obj.transform.parent = this.transform; // Set as child of the player object
        obj.transform.localPosition = Vector3.zero; // Optionally, reset local position to align with the player object
        obj.transform.localRotation = Quaternion.identity; // Optionally, reset local rotation

    }
}