using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000070 RID: 112
	[ImmutableObject(false)]
	internal sealed class StateMachine<TState, TInput, TContext>
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000A937 File Offset: 0x00008B37
		internal StateMachine(StateMachineDefinition<TState, TInput, TContext> definition, TContext context, TState initialState)
		{
			this._definition = definition;
			this._context = context;
			this._state = initialState;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000A954 File Offset: 0x00008B54
		internal TState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000A95C File Offset: 0x00008B5C
		internal void PerformTransition(TInput input)
		{
			if (this._transitions == null && !this._definition.Transitions.TryGetValue(this._state, out this._transitions))
			{
				return;
			}
			StateMachineTransition<TState, TContext> stateMachineTransition;
			if (!this._transitions.TryGetValue(input, out stateMachineTransition))
			{
				return;
			}
			ref TState ptr = ref this._state;
			if (default(TState) == null)
			{
				TState state = this._state;
				ptr = ref state;
			}
			if (!ptr.Equals(stateMachineTransition.ToState))
			{
				this._state = stateMachineTransition.ToState;
				this._transitions = null;
			}
			this.ExecuteActions(stateMachineTransition.Actions);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		private void ExecuteActions(ReadOnlyCollection<Action<TContext>> actions)
		{
			for (int i = 0; i < actions.Count; i++)
			{
				actions[i](this._context);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000AA28 File Offset: 0x00008C28
		public override string ToString()
		{
			return "State: " + this._state.ToString();
		}

		// Token: 0x040000E2 RID: 226
		private readonly StateMachineDefinition<TState, TInput, TContext> _definition;

		// Token: 0x040000E3 RID: 227
		private readonly TContext _context;

		// Token: 0x040000E4 RID: 228
		private TState _state;

		// Token: 0x040000E5 RID: 229
		private ReadOnlyDictionary<TInput, StateMachineTransition<TState, TContext>> _transitions;
	}
}
