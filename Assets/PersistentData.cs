using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    [SerializeField]int shotsFired;
    [SerializeField]int totalScore;

    public static PersistentData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        shotsFired = 0;
        totalScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTotalScore(int num){
        totalScore += num;
    }
    public int GetTotalScore(){
        return totalScore;
    }
    public void AddShots(){
        shotsFired++;
    }
    public int GetShotsFired(){
        return shotsFired;
    }
    public void ResetTotalScore(){
        totalScore = 0;
    }
    public void ResetShotsFired(){
        shotsFired = 0;
    }
}
