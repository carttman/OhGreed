using System;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public static Portal instance;
    [SerializeField] private Animator transitionAnim;

    public string targetSceneName;
    public bool startActive = true;  
    private bool isActive;

    public Vector3 _SpawnPoint;
    
    private void Start()
    {
        isActive = startActive; // 맵마다 설정해주기!!필수!!
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("포탈에 들어감! 다음 씬으로 이동할께!");
            StartCoroutine(LoadSceneAsync(_SpawnPoint));
        }
    }

    private System.Collections.IEnumerator LoadSceneAsync(Vector3 SpawnPoint)
    {
        Fader.Play();
        yield return new WaitForSeconds(Fader.Instance.duration);

        // transitionAnim.SetTrigger("End");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);
        ItemManager.Instance.Player.transform.position = SpawnPoint;

        var followCam = GameManager.Instance.FollowCamera.GetComponent<CinemachineCamera>();
        followCam.ForceCameraPosition(SpawnPoint, Quaternion.identity);

        yield return new WaitUntil(() => asyncLoad.isDone);
    }

    public void ActivePortal()
    {
        isActive = true;
        Debug.Log("포탈이 열렸습니다");
    }
    
}
