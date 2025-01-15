using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000663 RID: 1635
	public static class ObservableOperators
	{
		// Token: 0x0600234F RID: 9039 RVA: 0x0006384E File Offset: 0x00061A4E
		public static IObservable<TResult> Select<TSource, TResult>(this IObservable<TSource> source, Func<TSource, TResult> selector)
		{
			return Observable.Create<TResult>((IObserver<TResult> observer) => source.Subscribe(delegate(TSource x)
			{
				TResult tresult;
				try
				{
					tresult = selector(x);
				}
				catch (Exception ex)
				{
					observer.OnError(ex);
					return;
				}
				observer.OnNext(tresult);
			}, observer));
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x00063873 File Offset: 0x00061A73
		public static IObservable<TSource> Do<TSource>(this IObservable<TSource> source, Action<TSource> onNext)
		{
			return Observable.Create<TSource>((IObserver<TSource> observer) => source.Subscribe(delegate(TSource x)
			{
				try
				{
					onNext(x);
				}
				catch (Exception ex)
				{
					observer.OnError(ex);
					return;
				}
				observer.OnNext(x);
			}, observer));
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x00063898 File Offset: 0x00061A98
		public static IObservable<TResult> SelectMany<TSource, TResult>(this IObservable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			return Observable.Create<TResult>((IObserver<TResult> observer) => source.Subscribe(delegate(TSource x)
			{
				IEnumerable<TResult> enumerable;
				try
				{
					enumerable = selector(x);
				}
				catch (Exception ex)
				{
					observer.OnError(ex);
					return;
				}
				foreach (TResult tresult in enumerable)
				{
					observer.OnNext(tresult);
				}
			}, observer));
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000638BD File Offset: 0x00061ABD
		public static IObservable<TSource> Where<TSource>(this IObservable<TSource> source, Func<TSource, bool> filter)
		{
			return Observable.Create<TSource>((IObserver<TSource> observer) => source.Subscribe(delegate(TSource x)
			{
				bool flag;
				try
				{
					flag = filter(x);
				}
				catch (Exception ex)
				{
					observer.OnError(ex);
					return;
				}
				if (flag)
				{
					observer.OnNext(x);
				}
			}, observer));
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000638E4 File Offset: 0x00061AE4
		public static IObservable<TSource> SelectValues<TSource>(this IObservable<Optional<TSource>> source)
		{
			return from option in source
				where option.HasValue
				select option.Value;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x0006393A File Offset: 0x00061B3A
		public static IObservable<TSource> First<TSource>(this IObservable<TSource> source)
		{
			return Observable.Create<TSource>(delegate(IObserver<TSource> observer)
			{
				IDisposable[] subscriptionReference = new IDisposable[1];
				subscriptionReference[0] = source.Subscribe(delegate(TSource x)
				{
					observer.OnNext(x);
					observer.OnCompleted();
					subscriptionReference[0].Dispose();
				}, observer);
				return subscriptionReference[0];
			});
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x00063958 File Offset: 0x00061B58
		public static Task<Optional<T>> FirstAsync<T>(this IObservable<T> source)
		{
			ObservableOperators.<>c__DisplayClass6_0<T> CS$<>8__locals1 = new ObservableOperators.<>c__DisplayClass6_0<T>();
			CS$<>8__locals1.taskSource = new TaskCompletionSource<Optional<T>>();
			CS$<>8__locals1.subscriptionReference = new IDisposable[1];
			IConnectableObservable<T> connectableObservable = source.Publish<T>();
			CS$<>8__locals1.subscriptionReference[0] = connectableObservable.Subscribe(new Action<T>(CS$<>8__locals1.<FirstAsync>g__OnNext|0), new Action<Exception>(CS$<>8__locals1.<FirstAsync>g__OnError|1), new Action(CS$<>8__locals1.<FirstAsync>g__OnCompleted|2));
			connectableObservable.Connect();
			return CS$<>8__locals1.taskSource.Task;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000639CD File Offset: 0x00061BCD
		public static IObservable<TSource> Last<TSource>(this IObservable<TSource> source)
		{
			return Observable.Create<TSource>(delegate(IObserver<TSource> observer)
			{
				Optional<TSource> last = Optional<TSource>.Nothing;
				return source.Subscribe(delegate(TSource x)
				{
					last = x.Some<TSource>();
				}, new Action<Exception>(observer.OnError), delegate
				{
					if (last.HasValue)
					{
						observer.OnNext(last.Value);
					}
					observer.OnCompleted();
				});
			});
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000639EB File Offset: 0x00061BEB
		public static Task<Optional<TSource>> LastAsync<TSource>(this IObservable<TSource> source)
		{
			return source.Last<TSource>().FirstAsync<TSource>();
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000639F8 File Offset: 0x00061BF8
		public static IObservable<TSource> Scan<TSource>(this IObservable<TSource> source, Func<TSource, TSource, TSource> scanner)
		{
			return Observable.Create<TSource>(delegate(IObserver<TSource> observer)
			{
				Optional<TSource> previous = Optional<TSource>.Nothing;
				return source.Subscribe(delegate(TSource x)
				{
					TSource tsource;
					if (previous.HasValue)
					{
						try
						{
							tsource = scanner(previous.Value, x);
							goto IL_003D;
						}
						catch (Exception ex)
						{
							observer.OnError(ex);
							return;
						}
					}
					tsource = x;
					IL_003D:
					observer.OnNext(tsource);
					previous = tsource.Some<TSource>();
				}, observer);
			});
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x00063A1D File Offset: 0x00061C1D
		public static IObservable<TSource> TakeUntil<TSource, TOther>(this IObservable<TSource> source, IObservable<TOther> other)
		{
			return Observable.Create<TSource>(delegate(IObserver<TSource> observer)
			{
				CompositeDisposable compositeDisposable = new CompositeDisposable();
				compositeDisposable.Add(source.Subscribe(delegate(TSource x)
				{
					CompositeDisposable compositeDisposable7 = compositeDisposable;
					lock (compositeDisposable7)
					{
						observer.OnNext(x);
					}
				}, delegate(Exception exception)
				{
					CompositeDisposable compositeDisposable2 = compositeDisposable;
					lock (compositeDisposable2)
					{
						observer.OnError(exception);
						compositeDisposable.Dispose();
					}
				}, delegate
				{
					CompositeDisposable compositeDisposable3 = compositeDisposable;
					lock (compositeDisposable3)
					{
						observer.OnCompleted();
						compositeDisposable.Dispose();
					}
				}));
				compositeDisposable.Add(other.Subscribe(delegate(TOther x)
				{
					CompositeDisposable compositeDisposable4 = compositeDisposable;
					lock (compositeDisposable4)
					{
						observer.OnCompleted();
						compositeDisposable.Dispose();
					}
				}, delegate(Exception exception)
				{
					CompositeDisposable compositeDisposable5 = compositeDisposable;
					lock (compositeDisposable5)
					{
						observer.OnError(exception);
						compositeDisposable.Dispose();
					}
				}, delegate
				{
					CompositeDisposable compositeDisposable6 = compositeDisposable;
					lock (compositeDisposable6)
					{
						observer.OnCompleted();
						compositeDisposable.Dispose();
					}
				}));
				return compositeDisposable;
			});
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x00063A42 File Offset: 0x00061C42
		public static IObservable<TSource> TakeWhile<TSource>(this IObservable<TSource> source, Func<TSource, bool> predicate)
		{
			return Observable.Create<TSource>(delegate(IObserver<TSource> observer)
			{
				IDisposable[] subscriptionReference = new IDisposable[1];
				subscriptionReference[0] = source.Subscribe(delegate(TSource x)
				{
					bool flag;
					try
					{
						flag = predicate(x);
					}
					catch (Exception ex)
					{
						observer.OnError(ex);
						return;
					}
					if (flag)
					{
						observer.OnNext(x);
						return;
					}
					observer.OnCompleted();
					subscriptionReference[0].Dispose();
				}, observer);
				return subscriptionReference[0];
			});
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x00063A67 File Offset: 0x00061C67
		public static IObservable<TAccumulate> Scan<TSource, TAccumulate>(this IObservable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> scanner)
		{
			return Observable.Create<TAccumulate>(delegate(IObserver<TAccumulate> observer)
			{
				TAccumulate previous = seed;
				return source.Subscribe(delegate(TSource x)
				{
					try
					{
						previous = scanner(previous, x);
					}
					catch (Exception ex)
					{
						observer.OnError(ex);
						return;
					}
					observer.OnNext(previous);
				}, observer);
			});
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x00063A94 File Offset: 0x00061C94
		public static IObservable<TSource> Nth<TSource>(this IObservable<TSource> source, int index)
		{
			return from record in (from record in source.Scan(Record.Create<int, TSource>(-1, default(TSource)), (Record<int, TSource> record, TSource value) => Record.Create<int, TSource>(record.Item1 + 1, value))
					where record.Item1 == index
					select record).First<Record<int, TSource>>()
				select record.Item2;
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00063B1C File Offset: 0x00061D1C
		public static Task<Optional<TSource>> NthAsync<TSource>(this IObservable<TSource> source, int index)
		{
			return source.Nth(index).FirstAsync<TSource>();
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x00063B2A File Offset: 0x00061D2A
		public static IObservable<TSource> Aggregate<TSource>(this IObservable<TSource> source, Func<TSource, TSource, TSource> aggregator)
		{
			return source.Scan(aggregator).Last<TSource>();
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x00063B38 File Offset: 0x00061D38
		public static Task<Optional<TSource>> AggregateAsync<TSource>(this IObservable<TSource> source, Func<TSource, TSource, TSource> aggregator)
		{
			return source.Scan(aggregator).LastAsync<TSource>();
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x00063B46 File Offset: 0x00061D46
		public static IObservable<TAccumulate> Aggregate<TSource, TAccumulate>(this IObservable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> aggregator)
		{
			return source.Scan(seed, aggregator).Last<TAccumulate>();
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x00063B58 File Offset: 0x00061D58
		public static async Task<TAccumulate> AggregateAsync<TSource, TAccumulate>(this IObservable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> aggregator)
		{
			return (await source.Scan(seed, aggregator).LastAsync<TAccumulate>().ConfigureAwait(false)).OrElse(seed);
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x00063BAC File Offset: 0x00061DAC
		public static IObservable<TResult> Select<TSource, TResult>(this IObservable<TSource> source, Func<TSource, int, TResult> selector)
		{
			return from record in source.Scan(Record.Create<int, TResult>(0, default(TResult)), (Record<int, TResult> record, TSource value) => Record.Create<int, TResult>(record.Item1 + 1, selector(value, record.Item1)))
				select record.Item2;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x00063C0B File Offset: 0x00061E0B
		public static Task<List<TSource>> ToList<TSource>(this IObservable<TSource> source)
		{
			return source.AggregateAsync(new List<TSource>(), delegate(List<TSource> list, TSource value)
			{
				list.Add(value);
				return list;
			});
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x00063C37 File Offset: 0x00061E37
		public static IConnectableObservable<TSource> Publish<TSource>(this IObservable<TSource> source)
		{
			return new ConnectableObservable<TSource>(source, new Subject<TSource>());
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x00063C44 File Offset: 0x00061E44
		public static IObservable<Notification<TSource>> Materialize<TSource>(this IObservable<TSource> source)
		{
			return Observable.Create<Notification<TSource>>((IObserver<Notification<TSource>> observer) => source.Subscribe(delegate(TSource x)
			{
				observer.OnNext(new OnNextNotification<TSource>(x));
			}, delegate(Exception exception)
			{
				observer.OnNext(new OnErrorNotification<TSource>(exception));
				observer.OnCompleted();
			}, delegate
			{
				observer.OnNext(new OnCompletedNotification<TSource>());
				observer.OnCompleted();
			}));
		}
	}
}
