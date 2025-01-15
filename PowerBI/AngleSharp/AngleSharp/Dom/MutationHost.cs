using System;
using System.Collections.Generic;
using AngleSharp.Extensions;

namespace AngleSharp.Dom
{
	// Token: 0x02000159 RID: 345
	internal sealed class MutationHost
	{
		// Token: 0x06000BE5 RID: 3045 RVA: 0x0004410C File Offset: 0x0004230C
		public MutationHost(IEventLoop loop)
		{
			this._observers = new List<MutationObserver>();
			this._queued = false;
			this._loop = loop;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0004412D File Offset: 0x0004232D
		public IEnumerable<MutationObserver> Observers
		{
			get
			{
				return this._observers;
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00044135 File Offset: 0x00042335
		public void Register(MutationObserver observer)
		{
			if (!this._observers.Contains(observer))
			{
				this._observers.Add(observer);
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00044151 File Offset: 0x00042351
		public void Unregister(MutationObserver observer)
		{
			if (this._observers.Contains(observer))
			{
				this._observers.Remove(observer);
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0004416E File Offset: 0x0004236E
		public void ScheduleCallback()
		{
			if (!this._queued)
			{
				this._queued = true;
				this._loop.Enqueue(new Action(this.DispatchCallback), TaskPriority.Normal);
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00044198 File Offset: 0x00042398
		private void DispatchCallback()
		{
			MutationObserver[] array = this._observers.ToArray();
			this._queued = false;
			foreach (MutationObserver mutationObserver in array)
			{
				this._loop.Enqueue(new Action(mutationObserver.Trigger), TaskPriority.Microtask);
			}
		}

		// Token: 0x0400094B RID: 2379
		private readonly List<MutationObserver> _observers;

		// Token: 0x0400094C RID: 2380
		private readonly IEventLoop _loop;

		// Token: 0x0400094D RID: 2381
		private bool _queued;
	}
}
