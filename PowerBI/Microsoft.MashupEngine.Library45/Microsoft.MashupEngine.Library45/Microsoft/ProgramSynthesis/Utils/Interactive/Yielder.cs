using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace Microsoft.ProgramSynthesis.Utils.Interactive
{
	// Token: 0x0200069B RID: 1691
	internal class Yielder<T> : IYielder<T>, IAwaitable, IAwaiter, ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06002451 RID: 9297 RVA: 0x0006624B File Offset: 0x0006444B
		public Yielder(Action<Yielder<T>> create)
		{
			this._create = create;
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x0006625A File Offset: 0x0006445A
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x00066262 File Offset: 0x00064462
		public T Current { get; private set; }

		// Token: 0x06002454 RID: 9300 RVA: 0x00004FAE File Offset: 0x000031AE
		public IAwaiter GetAwaiter()
		{
			return this;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public bool IsCompleted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public void GetResult()
		{
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0006626B File Offset: 0x0006446B
		[SecurityCritical]
		public void UnsafeOnCompleted(Action continuation)
		{
			this._continuation = continuation;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x0006626B File Offset: 0x0006446B
		public void OnCompleted(Action continuation)
		{
			this._continuation = continuation;
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00066274 File Offset: 0x00064474
		public IAwaitable Return(T value)
		{
			this._hasValue = true;
			this.Current = value;
			return this;
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00066285 File Offset: 0x00064485
		public IAwaitable Break()
		{
			this._stopped = true;
			return this;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00004FAE File Offset: 0x000031AE
		public Yielder<T> GetEnumerator()
		{
			return this;
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00066290 File Offset: 0x00064490
		public bool MoveNext()
		{
			if (!this._running)
			{
				this._running = true;
				this._create(this);
			}
			else
			{
				this._hasValue = false;
				this._continuation();
			}
			return !this._stopped && this._hasValue;
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00002C72 File Offset: 0x00000E72
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400115F RID: 4447
		private readonly Action<Yielder<T>> _create;

		// Token: 0x04001160 RID: 4448
		private Action _continuation;

		// Token: 0x04001161 RID: 4449
		private bool _hasValue;

		// Token: 0x04001162 RID: 4450
		private bool _running;

		// Token: 0x04001163 RID: 4451
		private bool _stopped;
	}
}
