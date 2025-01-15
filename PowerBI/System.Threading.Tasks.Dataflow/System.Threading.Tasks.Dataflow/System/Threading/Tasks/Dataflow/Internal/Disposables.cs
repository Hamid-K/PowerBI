using System;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000036 RID: 54
	internal sealed class Disposables
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x00008848 File Offset: 0x00006A48
		internal static IDisposable Create<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			return new Disposables.Disposable<T1, T2>(action, arg1, arg2);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008852 File Offset: 0x00006A52
		internal static IDisposable Create<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			return new Disposables.Disposable<T1, T2, T3>(action, arg1, arg2, arg3);
		}

		// Token: 0x04000080 RID: 128
		internal static readonly IDisposable Nop = new Disposables.NopDisposable();

		// Token: 0x0200007D RID: 125
		[DebuggerDisplay("Disposed = true")]
		private sealed class NopDisposable : IDisposable
		{
			// Token: 0x0600041F RID: 1055 RVA: 0x0000FAC3 File Offset: 0x0000DCC3
			void IDisposable.Dispose()
			{
			}
		}

		// Token: 0x0200007E RID: 126
		[DebuggerDisplay("Disposed = {Disposed}")]
		private sealed class Disposable<T1, T2> : IDisposable
		{
			// Token: 0x06000421 RID: 1057 RVA: 0x0000FACD File Offset: 0x0000DCCD
			internal Disposable(Action<T1, T2> action, T1 arg1, T2 arg2)
			{
				this._action = action;
				this._arg1 = arg1;
				this._arg2 = arg2;
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000FAEA File Offset: 0x0000DCEA
			private bool Disposed
			{
				get
				{
					return this._action == null;
				}
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
			void IDisposable.Dispose()
			{
				Action<T1, T2> action = this._action;
				if (action != null && Interlocked.CompareExchange<Action<T1, T2>>(ref this._action, null, action) == action)
				{
					action(this._arg1, this._arg2);
				}
			}

			// Token: 0x040001A0 RID: 416
			private readonly T1 _arg1;

			// Token: 0x040001A1 RID: 417
			private readonly T2 _arg2;

			// Token: 0x040001A2 RID: 418
			private Action<T1, T2> _action;
		}

		// Token: 0x0200007F RID: 127
		[DebuggerDisplay("Disposed = {Disposed}")]
		private sealed class Disposable<T1, T2, T3> : IDisposable
		{
			// Token: 0x06000424 RID: 1060 RVA: 0x0000FB36 File Offset: 0x0000DD36
			internal Disposable(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
			{
				this._action = action;
				this._arg1 = arg1;
				this._arg2 = arg2;
				this._arg3 = arg3;
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000FB5B File Offset: 0x0000DD5B
			private bool Disposed
			{
				get
				{
					return this._action == null;
				}
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x0000FB68 File Offset: 0x0000DD68
			void IDisposable.Dispose()
			{
				Action<T1, T2, T3> action = this._action;
				if (action != null && Interlocked.CompareExchange<Action<T1, T2, T3>>(ref this._action, null, action) == action)
				{
					action(this._arg1, this._arg2, this._arg3);
				}
			}

			// Token: 0x040001A3 RID: 419
			private readonly T1 _arg1;

			// Token: 0x040001A4 RID: 420
			private readonly T2 _arg2;

			// Token: 0x040001A5 RID: 421
			private readonly T3 _arg3;

			// Token: 0x040001A6 RID: 422
			private Action<T1, T2, T3> _action;
		}
	}
}
