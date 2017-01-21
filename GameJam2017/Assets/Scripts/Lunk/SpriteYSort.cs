using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteYSort : MonoBehaviour {

    //public static SortedList spritesToSort;
    public SpriteRenderer m_renderer;
    public int currentSortingOrder;

    [Header("Y OffSet")]

    public bool useYOffset = false;
    public float yOffset;

    [Header("Debug")]
    public bool debugLines;

    void OnEnable()
    {
        //TODO(xDura): may have to do this for all children spriteRenderers
        m_renderer = GetComponent<SpriteRenderer>();
        //if (!spritesToSort.Contains(m_renderer))
        //    spritesToSort.Add(m_renderer);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //TODO(xDura): Check cases where this can cause trouble
        float y = m_renderer.bounds.min.y;
        if (useYOffset)
            y += yOffset;

        currentSortingOrder = YToInt(y);

        if (m_renderer.isVisible)
            m_renderer.sortingOrder = currentSortingOrder;

        DrawDebugs();
	}

    public int YToInt(float y)
    {
        return (int)(-100 * y);
    }

    public void DrawDebugs()
    {

        if (debugLines)
        {
            Vector3 startPoint = Vector3.zero;
            Vector3 endPoint = Vector3.zero;

            startPoint.x = -1000;
            startPoint.y = m_renderer.bounds.min.y;

            endPoint.x = 1000;
            endPoint.y = m_renderer.bounds.min.y;

            if (useYOffset)
            {
                endPoint.y += yOffset;
                startPoint.y += yOffset;
            }

            Debug.DrawLine(startPoint, endPoint, Color.red);

            startPoint.y = currentSortingOrder;
            endPoint.y = currentSortingOrder;

            Debug.DrawLine(startPoint, endPoint, Color.blue);
        }
    }
}
