using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000073 RID: 115
	[ImmutableObject(true)]
	internal sealed class StateMachineTransition<TState, TContext>
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		internal StateMachineTransition(TState fromState, TState toState, IEnumerable<Action<TContext>> actions)
		{
			this._fromState = fromState;
			this._toState = toState;
			this._actions = actions.AsReadOnlyCollection<Action<TContext>>();
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000AB1A File Offset: 0x00008D1A
		internal TState FromState
		{
			get
			{
				return this._fromState;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000AB22 File Offset: 0x00008D22
		internal TState ToState
		{
			get
			{
				return this._toState;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000AB2A File Offset: 0x00008D2A
		internal ReadOnlyCollection<Action<TContext>> Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x040000E8 RID: 232
		private readonly TState _fromState;

		// Token: 0x040000E9 RID: 233
		private readonly TState _toState;

		// Token: 0x040000EA RID: 234
		private readonly ReadOnlyCollection<Action<TContext>> _actions;
	}
}
