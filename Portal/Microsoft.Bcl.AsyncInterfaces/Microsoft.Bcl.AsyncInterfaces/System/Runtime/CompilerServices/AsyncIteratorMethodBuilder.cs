using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncIteratorMethodBuilder
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002208 File Offset: 0x00000408
		public static AsyncIteratorMethodBuilder Create()
		{
			return new AsyncIteratorMethodBuilder
			{
				_methodBuilder = AsyncTaskMethodBuilder.Create()
			};
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000222A File Offset: 0x0000042A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveNext<[Nullable(0)] TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002238 File Offset: 0x00000438
		public void AwaitOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002247 File Offset: 0x00000447
		public void AwaitUnsafeOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002256 File Offset: 0x00000456
		public void Complete()
		{
			this._methodBuilder.SetResult();
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002263 File Offset: 0x00000463
		internal object ObjectIdForDebugger
		{
			get
			{
				object obj;
				if ((obj = this._id) == null)
				{
					obj = Interlocked.CompareExchange(ref this._id, new object(), null) ?? this._id;
				}
				return obj;
			}
		}

		// Token: 0x04000015 RID: 21
		private AsyncTaskMethodBuilder _methodBuilder;

		// Token: 0x04000016 RID: 22
		private object _id;
	}
}
