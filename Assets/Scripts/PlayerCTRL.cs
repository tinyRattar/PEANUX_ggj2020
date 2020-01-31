using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerState
{
    idle,
    work,
    repair
}

public class PlayerCTRL : MonoBehaviour
{
    public static PlayerCTRL instance;
    [SerializeField] int money;
    [SerializeField] float moveSpeed = 1.0f;
    playerState state = playerState.idle;
    List<InteractiveEntity> attachedEntities = new List<InteractiveEntity>();

    public void AddMoney(int value)
    {
        this.money += value;
    }

    public int GetMoney() { return this.money; }

    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (var item in attachedEntities)
            {
                item.OnInteract(KeyCode.E);
            }
        }

        Vector2 dV = new Vector2(dx, dy);
        this.transform.Translate(dV * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "interact")
        {
            attachedEntities.Add(collision.GetComponent<InteractiveEntity>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "interact")
        {
            attachedEntities.Remove(collision.GetComponent<InteractiveEntity>());
        }
    }
}
