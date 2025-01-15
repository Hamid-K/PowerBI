using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200000D RID: 13
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncValueTaskMethodBuilder
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000027D0 File Offset: 0x000009D0
		public static AsyncValueTaskMethodBuilder Create()
		{
			return default(AsyncValueTaskMethodBuilder);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027E6 File Offset: 0x000009E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027F4 File Offset: 0x000009F4
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this._methodBuilder.SetStateMachine(stateMachine);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002802 File Offset: 0x00000A02
		public void SetResult()
		{
			if (this._useBuilder)
			{
				this._methodBuilder.SetResult();
				return;
			}
			this._haveResult = true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000281F File Offset: 0x00000A1F
		public void SetException(Exception exception)
		{
			this._methodBuilder.SetException(exception);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002830 File Offset: 0x00000A30
		public ValueTask Task
		{
			get
			{
				if (this._haveResult)
				{
					return default(ValueTask);
				}
				this._useBuilder = true;
				return new ValueTask(this._methodBuilder.Task);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002866 File Offset: 0x00000A66
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000287C File Offset: 0x00000A7C
		[SecuritySafeCritical]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x04000019 RID: 25
		private AsyncTaskMethodBuilder _methodBuilder;

		// Token: 0x0400001A RID: 26
		private bool _haveResult;

		// Token: 0x0400001B RID: 27
		private bool _useBuilder;
	}
}
