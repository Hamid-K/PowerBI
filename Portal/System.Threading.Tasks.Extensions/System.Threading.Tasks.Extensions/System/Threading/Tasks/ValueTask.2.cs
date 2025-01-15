using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;

namespace System.Threading.Tasks
{
	// Token: 0x02000007 RID: 7
	[AsyncMethodBuilder(typeof(AsyncValueTaskMethodBuilder<>))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ValueTask<TResult> : IEquatable<ValueTask<TResult>>
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000023AD File Offset: 0x000005AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(TResult result)
		{
			this._result = result;
			this._obj = null;
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023CB File Offset: 0x000005CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(Task<TResult> task)
		{
			if (task == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.task);
			}
			this._obj = task;
			this._result = default(TResult);
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023F7 File Offset: 0x000005F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(IValueTaskSource<TResult> source, short token)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			this._obj = source;
			this._token = token;
			this._result = default(TResult);
			this._continueOnCapturedContext = true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002423 File Offset: 0x00000623
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ValueTask(object obj, TResult result, short token, bool continueOnCapturedContext)
		{
			this._obj = obj;
			this._result = result;
			this._token = token;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002444 File Offset: 0x00000644
		public override int GetHashCode()
		{
			if (this._obj != null)
			{
				return this._obj.GetHashCode();
			}
			if (this._result == null)
			{
				return 0;
			}
			TResult result = this._result;
			return result.GetHashCode();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002488 File Offset: 0x00000688
		public override bool Equals(object obj)
		{
			return obj is ValueTask<TResult> && this.Equals((ValueTask<TResult>)obj);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024A0 File Offset: 0x000006A0
		public bool Equals(ValueTask<TResult> other)
		{
			if (this._obj == null && other._obj == null)
			{
				return EqualityComparer<TResult>.Default.Equals(this._result, other._result);
			}
			return this._obj == other._obj && this._token == other._token;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024F2 File Offset: 0x000006F2
		public static bool operator ==(ValueTask<TResult> left, ValueTask<TResult> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024FC File Offset: 0x000006FC
		public static bool operator !=(ValueTask<TResult> left, ValueTask<TResult> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000250C File Offset: 0x0000070C
		public Task<TResult> AsTask()
		{
			object obj = this._obj;
			if (obj == null)
			{
				return Task.FromResult<TResult>(this._result);
			}
			Task<TResult> task;
			if ((task = obj as Task<TResult>) != null)
			{
				return task;
			}
			return this.GetTaskForValueTaskSource(Unsafe.As<IValueTaskSource<TResult>>(obj));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002547 File Offset: 0x00000747
		public ValueTask<TResult> Preserve()
		{
			if (this._obj != null)
			{
				return new ValueTask<TResult>(this.AsTask());
			}
			return this;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002564 File Offset: 0x00000764
		private Task<TResult> GetTaskForValueTaskSource(IValueTaskSource<TResult> t)
		{
			ValueTaskSourceStatus status = t.GetStatus(this._token);
			if (status != ValueTaskSourceStatus.Pending)
			{
				try
				{
					return Task.FromResult<TResult>(t.GetResult(this._token));
				}
				catch (Exception ex)
				{
					if (status == ValueTaskSourceStatus.Canceled)
					{
						Task<TResult> task = ValueTask<TResult>.s_canceledTask;
						if (task == null)
						{
							TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
							taskCompletionSource.TrySetCanceled();
							task = taskCompletionSource.Task;
							ValueTask<TResult>.s_canceledTask = task;
						}
						return task;
					}
					TaskCompletionSource<TResult> taskCompletionSource2 = new TaskCompletionSource<TResult>();
					taskCompletionSource2.TrySetException(ex);
					return taskCompletionSource2.Task;
				}
			}
			ValueTask<TResult>.ValueTaskSourceAsTask valueTaskSourceAsTask = new ValueTask<TResult>.ValueTaskSourceAsTask(t, this._token);
			return valueTaskSourceAsTask.Task;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002608 File Offset: 0x00000808
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
				Task<TResult> task;
				if ((task = obj as Task<TResult>) != null)
				{
					return task.IsCompleted;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) > ValueTaskSourceStatus.Pending;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002648 File Offset: 0x00000848
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
				Task<TResult> task;
				if ((task = obj as Task<TResult>) != null)
				{
					return task.Status == TaskStatus.RanToCompletion;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Succeeded;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000268C File Offset: 0x0000088C
		public bool IsFaulted
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task<TResult> task;
				if ((task = obj as Task<TResult>) != null)
				{
					return task.IsFaulted;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Faulted;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026CC File Offset: 0x000008CC
		public bool IsCanceled
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task<TResult> task;
				if ((task = obj as Task<TResult>) != null)
				{
					return task.IsCanceled;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Canceled;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000270C File Offset: 0x0000090C
		public TResult Result
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return this._result;
				}
				Task<TResult> task;
				if ((task = obj as Task<TResult>) != null)
				{
					return task.GetAwaiter().GetResult();
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetResult(this._token);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002754 File Offset: 0x00000954
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTaskAwaiter<TResult> GetAwaiter()
		{
			return new ValueTaskAwaiter<TResult>(this);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002761 File Offset: 0x00000961
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable<TResult> ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredValueTaskAwaitable<TResult>(new ValueTask<TResult>(this._obj, this._result, this._token, continueOnCapturedContext));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002780 File Offset: 0x00000980
		public override string ToString()
		{
			if (this.IsCompletedSuccessfully)
			{
				TResult result = this.Result;
				if (result != null)
				{
					return result.ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x0400000A RID: 10
		private static Task<TResult> s_canceledTask;

		// Token: 0x0400000B RID: 11
		internal readonly object _obj;

		// Token: 0x0400000C RID: 12
		internal readonly TResult _result;

		// Token: 0x0400000D RID: 13
		internal readonly short _token;

		// Token: 0x0400000E RID: 14
		internal readonly bool _continueOnCapturedContext;

		// Token: 0x02000015 RID: 21
		private sealed class ValueTaskSourceAsTask : TaskCompletionSource<TResult>
		{
			// Token: 0x0600005A RID: 90 RVA: 0x00002BBA File Offset: 0x00000DBA
			public ValueTaskSourceAsTask(IValueTaskSource<TResult> source, short token)
			{
				this._source = source;
				this._token = token;
				source.OnCompleted(ValueTask<TResult>.ValueTaskSourceAsTask.s_completionAction, this, token, ValueTaskSourceOnCompletedFlags.None);
			}

			// Token: 0x04000028 RID: 40
			private static readonly Action<object> s_completionAction = delegate(object state)
			{
				ValueTask<TResult>.ValueTaskSourceAsTask valueTaskSourceAsTask;
				IValueTaskSource<TResult> source;
				if ((valueTaskSourceAsTask = state as ValueTask<TResult>.ValueTaskSourceAsTask) == null || (source = valueTaskSourceAsTask._source) == null)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
					return;
				}
				valueTaskSourceAsTask._source = null;
				ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
				try
				{
					valueTaskSourceAsTask.TrySetResult(source.GetResult(valueTaskSourceAsTask._token));
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

			// Token: 0x04000029 RID: 41
			private IValueTaskSource<TResult> _source;

			// Token: 0x0400002A RID: 42
			private readonly short _token;
		}
	}
}
