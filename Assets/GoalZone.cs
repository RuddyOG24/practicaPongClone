using UnityEngine;
using System;

public enum GoalSide { Left, Right }

[RequireComponent(typeof(BoxCollider2D))]
public class GoalZone : MonoBehaviour
{
    public GoalSide side = GoalSide.Left;
    public static event Action<GoalSide> OnGoal;

    void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Ball>() != null)
        {
            OnGoal?.Invoke(side);
        }
    }
}
