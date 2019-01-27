using UnityEngine;

public class Note : BaseEntity
{
    public Vector3[] paths;
    public float spd;
    private Vector3 m_tp;
    private int m_index;

    public void Start()
    {
        transform.position = paths[0];
        m_index++;
        m_tp = paths[m_index];
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_tp, spd * Time.deltaTime);
        if (transform.position == m_tp)
        {
            m_index++;

            if (m_index >= paths.Length)
            {
                Destroy();
                return;
            }

            m_tp = paths[m_index];
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Luggage"))
        {
            if (c.GetComponent<Luggage>().onPick)
                return;

            c.GetComponent<Luggage>().PlayMelody();
        }

        if (c.CompareTag("Through"))
            return;

        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}