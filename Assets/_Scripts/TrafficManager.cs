using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class TrafficManager : MonoBehaviour
{
    public GameObject cars;
    public GameObject humans;
    
    public List<Character> carsList;
    public List<Character> humansList;
    
    public GameObject goalsHuman;
    public GameObject goalsCar;

    public List<Goal> goalsHumanList;
    public List<Goal> goalsCarList;

    void Start()
    {
        carsList = cars.GetComponentsInChildren<Character>().ToList();
        humansList = humans.GetComponentsInChildren<Character>().ToList();
        
        goalsHumanList = goalsHuman.GetComponentsInChildren<Goal>().ToList();
        goalsCarList = goalsCar.GetComponentsInChildren<Goal>().ToList();

        SetRandomGoals(carsList, goalsCarList);
        SetRandomGoals(humansList, goalsHumanList);
    }

    public void SetRandomGoals(List<Character> characters, List<Goal> goals)
    {
        List<Transform> ShuffleGoals = new List<Transform>();
        foreach (var goal in goals)
        {
            ShuffleGoals.Add(goal.transform);
        }
        foreach (var character in characters)
        {
            Shuffle(ref ShuffleGoals);
            character.goalPoints.AddRange(ShuffleGoals.Take(4));
        }
    }

    public static void Shuffle(ref List<Transform> list)
    {
        Random random = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
    
    
    
    void Update()
    {
        
    }
}
