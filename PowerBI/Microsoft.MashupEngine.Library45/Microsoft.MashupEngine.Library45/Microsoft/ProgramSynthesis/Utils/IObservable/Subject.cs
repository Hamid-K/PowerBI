using System;
using System.Threading;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000640 RID: 1600
	public class Subject<T> : IObservable<T>, IObserver<T>, IDisposable
	{
		// Token: 0x060022D4 RID: 8916 RVA: 0x00062655 File Offset: 0x00060855
		public Subject()
		{
			Volatile.Write<Subject<T>.SubjectDisposable[]>(ref this._observers, new Subject<T>.SubjectDisposable[0]);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0006266E File Offset: 0x0006086E
		public bool HasObservers()
		{
			return Volatile.Read<Subject<T>.SubjectDisposable[]>(ref this._observers).Length != 0;
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x0006267F File Offset: 0x0006087F
		public bool IsDisposed
		{
			get
			{
				return Volatile.Read<Subject<T>.SubjectDisposable[]>(ref this._observers) == Subject<T>.Disposed;
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00062693 File Offset: 0x00060893
		private void ThrowDisposed()
		{
			throw new ObjectDisposedException(string.Empty);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000626A0 File Offset: 0x000608A0
		public virtual IDisposable Subscribe(IObserver<T> observer)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			Subject<T>.SubjectDisposable subjectDisposable = null;
			for (;;)
			{
				Subject<T>.SubjectDisposable[] array = Volatile.Read<Subject<T>.SubjectDisposable[]>(ref this._observers);
				if (array == Subject<T>.Disposed)
				{
					break;
				}
				if (array == Subject<T>.Terminated)
				{
					goto Block_3;
				}
				if (subjectDisposable == null)
				{
					subjectDisposable = new Subject<T>.SubjectDisposable(this, observer);
				}
				int num = array.Length;
				Subject<T>.SubjectDisposable[] array2 = new Subject<T>.SubjectDisposable[num + 1];
				Array.Copy(array, 0, array2, 0, num);
				array2[num] = subjectDisposable;
				if (Interlocked.CompareExchange<Subject<T>.SubjectDisposable[]>(ref this._observers, array2, array) == array)
				{
					return subjectDisposable;
				}
			}
			this._exception = null;
			this.ThrowDisposed();
			goto IL_0091;
			Block_3:
			Exception exception = this._exception;
			if (exception != null)
			{
				observer.OnError(exception);
				goto IL_0091;
			}
			observer.OnCompleted();
			IL_0091:
			return Disposable.Empty;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x00062744 File Offset: 0x00060944
		public virtual void OnNext(T value)
		{
			Subject<T>.SubjectDisposable[] array = Volatile.Read<Subject<T>.SubjectDisposable[]>(ref this._observers);
			if (array == Subject<T>.Disposed)
			{
				this._exception = null;
				this.ThrowDisposed();
				return;
			}
			Subject<T>.SubjectDisposable[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				IObserver<T> observer = array2[i].Observer;
				if (observer != null)
				{
					observer.OnNext(value);
				}
			}
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00062798 File Offset: 0x00060998
		public virtual void OnError(Exception error)
		{
			if (error == null)
			{
				throw new ArgumentNullException("error");
			}
			Subject<T>.SubjectDisposable[] array;
			for (;;)
			{
				array = Volatile.Read<Subject<T>.SubjectDisposable[]>(ref this._observers);
				if (array == Subject<T>.Disposed)
				{
					break;
				}
				if (array == Subject<T>.Terminated)
				{
					return;
				}
				this._exception = error;
				if (Interlocked.CompareExchange<Subject<T>.SubjectDisposable[]>(ref this._observers, Subject<T>.Terminated, array) == array)
				{
					goto Block_4;
				}
			}
			this._exception = null;
			this.ThrowDisposed();
			return;
			Block_4:
			Subject<T>.SubjectDisposable[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				IObserver<T> observer = array2[i].Observer;
				if (observer != null)
				{
					observer.OnError(error);
				}
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0006281C File Offset: 0x00060A1C
		public void OnCompleted()
		{
			Subject<T>.SubjectDisposable[] array;
			for (;;)
			{
				array = Volatile.Read<Subject<T>.SubjectDisposable[]>(ref this._observers);
				if (array == Subject<T>.Disposed)
				{
					break;
				}
				if (array == Subject<T>.Terminated)
				{
					return;
				}
				if (Interlocked.CompareExchange<Subject<T>.SubjectDisposable[]>(ref this._observers, Subject<T>.Terminated, array) == array)
				{
					goto Block_2;
				}
			}
			this._exception = null;
			this.ThrowDisposed();
			return;
			Block_2:
			Subject<T>.SubjectDisposable[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				IObserver<T> observer = array2[i].Observer;
				if (observer != null)
				{
					observer.OnCompleted();
				}
			}
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x0006288C File Offset: 0x00060A8C
		private void Unsubscribe(Subject<T>.SubjectDisposable observer)
		{
			Subject<T>.SubjectDisposable[] array;
			Subject<T>.SubjectDisposable[] array2;
			do
			{
				array = Volatile.Read<Subject<T>.SubjectDisposable[]>(ref this._observers);
				int num = array.Length;
				if (num == 0)
				{
					break;
				}
				int num2 = Array.IndexOf<Subject<T>.SubjectDisposable>(array, observer);
				if (num2 < 0)
				{
					break;
				}
				if (num == 1)
				{
					array2 = new Subject<T>.SubjectDisposable[0];
				}
				else
				{
					array2 = new Subject<T>.SubjectDisposable[num - 1];
					Array.Copy(array, 0, array2, 0, num2);
					Array.Copy(array, num2 + 1, array2, num2, num - num2 - 1);
				}
			}
			while (Interlocked.CompareExchange<Subject<T>.SubjectDisposable[]>(ref this._observers, array2, array) != array);
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000628FA File Offset: 0x00060AFA
		public void Dispose()
		{
			Interlocked.Exchange<Subject<T>.SubjectDisposable[]>(ref this._observers, Subject<T>.Disposed);
			this._exception = null;
		}

		// Token: 0x0400107A RID: 4218
		private Subject<T>.SubjectDisposable[] _observers;

		// Token: 0x0400107B RID: 4219
		private Exception _exception;

		// Token: 0x0400107C RID: 4220
		private static readonly Subject<T>.SubjectDisposable[] Terminated = new Subject<T>.SubjectDisposable[0];

		// Token: 0x0400107D RID: 4221
		private static readonly Subject<T>.SubjectDisposable[] Disposed = new Subject<T>.SubjectDisposable[0];

		// Token: 0x02000641 RID: 1601
		private class SubjectDisposable : IDisposable
		{
			// Token: 0x060022DF RID: 8927 RVA: 0x0006292C File Offset: 0x00060B2C
			public SubjectDisposable(Subject<T> subject, IObserver<T> observer)
			{
				this._subject = subject;
				Volatile.Write<IObserver<T>>(ref this._observer, observer);
			}

			// Token: 0x060022E0 RID: 8928 RVA: 0x00062947 File Offset: 0x00060B47
			public void Dispose()
			{
				if (Interlocked.Exchange<IObserver<T>>(ref this._observer, null) == null)
				{
					return;
				}
				this._subject.Unsubscribe(this);
				this._subject = null;
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x060022E1 RID: 8929 RVA: 0x0006296B File Offset: 0x00060B6B
			public IObserver<T> Observer
			{
				get
				{
					return Volatile.Read<IObserver<T>>(ref this._observer);
				}
			}

			// Token: 0x0400107E RID: 4222
			private Subject<T> _subject;

			// Token: 0x0400107F RID: 4223
			private IObserver<T> _observer;
		}
	}
}
