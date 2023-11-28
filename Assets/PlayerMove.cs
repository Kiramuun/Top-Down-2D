using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    Vector2 _movementInput;
    public Animator _animatorRef;
    Rigidbody2D _rigidB;
    public float _speed;
    UIManager _managerUI;

    void Awake()
    {
        _rigidB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"),
              vertical = Input.GetAxisRaw("Vertical");

        _movementInput = new Vector2(horizontal, vertical).normalized;

        /*if( _movementInput.sqrMagnitude > _speed)
        {
            _animatorRef.SetFloat("dirX", orientation.x);
            _animatorRef.SetFloat("dirY", orientation.y);
        }*/

        //Le personnage fais une roulade !
        if (Input.GetButtonDown("Jump") && !_animatorRef.GetBool("touchRoll"))
        {
            _animatorRef.SetBool("touchRoll", true);
        }

        //Fais tournez le personnage sur lui meme !
        if (Input.GetButtonDown("Turn") && !_animatorRef.GetBool("touchTurn"))
        {
            _animatorRef.SetBool("touchTurn", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) { _speed *= 2f; }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { _speed /= 2f; }

        _animatorRef.SetFloat("Horizontal", horizontal);
        _animatorRef.SetFloat("Vertical", vertical);

        Menu();
    }

    void FixedUpdate()
    {
        Vector2 velocity = _movementInput * _speed;
        _rigidB.velocity = velocity;

        _animatorRef.SetFloat("speedMove",Mathf.Abs(_rigidB.velocity.magnitude));
    }

    void Menu()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("UI Menu", LoadSceneMode.Single);
        }
    }
}
