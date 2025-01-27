using System;

namespace KJH.Utils
{
    public class FSM<TState> where TState : IState
    {
        public Action<TState, TState> OnStateChanged;

        private TState currentState;

        public TState CurrentState { get { return currentState; } }

        private bool isInitialized = false;

        public FSM()
        {

        }

        public FSM(TState initialState)
        {
            currentState = initialState;
        }

        public void Initialize()
        {
            if (isInitialized)
                throw new Exception("FSM is already initialized.");

            if (currentState == null)
                throw new Exception("No initial state is set.");

            isInitialized = true;

            currentState.Enter();
        }

        public void Initialize(TState initialState)
        {
            if (isInitialized)
                throw new Exception("FSM is already initialized.");

            currentState = initialState;
            Initialize();
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
