using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000043 RID: 67
	[NullableContext(1)]
	[Nullable(0)]
	[SecuritySafeCritical]
	public abstract class Instrument
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000925C File Offset: 0x0000745C
		[Nullable(new byte[] { 1, 0, 1, 2 })]
		internal static KeyValuePair<string, object>[] EmptyTags
		{
			get
			{
				return Array.Empty<KeyValuePair<string, object>>();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00009263 File Offset: 0x00007463
		internal static object SyncObject { get; } = new object();

		// Token: 0x06000212 RID: 530 RVA: 0x0000926C File Offset: 0x0000746C
		protected Instrument(Meter meter, string name, [Nullable(2)] string unit, [Nullable(2)] string description)
		{
			if (meter == null)
			{
				throw new ArgumentNullException("meter");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Meter = meter;
			this.Name = name;
			this.Description = description;
			this.Unit = unit;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000092C4 File Offset: 0x000074C4
		protected void Publish()
		{
			List<MeterListener> list = null;
			object syncObject = Instrument.SyncObject;
			lock (syncObject)
			{
				if (this.Meter.Disposed || !this.Meter.AddInstrument(this))
				{
					return;
				}
				list = MeterListener.GetAllListeners();
			}
			if (list != null)
			{
				foreach (MeterListener meterListener in list)
				{
					Action<Instrument, MeterListener> instrumentPublished = meterListener.InstrumentPublished;
					if (instrumentPublished != null)
					{
						instrumentPublished(this, meterListener);
					}
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00009374 File Offset: 0x00007574
		public Meter Meter { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000937C File Offset: 0x0000757C
		public string Name { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00009384 File Offset: 0x00007584
		[Nullable(2)]
		public string Description
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000938C File Offset: 0x0000758C
		[Nullable(2)]
		public string Unit
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00009394 File Offset: 0x00007594
		public bool Enabled
		{
			get
			{
				return this._subscriptions.First != null;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000093A7 File Offset: 0x000075A7
		public virtual bool IsObservable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000093AC File Offset: 0x000075AC
		internal void NotifyForUnpublishedInstrument()
		{
			for (DiagNode<ListenerSubscription> diagNode = this._subscriptions.First; diagNode != null; diagNode = diagNode.Next)
			{
				diagNode.Value.Listener.DisableMeasurementEvents(this);
			}
			this._subscriptions.Clear();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000093F0 File Offset: 0x000075F0
		internal static void ValidateTypeParameter<T>()
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle != typeof(byte) && typeFromHandle != typeof(short) && typeFromHandle != typeof(int) && typeFromHandle != typeof(long) && typeFromHandle != typeof(double) && typeFromHandle != typeof(float) && typeFromHandle != typeof(decimal))
			{
				throw new InvalidOperationException(SR.Format(SR.UnsupportedType, typeFromHandle));
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009498 File Offset: 0x00007698
		internal object EnableMeasurement(ListenerSubscription subscription, out bool oldStateStored)
		{
			oldStateStored = false;
			if (!this._subscriptions.AddIfNotExist(subscription, (ListenerSubscription s1, ListenerSubscription s2) => s1.Listener == s2.Listener))
			{
				ListenerSubscription listenerSubscription = this._subscriptions.Remove(subscription, (ListenerSubscription s1, ListenerSubscription s2) => s1.Listener == s2.Listener);
				this._subscriptions.AddIfNotExist(subscription, (ListenerSubscription s1, ListenerSubscription s2) => s1.Listener == s2.Listener);
				oldStateStored = listenerSubscription.Listener == subscription.Listener;
				return listenerSubscription.State;
			}
			return false;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009550 File Offset: 0x00007750
		internal object DisableMeasurements(MeterListener listener)
		{
			return this._subscriptions.Remove(new ListenerSubscription(listener, null), (ListenerSubscription s1, ListenerSubscription s2) => s1.Listener == s2.Listener).State;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009596 File Offset: 0x00007796
		internal virtual void Observe(MeterListener listener)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000095A0 File Offset: 0x000077A0
		internal object GetSubscriptionState(MeterListener listener)
		{
			for (DiagNode<ListenerSubscription> diagNode = this._subscriptions.First; diagNode != null; diagNode = diagNode.Next)
			{
				if (listener == diagNode.Value.Listener)
				{
					return diagNode.Value.State;
				}
			}
			return null;
		}

		// Token: 0x040000F5 RID: 245
		internal readonly DiagLinkedList<ListenerSubscription> _subscriptions = new DiagLinkedList<ListenerSubscription>();
	}
}
