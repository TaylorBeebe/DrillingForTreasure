using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameController : MonoBehaviour {

    //objectiveVariables
    [Tooltip("List of Objective")]
    [SerializeField]
    public List<Objective> objectives;

    public Objective prevObjective;
    public Objective currentObjective;

    public bool allObjectivesCompleted;

    //Economy!!!!! Capitalism!!!!!!!!!
    public int Currency;

    public List<Upgrade> upgrades;

    public void AddMoneytoPremium(int MoneytoAdd)
    {
        Currency += MoneytoAdd;
    }
    
    public void DoTransaction(Upgrade upgrade)
    {
        upgrades.Add(upgrade);
        Currency -= upgrade.cost;
    }

    private void Start()
    { 
        DontDestroyOnLoad(this.gameObject);
    }

    /*<summary>
        description
    </summary>
    */
    public void CompleteObjective()
    {
        if (objectives.Count != 0)
        {
            prevObjective = objectives[objectives.Count - 1];
            prevObjective.onObjectiveComplete.Invoke();

            objectives.RemoveAt(objectives.Count - 1);
            currentObjective = objectives[objectives.Count - 1];
        }
        else
        {
            allObjectivesCompleted = true;
        }
    } 
    public void AddObjective(bool urgent,Objective objective)
    {
        if (urgent)
        {
            objectives.Add(objective);
        }
    }

}

[System.Serializable]
public class Objective
{   
    [SerializeField]
    public string ObjectiveName;

    [TextArea]
    [SerializeField]
    public string ObjectiveDiscription;

    public UnityEvent onObjectiveComplete;
    public UnityEvent onObjectiveStart;
}