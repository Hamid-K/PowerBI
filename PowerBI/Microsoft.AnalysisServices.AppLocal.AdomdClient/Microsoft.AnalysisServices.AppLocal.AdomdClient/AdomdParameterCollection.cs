using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000062 RID: 98
	public sealed class AdomdParameterCollection : MarshalByRefObject, IDataParameterCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x000225E0 File Offset: 0x000207E0
		internal AdomdParameterCollection(AdomdCommand parent)
		{
			this.parent = parent;
			this.items = new ArrayList();
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x000225FA File Offset: 0x000207FA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00022607 File Offset: 0x00020807
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0002260A File Offset: 0x0002080A
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0002260D File Offset: 0x0002080D
		int IList.Add(object value)
		{
			this.Add((AdomdParameter)value);
			return this.Count - 1;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00022624 File Offset: 0x00020824
		public void Clear()
		{
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				((AdomdParameter)this.items[i]).Parent = null;
			}
			this.items.Clear();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0002266B File Offset: 0x0002086B
		bool IList.Contains(object value)
		{
			return this.Contains((AdomdParameter)value);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00022679 File Offset: 0x00020879
		public bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00022688 File Offset: 0x00020888
		public bool Contains(AdomdParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00022697 File Offset: 0x00020897
		int IList.IndexOf(object value)
		{
			return this.IndexOf((AdomdParameter)value);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000226A5 File Offset: 0x000208A5
		public int IndexOf(AdomdParameter value)
		{
			return this.items.IndexOf(value);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000226B3 File Offset: 0x000208B3
		public void Insert(int index, AdomdParameter value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.items.Insert(index, value);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000226D1 File Offset: 0x000208D1
		void IList.Insert(int index, object value)
		{
			this.ValidateType(value);
			this.Insert(index, (AdomdParameter)value);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000226E8 File Offset: 0x000208E8
		public void Remove(AdomdParameter value)
		{
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			throw new ArgumentException(SR.Property_DoesNotExist, "value");
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00022718 File Offset: 0x00020918
		void IList.Remove(object value)
		{
			this.ValidateType(value);
			this.Remove((AdomdParameter)value);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0002272D File Offset: 0x0002092D
		public void RemoveAt(int index)
		{
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0002273D File Offset: 0x0002093D
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00022740 File Offset: 0x00020940
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A5 RID: 421
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (AdomdParameter)value;
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00022762 File Offset: 0x00020962
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00022770 File Offset: 0x00020970
		public int IndexOf(string parameterName)
		{
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (parameterName == ((AdomdParameter)this.items[i]).ParameterName)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000227B8 File Offset: 0x000209B8
		public void RemoveAt(string parameterName)
		{
			int num = this.RangeCheck(parameterName);
			this.RemoveIndex(num);
		}

		// Token: 0x170001A6 RID: 422
		object IDataParameterCollection.this[string index]
		{
			get
			{
				if (index == null)
				{
					throw new ArgumentNullException("index");
				}
				int num = this.items.IndexOf(index);
				if (-1 == num)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "key");
				}
				return this.items[num];
			}
			set
			{
				this.ValidateType(value);
				int num = this.items.IndexOf(index);
				this[num] = (AdomdParameter)value;
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0002284E File Offset: 0x00020A4E
		public void CopyTo(AdomdParameter[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0002285D File Offset: 0x00020A5D
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0002286C File Offset: 0x00020A6C
		public AdomdParameter Add(AdomdParameter value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.items.Add(value);
			return value;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0002288B File Offset: 0x00020A8B
		public AdomdParameter Add(string parameterName, object value)
		{
			return this.Add(new AdomdParameter(parameterName, value));
		}

		// Token: 0x170001A7 RID: 423
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AdomdParameter this[int index]
		{
			get
			{
				this.RangeCheck(index);
				return (AdomdParameter)this.items[index];
			}
			set
			{
				this.RangeCheck(index);
				this.Replace(index, value);
			}
		}

		// Token: 0x170001A8 RID: 424
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AdomdParameter this[string parameterName]
		{
			get
			{
				AdomdParameter adomdParameter = this.Find(parameterName);
				if (adomdParameter == null)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(parameterName), "parameterName");
				}
				return adomdParameter;
			}
			set
			{
				int num = this.RangeCheck(parameterName);
				this.Replace(num, value);
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00022914 File Offset: 0x00020B14
		public AdomdParameter Find(string parameterName)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException("parameterName");
			}
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				return null;
			}
			return (AdomdParameter)this.items[num];
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0002294E File Offset: 0x00020B4E
		private Type ItemType
		{
			get
			{
				return typeof(AdomdParameter);
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0002295A File Offset: 0x00020B5A
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00022974 File Offset: 0x00020B74
		private int RangeCheck(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("parameterName");
			}
			return num;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0002298C File Offset: 0x00020B8C
		private void Replace(int index, AdomdParameter newValue)
		{
			this.Validate(index, newValue);
			((AdomdParameter)this.items[index]).Parent = null;
			newValue.Parent = this;
			this.items[index] = newValue;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000229C4 File Offset: 0x00020BC4
		internal void Validate(int index, AdomdParameter value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Parent != null)
			{
				if (this != value.Parent)
				{
					throw new ArgumentException(SR.Parameter_Parent_Mismatch, "value");
				}
				if (index != this.IndexOf(value.ParameterName))
				{
					throw new ArgumentException(SR.Parameter_Already_Exists(value.ParameterName), "value");
				}
			}
			string text = value.ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				int num = 0;
				while (index < 2147483647 && num != -1)
				{
					text = "Parameter" + index.ToString(CultureInfo.InvariantCulture);
					num = this.IndexOf(text);
					index++;
				}
				if (-1 != num)
				{
					text = "Parameter" + Guid.NewGuid().ToString();
				}
				value.ParameterName = text;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00022A94 File Offset: 0x00020C94
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.ItemType.IsInstanceOfType(value))
			{
				throw new ArgumentException(SR.Parameter_Value_Wrong_Type, "value");
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00022AC2 File Offset: 0x00020CC2
		private void RemoveIndex(int index)
		{
			((AdomdParameter)this.items[index]).Parent = null;
			this.items.RemoveAt(index);
		}

		// Token: 0x04000455 RID: 1109
		private AdomdCommand parent;

		// Token: 0x04000456 RID: 1110
		private ArrayList items;
	}
}
