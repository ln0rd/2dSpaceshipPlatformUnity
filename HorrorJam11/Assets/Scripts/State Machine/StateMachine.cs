using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    private IState _currentState;
    public event Action<IState, IState> OnStateChange;  //IState from, IState to

    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> _currentTransitions = new List<Transition>();
    private List<Transition> _anyTransitions = new List<Transition>();
    private List<Transition> EmptyTransitions = new List<Transition>(0);


    public void Tick()
    {
        var transition = GetTransition();

        if (transition != null)
        {
            SetState(transition.To);
        }

        _currentState?.Tick();
    }


    public void SetState(IState state)
    {
        if (state == _currentState)
        {
            return;
        }

        if (_currentState == null)
        {
            OnStateChange?.Invoke(_currentState, state);
        }

        OnStateChange?.Invoke(_currentState, state);
        _currentState?.OnExit();        //Executes the things to exit the previous state

        _currentState = state;          //Changes to the current state
        UpdateCurrentTransitions();     //Updates the current transitions list 

        _currentState.OnEnter();        //Executes the things to enter current state

    }



    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        if (_transitions.TryGetValue(from.GetType(), out var transitions) == false) //This line is always executed, so the "transitions" variable is always populated
        {
            ///If there is none transition for that state already, 
            ///then this block will create a key in the dictionary with an empty (but not null) list.
            ///Then, the .Add method can be called freely outside this block
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, condition));
    }

    public void AddAnyTransition(IState to, Func<bool> condition)
    {
        _anyTransitions.Add(new Transition(to, condition));
        /// _anyTransition is always initiated in the variable declarations.
        /// So the logic to check if it is null is not necessary.
        /// Also, no "From" state is necessary, because it can go from any state to the next one
    }





    private Transition GetTransition()
    {
        foreach (var transition in _anyTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        foreach (var transition in _currentTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        return null;
    }

    private void UpdateCurrentTransitions()
    {
        _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        if (_currentTransitions == null)
        {
            _currentTransitions = EmptyTransitions;
        }
    }
    private class Transition
    {
        public IState To { get; }
        public Func<bool> Condition { get; }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }

    }

    public IState GetCurrentState()
    {
        return _currentState;
    }

    public void ListTransitions()
    {
        foreach (var transition in _transitions)
        {
            Debug.Log("FROM: " + transition.Key);
            foreach (Transition value in transition.Value)
            {
                Debug.Log("TO: " + value.To);
            }
        }
        Debug.Log("---------");
    }

    public void ListCurrentTransitions()
    {
        foreach (Transition transition in _currentTransitions)
        {
            Debug.Log("Current state: " + _currentState);
            Debug.Log("TO: " + transition.To);
        }
        Debug.Log("---------");
    }
}
