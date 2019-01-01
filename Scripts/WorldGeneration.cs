using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public static int radius = 50;
    Vector3 center = new Vector3(0,0,0);

    public int resolution = 3;
    int count = 0;
    Dictionary <Vector3, Point> points;

    List <Point> vertices;
    List <Mesh> meshes = new List<Mesh>();

    void Start(){
        vertices = new List<Point>();
        points = new Dictionary<Vector3, Point>();
        for (int x = 1; x >= -1; x-=2)
        {
            for (int y = 1; y >= -1; y-=2)
            {
                for (int z = 1; z >= -1; z-=2)
                {
                    
                    Vector3 dir = new Vector3(x,y,z);
                    float height = Random.Range(-1f,1f);
                    Point point = new Point(dir, height);
                    vertices.Add(point);
                    points[dir] = point;
                }
            }
        }

        cubeToSphere();
        setMesh();
        
    }



    void cubeToSphere(){   
        quadToSphere(
            vertices[0], 
            vertices[1], 
            vertices[3], 
            vertices[2], 
            1
        );
         quadToSphere(
            vertices[6], 
            vertices[7], 
            vertices[5], 
            vertices[4], 
            1
        ); 

        quadToSphere(
            vertices[4], 
            vertices[5], 
            vertices[1], 
            vertices[0], 
            1
        );
        quadToSphere(
            vertices[2], 
            vertices[3], 
            vertices[7], 
            vertices[6], 
            1
        );
        quadToSphere(
            vertices[1], 
            vertices[5], 
            vertices[7], 
            vertices[3], 
            1
        );
        quadToSphere(
            vertices[2], 
            vertices[6], 
            vertices[4], 
            vertices[0], 
            1
        ); 
    }

    void setMesh(){
        var combine = new CombineInstance[meshes.Count];
        int i = 0;
        foreach (var meshItem in meshes)
        {
            combine[i].mesh = meshItem;
            i++;
        }
        
        var mesh = new Mesh();
        mesh.CombineMeshes(combine, true, false);
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    void quadToSphere(Point lu, Point ru, Point rd, Point ld, float step){
        if (step>16){
            return;
        }
        Point mu = getPoint((lu.d + ru.d)/2, (lu.h+ru.h)/2);
        Point mr = getPoint((ru.d + rd.d)/2, (ru.h+rd.h)/2);

        Point md = getPoint((rd.d + ld.d)/2, (rd.h+ld.h)/2);
        Point ml = getPoint((ld.d + lu.d)/2, (ld.h+lu.h)/2);

        Vector3 dir = (lu.d + ru.d + rd.d + ld.d)/4;
        Point mm = getPoint(dir, (lu.h + ru.h + rd.h + ld.h)/4+Random.Range(-10, 10)*(lu.d-ru.d).magnitude);
  

        if (step >= 16){
            meshes.Add(Triangle(mm, mu, lu));
            meshes.Add(Triangle(lu, ml, mm));

            meshes.Add(Triangle(mr, ru, mu));
            meshes.Add(Triangle(mu, mm, mr));

            meshes.Add(Triangle(md, mm, ml));
            meshes.Add(Triangle(ml, ld, md));

            meshes.Add(Triangle(rd, mr, mm));
            meshes.Add(Triangle(mm, md, rd));  
        }

        quadToSphere(lu, mu, mm, ml, step*2);
        quadToSphere(mu, ru, mr, mm, step*2);
        quadToSphere(ml, mm, md, ld, step*2);
        quadToSphere(mm, mr, rd, md, step*2);
    }
    
    Point getPoint(Vector3 dir, float height){
        if (points.ContainsKey(dir))
            return points[dir];
        else {
            Point point = new Point(dir, height);
            points.Add(dir, point);
            return point;
        }
    }
    Mesh Triangle(Point p1, Point p2, Point p3)
    {   
        Vector3 v1, v2, v3;
        v1 = p1.p;
        v2 = p2.p;
        v3 = p3.p;

        
        var normal = Vector3.Cross((v2 - v1), (v3 - v1)).normalized;
        var mesh = new Mesh
        {
            vertices = new [] {v1, v2, v3},
            normals = new [] {normal, normal, normal},
            uv = new [] {getUv(v1), getUv(v2), getUv(v3) },
            triangles = new [] {0, 1, 2}
        };
        return mesh;
    }

    Vector2 getUv(Vector3 pos){
        Vector3 n = pos.normalized;
        if (n.z<0){
            n = new Vector3(n.x, n.y, -n.z);
        }
        float u = (float)(Mathf.Atan2(n.x, n.z) / (-2*Mathf.PI));
        float v = Mathf.Asin(n.y) / Mathf.PI + 0.5f;
        return new Vector2(u, v);
    }
}

class Point {
    public float h;
    public Vector3 p;
    public Vector3 d;

    public Point(Vector3 dir, float height){
        h = height;
        if (dir.magnitude!=1){
            dir = dir.normalized;
        }
        p = dir*(WorldGeneration.radius+height);    
        d = dir;
    }
} 
