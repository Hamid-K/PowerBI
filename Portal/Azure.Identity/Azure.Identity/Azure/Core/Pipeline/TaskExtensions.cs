using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200001D RID: 29
	internal static class TaskExtensions
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00003B13 File Offset: 0x00001D13
		public static TaskExtensions.WithCancellationTaskAwaitable AwaitWithCancellation(this Task task, CancellationToken cancellationToken)
		{
			return new TaskExtensions.WithCancellationTaskAwaitable(task, cancellationToken);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003B1C File Offset: 0x00001D1C
		public static TaskExtensions.WithCancellationTaskAwaitable<T> AwaitWithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
		{
			return new TaskExtensions.WithCancellationTaskAwaitable<T>(task, cancellationToken);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003B25 File Offset: 0x00001D25
		public static TaskExtensions.WithCancellationValueTaskAwaitable<T> AwaitWithCancellation<T>(this ValueTask<T> task, CancellationToken cancellationToken)
		{
			return new TaskExtensions.WithCancellationValueTaskAwaitable<T>(task, cancellationToken);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003B30 File Offset: 0x00001D30
		public static T EnsureCompleted<T>(this Task<T> task)
		{
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003B4C File Offset: 0x00001D4C
		public static void EnsureCompleted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003B68 File Offset: 0x00001D68
		public static T EnsureCompleted<T>(this ValueTask<T> task)
		{
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003B84 File Offset: 0x00001D84
		public static void EnsureCompleted(this ValueTask task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public static TaskExtensions.Enumerable<T> EnsureSyncEnumerable<T>(this IAsyncEnumerable<T> asyncEnumerable)
		{
			return new TaskExtensions.Enumerable<T>(asyncEnumerable);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public static ConfiguredValueTaskAwaitable<T> EnsureCompleted<T>(this ConfiguredValueTaskAwaitable<T> awaitable, bool async)
		{
			return awaitable;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003BAD File Offset: 0x00001DAD
		public static ConfiguredValueTaskAwaitable EnsureCompleted(this ConfiguredValueTaskAwaitable awaitable, bool async)
		{
			return awaitable;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003BB2 File Offset: 0x00001DB2
		[Conditional("DEBUG")]
		private static void VerifyTaskCompleted(bool isCompleted)
		{
			if (!isCompleted)
			{
				if (Debugger.IsAttached)
				{
					Debugger.Break();
				}
				throw new InvalidOperationException("Task is not completed");
			}
		}

		// Token: 0x0200009C RID: 156
		public readonly struct Enumerable<T> : IEnumerable<T>, IEnumerable
		{
			// Token: 0x060004CD RID: 1229 RVA: 0x0000F3AE File Offset: 0x0000D5AE
			public Enumerable(IAsyncEnumerable<T> asyncEnumerable)
			{
				this._asyncEnumerable = asyncEnumerable;
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
			public TaskExtensions.Enumerator<T> GetEnumerator()
			{
				return new TaskExtensions.Enumerator<T>(this._asyncEnumerable.GetAsyncEnumerator(default(CancellationToken)));
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return new TaskExtensions.Enumerator<T>(this._asyncEnumerable.GetAsyncEnumerator(default(CancellationToken)));
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x0000F40B File Offset: 0x0000D60B
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040002D0 RID: 720
			private readonly IAsyncEnumerable<T> _asyncEnumerable;
		}

		// Token: 0x0200009D RID: 157
		public readonly struct Enumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060004D1 RID: 1233 RVA: 0x0000F418 File Offset: 0x0000D618
			public Enumerator(IAsyncEnumerator<T> asyncEnumerator)
			{
				this._asyncEnumerator = asyncEnumerator;
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x0000F421 File Offset: 0x0000D621
			public bool MoveNext()
			{
				return this._asyncEnumerator.MoveNextAsync().EnsureCompleted<bool>();
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x0000F433 File Offset: 0x0000D633
			public void Reset()
			{
				throw new NotSupportedException(string.Format("{0} is a synchronous wrapper for {1} async enumerator, which can't be reset, so IEnumerable.Reset() calls aren't supported.", base.GetType(), this._asyncEnumerator.GetType()));
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000F45F File Offset: 0x0000D65F
			public T Current
			{
				get
				{
					return this._asyncEnumerator.Current;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000F46C File Offset: 0x0000D66C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x0000F479 File Offset: 0x0000D679
			public void Dispose()
			{
				this._asyncEnumerator.DisposeAsync().EnsureCompleted();
			}

			// Token: 0x040002D1 RID: 721
			private readonly IAsyncEnumerator<T> _asyncEnumerator;
		}

		// Token: 0x0200009E RID: 158
		public readonly struct WithCancellationTaskAwaitable
		{
			// Token: 0x060004D7 RID: 1239 RVA: 0x0000F48B File Offset: 0x0000D68B
			public WithCancellationTaskAwaitable(Task task, CancellationToken cancellationToken)
			{
				this._awaitable = task.ConfigureAwait(false);
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x0000F4A4 File Offset: 0x0000D6A4
			public TaskExtensions.WithCancellationTaskAwaiter GetAwaiter()
			{
				return new TaskExtensions.WithCancellationTaskAwaiter(this._awaitable.GetAwaiter(), this._cancellationToken);
			}

			// Token: 0x040002D2 RID: 722
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040002D3 RID: 723
			private readonly ConfiguredTaskAwaitable _awaitable;
		}

		// Token: 0x0200009F RID: 159
		public readonly struct WithCancellationTaskAwaitable<T>
		{
			// Token: 0x060004D9 RID: 1241 RVA: 0x0000F4CA File Offset: 0x0000D6CA
			public WithCancellationTaskAwaitable(Task<T> task, CancellationToken cancellationToken)
			{
				this._awaitable = task.ConfigureAwait(false);
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
			public TaskExtensions.WithCancellationTaskAwaiter<T> GetAwaiter()
			{
				return new TaskExtensions.WithCancellationTaskAwaiter<T>(this._awaitable.GetAwaiter(), this._cancellationToken);
			}

			// Token: 0x040002D4 RID: 724
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040002D5 RID: 725
			private readonly ConfiguredTaskAwaitable<T> _awaitable;
		}

		// Token: 0x020000A0 RID: 160
		public readonly struct WithCancellationValueTaskAwaitable<T>
		{
			// Token: 0x060004DB RID: 1243 RVA: 0x0000F506 File Offset: 0x0000D706
			public WithCancellationValueTaskAwaitable(ValueTask<T> task, CancellationToken cancellationToken)
			{
				this._awaitable = task.ConfigureAwait(false);
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x0000F51D File Offset: 0x0000D71D
			public TaskExtensions.WithCancellationValueTaskAwaiter<T> GetAwaiter()
			{
				return new TaskExtensions.WithCancellationValueTaskAwaiter<T>(this._awaitable.GetAwaiter(), this._cancellationToken);
			}

			// Token: 0x040002D6 RID: 726
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040002D7 RID: 727
			private readonly ConfiguredValueTaskAwaitable<T> _awaitable;
		}

		// Token: 0x020000A1 RID: 161
		public readonly struct WithCancellationTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x060004DD RID: 1245 RVA: 0x0000F535 File Offset: 0x0000D735
			public WithCancellationTaskAwaiter(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter, CancellationToken cancellationToken)
			{
				this._taskAwaiter = awaiter;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000F548 File Offset: 0x0000D748
			public bool IsCompleted
			{
				get
				{
					return this._taskAwaiter.IsCompleted || this._cancellationToken.IsCancellationRequested;
				}
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x0000F578 File Offset: 0x0000D778
			public void OnCompleted(Action continuation)
			{
				this._taskAwaiter.OnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x0000F59C File Offset: 0x0000D79C
			public void UnsafeOnCompleted(Action continuation)
			{
				this._taskAwaiter.UnsafeOnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
			public void GetResult()
			{
				if (!this._taskAwaiter.IsCompleted)
				{
					this._cancellationToken.ThrowIfCancellationRequested();
				}
				this._taskAwaiter.GetResult();
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x0000F5FC File Offset: 0x0000D7FC
			private Action WrapContinuation(in Action originalContinuation)
			{
				if (!this._cancellationToken.CanBeCanceled)
				{
					return originalContinuation;
				}
				return new TaskExtensions.WithCancellationContinuationWrapper(originalContinuation, this._cancellationToken).Continuation;
			}

			// Token: 0x040002D8 RID: 728
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040002D9 RID: 729
			private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _taskAwaiter;
		}

		// Token: 0x020000A2 RID: 162
		public readonly struct WithCancellationTaskAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x060004E3 RID: 1251 RVA: 0x0000F62E File Offset: 0x0000D82E
			public WithCancellationTaskAwaiter(ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter awaiter, CancellationToken cancellationToken)
			{
				this._taskAwaiter = awaiter;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000F640 File Offset: 0x0000D840
			public bool IsCompleted
			{
				get
				{
					return this._taskAwaiter.IsCompleted || this._cancellationToken.IsCancellationRequested;
				}
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x0000F670 File Offset: 0x0000D870
			public void OnCompleted(Action continuation)
			{
				this._taskAwaiter.OnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x0000F694 File Offset: 0x0000D894
			public void UnsafeOnCompleted(Action continuation)
			{
				this._taskAwaiter.UnsafeOnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x0000F6B8 File Offset: 0x0000D8B8
			public T GetResult()
			{
				if (!this._taskAwaiter.IsCompleted)
				{
					this._cancellationToken.ThrowIfCancellationRequested();
				}
				return this._taskAwaiter.GetResult();
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
			private Action WrapContinuation(in Action originalContinuation)
			{
				if (!this._cancellationToken.CanBeCanceled)
				{
					return originalContinuation;
				}
				return new TaskExtensions.WithCancellationContinuationWrapper(originalContinuation, this._cancellationToken).Continuation;
			}

			// Token: 0x040002DA RID: 730
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040002DB RID: 731
			private readonly ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter _taskAwaiter;
		}

		// Token: 0x020000A3 RID: 163
		public readonly struct WithCancellationValueTaskAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x060004E9 RID: 1257 RVA: 0x0000F726 File Offset: 0x0000D926
			public WithCancellationValueTaskAwaiter(ConfiguredValueTaskAwaitable<T>.ConfiguredValueTaskAwaiter awaiter, CancellationToken cancellationToken)
			{
				this._taskAwaiter = awaiter;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000F738 File Offset: 0x0000D938
			public bool IsCompleted
			{
				get
				{
					return this._taskAwaiter.IsCompleted || this._cancellationToken.IsCancellationRequested;
				}
			}

			// Token: 0x060004EB RID: 1259 RVA: 0x0000F762 File Offset: 0x0000D962
			public void OnCompleted(Action continuation)
			{
				this._taskAwaiter.OnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x060004EC RID: 1260 RVA: 0x0000F777 File Offset: 0x0000D977
			public void UnsafeOnCompleted(Action continuation)
			{
				this._taskAwaiter.UnsafeOnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x0000F78C File Offset: 0x0000D98C
			public T GetResult()
			{
				if (!this._taskAwaiter.IsCompleted)
				{
					this._cancellationToken.ThrowIfCancellationRequested();
				}
				return this._taskAwaiter.GetResult();
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
			private Action WrapContinuation(in Action originalContinuation)
			{
				if (!this._cancellationToken.CanBeCanceled)
				{
					return originalContinuation;
				}
				return new TaskExtensions.WithCancellationContinuationWrapper(originalContinuation, this._cancellationToken).Continuation;
			}

			// Token: 0x040002DC RID: 732
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040002DD RID: 733
			private readonly ConfiguredValueTaskAwaitable<T>.ConfiguredValueTaskAwaiter _taskAwaiter;
		}

		// Token: 0x020000A4 RID: 164
		private class WithCancellationContinuationWrapper
		{
			// Token: 0x060004EF RID: 1263 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
			public WithCancellationContinuationWrapper(Action originalContinuation, CancellationToken cancellationToken)
			{
				Action action = new Action(this.ContinuationImplementation);
				this._originalContinuation = originalContinuation;
				this._registration = cancellationToken.Register(action);
				this.Continuation = action;
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000F830 File Offset: 0x0000DA30
			public Action Continuation { get; }

			// Token: 0x060004F1 RID: 1265 RVA: 0x0000F838 File Offset: 0x0000DA38
			private void ContinuationImplementation()
			{
				Action action = Interlocked.Exchange<Action>(ref this._originalContinuation, null);
				if (action != null)
				{
					this._registration.Dispose();
					action();
				}
			}

			// Token: 0x040002DE RID: 734
			private Action _originalContinuation;

			// Token: 0x040002DF RID: 735
			private readonly CancellationTokenRegistration _registration;
		}
	}
}
