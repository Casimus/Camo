using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    [SerializeField] private GameObject personPrefab;
    [SerializeField] int populationSize = 10;
    [SerializeField] private int trialTime = 15;
    
    [SerializeField]
    [Range(0,100)]
    private int chanceToMutation = 10;
    
    private List<GameObject> population = new List<GameObject>();

    public static float elapsed = 0;

    
    private int generation = 1;

    private GUIStyle guiStyle = new();



    private void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new(Random.Range(-9f, 9f), Random.Range(-4.5f, 4.5f), 0);
            GameObject newPerson = Instantiate(personPrefab, pos, Quaternion.identity);
            newPerson.GetComponent<DNA>().SetRed(Random.Range(0f, 1f));
            newPerson.GetComponent<DNA>().SetGreen(Random.Range(0f, 1f));
            newPerson.GetComponent<DNA>().SetBlue(Random.Range(0f, 1f));
            newPerson.GetComponent<DNA>().SetSize(Random.Range(0.1f, 0.4f));
            
            population.Add(newPerson);
        }
    }

    void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial time: " + (int)elapsed, guiStyle);
    }

    private void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }

    private void BreedNewPopulation()
    {
       // List<GameObject> newPoppulation = new List<GameObject>();

        List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<DNA>().TimeToDie).ToList();

        population.Clear();


        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i ++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        foreach (var item in sortedList)
        {
            Destroy(item.gameObject);
        }
        generation++;
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new(Random.Range(-9f, 9f), Random.Range(-4.5f, 4.5f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        if (Random.Range(0, 100) >= chanceToMutation ) // inherit
        {
            offspring.GetComponent<DNA>().SetRed(Random.Range(0, 10) < 5 ? dna1.Red : dna2.Red);
            offspring.GetComponent<DNA>().SetGreen(Random.Range(0, 10) < 5 ? dna1.Green : dna2.Green);
            offspring.GetComponent<DNA>().SetRed(Random.Range(0, 10) < 5 ? dna1.Blue : dna2.Blue);
            offspring.GetComponent<DNA>().SetSize(Random.Range(0,10)< 5 ? dna1.Size : dna2.Size);
        }
        else // mutation
        {
            offspring.GetComponent<DNA>().SetRed(Random.Range(0f,1f));
            offspring.GetComponent<DNA>().SetGreen(Random.Range(0f, 1f));
            offspring.GetComponent<DNA>().SetRed(Random.Range(0f, 1f));
            offspring.GetComponent<DNA>().SetSize(Random.Range(0.1f,0.4f));

        }
        return offspring;
    }
}
