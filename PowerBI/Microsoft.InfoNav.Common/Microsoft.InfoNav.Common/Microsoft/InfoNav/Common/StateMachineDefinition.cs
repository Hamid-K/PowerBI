using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000071 RID: 113
	[ImmutableObject(true)]
	internal sealed class StateMachineDefinition<TState, TInput, TContext>
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0000AA45 File Offset: 0x00008C45
		internal StateMachineDefinition(Dictionary<TState, Dictionary<TInput, StateMachineTransition<TState, TContext>>> transitions)
		{
			this._transitions = transitions.ToDictionary(Util.KeyDelegate<TState, Dictionary<TInput, StateMachineTransition<TState, TContext>>>(), (KeyValuePair<TState, Dictionary<TInput, StateMachineTransition<TState, TContext>>> kvp) => kvp.Value.ToReadOnlyDictionary<TInput, StateMachineTransition<TState, TContext>>()).AsReadOnlyDictionary<TState, ReadOnlyDictionary<TInput, StateMachineTransition<TState, TContext>>>();
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000AA82 File Offset: 0x00008C82
		internal ReadOnlyDictionary<TState, ReadOnlyDictionary<TInput, StateMachineTransition<TState, TContext>>> Transitions
		{
			get
			{
				return this._transitions;
			}
		}

		// Token: 0x040000E6 RID: 230
		private readonly ReadOnlyDictionary<TState, ReadOnlyDictionary<TInput, StateMachineTransition<TState, TContext>>> _transitions;
	}
}
