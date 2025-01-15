using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004A7 RID: 1191
	public static class LastDistinctResultObservable
	{
		// Token: 0x06001ABB RID: 6843 RVA: 0x0005069C File Offset: 0x0004E89C
		public static IObservable<T> ObserveResults<T>(this IEnumerable<Func<Task<Optional<T>>>> computations, CancellationToken cancel = default(CancellationToken))
		{
			return new LastDistinctResultObservable.ObservableImpl<T>(computations, LastDistinctResultObservable.ObservableImpl<T>.LastResult, cancel);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000506AA File Offset: 0x0004E8AA
		public static IObservable<IReadOnlyList<T>> ObserveAccumulatedResults<T>(this IEnumerable<Func<Task<Optional<IReadOnlyList<T>>>>> computations, CancellationToken cancel = default(CancellationToken))
		{
			return new LastDistinctResultObservable.ObservableImpl<IReadOnlyList<T>>(computations, new LastDistinctResultObservable.CombineResults<IReadOnlyList<T>>(LastDistinctResultObservable.<ObserveAccumulatedResults>g__AppendCombiner|1_0<T>), cancel);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000506BF File Offset: 0x0004E8BF
		[CompilerGenerated]
		internal static Optional<IReadOnlyList<T>> <ObserveAccumulatedResults>g__AppendCombiner|1_0<T>(Optional<IReadOnlyList<T>> existingList, IReadOnlyList<T> newList)
		{
			if (newList == null || newList.Count == 0)
			{
				return Optional<IReadOnlyList<T>>.Nothing;
			}
			if (!existingList.HasValue)
			{
				return newList.Some<IReadOnlyList<T>>();
			}
			return existingList.Value.Concat(newList).ToList<T>().Some<IReadOnlyList<T>>();
		}

		// Token: 0x020004A8 RID: 1192
		// (Invoke) Token: 0x06001ABF RID: 6847
		private delegate Optional<T> CombineResults<T>(Optional<T> oldResult, T newResult);

		// Token: 0x020004A9 RID: 1193
		private class ObservableImpl<T> : IObservable<T>
		{
			// Token: 0x06001AC2 RID: 6850 RVA: 0x000506FC File Offset: 0x0004E8FC
			public ObservableImpl(IEnumerable<Func<Task<Optional<T>>>> computations, LastDistinctResultObservable.CombineResults<T> resultsCombiner, CancellationToken cancel = default(CancellationToken))
			{
				this._computations = computations;
				this._resultsCombiner = resultsCombiner;
				this._cancel = cancel;
				this._last = Optional<T>.Nothing;
				this._done = false;
				this._observers = new List<IObserver<T>>();
				new Task(new Action(this.DoComputations)).Start();
			}

			// Token: 0x06001AC3 RID: 6851 RVA: 0x00050758 File Offset: 0x0004E958
			public IDisposable Subscribe(IObserver<T> observer)
			{
				IList<IObserver<T>> observers = this._observers;
				IDisposable disposable;
				lock (observers)
				{
					if (this._last.HasValue)
					{
						observer.OnNext(this._last.Value);
					}
					if (this._exception != null)
					{
						observer.OnError(this._exception);
					}
					else if (this._done)
					{
						observer.OnCompleted();
					}
					else
					{
						this._observers.Add(observer);
					}
					disposable = new LastDistinctResultObservable.ObservableImpl<T>.Subscription(this, observer);
				}
				return disposable;
			}

			// Token: 0x06001AC4 RID: 6852 RVA: 0x000507EC File Offset: 0x0004E9EC
			private void Unsubscribe(IObserver<T> observer)
			{
				IList<IObserver<T>> observers = this._observers;
				lock (observers)
				{
					this._observers.Remove(observer);
				}
			}

			// Token: 0x06001AC5 RID: 6853 RVA: 0x00050834 File Offset: 0x0004EA34
			private async void DoComputations()
			{
				Task<Optional<T>> runningTask = null;
				IList<IObserver<T>> list;
				foreach (Func<Task<Optional<T>>> computation in this._computations.Where((Func<Task<Optional<T>>> c) => c != null).AppendItem(null))
				{
					this._cancel.ThrowIfCancellationRequested();
					Optional<T> optional = Optional<T>.Nothing;
					if (runningTask != null)
					{
						try
						{
							optional = await runningTask;
						}
						catch (Exception ex)
						{
							list = this._observers;
							bool flag = false;
							try
							{
								Monitor.Enter(list, ref flag);
								this._exception = ex;
								List<IObserver<T>>.Enumerator enumerator2 = this._observers.ToList<IObserver<T>>().GetEnumerator();
								try
								{
									while (enumerator2.MoveNext())
									{
										IObserver<T> observer = enumerator2.Current;
										observer.OnError(ex);
									}
								}
								finally
								{
									int num;
									if (num < 0)
									{
										((IDisposable)enumerator2).Dispose();
									}
								}
								return;
							}
							finally
							{
								int num;
								if (num < 0 && flag)
								{
									Monitor.Exit(list);
								}
							}
						}
					}
					if (computation != null)
					{
						runningTask = computation();
					}
					if (optional.HasValue)
					{
						Optional<T> optional2 = this._resultsCombiner(this._last, optional.Value);
						if (optional2.HasValue && !optional2.Equals(this._last))
						{
							list = this._observers;
							lock (list)
							{
								this._last = optional2;
								foreach (IObserver<T> observer2 in this._observers.ToList<IObserver<T>>())
								{
									observer2.OnNext(optional2.Value);
								}
							}
							computation = null;
						}
					}
				}
				IEnumerator<Func<Task<Optional<T>>>> enumerator = null;
				list = this._observers;
				lock (list)
				{
					this._done = true;
					foreach (IObserver<T> observer3 in this._observers.ToList<IObserver<T>>())
					{
						observer3.OnCompleted();
					}
				}
			}

			// Token: 0x04000D27 RID: 3367
			internal static readonly LastDistinctResultObservable.CombineResults<T> LastResult = (Optional<T> _, T newResult) => newResult.Some<T>();

			// Token: 0x04000D28 RID: 3368
			private readonly CancellationToken _cancel;

			// Token: 0x04000D29 RID: 3369
			private readonly IEnumerable<Func<Task<Optional<T>>>> _computations;

			// Token: 0x04000D2A RID: 3370
			private readonly LastDistinctResultObservable.CombineResults<T> _resultsCombiner;

			// Token: 0x04000D2B RID: 3371
			private readonly IList<IObserver<T>> _observers;

			// Token: 0x04000D2C RID: 3372
			private Optional<T> _last;

			// Token: 0x04000D2D RID: 3373
			private bool _done;

			// Token: 0x04000D2E RID: 3374
			private Exception _exception;

			// Token: 0x020004AA RID: 1194
			private class Subscription : IDisposable
			{
				// Token: 0x06001AC7 RID: 6855 RVA: 0x00050882 File Offset: 0x0004EA82
				public Subscription(LastDistinctResultObservable.ObservableImpl<T> observable, IObserver<T> observer)
				{
					this._observable = observable;
					this._observer = observer;
				}

				// Token: 0x06001AC8 RID: 6856 RVA: 0x00050898 File Offset: 0x0004EA98
				public void Dispose()
				{
					this._observable.Unsubscribe(this._observer);
				}

				// Token: 0x04000D2F RID: 3375
				private readonly LastDistinctResultObservable.ObservableImpl<T> _observable;

				// Token: 0x04000D30 RID: 3376
				private readonly IObserver<T> _observer;
			}
		}
	}
}
