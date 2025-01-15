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
		// Token: 0x0600065B RID: 1627 RVA: 0x000222B0 File Offset: 0x000204B0
		internal AdomdParameterCollection(AdomdCommand parent)
		{
			this.parent = parent;
			this.items = new ArrayList();
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x000222CA File Offset: 0x000204CA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x000222D7 File Offset: 0x000204D7
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x000222DA File Offset: 0x000204DA
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000222DD File Offset: 0x000204DD
		int IList.Add(object value)
		{
			this.Add((AdomdParameter)value);
			return this.Count - 1;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000222F4 File Offset: 0x000204F4
		public void Clear()
		{
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				((AdomdParameter)this.items[i]).Parent = null;
			}
			this.items.Clear();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0002233B File Offset: 0x0002053B
		bool IList.Contains(object value)
		{
			return this.Contains((AdomdParameter)value);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00022349 File Offset: 0x00020549
		public bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00022358 File Offset: 0x00020558
		public bool Contains(AdomdParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00022367 File Offset: 0x00020567
		int IList.IndexOf(object value)
		{
			return this.IndexOf((AdomdParameter)value);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00022375 File Offset: 0x00020575
		public int IndexOf(AdomdParameter value)
		{
			return this.items.IndexOf(value);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00022383 File Offset: 0x00020583
		public void Insert(int index, AdomdParameter value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.items.Insert(index, value);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000223A1 File Offset: 0x000205A1
		void IList.Insert(int index, object value)
		{
			this.ValidateType(value);
			this.Insert(index, (AdomdParameter)value);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000223B8 File Offset: 0x000205B8
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

		// Token: 0x06000669 RID: 1641 RVA: 0x000223E8 File Offset: 0x000205E8
		void IList.Remove(object value)
		{
			this.ValidateType(value);
			this.Remove((AdomdParameter)value);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000223FD File Offset: 0x000205FD
		public void RemoveAt(int index)
		{
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0002240D File Offset: 0x0002060D
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00022410 File Offset: 0x00020610
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700019F RID: 415
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

		// Token: 0x0600066F RID: 1647 RVA: 0x00022432 File Offset: 0x00020632
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00022440 File Offset: 0x00020640
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

		// Token: 0x06000671 RID: 1649 RVA: 0x00022488 File Offset: 0x00020688
		public void RemoveAt(string parameterName)
		{
			int num = this.RangeCheck(parameterName);
			this.RemoveIndex(num);
		}

		// Token: 0x170001A0 RID: 416
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

		// Token: 0x06000674 RID: 1652 RVA: 0x0002251E File Offset: 0x0002071E
		public void CopyTo(AdomdParameter[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0002252D File Offset: 0x0002072D
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0002253C File Offset: 0x0002073C
		public AdomdParameter Add(AdomdParameter value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.items.Add(value);
			return value;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0002255B File Offset: 0x0002075B
		public AdomdParameter Add(string parameterName, object value)
		{
			return this.Add(new AdomdParameter(parameterName, value));
		}

		// Token: 0x170001A1 RID: 417
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

		// Token: 0x170001A2 RID: 418
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

		// Token: 0x0600067C RID: 1660 RVA: 0x000225E4 File Offset: 0x000207E4
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

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0002261E File Offset: 0x0002081E
		private Type ItemType
		{
			get
			{
				return typeof(AdomdParameter);
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0002262A File Offset: 0x0002082A
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00022644 File Offset: 0x00020844
		private int RangeCheck(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("parameterName");
			}
			return num;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0002265C File Offset: 0x0002085C
		private void Replace(int index, AdomdParameter newValue)
		{
			this.Validate(index, newValue);
			((AdomdParameter)this.items[index]).Parent = null;
			newValue.Parent = this;
			this.items[index] = newValue;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00022694 File Offset: 0x00020894
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

		// Token: 0x06000682 RID: 1666 RVA: 0x00022764 File Offset: 0x00020964
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

		// Token: 0x06000683 RID: 1667 RVA: 0x00022792 File Offset: 0x00020992
		private void RemoveIndex(int index)
		{
			((AdomdParameter)this.items[index]).Parent = null;
			this.items.RemoveAt(index);
		}

		// Token: 0x04000448 RID: 1096
		private AdomdCommand parent;

		// Token: 0x04000449 RID: 1097
		private ArrayList items;
	}
}
