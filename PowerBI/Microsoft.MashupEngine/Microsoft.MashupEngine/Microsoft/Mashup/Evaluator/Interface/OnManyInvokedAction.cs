using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E43 RID: 7747
	public sealed class OnManyInvokedAction<T>
	{
		// Token: 0x0600BE78 RID: 48760 RVA: 0x0026846C File Offset: 0x0026666C
		public Action<T> NewInput()
		{
			List<T> list = this.results;
			Action<T> action;
			lock (list)
			{
				if (this.invokedCount == -1)
				{
					throw new InvalidOperationException("NewInput called after OnManyInvoked action was invoked.");
				}
				int actionIndex = this.invoked.Count;
				this.invoked.Add(false);
				this.results.Add(default(T));
				action = delegate(T result)
				{
					this.OnAction(actionIndex, result);
				};
			}
			return action;
		}

		// Token: 0x0600BE79 RID: 48761 RVA: 0x00268508 File Offset: 0x00266708
		public void OnManyInvoked(Action<IEnumerable<T>> action)
		{
			List<T> list = this.results;
			lock (list)
			{
				if (this.action != null)
				{
					throw new InvalidOperationException("OnManyInvoked called multiple times.");
				}
				this.action = action;
			}
			this.CheckDone();
		}

		// Token: 0x0600BE7A RID: 48762 RVA: 0x00268564 File Offset: 0x00266764
		private void OnAction(int actionIndex, T result)
		{
			List<T> list = this.results;
			lock (list)
			{
				if (this.invoked[actionIndex])
				{
					throw new InvalidOperationException("Input " + actionIndex.ToString() + " invoked multiple times.");
				}
				this.invoked[actionIndex] = true;
				this.results[actionIndex] = result;
				this.invokedCount++;
			}
			this.CheckDone();
		}

		// Token: 0x0600BE7B RID: 48763 RVA: 0x002685F8 File Offset: 0x002667F8
		private void CheckDone()
		{
			Action<IEnumerable<T>> action = null;
			List<T> list = this.results;
			lock (list)
			{
				if (this.action != null && this.invoked.Count == this.invokedCount)
				{
					this.invokedCount = -1;
					action = this.action;
				}
			}
			if (action != null)
			{
				action(this.results);
			}
		}

		// Token: 0x04006102 RID: 24834
		private readonly List<bool> invoked = new List<bool>();

		// Token: 0x04006103 RID: 24835
		private readonly List<T> results = new List<T>();

		// Token: 0x04006104 RID: 24836
		private int invokedCount;

		// Token: 0x04006105 RID: 24837
		private Action<IEnumerable<T>> action;
	}
}
