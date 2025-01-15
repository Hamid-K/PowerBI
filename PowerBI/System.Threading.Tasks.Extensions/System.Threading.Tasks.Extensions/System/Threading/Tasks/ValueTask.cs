using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;

namespace System.Threading.Tasks
{
	// Token: 0x02000006 RID: 6
	[AsyncMethodBuilder(typeof(AsyncValueTaskMethodBuilder))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ValueTask : IEquatable<ValueTask>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002091 File Offset: 0x00000291
		internal static Task CompletedTask { get; } = Task.Delay(0);

		// Token: 0x06000009 RID: 9 RVA: 0x00002098 File Offset: 0x00000298
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(Task task)
		{
			if (task == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.task);
			}
			this._obj = task;
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020B8 File Offset: 0x000002B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(IValueTaskSource source, short token)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			this._obj = source;
			this._token = token;
			this._continueOnCapturedContext = true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020D8 File Offset: 0x000002D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ValueTask(object obj, short token, bool continueOnCapturedContext)
		{
			this._obj = obj;
			this._token = token;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020EF File Offset: 0x000002EF
		public override int GetHashCode()
		{
			object obj = this._obj;
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002102 File Offset: 0x00000302
		public override bool Equals(object obj)
		{
			return obj is ValueTask && this.Equals((ValueTask)obj);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000211A File Offset: 0x0000031A
		public bool Equals(ValueTask other)
		{
			return this._obj == other._obj && this._token == other._token;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000213A File Offset: 0x0000033A
		public static bool operator ==(ValueTask left, ValueTask right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002144 File Offset: 0x00000344
		public static bool operator !=(ValueTask left, ValueTask right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002154 File Offset: 0x00000354
		public Task AsTask()
		{
			object obj = this._obj;
			Task task;
			if (obj != null)
			{
				if ((task = obj as Task) == null)
				{
					return this.GetTaskForValueTaskSource(Unsafe.As<IValueTaskSource>(obj));
				}
			}
			else
			{
				task = ValueTask.CompletedTask;
			}
			return task;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002187 File Offset: 0x00000387
		public ValueTask Preserve()
		{
			if (this._obj != null)
			{
				return new ValueTask(this.AsTask());
			}
			return this;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021A4 File Offset: 0x000003A4
		private Task GetTaskForValueTaskSource(IValueTaskSource t)
		{
			ValueTaskSourceStatus status = t.GetStatus(this._token);
			if (status != ValueTaskSourceStatus.Pending)
			{
				try
				{
					t.GetResult(this._token);
					return ValueTask.CompletedTask;
				}
				catch (Exception ex)
				{
					if (status == ValueTaskSourceStatus.Canceled)
					{
						return ValueTask.s_canceledTask;
					}
					TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
					taskCompletionSource.TrySetException(ex);
					return taskCompletionSource.Task;
				}
			}
			ValueTask.ValueTaskSourceAsTask valueTaskSourceAsTask = new ValueTask.ValueTaskSourceAsTask(t, this._token);
			return valueTaskSourceAsTask.Task;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002224 File Offset: 0x00000424
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return true;
				}
				Task task;
				if ((task = obj as Task) != null)
				{
					return task.IsCompleted;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) > ValueTaskSourceStatus.Pending;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002264 File Offset: 0x00000464
		public bool IsCompletedSuccessfully
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return true;
				}
				Task task;
				if ((task = obj as Task) != null)
				{
					return task.Status == TaskStatus.RanToCompletion;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Succeeded;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022A8 File Offset: 0x000004A8
		public bool IsFaulted
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task task;
				if ((task = obj as Task) != null)
				{
					return task.IsFaulted;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Faulted;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022E8 File Offset: 0x000004E8
		public bool IsCanceled
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task task;
				if ((task = obj as Task) != null)
				{
					return task.IsCanceled;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Canceled;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002328 File Offset: 0x00000528
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ThrowIfCompletedUnsuccessfully()
		{
			object obj = this._obj;
			if (obj != null)
			{
				Task task;
				if ((task = obj as Task) != null)
				{
					task.GetAwaiter().GetResult();
					return;
				}
				Unsafe.As<IValueTaskSource>(obj).GetResult(this._token);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002369 File Offset: 0x00000569
		public ValueTaskAwaiter GetAwaiter()
		{
			return new ValueTaskAwaiter(this);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002376 File Offset: 0x00000576
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredValueTaskAwaitable(new ValueTask(this._obj, this._token, continueOnCapturedContext));
		}

		// Token: 0x04000005 RID: 5
		private static readonly Task s_canceledTask = Task.Delay(-1, new CancellationToken(true));

		// Token: 0x04000007 RID: 7
		internal readonly object _obj;

		// Token: 0x04000008 RID: 8
		internal readonly short _token;

		// Token: 0x04000009 RID: 9
		internal readonly bool _continueOnCapturedContext;

		// Token: 0x02000014 RID: 20
		private sealed class ValueTaskSourceAsTask : TaskCompletionSource<bool>
		{
			// Token: 0x06000058 RID: 88 RVA: 0x00002B7F File Offset: 0x00000D7F
			public ValueTaskSourceAsTask(IValueTaskSource source, short token)
			{
				this._token = token;
				this._source = source;
				source.OnCompleted(ValueTask.ValueTaskSourceAsTask.s_completionAction, this, token, ValueTaskSourceOnCompletedFlags.None);
			}

			// Token: 0x04000025 RID: 37
			private static readonly Action<object> s_completionAction = delegate(object state)
			{
				ValueTask.ValueTaskSourceAsTask valueTaskSourceAsTask;
				IValueTaskSource source;
				if ((valueTaskSourceAsTask = state as ValueTask.ValueTaskSourceAsTask) == null || (source = valueTaskSourceAsTask._source) == null)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
					return;
				}
				valueTaskSourceAsTask._source = null;
				ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
				try
				{
					source.GetResult(valueTaskSourceAsTask._token);
					valueTaskSourceAsTask.TrySetResult(false);
				}
				catch (Exception ex)
				{
					if (status == ValueTaskSourceStatus.Canceled)
					{
						valueTaskSourceAsTask.TrySetCanceled();
					}
					else
					{
						valueTaskSourceAsTask.TrySetException(ex);
					}
				}
			};

			// Token: 0x04000026 RID: 38
			private IValueTaskSource _source;

			// Token: 0x04000027 RID: 39
			private readonly short _token;
		}
	}
}
