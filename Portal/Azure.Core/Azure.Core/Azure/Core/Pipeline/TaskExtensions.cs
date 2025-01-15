using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x020000A9 RID: 169
	internal static class TaskExtensions
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00010203 File Offset: 0x0000E403
		public static TaskExtensions.WithCancellationTaskAwaitable AwaitWithCancellation(this Task task, CancellationToken cancellationToken)
		{
			return new TaskExtensions.WithCancellationTaskAwaitable(task, cancellationToken);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001020C File Offset: 0x0000E40C
		public static TaskExtensions.WithCancellationTaskAwaitable<T> AwaitWithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
		{
			return new TaskExtensions.WithCancellationTaskAwaitable<T>(task, cancellationToken);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00010215 File Offset: 0x0000E415
		public static TaskExtensions.WithCancellationValueTaskAwaitable<T> AwaitWithCancellation<T>(this ValueTask<T> task, CancellationToken cancellationToken)
		{
			return new TaskExtensions.WithCancellationValueTaskAwaitable<T>(task, cancellationToken);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00010220 File Offset: 0x0000E420
		public static T EnsureCompleted<T>(this Task<T> task)
		{
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001023C File Offset: 0x0000E43C
		public static void EnsureCompleted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00010258 File Offset: 0x0000E458
		public static T EnsureCompleted<T>(this ValueTask<T> task)
		{
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00010274 File Offset: 0x0000E474
		public static void EnsureCompleted(this ValueTask task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00010290 File Offset: 0x0000E490
		public static TaskExtensions.Enumerable<T> EnsureSyncEnumerable<T>(this IAsyncEnumerable<T> asyncEnumerable)
		{
			return new TaskExtensions.Enumerable<T>(asyncEnumerable);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00010298 File Offset: 0x0000E498
		public static ConfiguredValueTaskAwaitable<T> EnsureCompleted<T>(this ConfiguredValueTaskAwaitable<T> awaitable, bool async)
		{
			return awaitable;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001029D File Offset: 0x0000E49D
		public static ConfiguredValueTaskAwaitable EnsureCompleted(this ConfiguredValueTaskAwaitable awaitable, bool async)
		{
			return awaitable;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000102A2 File Offset: 0x0000E4A2
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

		// Token: 0x02000136 RID: 310
		public readonly struct Enumerable<T> : IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600084E RID: 2126 RVA: 0x000200F2 File Offset: 0x0001E2F2
			public Enumerable(IAsyncEnumerable<T> asyncEnumerable)
			{
				this._asyncEnumerable = asyncEnumerable;
			}

			// Token: 0x0600084F RID: 2127 RVA: 0x000200FC File Offset: 0x0001E2FC
			public TaskExtensions.Enumerator<T> GetEnumerator()
			{
				return new TaskExtensions.Enumerator<T>(this._asyncEnumerable.GetAsyncEnumerator(default(CancellationToken)));
			}

			// Token: 0x06000850 RID: 2128 RVA: 0x00020124 File Offset: 0x0001E324
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return new TaskExtensions.Enumerator<T>(this._asyncEnumerable.GetAsyncEnumerator(default(CancellationToken)));
			}

			// Token: 0x06000851 RID: 2129 RVA: 0x0002014F File Offset: 0x0001E34F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040004C9 RID: 1225
			private readonly IAsyncEnumerable<T> _asyncEnumerable;
		}

		// Token: 0x02000137 RID: 311
		public readonly struct Enumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000852 RID: 2130 RVA: 0x0002015C File Offset: 0x0001E35C
			public Enumerator(IAsyncEnumerator<T> asyncEnumerator)
			{
				this._asyncEnumerator = asyncEnumerator;
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x00020165 File Offset: 0x0001E365
			public bool MoveNext()
			{
				return this._asyncEnumerator.MoveNextAsync().EnsureCompleted<bool>();
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x00020177 File Offset: 0x0001E377
			public void Reset()
			{
				throw new NotSupportedException(string.Format("{0} is a synchronous wrapper for {1} async enumerator, which can't be reset, so IEnumerable.Reset() calls aren't supported.", base.GetType(), this._asyncEnumerator.GetType()));
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06000855 RID: 2133 RVA: 0x000201A3 File Offset: 0x0001E3A3
			public T Current
			{
				get
				{
					return this._asyncEnumerator.Current;
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x06000856 RID: 2134 RVA: 0x000201B0 File Offset: 0x0001E3B0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000857 RID: 2135 RVA: 0x000201BD File Offset: 0x0001E3BD
			public void Dispose()
			{
				this._asyncEnumerator.DisposeAsync().EnsureCompleted();
			}

			// Token: 0x040004CA RID: 1226
			private readonly IAsyncEnumerator<T> _asyncEnumerator;
		}

		// Token: 0x02000138 RID: 312
		public readonly struct WithCancellationTaskAwaitable
		{
			// Token: 0x06000858 RID: 2136 RVA: 0x000201CF File Offset: 0x0001E3CF
			public WithCancellationTaskAwaitable(Task task, CancellationToken cancellationToken)
			{
				this._awaitable = task.ConfigureAwait(false);
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000859 RID: 2137 RVA: 0x000201E8 File Offset: 0x0001E3E8
			public TaskExtensions.WithCancellationTaskAwaiter GetAwaiter()
			{
				return new TaskExtensions.WithCancellationTaskAwaiter(this._awaitable.GetAwaiter(), this._cancellationToken);
			}

			// Token: 0x040004CB RID: 1227
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040004CC RID: 1228
			private readonly ConfiguredTaskAwaitable _awaitable;
		}

		// Token: 0x02000139 RID: 313
		public readonly struct WithCancellationTaskAwaitable<T>
		{
			// Token: 0x0600085A RID: 2138 RVA: 0x0002020E File Offset: 0x0001E40E
			public WithCancellationTaskAwaitable(Task<T> task, CancellationToken cancellationToken)
			{
				this._awaitable = task.ConfigureAwait(false);
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x0600085B RID: 2139 RVA: 0x00020224 File Offset: 0x0001E424
			public TaskExtensions.WithCancellationTaskAwaiter<T> GetAwaiter()
			{
				return new TaskExtensions.WithCancellationTaskAwaiter<T>(this._awaitable.GetAwaiter(), this._cancellationToken);
			}

			// Token: 0x040004CD RID: 1229
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040004CE RID: 1230
			private readonly ConfiguredTaskAwaitable<T> _awaitable;
		}

		// Token: 0x0200013A RID: 314
		public readonly struct WithCancellationValueTaskAwaitable<T>
		{
			// Token: 0x0600085C RID: 2140 RVA: 0x0002024A File Offset: 0x0001E44A
			public WithCancellationValueTaskAwaitable(ValueTask<T> task, CancellationToken cancellationToken)
			{
				this._awaitable = task.ConfigureAwait(false);
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x0600085D RID: 2141 RVA: 0x00020261 File Offset: 0x0001E461
			public TaskExtensions.WithCancellationValueTaskAwaiter<T> GetAwaiter()
			{
				return new TaskExtensions.WithCancellationValueTaskAwaiter<T>(this._awaitable.GetAwaiter(), this._cancellationToken);
			}

			// Token: 0x040004CF RID: 1231
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040004D0 RID: 1232
			private readonly ConfiguredValueTaskAwaitable<T> _awaitable;
		}

		// Token: 0x0200013B RID: 315
		public readonly struct WithCancellationTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600085E RID: 2142 RVA: 0x00020279 File Offset: 0x0001E479
			public WithCancellationTaskAwaiter(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter, CancellationToken cancellationToken)
			{
				this._taskAwaiter = awaiter;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x0600085F RID: 2143 RVA: 0x0002028C File Offset: 0x0001E48C
			public bool IsCompleted
			{
				get
				{
					return this._taskAwaiter.IsCompleted || this._cancellationToken.IsCancellationRequested;
				}
			}

			// Token: 0x06000860 RID: 2144 RVA: 0x000202BC File Offset: 0x0001E4BC
			public void OnCompleted(Action continuation)
			{
				this._taskAwaiter.OnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x06000861 RID: 2145 RVA: 0x000202E0 File Offset: 0x0001E4E0
			public void UnsafeOnCompleted(Action continuation)
			{
				this._taskAwaiter.UnsafeOnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x06000862 RID: 2146 RVA: 0x00020304 File Offset: 0x0001E504
			public void GetResult()
			{
				if (!this._taskAwaiter.IsCompleted)
				{
					this._cancellationToken.ThrowIfCancellationRequested();
				}
				this._taskAwaiter.GetResult();
			}

			// Token: 0x06000863 RID: 2147 RVA: 0x00020340 File Offset: 0x0001E540
			private Action WrapContinuation(in Action originalContinuation)
			{
				if (!this._cancellationToken.CanBeCanceled)
				{
					return originalContinuation;
				}
				return new TaskExtensions.WithCancellationContinuationWrapper(originalContinuation, this._cancellationToken).Continuation;
			}

			// Token: 0x040004D1 RID: 1233
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040004D2 RID: 1234
			private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _taskAwaiter;
		}

		// Token: 0x0200013C RID: 316
		public readonly struct WithCancellationTaskAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000864 RID: 2148 RVA: 0x00020372 File Offset: 0x0001E572
			public WithCancellationTaskAwaiter(ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter awaiter, CancellationToken cancellationToken)
			{
				this._taskAwaiter = awaiter;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06000865 RID: 2149 RVA: 0x00020384 File Offset: 0x0001E584
			public bool IsCompleted
			{
				get
				{
					return this._taskAwaiter.IsCompleted || this._cancellationToken.IsCancellationRequested;
				}
			}

			// Token: 0x06000866 RID: 2150 RVA: 0x000203B4 File Offset: 0x0001E5B4
			public void OnCompleted(Action continuation)
			{
				this._taskAwaiter.OnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x000203D8 File Offset: 0x0001E5D8
			public void UnsafeOnCompleted(Action continuation)
			{
				this._taskAwaiter.UnsafeOnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x06000868 RID: 2152 RVA: 0x000203FC File Offset: 0x0001E5FC
			public T GetResult()
			{
				if (!this._taskAwaiter.IsCompleted)
				{
					this._cancellationToken.ThrowIfCancellationRequested();
				}
				return this._taskAwaiter.GetResult();
			}

			// Token: 0x06000869 RID: 2153 RVA: 0x00020438 File Offset: 0x0001E638
			private Action WrapContinuation(in Action originalContinuation)
			{
				if (!this._cancellationToken.CanBeCanceled)
				{
					return originalContinuation;
				}
				return new TaskExtensions.WithCancellationContinuationWrapper(originalContinuation, this._cancellationToken).Continuation;
			}

			// Token: 0x040004D3 RID: 1235
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040004D4 RID: 1236
			private readonly ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter _taskAwaiter;
		}

		// Token: 0x0200013D RID: 317
		public readonly struct WithCancellationValueTaskAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600086A RID: 2154 RVA: 0x0002046A File Offset: 0x0001E66A
			public WithCancellationValueTaskAwaiter(ConfiguredValueTaskAwaitable<T>.ConfiguredValueTaskAwaiter awaiter, CancellationToken cancellationToken)
			{
				this._taskAwaiter = awaiter;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x0600086B RID: 2155 RVA: 0x0002047C File Offset: 0x0001E67C
			public bool IsCompleted
			{
				get
				{
					return this._taskAwaiter.IsCompleted || this._cancellationToken.IsCancellationRequested;
				}
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x000204A6 File Offset: 0x0001E6A6
			public void OnCompleted(Action continuation)
			{
				this._taskAwaiter.OnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x0600086D RID: 2157 RVA: 0x000204BB File Offset: 0x0001E6BB
			public void UnsafeOnCompleted(Action continuation)
			{
				this._taskAwaiter.UnsafeOnCompleted(this.WrapContinuation(in continuation));
			}

			// Token: 0x0600086E RID: 2158 RVA: 0x000204D0 File Offset: 0x0001E6D0
			public T GetResult()
			{
				if (!this._taskAwaiter.IsCompleted)
				{
					this._cancellationToken.ThrowIfCancellationRequested();
				}
				return this._taskAwaiter.GetResult();
			}

			// Token: 0x0600086F RID: 2159 RVA: 0x00020504 File Offset: 0x0001E704
			private Action WrapContinuation(in Action originalContinuation)
			{
				if (!this._cancellationToken.CanBeCanceled)
				{
					return originalContinuation;
				}
				return new TaskExtensions.WithCancellationContinuationWrapper(originalContinuation, this._cancellationToken).Continuation;
			}

			// Token: 0x040004D5 RID: 1237
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040004D6 RID: 1238
			private readonly ConfiguredValueTaskAwaitable<T>.ConfiguredValueTaskAwaiter _taskAwaiter;
		}

		// Token: 0x0200013E RID: 318
		private class WithCancellationContinuationWrapper
		{
			// Token: 0x06000870 RID: 2160 RVA: 0x00020538 File Offset: 0x0001E738
			public WithCancellationContinuationWrapper(Action originalContinuation, CancellationToken cancellationToken)
			{
				Action action = new Action(this.ContinuationImplementation);
				this._originalContinuation = originalContinuation;
				this._registration = cancellationToken.Register(action);
				this.Continuation = action;
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000871 RID: 2161 RVA: 0x00020574 File Offset: 0x0001E774
			public Action Continuation { get; }

			// Token: 0x06000872 RID: 2162 RVA: 0x0002057C File Offset: 0x0001E77C
			private void ContinuationImplementation()
			{
				Action action = Interlocked.Exchange<Action>(ref this._originalContinuation, null);
				if (action != null)
				{
					this._registration.Dispose();
					action();
				}
			}

			// Token: 0x040004D7 RID: 1239
			private Action _originalContinuation;

			// Token: 0x040004D8 RID: 1240
			private readonly CancellationTokenRegistration _registration;
		}
	}
}
