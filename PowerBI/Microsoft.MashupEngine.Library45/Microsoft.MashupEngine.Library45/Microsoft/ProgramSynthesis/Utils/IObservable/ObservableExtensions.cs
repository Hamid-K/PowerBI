using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000657 RID: 1623
	public static class ObservableExtensions
	{
		// Token: 0x0600232C RID: 9004 RVA: 0x00062F00 File Offset: 0x00061100
		public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext)
		{
			return source.Subscribe(new AnonymousObserver<T>(onNext));
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x00062F0E File Offset: 0x0006110E
		public static IDisposable Subscribe<T, TObserved>(this IObservable<T> source, Action<T> onNext, IObserver<TObserved> observer)
		{
			return source.Subscribe(new AnonymousObserver<T>(onNext, new Action<Exception>(observer.OnError), new Action(observer.OnCompleted)));
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00062F36 File Offset: 0x00061136
		public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError)
		{
			return source.Subscribe(new AnonymousObserver<T>(onNext, onError));
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00062F45 File Offset: 0x00061145
		public static IDisposable Subscribe<T, TObserved>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError, IObserver<TObserved> observer)
		{
			return source.Subscribe(new AnonymousObserver<T>(onNext, onError, new Action(observer.OnCompleted)));
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00062F61 File Offset: 0x00061161
		public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action onCompleted)
		{
			return source.Subscribe(new AnonymousObserver<T>(onNext, onCompleted));
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x00062F70 File Offset: 0x00061170
		public static IDisposable Subscribe<T, TObserved>(this IObservable<T> source, Action<T> onNext, Action onCompleted, Subject<TObserved> observer)
		{
			return source.Subscribe(new AnonymousObserver<T>(onNext, new Action<Exception>(observer.OnError), onCompleted));
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00062F8C File Offset: 0x0006118C
		public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError, Action onCompleted)
		{
			return source.Subscribe(new AnonymousObserver<T>(onNext, onError, onCompleted));
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00062F9C File Offset: 0x0006119C
		public static IObservable<TSource> Merge<TSource>(this IEnumerable<IObservable<TSource>> sources)
		{
			return Observable.Create<TSource>(delegate(IObserver<TSource> observer)
			{
				CompositeDisposable compositeDisposable = new CompositeDisposable();
				Action<TSource> <>9__1;
				Action<Exception> <>9__2;
				foreach (IObservable<TSource> observable in sources)
				{
					IDisposable[] disposableReference = new IDisposable[1];
					IDisposable[] disposableReference2 = disposableReference;
					int num = 0;
					IObservable<TSource> observable2 = observable;
					Action<TSource> action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate(TSource x)
						{
							CompositeDisposable compositeDisposable4 = compositeDisposable;
							lock (compositeDisposable4)
							{
								observer.OnNext(x);
							}
						});
					}
					Action<Exception> action2;
					if ((action2 = <>9__2) == null)
					{
						action2 = (<>9__2 = delegate(Exception exception)
						{
							CompositeDisposable compositeDisposable2 = compositeDisposable;
							lock (compositeDisposable2)
							{
								observer.OnError(exception);
								compositeDisposable.Dispose();
							}
						});
					}
					disposableReference2[num] = observable2.Subscribe(action, action2, delegate
					{
						CompositeDisposable compositeDisposable3 = compositeDisposable;
						lock (compositeDisposable3)
						{
							compositeDisposable.Remove(disposableReference[0]);
							if (compositeDisposable.Count == 0)
							{
								observer.OnCompleted();
							}
						}
					});
					compositeDisposable.Add(disposableReference[0]);
				}
				return compositeDisposable;
			});
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x00062FBA File Offset: 0x000611BA
		public static IObservable<T> ToObservable<T>(this Task<T> valueTask)
		{
			ObservableExtensions.<>c__DisplayClass8_0<T> CS$<>8__locals1 = new ObservableExtensions.<>c__DisplayClass8_0<T>();
			CS$<>8__locals1.valueTask = valueTask;
			return Observable.Create<T>(delegate(IObserver<T> observer)
			{
				ObservableExtensions.<>c__DisplayClass8_1<T> CS$<>8__locals2 = new ObservableExtensions.<>c__DisplayClass8_1<T>();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.observer = observer;
				CS$<>8__locals2.cancel = Disposable.CreateCancelable();
				Task.Run(delegate
				{
					ObservableExtensions.<>c__DisplayClass8_1<T>.<<ToObservable>b__2>d <<ToObservable>b__2>d;
					<<ToObservable>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<ToObservable>b__2>d.<>4__this = CS$<>8__locals2;
					<<ToObservable>b__2>d.<>1__state = -1;
					<<ToObservable>b__2>d.<>t__builder.Start<ObservableExtensions.<>c__DisplayClass8_1<T>.<<ToObservable>b__2>d>(ref <<ToObservable>b__2>d);
					return <<ToObservable>b__2>d.<>t__builder.Task;
				});
				return CS$<>8__locals2.cancel;
			});
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00062FD8 File Offset: 0x000611D8
		public static IObservable<T> ToObservable<T>(this IEnumerable<Func<Task<T>>> valueFuncsEnumerable)
		{
			ObservableExtensions.<>c__DisplayClass9_0<T> CS$<>8__locals1 = new ObservableExtensions.<>c__DisplayClass9_0<T>();
			CS$<>8__locals1.valueFuncs = (valueFuncsEnumerable as IReadOnlyList<Func<Task<T>>>) ?? valueFuncsEnumerable.ToList<Func<Task<T>>>();
			return Observable.Create<T>(delegate(IObserver<T> observer)
			{
				ObservableExtensions.<>c__DisplayClass9_1<T> CS$<>8__locals2 = new ObservableExtensions.<>c__DisplayClass9_1<T>();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.observer = observer;
				CS$<>8__locals2.cancel = Disposable.CreateCancelable();
				Task.Run(delegate
				{
					ObservableExtensions.<>c__DisplayClass9_1<T>.<<ToObservable>b__2>d <<ToObservable>b__2>d;
					<<ToObservable>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<ToObservable>b__2>d.<>4__this = CS$<>8__locals2;
					<<ToObservable>b__2>d.<>1__state = -1;
					<<ToObservable>b__2>d.<>t__builder.Start<ObservableExtensions.<>c__DisplayClass9_1<T>.<<ToObservable>b__2>d>(ref <<ToObservable>b__2>d);
					return <<ToObservable>b__2>d.<>t__builder.Task;
				});
				return CS$<>8__locals2.cancel;
			});
		}
	}
}
