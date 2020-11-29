using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text _score;

    public void InstantiateScore(int point)
    {
        _score.text = point.ToString();
    }
}
