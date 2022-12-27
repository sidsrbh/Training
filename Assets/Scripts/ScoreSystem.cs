using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _highScoreText;
    [SerializeField] TMP_Text _multiplierText;
    [SerializeField] FloatScoreText _floatingTextPrefab;
    [SerializeField] Canvas _floatingScoreCanvas;
    int _score;
    private int _highScore;
    private float _scoreMultiplierExpiration;
    private int _killMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        MummyMovement.Died += Mummy_Died;
        _highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreText.SetText("High Score: " + _highScore);

    }

    private void Mummy_Died(MummyMovement mummy)
    {
       
        UpdateKillMultiplier();
        _score += _killMultiplier;

        if (_score > _highScore)
        {
            _highScore = _score;
            _highScoreText.SetText("High Score: " + _highScore);
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
        _scoreText.SetText(_score.ToString());
        var floatingText = Instantiate(
            _floatingTextPrefab, 
            mummy.transform.position,
            _floatingScoreCanvas.transform.rotation, 
            _floatingScoreCanvas.transform);

        floatingText.SetScoreValue(_killMultiplier);
    }

    private void UpdateKillMultiplier()
    {
        Debug.Log(Time.time);
        if (Time.time <= _scoreMultiplierExpiration)
        {
            _killMultiplier++;
        }
        else
        {
            _killMultiplier = 1;
        }

        _scoreMultiplierExpiration = Time.time + 1f;
        _multiplierText.SetText("x " + _killMultiplier);
        if (_killMultiplier < 3)
            _multiplierText.color = Color.white;
        else if (_killMultiplier < 10)
            _multiplierText.color = Color.green;
        else if (_killMultiplier < 20)
            _multiplierText.color = Color.yellow;
        else if (_killMultiplier < 30)
            _multiplierText.color = Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
