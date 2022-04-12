public abstract class PlayerState {
	protected bool _isRootState = false;

	protected Player _context;
	protected PlayerStateFactory _factory;
	protected PlayerState _currentSuperState;
	protected PlayerState _currentSubState;

	public PlayerState(Player currentContext, PlayerStateFactory playerStateFactory) {
		_context = currentContext;
		_factory = playerStateFactory;
	}

	protected void SwitchState(PlayerState newState) {
		ExitState();

		newState.EnterState();

		if (_isRootState) {
			_context.CurrentState = newState;
		} else if (_currentSuperState != null) {
			_currentSuperState.SetSubState(newState);
		}
	}

	protected void SetSuperState(PlayerState newSuperState) {
		_currentSuperState = newSuperState;
	}

	protected void SetSubState(PlayerState newSubState) {
		_currentSubState = newSubState;

		newSubState.SetSuperState(this);
	}

	public void UpdateStates() {
		UpdateState();

		_currentSubState?.UpdateStates();
	}

	public abstract void EnterState();

	public abstract void UpdateState();

	public abstract void ExitState();

	public abstract void CheckSwitchState();

	public abstract void InitializeSubState();
}
