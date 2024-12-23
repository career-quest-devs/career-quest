using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenElevator : MonoBehaviour
{
    private Animator _elevatorAnimator;
    [SerializeField] private GameObject _levelEndCheckPoint;
    // Start is called before the first frame update
    void Start()
    {
        _elevatorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        _elevatorAnimator.SetTrigger("Open");
        StartCoroutine(addExitTrigger());
    }

    IEnumerator addExitTrigger()
    {
        yield return new WaitForSeconds(3f);
        _levelEndCheckPoint.SetActive(true);
    }
}
