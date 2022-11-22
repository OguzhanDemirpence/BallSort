using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [SerializeField] private LayerMask tubeLayer;
    [SerializeField] private LayerMask draggingLayer;
    [SerializeField] private UIControl uIControl;
    [SerializeField] private MaterialManager materialManager;

    private Tube _lastTube;
    private Ball _currentDraggingBall;
    private EDragState _dragState;
    private int stepsCount = 0;

    private void Start()
    {
        _dragState = EDragState.Idle;
        stepsCount = 0;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _dragState == EDragState.Idle)
        {
            StartDrag();
        }
        else if (Input.GetMouseButton(0) && _dragState == EDragState.Dragging)
        {
            Drag();
        }
        else if (Input.GetMouseButtonUp(0) && _dragState == EDragState.Dragging)
        {
            StopDrag();
        }
    }

    private void StartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tubeLayer))
        {
            Tube tube = hit.transform.GetComponent<Tube>();

            if (tube.HasBall())
            {
                _lastTube = tube;
                _currentDraggingBall = _lastTube.GetLastBall();
                _dragState = EDragState.Dragging;
            }
        }
    }
    private void Drag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, draggingLayer))
        {
            _currentDraggingBall.transform.position = hit.point;
        }
    }
    private void StopDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tubeLayer))
        {
            Tube tube = hit.transform.GetComponent<Tube>();

            if (tube.HasSpaceForBall())
            {
                tube.SetBall(_currentDraggingBall);
                _dragState = EDragState.Idle;
                stepsCount++;
                uIControl.StepsCount(stepsCount);
                GameEvents.BallChangedTube?.Invoke();
            }
            else
            {
                _lastTube.SetBall(_currentDraggingBall);
                _dragState = EDragState.Idle;
            }
        }
        else
        {
            _lastTube.SetBall(_currentDraggingBall);
            _dragState = EDragState.Idle;
        }
    }

    private enum EDragState
    {
        None,
        Idle,
        Dragging
    }

}
