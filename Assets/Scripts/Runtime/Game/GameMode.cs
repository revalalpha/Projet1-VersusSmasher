using UnityEngine;
using Unity.Cinemachine;

public class GameMode : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject m_playerControllerPrefab;
    [SerializeField] private GameObject m_playerCharacterPrefab;
    [SerializeField] private GameObject m_enemyCharacterPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform m_playerSpawnPoint;
    [SerializeField] private Transform m_enemySpawnPoint;

    [Header("Camera")]
    [SerializeField] private CinemachineCamera m_mainCamera;

    private EnemyCharacter m_enemy;
    public EnemyCharacter Enemy => m_enemy;

    private PlayerCharacter m_player;
    public PlayerCharacter Player => m_player;

    public void SetPlayer(PlayerCharacter player)
    {
        m_player = player;
    }


    private void Start()
    {
        SpawnCharacters();
        LinkCharacters();
    }

    private void SpawnCharacters()
    {
        GameObject playerControllerObj = Instantiate(m_playerControllerPrefab);
        PlayerController playerController = playerControllerObj.GetComponent<PlayerController>();

        GameObject playerCharacterObj = Instantiate(m_playerCharacterPrefab, m_playerSpawnPoint.position, Quaternion.identity);
        m_player = playerCharacterObj.GetComponent<PlayerCharacter>();

        playerController.SetPlayerCharacter(m_player);

        m_mainCamera.Target.TrackingTarget = m_player.transform;

        GameObject enemyObj = Instantiate(m_enemyCharacterPrefab, m_enemySpawnPoint.position, Quaternion.identity);
        m_enemy = enemyObj.GetComponent<EnemyCharacter>();
    }

    private void LinkCharacters()
    {
        m_player.SetEnemy(m_enemy);
        m_enemy.SetPlayer(m_player);
    }
}