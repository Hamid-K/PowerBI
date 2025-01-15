using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public class DiagnosticListener : DiagnosticSource, IObservable<KeyValuePair<string, object>>, IDisposable
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002BAB File Offset: 0x00000DAB
		public static IObservable<DiagnosticListener> AllListeners
		{
			get
			{
				GC.KeepAlive(HttpHandlerDiagnosticListener.s_instance);
				DiagnosticListener.AllListenerObservable allListenerObservable;
				if ((allListenerObservable = DiagnosticListener.s_allListenerObservable) == null)
				{
					allListenerObservable = Interlocked.CompareExchange<DiagnosticListener.AllListenerObservable>(ref DiagnosticListener.s_allListenerObservable, new DiagnosticListener.AllListenerObservable(), null) ?? DiagnosticListener.s_allListenerObservable;
				}
				return allListenerObservable;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public virtual IDisposable Subscribe([Nullable(new byte[] { 1, 0, 1, 2 })] IObserver<KeyValuePair<string, object>> observer, [Nullable(new byte[] { 2, 1 })] Predicate<string> isEnabled)
		{
			IDisposable disposable;
			if (isEnabled == null)
			{
				disposable = this.SubscribeInternal(observer, null, null, null, null);
			}
			else
			{
				Predicate<string> localIsEnabled = isEnabled;
				disposable = this.SubscribeInternal(observer, isEnabled, (string name, object arg1, object arg2) => localIsEnabled(name), null, null);
			}
			return disposable;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C23 File Offset: 0x00000E23
		public virtual IDisposable Subscribe([Nullable(new byte[] { 1, 0, 1, 2 })] IObserver<KeyValuePair<string, object>> observer, [Nullable(new byte[] { 2, 1, 2, 2 })] Func<string, object, object, bool> isEnabled)
		{
			if (isEnabled != null)
			{
				return this.SubscribeInternal(observer, (string name) => this.IsEnabled(name, null, null), isEnabled, null, null);
			}
			return this.SubscribeInternal(observer, null, null, null, null);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C4A File Offset: 0x00000E4A
		public virtual IDisposable Subscribe([Nullable(new byte[] { 1, 0, 1, 2 })] IObserver<KeyValuePair<string, object>> observer)
		{
			return this.SubscribeInternal(observer, null, null, null, null);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C58 File Offset: 0x00000E58
		public DiagnosticListener(string name)
		{
			this.Name = name;
			object obj = DiagnosticListener.s_allListenersLock;
			lock (obj)
			{
				DiagnosticListener.AllListenerObservable allListenerObservable = DiagnosticListener.s_allListenerObservable;
				if (allListenerObservable != null)
				{
					allListenerObservable.OnNewDiagnosticListener(this);
				}
				this._next = DiagnosticListener.s_allListeners;
				DiagnosticListener.s_allListeners = this;
			}
			GC.KeepAlive(DiagnosticSourceEventSource.Log);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CCC File Offset: 0x00000ECC
		public virtual void Dispose()
		{
			object obj = DiagnosticListener.s_allListenersLock;
			lock (obj)
			{
				if (this._disposed)
				{
					return;
				}
				this._disposed = true;
				if (DiagnosticListener.s_allListeners == this)
				{
					DiagnosticListener.s_allListeners = DiagnosticListener.s_allListeners._next;
				}
				else
				{
					for (DiagnosticListener next = DiagnosticListener.s_allListeners; next != null; next = next._next)
					{
						if (next._next == this)
						{
							next._next = this._next;
							break;
						}
					}
				}
				this._next = null;
			}
			DiagnosticListener.DiagnosticSubscription diagnosticSubscription = null;
			Interlocked.Exchange<DiagnosticListener.DiagnosticSubscription>(ref diagnosticSubscription, this._subscriptions);
			while (diagnosticSubscription != null)
			{
				diagnosticSubscription.Observer.OnCompleted();
				diagnosticSubscription = diagnosticSubscription.Next;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002D8C File Offset: 0x00000F8C
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002D94 File Offset: 0x00000F94
		public string Name { get; private set; }

		// Token: 0x06000063 RID: 99 RVA: 0x00002D9D File Offset: 0x00000F9D
		public override string ToString()
		{
			return this.Name ?? string.Empty;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002DAE File Offset: 0x00000FAE
		public bool IsEnabled()
		{
			return this._subscriptions != null;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002DBC File Offset: 0x00000FBC
		public override bool IsEnabled(string name)
		{
			for (DiagnosticListener.DiagnosticSubscription diagnosticSubscription = this._subscriptions; diagnosticSubscription != null; diagnosticSubscription = diagnosticSubscription.Next)
			{
				if (diagnosticSubscription.IsEnabled1Arg == null || diagnosticSubscription.IsEnabled1Arg(name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002DF8 File Offset: 0x00000FF8
		[NullableContext(2)]
		public override bool IsEnabled([Nullable(1)] string name, object arg1, object arg2 = null)
		{
			for (DiagnosticListener.DiagnosticSubscription diagnosticSubscription = this._subscriptions; diagnosticSubscription != null; diagnosticSubscription = diagnosticSubscription.Next)
			{
				if (diagnosticSubscription.IsEnabled3Arg == null || diagnosticSubscription.IsEnabled3Arg(name, arg1, arg2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E38 File Offset: 0x00001038
		[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
		public override void Write(string name, [Nullable(2)] object value)
		{
			for (DiagnosticListener.DiagnosticSubscription diagnosticSubscription = this._subscriptions; diagnosticSubscription != null; diagnosticSubscription = diagnosticSubscription.Next)
			{
				diagnosticSubscription.Observer.OnNext(new KeyValuePair<string, object>(name, value));
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E6C File Offset: 0x0000106C
		private IDisposable SubscribeInternal(IObserver<KeyValuePair<string, object>> observer, Predicate<string> isEnabled1Arg, Func<string, object, object, bool> isEnabled3Arg, Action<Activity, object> onActivityImport, Action<Activity, object> onActivityExport)
		{
			if (this._disposed)
			{
				return new DiagnosticListener.DiagnosticSubscription
				{
					Owner = this
				};
			}
			DiagnosticListener.DiagnosticSubscription diagnosticSubscription = new DiagnosticListener.DiagnosticSubscription
			{
				Observer = observer,
				IsEnabled1Arg = isEnabled1Arg,
				IsEnabled3Arg = isEnabled3Arg,
				OnActivityImport = onActivityImport,
				OnActivityExport = onActivityExport,
				Owner = this,
				Next = this._subscriptions
			};
			while (Interlocked.CompareExchange<DiagnosticListener.DiagnosticSubscription>(ref this._subscriptions, diagnosticSubscription, diagnosticSubscription.Next) != diagnosticSubscription.Next)
			{
				diagnosticSubscription.Next = this._subscriptions;
			}
			return diagnosticSubscription;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002EFC File Offset: 0x000010FC
		public override void OnActivityImport(Activity activity, [Nullable(2)] object payload)
		{
			for (DiagnosticListener.DiagnosticSubscription diagnosticSubscription = this._subscriptions; diagnosticSubscription != null; diagnosticSubscription = diagnosticSubscription.Next)
			{
				Action<Activity, object> onActivityImport = diagnosticSubscription.OnActivityImport;
				if (onActivityImport != null)
				{
					onActivityImport(activity, payload);
				}
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F34 File Offset: 0x00001134
		public override void OnActivityExport(Activity activity, [Nullable(2)] object payload)
		{
			for (DiagnosticListener.DiagnosticSubscription diagnosticSubscription = this._subscriptions; diagnosticSubscription != null; diagnosticSubscription = diagnosticSubscription.Next)
			{
				Action<Activity, object> onActivityExport = diagnosticSubscription.OnActivityExport;
				if (onActivityExport != null)
				{
					onActivityExport(activity, payload);
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F69 File Offset: 0x00001169
		public virtual IDisposable Subscribe([Nullable(new byte[] { 1, 0, 1, 2 })] IObserver<KeyValuePair<string, object>> observer, [Nullable(new byte[] { 2, 1, 2, 2 })] Func<string, object, object, bool> isEnabled, [Nullable(new byte[] { 2, 1, 2 })] Action<Activity, object> onActivityImport = null, [Nullable(new byte[] { 2, 1, 2 })] Action<Activity, object> onActivityExport = null)
		{
			if (isEnabled != null)
			{
				return this.SubscribeInternal(observer, (string name) => this.IsEnabled(name, null, null), isEnabled, onActivityImport, onActivityExport);
			}
			return this.SubscribeInternal(observer, null, null, onActivityImport, onActivityExport);
		}

		// Token: 0x04000010 RID: 16
		private volatile DiagnosticListener.DiagnosticSubscription _subscriptions;

		// Token: 0x04000011 RID: 17
		private DiagnosticListener _next;

		// Token: 0x04000012 RID: 18
		private bool _disposed;

		// Token: 0x04000013 RID: 19
		private static DiagnosticListener s_allListeners;

		// Token: 0x04000014 RID: 20
		private static volatile DiagnosticListener.AllListenerObservable s_allListenerObservable;

		// Token: 0x04000015 RID: 21
		private static readonly object s_allListenersLock = new object();

		// Token: 0x02000070 RID: 112
		private sealed class DiagnosticSubscription : IDisposable
		{
			// Token: 0x060002F2 RID: 754 RVA: 0x0000B42C File Offset: 0x0000962C
			public void Dispose()
			{
				DiagnosticListener.DiagnosticSubscription subscriptions;
				DiagnosticListener.DiagnosticSubscription diagnosticSubscription;
				do
				{
					subscriptions = this.Owner._subscriptions;
					diagnosticSubscription = DiagnosticListener.DiagnosticSubscription.Remove(subscriptions, this);
				}
				while (Interlocked.CompareExchange<DiagnosticListener.DiagnosticSubscription>(ref this.Owner._subscriptions, diagnosticSubscription, subscriptions) != subscriptions);
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x0000B464 File Offset: 0x00009664
			private static DiagnosticListener.DiagnosticSubscription Remove(DiagnosticListener.DiagnosticSubscription subscriptions, DiagnosticListener.DiagnosticSubscription subscription)
			{
				if (subscriptions == null)
				{
					return null;
				}
				if (subscriptions.Observer == subscription.Observer && subscriptions.IsEnabled1Arg == subscription.IsEnabled1Arg && subscriptions.IsEnabled3Arg == subscription.IsEnabled3Arg)
				{
					return subscriptions.Next;
				}
				return new DiagnosticListener.DiagnosticSubscription
				{
					Observer = subscriptions.Observer,
					Owner = subscriptions.Owner,
					IsEnabled1Arg = subscriptions.IsEnabled1Arg,
					IsEnabled3Arg = subscriptions.IsEnabled3Arg,
					Next = DiagnosticListener.DiagnosticSubscription.Remove(subscriptions.Next, subscription)
				};
			}

			// Token: 0x04000156 RID: 342
			internal IObserver<KeyValuePair<string, object>> Observer;

			// Token: 0x04000157 RID: 343
			internal Predicate<string> IsEnabled1Arg;

			// Token: 0x04000158 RID: 344
			internal Func<string, object, object, bool> IsEnabled3Arg;

			// Token: 0x04000159 RID: 345
			internal Action<Activity, object> OnActivityImport;

			// Token: 0x0400015A RID: 346
			internal Action<Activity, object> OnActivityExport;

			// Token: 0x0400015B RID: 347
			internal DiagnosticListener Owner;

			// Token: 0x0400015C RID: 348
			internal DiagnosticListener.DiagnosticSubscription Next;
		}

		// Token: 0x02000071 RID: 113
		private sealed class AllListenerObservable : IObservable<DiagnosticListener>
		{
			// Token: 0x060002F5 RID: 757 RVA: 0x0000B500 File Offset: 0x00009700
			public IDisposable Subscribe(IObserver<DiagnosticListener> observer)
			{
				object s_allListenersLock = DiagnosticListener.s_allListenersLock;
				IDisposable subscriptions;
				lock (s_allListenersLock)
				{
					for (DiagnosticListener diagnosticListener = DiagnosticListener.s_allListeners; diagnosticListener != null; diagnosticListener = diagnosticListener._next)
					{
						observer.OnNext(diagnosticListener);
					}
					this._subscriptions = new DiagnosticListener.AllListenerObservable.AllListenerSubscription(this, observer, this._subscriptions);
					subscriptions = this._subscriptions;
				}
				return subscriptions;
			}

			// Token: 0x060002F6 RID: 758 RVA: 0x0000B570 File Offset: 0x00009770
			internal void OnNewDiagnosticListener(DiagnosticListener diagnosticListener)
			{
				for (DiagnosticListener.AllListenerObservable.AllListenerSubscription allListenerSubscription = this._subscriptions; allListenerSubscription != null; allListenerSubscription = allListenerSubscription.Next)
				{
					allListenerSubscription.Subscriber.OnNext(diagnosticListener);
				}
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x0000B59C File Offset: 0x0000979C
			private bool Remove(DiagnosticListener.AllListenerObservable.AllListenerSubscription subscription)
			{
				object s_allListenersLock = DiagnosticListener.s_allListenersLock;
				bool flag2;
				lock (s_allListenersLock)
				{
					if (this._subscriptions == subscription)
					{
						this._subscriptions = subscription.Next;
						flag2 = true;
					}
					else
					{
						if (this._subscriptions != null)
						{
							DiagnosticListener.AllListenerObservable.AllListenerSubscription allListenerSubscription = this._subscriptions;
							while (allListenerSubscription.Next != null)
							{
								if (allListenerSubscription.Next == subscription)
								{
									allListenerSubscription.Next = allListenerSubscription.Next.Next;
									return true;
								}
								allListenerSubscription = allListenerSubscription.Next;
							}
						}
						flag2 = false;
					}
				}
				return flag2;
			}

			// Token: 0x0400015D RID: 349
			private DiagnosticListener.AllListenerObservable.AllListenerSubscription _subscriptions;

			// Token: 0x0200009E RID: 158
			internal sealed class AllListenerSubscription : IDisposable
			{
				// Token: 0x060003F4 RID: 1012 RVA: 0x0000E00C File Offset: 0x0000C20C
				internal AllListenerSubscription(DiagnosticListener.AllListenerObservable owner, IObserver<DiagnosticListener> subscriber, DiagnosticListener.AllListenerObservable.AllListenerSubscription next)
				{
					this._owner = owner;
					this.Subscriber = subscriber;
					this.Next = next;
				}

				// Token: 0x060003F5 RID: 1013 RVA: 0x0000E029 File Offset: 0x0000C229
				public void Dispose()
				{
					if (this._owner.Remove(this))
					{
						this.Subscriber.OnCompleted();
					}
				}

				// Token: 0x040001E0 RID: 480
				private readonly DiagnosticListener.AllListenerObservable _owner;

				// Token: 0x040001E1 RID: 481
				internal readonly IObserver<DiagnosticListener> Subscriber;

				// Token: 0x040001E2 RID: 482
				internal DiagnosticListener.AllListenerObservable.AllListenerSubscription Next;
			}
		}
	}
}
