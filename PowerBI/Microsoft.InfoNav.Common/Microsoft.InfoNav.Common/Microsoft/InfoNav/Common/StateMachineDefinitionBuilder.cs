using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000072 RID: 114
	[ImmutableObject(false)]
	internal sealed class StateMachineDefinitionBuilder<TState, TInput, TContext>
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0000AA8A File Offset: 0x00008C8A
		internal StateMachineDefinitionBuilder()
		{
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		internal void AddTransition(TState fromState, TInput input, TState toState, params Action<TContext>[] actions)
		{
			StateMachineTransition<TState, TContext> stateMachineTransition = new StateMachineTransition<TState, TContext>(fromState, toState, actions);
			Dictionary<TInput, StateMachineTransition<TState, TContext>> dictionary;
			if (!this._transitions.TryGetValue(fromState, out dictionary))
			{
				dictionary = new Dictionary<TInput, StateMachineTransition<TState, TContext>>();
				this._transitions.Add(fromState, dictionary);
				dictionary.Add(input, stateMachineTransition);
				return;
			}
			dictionary.Add(input, stateMachineTransition);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000AAEB File Offset: 0x00008CEB
		internal StateMachineDefinition<TState, TInput, TContext> GetDefinition()
		{
			return new StateMachineDefinition<TState, TInput, TContext>(this._transitions);
		}

		// Token: 0x040000E7 RID: 231
		private readonly Dictionary<TState, Dictionary<TInput, StateMachineTransition<TState, TContext>>> _transitions = new Dictionary<TState, Dictionary<TInput, StateMachineTransition<TState, TContext>>>();
	}
}
