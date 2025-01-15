using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001C7 RID: 455
	public abstract class RdlCollectionBase<T> : Collection<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IContainedObject
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00024358 File Offset: 0x00022558
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00024360 File Offset: 0x00022560
		[XmlIgnore]
		public IContainedObject Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
				if (typeof(IContainedObject).IsAssignableFrom(typeof(T)))
				{
					foreach (T t in this)
					{
						((IContainedObject)((object)t)).Parent = value;
					}
				}
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000243D4 File Offset: 0x000225D4
		protected override void InsertItem(int index, T item)
		{
			if (item is IContainedObject)
			{
				((IContainedObject)((object)item)).Parent = this.m_parent;
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00024401 File Offset: 0x00022601
		protected override void SetItem(int index, T item)
		{
			if (item is IContainedObject)
			{
				((IContainedObject)((object)item)).Parent = this.m_parent;
			}
			base.SetItem(index, item);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0002442E File Offset: 0x0002262E
		protected RdlCollectionBase()
		{
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00024436 File Offset: 0x00022636
		protected RdlCollectionBase(IContainedObject parent)
		{
			this.m_parent = parent;
		}

		// Token: 0x17000531 RID: 1329
		object IList.this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				base[index] = (T)((object)value);
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00024462 File Offset: 0x00022662
		int IList.Add(object item)
		{
			base.Add((T)((object)item));
			return base.Count - 1;
		}

		// Token: 0x04000554 RID: 1364
		private IContainedObject m_parent;
	}
}
