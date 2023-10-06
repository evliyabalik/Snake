using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] GameObject m_tailPrefab;

    [SerializeField] float m_moveSpeed = 1;
    [SerializeField] Vector2 m_direction = Vector2.down;
    List<Transform> m_snake = new List<Transform>();
    [SerializeField] float m_offset;


    Vector2 m_areaLimit = new Vector2(12, 23);

    private void Start()
    {
        StartCoroutine(Move());

        m_snake.Add(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            if (m_direction != Vector2.right)
            {
                m_direction = Vector2.left;


            }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            if (m_direction != Vector2.left)
            {
                m_direction = Vector2.right;


            }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            if (m_direction != Vector2.down)
            {
                m_direction = Vector2.up;


            }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            if (m_direction != Vector2.up)
            {
                m_direction = Vector2.down;


            }

        

    }

    void TailTransform()
    {
        
    }

    IEnumerator Move()
    {
        while (true)
        {

            for (int i = m_snake.Count - 1; i > 0; i--)
            {
                m_snake[i].position = m_snake[i - 1].position - ((Vector3)m_direction/m_offset);
            }

            var position = transform.position;
            position += (Vector3)m_direction;
            position.x = Mathf.RoundToInt(position.x);
            position.y = Mathf.RoundToInt(position.y);
            transform.position = position;


            yield return new WaitForSeconds(m_moveSpeed);
        }

    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

    private void Grow(GameObject obj)
    {
        var tail = Instantiate(m_tailPrefab);
        m_snake.Add(tail.transform);

        m_snake[m_snake.Count - 1].position = m_snake[m_snake.Count - 2].position-(Vector3)m_direction;
        obj.transform.position = new Vector2(Mathf.RoundToInt(Random.Range(1, m_areaLimit.x)), Mathf.RoundToInt(Random.Range(1, m_areaLimit.y)));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            GameOver();
        }

        if (collision.CompareTag("Food"))
        {
            Grow(collision.gameObject);
        }
    }


}
