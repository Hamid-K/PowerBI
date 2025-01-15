using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001C6 RID: 454
	internal class PropertyStore : IPropertyStore
	{
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00024040 File Offset: 0x00022240
		public ReportObject Owner
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00024048 File Offset: 0x00022248
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x00024050 File Offset: 0x00022250
		public IContainedObject Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00024059 File Offset: 0x00022259
		public PropertyStore(ReportObject owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00024068 File Offset: 0x00022268
		internal PropertyStore()
		{
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00024070 File Offset: 0x00022270
		internal void SetOwner(ReportObject owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00024079 File Offset: 0x00022279
		public void RemoveProperty(int propertyIndex)
		{
			this.RemoveObject(propertyIndex);
			this.RemoveInteger(propertyIndex);
			this.RemoveBoolean(propertyIndex);
			this.RemoveSize(propertyIndex);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00024097 File Offset: 0x00022297
		public object GetObject(int propertyIndex)
		{
			if (this.m_objEntries != null && this.m_objEntries.ContainsKey(propertyIndex))
			{
				return this.m_objEntries[propertyIndex];
			}
			return null;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x000240C0 File Offset: 0x000222C0
		public T GetObject<T>(int propertyIndex)
		{
			object @object = this.GetObject(propertyIndex);
			if (@object != null)
			{
				return (T)((object)@object);
			}
			return default(T);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000240E8 File Offset: 0x000222E8
		public void SetObject(int propertyIndex, object value)
		{
			if (this.m_objEntries == null)
			{
				this.m_objEntries = new Dictionary<int, object>();
			}
			if (value is IContainedObject)
			{
				((IContainedObject)value).Parent = this.Owner;
			}
			this.m_objEntries[propertyIndex] = value;
			if (this.m_owner != null)
			{
				this.m_owner.OnSetObject(propertyIndex);
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00024142 File Offset: 0x00022342
		public void RemoveObject(int propertyIndex)
		{
			if (this.m_objEntries != null)
			{
				this.m_objEntries.Remove(propertyIndex);
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00024159 File Offset: 0x00022359
		public bool ContainsObject(int propertyIndex)
		{
			return this.m_objEntries != null && this.m_objEntries.ContainsKey(propertyIndex);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00024171 File Offset: 0x00022371
		public int GetInteger(int propertyIndex)
		{
			if (this.m_intEntries != null && this.m_intEntries.ContainsKey(propertyIndex))
			{
				return this.m_intEntries[propertyIndex];
			}
			return 0;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00024197 File Offset: 0x00022397
		public void SetInteger(int propertyIndex, int value)
		{
			if (this.m_intEntries == null)
			{
				this.m_intEntries = new Dictionary<int, int>();
			}
			this.m_intEntries[propertyIndex] = value;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000241B9 File Offset: 0x000223B9
		public void RemoveInteger(int propertyIndex)
		{
			if (this.m_intEntries != null)
			{
				this.m_intEntries.Remove(propertyIndex);
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000241D0 File Offset: 0x000223D0
		public bool ContainsInteger(int propertyIndex)
		{
			return this.m_intEntries != null && this.m_intEntries.ContainsKey(propertyIndex);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000241E8 File Offset: 0x000223E8
		public bool GetBoolean(int propertyIndex)
		{
			return this.m_boolEntries != null && this.m_boolEntries.ContainsKey(propertyIndex) && this.m_boolEntries[propertyIndex];
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0002420E File Offset: 0x0002240E
		public void SetBoolean(int propertyIndex, bool value)
		{
			if (this.m_boolEntries == null)
			{
				this.m_boolEntries = new Dictionary<int, bool>();
			}
			this.m_boolEntries[propertyIndex] = value;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00024230 File Offset: 0x00022430
		public void RemoveBoolean(int propertyIndex)
		{
			if (this.m_boolEntries != null)
			{
				this.m_boolEntries.Remove(propertyIndex);
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00024247 File Offset: 0x00022447
		public bool ContainsBoolean(int propertyIndex)
		{
			return this.m_boolEntries != null && this.m_boolEntries.ContainsKey(propertyIndex);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00024260 File Offset: 0x00022460
		public ReportSize GetSize(int propertyIndex)
		{
			if (this.m_sizeEntries != null && this.m_sizeEntries.ContainsKey(propertyIndex))
			{
				return this.m_sizeEntries[propertyIndex];
			}
			return default(ReportSize);
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00024299 File Offset: 0x00022499
		public void SetSize(int propertyIndex, ReportSize value)
		{
			if (this.m_sizeEntries == null)
			{
				this.m_sizeEntries = new Dictionary<int, ReportSize>();
			}
			this.m_sizeEntries[propertyIndex] = value;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x000242BB File Offset: 0x000224BB
		public void RemoveSize(int propertyIndex)
		{
			if (this.m_sizeEntries != null)
			{
				this.m_sizeEntries.Remove(propertyIndex);
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x000242D2 File Offset: 0x000224D2
		public bool ContainsSize(int propertyIndex)
		{
			return this.m_sizeEntries != null && this.m_sizeEntries.ContainsKey(propertyIndex);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x000242EC File Offset: 0x000224EC
		public void IterateObjectEntries(VisitPropertyObject visitObject)
		{
			if (this.m_objEntries != null)
			{
				foreach (int num in this.m_objEntries.Keys)
				{
					visitObject(num, this.m_objEntries[num]);
				}
			}
		}

		// Token: 0x0400054E RID: 1358
		private ReportObject m_owner;

		// Token: 0x0400054F RID: 1359
		private IContainedObject m_parent;

		// Token: 0x04000550 RID: 1360
		private Dictionary<int, object> m_objEntries;

		// Token: 0x04000551 RID: 1361
		private Dictionary<int, int> m_intEntries;

		// Token: 0x04000552 RID: 1362
		private Dictionary<int, bool> m_boolEntries;

		// Token: 0x04000553 RID: 1363
		private Dictionary<int, ReportSize> m_sizeEntries;
	}
}
