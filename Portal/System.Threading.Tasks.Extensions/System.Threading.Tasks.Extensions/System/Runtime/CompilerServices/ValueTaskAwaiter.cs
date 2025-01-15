using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000011 RID: 17
	public readonly struct ValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000297E File Offset: 0x00000B7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ValueTaskAwaiter(ValueTask value)
		{
			this._value = value;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002987 File Offset: 0x00000B87
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._value.IsCompleted;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002994 File Offset: 0x00000B94
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetResult()
		{
			this._value.ThrowIfCompletedUnsuccessfully();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void OnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task task;
			if ((task = obj as Task) != null)
			{
				task.GetAwaiter().OnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext | ValueTaskSourceOnCompletedFlags.FlowExecutionContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().OnCompleted(continuation);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A0C File Offset: 0x00000C0C
		public void UnsafeOnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task task;
			if ((task = obj as Task) != null)
			{
				task.GetAwaiter().UnsafeOnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
		}

		// Token: 0x04000022 RID: 34
		internal static readonly Action<object> s_invokeActionDelegate = delegate(object state)
		{
			Action action;
			if ((action = state as Action) == null)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
				return;
			}
			action();
		};

		// Token: 0x04000023 RID: 35
		private readonly ValueTask _value;
	}
}
