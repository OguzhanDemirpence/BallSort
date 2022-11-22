using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private float startBallYPos;
    [SerializeField] private float offsetBetweenBalls;

    public List<Ball> balls = new List<Ball>();

    public void SetBall(Ball ball)
    {
        ball.transform.SetParent(transform);

        ball.transform.localPosition = new Vector3(0, startBallYPos + offsetBetweenBalls * balls.Count, 0);

        balls.Add(ball);

    }
    public bool HasBall()
    {
        return balls.Count > 0;
    }
    public bool HasSpaceForBall()
    {
        return balls.Count < 4;
    }
    public bool AreAllBallsSameColor()
    {
        if (balls.Count == 0)
        {
            return true;
        }
        if (balls.Count == 4)
        {
            EColor firstColor = balls[0].ColorType;

            for (int i = 1; i < balls.Count; i++)
            {
                if (firstColor != balls[i].ColorType)
                {
                    return false;
                }
            }

            return true;
        }

        return false; 
    }
    public Ball GetLastBall()
    {
        Ball ball = balls[balls.Count - 1];
        balls.Remove(ball);
        return ball;
    }
}
