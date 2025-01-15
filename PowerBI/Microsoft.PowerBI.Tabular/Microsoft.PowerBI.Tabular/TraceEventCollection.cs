using System;
using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000E0 RID: 224
	[Guid("8294A436-B408-4594-A0D1-31B2C7F2904A")]
	public sealed class TraceEventCollection : ICollection, IEnumerable
	{
		// Token: 0x06000ECF RID: 3791 RVA: 0x00072B0A File Offset: 0x00070D0A
		internal TraceEventCollection()
		{
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00072B1D File Offset: 0x00070D1D
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170003C6 RID: 966
		public TraceEvent this[int index]
		{
			get
			{
				return (TraceEvent)this.items[index];
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00072B40 File Offset: 0x00070D40
		public int Add(TraceEvent item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (item.owningCollection == this)
			{
				throw new InvalidOperationException(SR.Collections_ItemIsAlreadyInCollectionException(item.EventID.ToString(), typeof(TraceEvent).Name, typeof(TraceEventCollection).Name));
			}
			if (item.owningCollection != null)
			{
				throw new InvalidOperationException(SR.Collections_ItemIsAlreadyInAnotherCollection(typeof(TraceEvent).Name, typeof(TraceEventCollection).Name));
			}
			if (this.Contains(item.EventID))
			{
				throw new InvalidOperationException(SR.Collections_ItemIsAlreadyInCollectionException(item.EventID.ToString(), typeof(TraceEvent).Name, typeof(TraceEventCollection).Name));
			}
			item.owningCollection = this;
			return this.items.Add(item);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00072C38 File Offset: 0x00070E38
		public void Clear()
		{
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				this[i].owningCollection = null;
				i++;
			}
			this.items.Clear();
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00072C75 File Offset: 0x00070E75
		public bool Contains(TraceEvent item)
		{
			return item.owningCollection == this;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00072C80 File Offset: 0x00070E80
		public void Remove(TraceEvent item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (item.owningCollection != this)
			{
				throw new InvalidOperationException(SR.Collections_ItemNotInCollectionException(item.EventID.ToString()));
			}
			this.items.Remove(item);
			item.owningCollection = null;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00072CD8 File Offset: 0x00070ED8
		internal void CopyTo(TraceEventCollection col)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			if (col == this)
			{
				return;
			}
			col.Clear();
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				col.Add(this[i].Clone());
				i++;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00072D29 File Offset: 0x00070F29
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00072D2C File Offset: 0x00070F2C
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00072D2F File Offset: 0x00070F2F
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00072D3E File Offset: 0x00070F3E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x170003C9 RID: 969
		public TraceEvent this[TraceEventClass eventId]
		{
			get
			{
				TraceEvent traceEvent = this.Find(eventId);
				if (traceEvent == null)
				{
					throw Utils.CreateItemNotFoundException(eventId, "EventID", typeof(TraceEvent).Name);
				}
				return traceEvent;
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00072D88 File Offset: 0x00070F88
		public TraceEvent Add(TraceEventClass eventId)
		{
			if (this.Contains(eventId))
			{
				throw new InvalidOperationException(SR.Collections_ItemIsAlreadyInCollectionException(eventId.ToString(), typeof(TraceEvent).Name, typeof(TraceEventCollection).Name));
			}
			TraceEvent traceEvent = new TraceEvent(eventId);
			this.items.Add(traceEvent);
			traceEvent.owningCollection = this;
			return traceEvent;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00072DF0 File Offset: 0x00070FF0
		public bool Contains(TraceEventClass eventId)
		{
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				if (((TraceEvent)this.items[i]).EventID == eventId)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00072E34 File Offset: 0x00071034
		public TraceEvent Find(TraceEventClass eventId)
		{
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				if (((TraceEvent)this.items[i]).EventID == eventId)
				{
					return (TraceEvent)this.items[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00072E88 File Offset: 0x00071088
		public void Remove(TraceEventClass eventId)
		{
			TraceEvent traceEvent = this[eventId];
			if (traceEvent == null)
			{
				throw new InvalidOperationException(SR.Collections_ItemNotInCollectionException(eventId.ToString()));
			}
			this.items.Remove(traceEvent);
			traceEvent.owningCollection = null;
		}

		// Token: 0x040001B4 RID: 436
		private ArrayList items = new ArrayList();
	}
}
