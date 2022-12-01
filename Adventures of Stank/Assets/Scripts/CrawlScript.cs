using UnityEngine;
using UnityEngine.SceneManagement;

public class CrawlScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeScene", 30);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, -11);

    }
    void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}
