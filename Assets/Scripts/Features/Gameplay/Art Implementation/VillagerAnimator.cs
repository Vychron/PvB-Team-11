using UnityEditor;
using UnityEngine;

/// <summary>
/// Animates the villager sprite.
/// </summary>
public class VillagerAnimator : MonoBehaviour {

    private Sprite[]
        _upImages = null,
        _leftImages = null,
        _rightImages = null,
        _downImages = null;

    private SpriteRenderer _rend = null;

    private int
        _villagerVariant = 0,
        _currentIterator = 0;

    private void Start() {
        _rend = GetComponent<SpriteRenderer>();
        string[] subFolders = AssetDatabase.GetSubFolders("Assets/Resources/Villagers");
        _villagerVariant = Random.Range(0, subFolders.Length);

        _upImages = Resources.LoadAll<Sprite>("Villagers/Villager-" + _villagerVariant.ToString() + "/Up");
        _leftImages = Resources.LoadAll<Sprite>("Villagers/Villager-" + _villagerVariant.ToString() + "/Left");
        _rightImages = Resources.LoadAll<Sprite>("Villagers/Villager-" + _villagerVariant.ToString() + "/Right");
        _downImages = Resources.LoadAll<Sprite>("Villagers/Villager-" + _villagerVariant.ToString() + "/Down");
        _rend.sprite = _downImages[0];
    }

    /// <summary>
    /// Update the villager sprite based on movement vector.
    /// </summary>
    /// <param name="direction">The direcrion the villager is moving in.</param>
    public void UpdateSprite(Vector2 direction) {
        if (_currentIterator / 15 >= _rightImages.Length)
            _currentIterator = 0;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            if (direction.x > 0)
                _rend.sprite = _rightImages[Mathf.FloorToInt(_currentIterator / 15)];
            else
                _rend.sprite = _leftImages[Mathf.FloorToInt(_currentIterator / 15)];
        }
        else {
            if (direction.y > 0)
                _rend.sprite = _upImages[Mathf.FloorToInt(_currentIterator / 15)];
            else {

                if (direction.y != 0)
                    _rend.sprite = _downImages[Mathf.FloorToInt(_currentIterator / 15)];
                else
                    _rend.sprite = _downImages[0];
            }
        }
        _currentIterator++;
        
    }
}
