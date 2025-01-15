using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200000F RID: 15
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredValueTaskAwaitable
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002952 File Offset: 0x00000B52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ConfiguredValueTaskAwaitable(ValueTask value)
		{
			this._value = value;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000295B File Offset: 0x00000B5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter GetAwaiter()
		{
			return new ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter(this._value);
		}

		// Token: 0x04000020 RID: 32
		private readonly ValueTask _value;

		// Token: 0x02000016 RID: 22
		[StructLayout(LayoutKind.Auto)]
		public readonly struct ConfiguredValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600005C RID: 92 RVA: 0x00002BF5 File Offset: 0x00000DF5
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal ConfiguredValueTaskAwaiter(ValueTask value)
			{
				this._value = value;
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600005D RID: 93 RVA: 0x00002BFE File Offset: 0x00000DFE
			public bool IsCompleted
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._value.IsCompleted;
				}
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00002C0B File Offset: 0x00000E0B
			[StackTraceHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void GetResult()
			{
				this._value.ThrowIfCompletedUnsuccessfully();
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00002C18 File Offset: 0x00000E18
			public void OnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task task;
				if ((task = obj as Task) != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.FlowExecutionContext | (this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None));
					return;
				}
				ValueTask.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00002CB8 File Offset: 0x00000EB8
			public void UnsafeOnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task task;
				if ((task = obj as Task) != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None);
					return;
				}
				ValueTask.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
			}

			// Token: 0x0400002B RID: 43
			private readonly ValueTask _value;
		}
	}
}
