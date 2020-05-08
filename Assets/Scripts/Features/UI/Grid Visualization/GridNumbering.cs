using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Creates visual numbers to correspond with coordinates of the grid.
/// </summary>
public class GridNumbering : MonoBehaviour {

    [SerializeField]
    private GameObject _coordinate = null;

    [SerializeField]
    private bool _reversedOrder = false;

    private void Start() {
        int size = LevelGrid.Instance.GetGrid.Length;
        if (_reversedOrder)
            _coordinate.GetComponent<Text>().text = size.ToString();

        for (int i = 0; i < size; i++) {
            GameObject coordinate = Instantiate(_coordinate, transform);
            if (!_reversedOrder)
                coordinate.GetComponent<Text>().text = (i + 1).ToString();
            else
                coordinate.GetComponent<Text>().text = (size - i - 1).ToString();
        }
    }
}
