using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000012 RID: 18
	public readonly struct ValueTaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002A8A File Offset: 0x00000C8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ValueTaskAwaiter(ValueTask<TResult> value)
		{
			this._value = value;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002A93 File Offset: 0x00000C93
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._value.IsCompleted;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002AA0 File Offset: 0x00000CA0
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TResult GetResult()
		{
			return this._value.Result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public void OnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task<TResult> task;
			if ((task = obj as Task<TResult>) != null)
			{
				task.GetAwaiter().OnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext | ValueTaskSourceOnCompletedFlags.FlowExecutionContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().OnCompleted(continuation);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B18 File Offset: 0x00000D18
		public void UnsafeOnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task<TResult> task;
			if ((task = obj as Task<TResult>) != null)
			{
				task.GetAwaiter().UnsafeOnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
		}

		// Token: 0x04000024 RID: 36
		private readonly ValueTask<TResult> _value;
	}
}
