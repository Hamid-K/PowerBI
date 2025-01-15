using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000010 RID: 16
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredValueTaskAwaitable<TResult>
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002968 File Offset: 0x00000B68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ConfiguredValueTaskAwaitable(ValueTask<TResult> value)
		{
			this._value = value;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002971 File Offset: 0x00000B71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable<TResult>.ConfiguredValueTaskAwaiter GetAwaiter()
		{
			return new ConfiguredValueTaskAwaitable<TResult>.ConfiguredValueTaskAwaiter(this._value);
		}

		// Token: 0x04000021 RID: 33
		private readonly ValueTask<TResult> _value;

		// Token: 0x02000017 RID: 23
		[StructLayout(LayoutKind.Auto)]
		public readonly struct ConfiguredValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000061 RID: 97 RVA: 0x00002D55 File Offset: 0x00000F55
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal ConfiguredValueTaskAwaiter(ValueTask<TResult> value)
			{
				this._value = value;
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000062 RID: 98 RVA: 0x00002D5E File Offset: 0x00000F5E
			public bool IsCompleted
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._value.IsCompleted;
				}
			}

			// Token: 0x06000063 RID: 99 RVA: 0x00002D6B File Offset: 0x00000F6B
			[StackTraceHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TResult GetResult()
			{
				return this._value.Result;
			}

			// Token: 0x06000064 RID: 100 RVA: 0x00002D78 File Offset: 0x00000F78
			public void OnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task<TResult> task;
				if ((task = obj as Task<TResult>) != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.FlowExecutionContext | (this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None));
					return;
				}
				ValueTask.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00002E1C File Offset: 0x0000101C
			public void UnsafeOnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task<TResult> task;
				if ((task = obj as Task<TResult>) != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None);
					return;
				}
				ValueTask.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
			}

			// Token: 0x0400002C RID: 44
			private readonly ValueTask<TResult> _value;
		}
	}
}
