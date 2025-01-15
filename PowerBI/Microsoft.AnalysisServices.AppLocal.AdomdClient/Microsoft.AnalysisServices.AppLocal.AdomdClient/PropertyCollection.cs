using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000EB RID: 235
	public sealed class PropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x06000CCF RID: 3279 RVA: 0x0002F9D5 File Offset: 0x0002DBD5
		internal PropertyCollection(DataRow propertyRow, object parent)
			: this(propertyRow, parent, 1)
		{
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002F9E0 File Offset: 0x0002DBE0
		internal PropertyCollection(DataRow propertyRow, object parent, int propertiesOffset)
		{
			this.propertyRow = propertyRow;
			this.propertiesOffset = propertiesOffset;
			int num;
			if (propertyRow == null || propertyRow.Table == null)
			{
				num = 0;
				this.namesHash = new Hashtable();
			}
			else
			{
				num = Math.Max(propertyRow.Table.Columns.Count - this.propertiesOffset, 0);
				if (propertyRow.Table.ExtendedProperties["PropertiesColumnsHash"] is Hashtable)
				{
					this.namesHash = propertyRow.Table.ExtendedProperties["PropertiesColumnsHash"] as Hashtable;
				}
				else
				{
					this.namesHash = PropertyCollection.GetNamesHash(propertyRow.Table);
					propertyRow.Table.ExtendedProperties["PropertiesColumnsHash"] = this.namesHash;
				}
			}
			this.propInternal = new Property[num];
			this.parentObject = parent;
		}

		// Token: 0x170004E2 RID: 1250
		public Property this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.GetProperty(index);
			}
		}

		// Token: 0x170004E3 RID: 1251
		public Property this[string name]
		{
			get
			{
				Property property = this.Find(name);
				if (null == property)
				{
					throw new ArgumentException(SR.Property_PropertyNotFound(name), "name");
				}
				return property;
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002FB0C File Offset: 0x0002DD0C
		public Property Find(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.propertyRow == null || this.propertyRow.Table == null)
			{
				return null;
			}
			int propertyColumnIndex = this.GetPropertyColumnIndex(name);
			if (propertyColumnIndex == -1)
			{
				return null;
			}
			return this.GetProperty(propertyColumnIndex - this.propertiesOffset);
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002FB5A File Offset: 0x0002DD5A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002FB5D File Offset: 0x0002DD5D
		public object SyncRoot
		{
			get
			{
				return this.propInternal.SyncRoot;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002FB6A File Offset: 0x0002DD6A
		public int Count
		{
			get
			{
				return this.propInternal.Length;
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002FB74 File Offset: 0x0002DD74
		public void CopyTo(Property[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002FB7E File Offset: 0x0002DD7E
		void ICollection.CopyTo(Array array, int index)
		{
			this.propInternal.CopyTo(array, index);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002FB8D File Offset: 0x0002DD8D
		public PropertyCollection.Enumerator GetEnumerator()
		{
			return new PropertyCollection.Enumerator(this);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002FB95 File Offset: 0x0002DD95
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002FBA2 File Offset: 0x0002DDA2
		private Property GetProperty(int index)
		{
			if (this.propInternal[index] == null)
			{
				this.propInternal[index] = new Property(this.propertyRow, index + this.propertiesOffset, this.parentObject);
			}
			return this.propInternal[index];
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002FBE0 File Offset: 0x0002DDE0
		private static Hashtable GetNamesHash(DataTable table)
		{
			Hashtable hashtable = new Hashtable();
			if (table == null)
			{
				return hashtable;
			}
			AdomdUtils.FillNamesHashTable(table, hashtable);
			return hashtable;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002FC00 File Offset: 0x0002DE00
		private int GetPropertyColumnIndex(string propName)
		{
			object obj = this.namesHash[propName];
			if (obj is int)
			{
				int num = (int)obj;
				if (num < this.propertiesOffset)
				{
					num = -1;
				}
				return num;
			}
			return -1;
		}

		// Token: 0x04000824 RID: 2084
		private DataRow propertyRow;

		// Token: 0x04000825 RID: 2085
		private Property[] propInternal;

		// Token: 0x04000826 RID: 2086
		private object parentObject;

		// Token: 0x04000827 RID: 2087
		private const int standardMetadataOffset = 1;

		// Token: 0x04000828 RID: 2088
		private int propertiesOffset;

		// Token: 0x04000829 RID: 2089
		private Hashtable namesHash;

		// Token: 0x0400082A RID: 2090
		private const string propertiesColumnsHash = "PropertiesColumnsHash";

		// Token: 0x020001C7 RID: 455
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060013AA RID: 5034 RVA: 0x00044EC8 File Offset: 0x000430C8
			internal Enumerator(PropertyCollection properties)
			{
				this.properties = properties;
				this.currentIndex = -1;
			}

			// Token: 0x170006DF RID: 1759
			// (get) Token: 0x060013AB RID: 5035 RVA: 0x00044ED8 File Offset: 0x000430D8
			public Property Current
			{
				get
				{
					Property property;
					try
					{
						property = this.properties[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return property;
				}
			}

			// Token: 0x170006E0 RID: 1760
			// (get) Token: 0x060013AC RID: 5036 RVA: 0x00044F14 File Offset: 0x00043114
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060013AD RID: 5037 RVA: 0x00044F1C File Offset: 0x0004311C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.properties.Count;
			}

			// Token: 0x060013AE RID: 5038 RVA: 0x00044F47 File Offset: 0x00043147
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CFC RID: 3324
			private int currentIndex;

			// Token: 0x04000CFD RID: 3325
			private PropertyCollection properties;
		}
	}
}
