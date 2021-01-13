using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lr;
    public LayerMask grappleMask, playerLayer;
    public float moveSpeed, length;
    public int maxPoints;
    private Rigidbody2D rb;
    private List<Vector2> points;
    public bool grappled;
    public Transform firePoint;
    private Collider2D detectGrap;
    private Vector2 hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        points = new List<Vector2>();
        lr.positionCount = 0;
    }

    public void grapple()
    {
        //if(!grappled)
        //{
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2) firePoint.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, length, grappleMask);
            if(hit.collider != null)
            {
                hitPoint = hit.point;

                Vector2 moveTo = centroid(points.ToArray());

                rb.MovePosition(Vector2.MoveTowards(transform.position, hitPoint, moveSpeed * Time.deltaTime));
                //transform.position = Vector3.Lerp(transform.position, hitPoint, moveSpeed);

                lr.positionCount = 2;
                lr.SetPosition(0, firePoint.position);
                lr.SetPosition(1, hitPoint);

                rb.gravityScale = 0;
                
                detectGrap = Physics2D.OverlapCircle(hitPoint, 1f, playerLayer);
                if(detectGrap != null)
                    grappled = true;
            }
        //}
    }

    public void Detach()
    {
        points.Clear();
        lr.positionCount = 0;
        rb.gravityScale = 13;
        detectGrap = null;
        grappled = false;
    }

    private Vector2 centroid(Vector2[] points)
    {
        Vector2 centroid = Vector2.zero;

        foreach(Vector2 point in points)
            centroid += point;

        centroid /= points.Length;

        return centroid;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hitPoint, 1f);
    }
}
