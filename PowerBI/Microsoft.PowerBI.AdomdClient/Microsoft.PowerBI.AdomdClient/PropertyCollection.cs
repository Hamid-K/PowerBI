using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000EB RID: 235
	public sealed class PropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002F6A5 File Offset: 0x0002D8A5
		internal PropertyCollection(DataRow propertyRow, object parent)
			: this(propertyRow, parent, 1)
		{
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002F6B0 File Offset: 0x0002D8B0
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

		// Token: 0x170004DC RID: 1244
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

		// Token: 0x170004DD RID: 1245
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

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002F7DC File Offset: 0x0002D9DC
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

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0002F82A File Offset: 0x0002DA2A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0002F82D File Offset: 0x0002DA2D
		public object SyncRoot
		{
			get
			{
				return this.propInternal.SyncRoot;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0002F83A File Offset: 0x0002DA3A
		public int Count
		{
			get
			{
				return this.propInternal.Length;
			}
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002F844 File Offset: 0x0002DA44
		public void CopyTo(Property[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002F84E File Offset: 0x0002DA4E
		void ICollection.CopyTo(Array array, int index)
		{
			this.propInternal.CopyTo(array, index);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002F85D File Offset: 0x0002DA5D
		public PropertyCollection.Enumerator GetEnumerator()
		{
			return new PropertyCollection.Enumerator(this);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002F865 File Offset: 0x0002DA65
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002F872 File Offset: 0x0002DA72
		private Property GetProperty(int index)
		{
			if (this.propInternal[index] == null)
			{
				this.propInternal[index] = new Property(this.propertyRow, index + this.propertiesOffset, this.parentObject);
			}
			return this.propInternal[index];
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002F8B0 File Offset: 0x0002DAB0
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

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002F8D0 File Offset: 0x0002DAD0
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

		// Token: 0x04000817 RID: 2071
		private DataRow propertyRow;

		// Token: 0x04000818 RID: 2072
		private Property[] propInternal;

		// Token: 0x04000819 RID: 2073
		private object parentObject;

		// Token: 0x0400081A RID: 2074
		private const int standardMetadataOffset = 1;

		// Token: 0x0400081B RID: 2075
		private int propertiesOffset;

		// Token: 0x0400081C RID: 2076
		private Hashtable namesHash;

		// Token: 0x0400081D RID: 2077
		private const string propertiesColumnsHash = "PropertiesColumnsHash";

		// Token: 0x020001C7 RID: 455
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600139D RID: 5021 RVA: 0x0004498C File Offset: 0x00042B8C
			internal Enumerator(PropertyCollection properties)
			{
				this.properties = properties;
				this.currentIndex = -1;
			}

			// Token: 0x170006D9 RID: 1753
			// (get) Token: 0x0600139E RID: 5022 RVA: 0x0004499C File Offset: 0x00042B9C
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

			// Token: 0x170006DA RID: 1754
			// (get) Token: 0x0600139F RID: 5023 RVA: 0x000449D8 File Offset: 0x00042BD8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060013A0 RID: 5024 RVA: 0x000449E0 File Offset: 0x00042BE0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.properties.Count;
			}

			// Token: 0x060013A1 RID: 5025 RVA: 0x00044A0B File Offset: 0x00042C0B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CEB RID: 3307
			private int currentIndex;

			// Token: 0x04000CEC RID: 3308
			private PropertyCollection properties;
		}
	}
}
