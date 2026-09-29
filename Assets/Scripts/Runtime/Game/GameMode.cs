using UnityEngine;
using Unity.Cinemachine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameObject m_playerControllerPrefab;
    [SerializeField] private GameObject m_playerCharacterPrefab;
    [SerializeField] private Transform m_spawnPoint;
    [SerializeField] private CinemachineCamera m_mainCamera;

    private void SpawnPlayer()
    {

        // Spawn the player controller
        GameObject playerControllerObj = Instantiate(m_playerControllerPrefab);
        PlayerController playerContoller = playerControllerObj.GetComponent<PlayerController>();
        // Spawn the player character
        GameObject playerCharacterObj = Instantiate(m_playerCharacterPrefab, m_spawnPoint.position, Quaternion.identity);
        PlayerCharacter playerCharacter = playerCharacterObj.GetComponent<PlayerCharacter>();

        playerContoller.SetPlayerCharacter(playerCharacter);
        m_mainCamera.Target.TrackingTarget = playerCharacter.transform;
    }

    private void Start()
    {
        SpawnPlayer();
    }

}