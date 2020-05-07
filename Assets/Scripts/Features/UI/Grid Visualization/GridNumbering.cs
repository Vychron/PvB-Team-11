using UnityEngine;
using UnityEngine.UI;

public class GridNumbering : MonoBehaviour {

    [SerializeField]
    private GameObject _coordinate = null;

    [SerializeField]
    private bool _reversedOrder = false;

    void Start() {
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
