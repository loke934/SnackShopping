using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScenerySpawner : MonoBehaviour
{
    [Header("GRID")]
    [SerializeField, Range(10,30)]private int gridYSize = 22;
    [SerializeField, Range(10,30)]private int gridXSize = 20;
    [SerializeField,Range(0f,1f)] float gridDensity = 0.178f;
    [SerializeField, Range(0.1f,0.9f)]float obstaclesDensity = 0.286f;
    [SerializeField, Range(0f, 1f)] private float spawnWalkableTilesUnder = 0.752f;
    [SerializeField] private GameObject gridHolder;
    [SerializeField] private GameObject itemsHolder;

    [Header("STORE ITEMS")] 
    [SerializeField, Range(0f, 1f)] private float spawnItemsUnder = 0.3f;
    [SerializeField, Range(0.1f, 0.9f)] private float spawnStoreItemsUnder = 0.7f;
    
    [Header("PREFABS")]
    [SerializeField] private GameObject walkableTilePrefab;
    [SerializeField] private GameObject obstacleTilePrefab;
    [SerializeField] private GameObject storeItemsPrefab;
    [SerializeField] private GameObject badItemsPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Camera cameraPrefab;
    [SerializeField] private Music musicManagerPrefab;
    [SerializeField] private TimeManager timeManagerPrefab;
    [SerializeField] private Canvas canvasInScene;
    [SerializeField] private List<Sprite> itemsSprites = new List<Sprite>();
    
    private List<Vector2> _obstaclePositionsList; //So not to spawn items on not walkable tiles
    private List<Vector2> _walkablePositionsList; // So to only spawn items on walkable tiles
    private GameObject _playerInScene;
    private TimeManager _timeManagerInScene;
    private Music _musicManagerInScene;
    
    public GameObject[,] GridArray; //for movement only inside grid

    private void SpawnStoreSquareSize() {
        for (int y = 0; y < gridYSize; y++) {
            for (int x = 0; x < gridXSize; x++) {
                GameObject tileToSpawn = TileToSpawn(x, y);
                Vector3 spawnPosition = new Vector3(x,y);
                ItemBehaviour shelf = Instantiate(tileToSpawn, spawnPosition, Quaternion.identity)?
                    .GetComponent<ItemBehaviour>();
                //Setup in Colliders script
                if(!ReferenceEquals(shelf, null)) {
                    shelf.onPlaySound.AddListener(_musicManagerInScene.PlaySound);
                }
            }
        }
    }
    /// <summary>
    /// Spawns the grid with a irregular shape using perlin noise.
    /// </summary>
    private void SpawnStoreIrregularShape()
    {
        for (int y = 0; y < gridYSize; y++) {
            for (int x = 0; x < gridXSize; x++) {
                float perlinValue = Mathf.PerlinNoise(x * gridDensity, y * gridDensity);
                
                if (perlinValue > 0.3 && perlinValue < 0.65) {
                    Vector3 spawnPosition = new Vector3(x ,y);
                    GameObject tileToSpawn = TileToSpawn(x,y);
                    Instantiate(tileToSpawn, spawnPosition, Quaternion.identity).transform.SetParent(gridHolder.transform);
                }
            }
        }
    }
    
    /// <summary>
    /// Spawns items only on walkable tiles on the grid.
    /// </summary>
    private void SpawnItems() {
        for (int y = 0; y < gridYSize; y++) {
            for (int x = 0; x < gridXSize; x++) {
                Vector3 position = new Vector3(x,y);
                
                if (_obstaclePositionsList.Contains(position)) {
                    continue;
                }
                
                if (_walkablePositionsList.Contains(position)) {
                    GameObject objectToSpawn = ItemToSpawn();
                    
                    if (Random.Range(0f,1f) < spawnItemsUnder) {
                        ItemBehaviour item = Instantiate(objectToSpawn, position, Quaternion.identity)?.GetComponent<ItemBehaviour>();
                        item.transform.SetParent(itemsHolder.transform);
                        
                        //Setup in Item behaviour script
                        if (!ReferenceEquals(item,null)) {
                            item.onPlaySound.AddListener(_musicManagerInScene.PlaySound);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Chooses which tile to spawn at coordinate and saves to corresponding list.
    /// </summary>
    /// <param name="x">Position x-axis</param>
    /// <param name="y">Position y-axi</param>
    /// <returns>Type of tile to spawn</returns>
    private GameObject TileToSpawn(int x, int y) {
        GameObject tileToSpawn;
        float perlinValue = Mathf.PerlinNoise(x * obstaclesDensity, y * obstaclesDensity);
        
        if (perlinValue < spawnWalkableTilesUnder) {
            tileToSpawn = walkableTilePrefab;
            GridArray[x, y] = tileToSpawn;
            Vector2 positionToSave = new Vector2 (x,y);
            _walkablePositionsList.Add(positionToSave);
        }
        else {
            tileToSpawn = obstacleTilePrefab;
            Vector2 positionToSave = new Vector2 (x,y);
            _obstaclePositionsList.Add(positionToSave);
        }
        return tileToSpawn;
    }
    /// <summary>
    /// Chooses which item to spawn and sets sprite.
    /// </summary>
    /// <returns>Type of item to spawn</returns>
    private GameObject ItemToSpawn() {
        GameObject objectToSpawn;
        float value = Random.Range(0f, 1f);
        
        if (value < spawnStoreItemsUnder) {
            int randomIndex = Random.Range(0, itemsSprites.Count);
            storeItemsPrefab.GetComponent<SpriteRenderer>().sprite = itemsSprites[randomIndex];
            objectToSpawn = storeItemsPrefab;
        }
        else {
            objectToSpawn = badItemsPrefab;
        }
        return objectToSpawn;
    }

    #region Scrip Set Ups
    private void CameraSetUp() {
        Camera cameraInScene = Instantiate(cameraPrefab, transform);
        cameraInScene.GetComponent<CameraMovement>().Target = _playerInScene.GetComponent<Transform>();
    }

    private void TimeManagerSetUp() {
        TimeManager timeManager = Instantiate(timeManagerPrefab, transform);
        _timeManagerInScene = timeManager;
        _timeManagerInScene.GetComponent<TimeManager>().CanvasUI = canvasInScene.GetComponent<UI>();
    }

    private void PlayerSetUp() {
        GameObject player = Instantiate(playerPrefab, transform);
        _playerInScene = player;
        _playerInScene.GetComponent<PlayerInput>().ScenerySpawner = gameObject.GetComponent<ScenerySpawner>();
        _playerInScene.GetComponent<PlayerInput>().TimeManager = _timeManagerInScene;
    }

    private void CanvasSetUp() {
        canvasInScene.GetComponent<UI>().Score = _playerInScene.GetComponent<Score>();
        canvasInScene.GetComponent<UI>().Timer = _timeManagerInScene.GetComponent<TimeManager>().Timer;
    }

    private void MusicManagerSetUp() {
        Music musicManager = Instantiate(musicManagerPrefab, transform);
        _musicManagerInScene = musicManager;
    }
    #endregion
    
    private void Awake() {
        GridArray = new GameObject[gridXSize, gridYSize];
        _obstaclePositionsList = new List<Vector2>();
        _walkablePositionsList = new List<Vector2>();
        MusicManagerSetUp();
        TimeManagerSetUp();
        PlayerSetUp();
        CameraSetUp();
        SpawnStoreIrregularShape();
        SpawnItems();
        CanvasSetUp();
    }
}
