using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E16 RID: 7702
	public sealed class InvokeManyAction<T>
	{
		// Token: 0x0600BDF0 RID: 48624 RVA: 0x00267008 File Offset: 0x00265208
		public void AddOutput(Action<T> action)
		{
			List<Action<T>> list = this.actions;
			lock (list)
			{
				this.actions.Add(action);
			}
			this.CheckDone();
		}

		// Token: 0x0600BDF1 RID: 48625 RVA: 0x00267054 File Offset: 0x00265254
		public void InvokeMany(T value)
		{
			List<Action<T>> list = this.actions;
			lock (list)
			{
				if (this.invoked)
				{
					throw new InvalidOperationException("InvokeMany called multiple times.");
				}
				this.result = value;
				this.invoked = true;
			}
			this.CheckDone();
		}

		// Token: 0x0600BDF2 RID: 48626 RVA: 0x002670B8 File Offset: 0x002652B8
		private void CheckDone()
		{
			Action<T>[] array = null;
			T t = default(T);
			List<Action<T>> list = this.actions;
			lock (list)
			{
				if (this.invoked)
				{
					array = this.actions.ToArray();
					t = this.result;
					this.actions.Clear();
				}
			}
			if (array != null)
			{
				Action<T>[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i](t);
				}
			}
		}

		// Token: 0x040060D4 RID: 24788
		private readonly List<Action<T>> actions = new List<Action<T>>();

		// Token: 0x040060D5 RID: 24789
		private T result;

		// Token: 0x040060D6 RID: 24790
		private bool invoked;
	}
}
