using System;

namespace KJH.Utils
{
    public class FSM<TState> where TState : IState
    {
        public Action<TState, TState> OnStateChanged;

        private TState currentState;

        public TState CurrentState { get { return currentState; } }

        public FSM(TState initialState)
        {
            currentState = initialState;
            currentState.Enter();
        }

        public void ChangeState(TState newState)
        {
            if (currentState != null)
                currentState.Exit();

            TState oldState = currentState;
            currentState = newState;

            OnStateChanged?.Invoke(oldState, currentState);

            if (currentState != null)
                currentState.Enter();
        }
    }

    public interface IState
    {
        void Enter();
        void Exit();
    }
}
