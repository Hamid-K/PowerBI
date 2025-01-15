using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009FF RID: 2559
	public sealed class DrdaParameterCollection : DbParameterCollection
	{
		// Token: 0x06005069 RID: 20585 RVA: 0x001420B6 File Offset: 0x001402B6
		internal DrdaParameterCollection()
		{
		}

		// Token: 0x17001387 RID: 4999
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DrdaParameter this[int index]
		{
			get
			{
				return (DrdaParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		// Token: 0x17001388 RID: 5000
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DrdaParameter this[string parameterName]
		{
			get
			{
				return (DrdaParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x001420F5 File Offset: 0x001402F5
		private Type ItemType
		{
			get
			{
				return typeof(DrdaParameter);
			}
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x00142101 File Offset: 0x00140301
		public DrdaParameter Add(DrdaParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x0014210C File Offset: 0x0014030C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value).  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public DrdaParameter Add(string parameterName, object value)
		{
			return this.Add(new DrdaParameter(parameterName, value));
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x0014211C File Offset: 0x0014031C
		public DrdaParameter AddWithValue(string parameterName, object value)
		{
			DrdaParameter drdaParameter = new DrdaParameter();
			drdaParameter.ParameterName = parameterName;
			drdaParameter.Value = value;
			this.Add(drdaParameter);
			return drdaParameter;
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x00142146 File Offset: 0x00140346
		public DrdaParameter Add(string parameterName, DrdaType DrdaType)
		{
			return this.Add(new DrdaParameter(parameterName, DrdaType));
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x00142155 File Offset: 0x00140355
		public DrdaParameter Add(string parameterName, DrdaType DrdaType, int size)
		{
			return this.Add(new DrdaParameter(parameterName, DrdaType, size));
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x00142165 File Offset: 0x00140365
		public DrdaParameter Add(string parameterName, DrdaType DrdaType, int size, string sourceColumn)
		{
			return this.Add(new DrdaParameter(parameterName, DrdaType, size, sourceColumn));
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x00142177 File Offset: 0x00140377
		public void AddRange(DrdaParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x00142180 File Offset: 0x00140380
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x0014218F File Offset: 0x0014038F
		public bool Contains(DrdaParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x0014219E File Offset: 0x0014039E
		public void CopyTo(DrdaParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x001421A8 File Offset: 0x001403A8
		public int IndexOf(DrdaParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x001421B1 File Offset: 0x001403B1
		public void Insert(int index, DrdaParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x001421BB File Offset: 0x001403BB
		public void Remove(DrdaParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x0600507C RID: 20604 RVA: 0x001421C4 File Offset: 0x001403C4
		public override int Count
		{
			get
			{
				if (this._items == null)
				{
					return 0;
				}
				return this._items.Count;
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x0600507D RID: 20605 RVA: 0x001421DC File Offset: 0x001403DC
		private List<DrdaParameter> InnerList
		{
			get
			{
				List<DrdaParameter> list = this._items;
				if (list == null)
				{
					list = new List<DrdaParameter>();
					this._items = list;
				}
				return list;
			}
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x0600507E RID: 20606 RVA: 0x00142201 File Offset: 0x00140401
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x0600507F RID: 20607 RVA: 0x0014220E File Offset: 0x0014040E
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06005080 RID: 20608 RVA: 0x0014221B File Offset: 0x0014041B
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06005081 RID: 20609 RVA: 0x00142228 File Offset: 0x00140428
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x00142235 File Offset: 0x00140435
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((DrdaParameter)value);
			return this.Count - 1;
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x00142268 File Offset: 0x00140468
		public override void AddRange(Array values)
		{
			this.OnChange();
			if (values == null)
			{
				throw DrdaException.ArgumentNull("values");
			}
			foreach (object obj in values)
			{
				this.ValidateType(obj);
			}
			foreach (object obj2 in values)
			{
				DrdaParameter drdaParameter = (DrdaParameter)obj2;
				this.Validate(-1, drdaParameter);
				this.InnerList.Add(drdaParameter);
			}
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x0014231C File Offset: 0x0014051C
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw DrdaException.ParametersSourceIndex(parameterName, this, this.ItemType);
			}
			return num;
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x00142338 File Offset: 0x00140538
		public override void Clear()
		{
			this.OnChange();
			List<DrdaParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				foreach (DrdaParameter drdaParameter in innerList)
				{
					drdaParameter.ResetParent();
				}
				innerList.Clear();
			}
			this._initialCount = 0;
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x001423A0 File Offset: 0x001405A0
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x001423AF File Offset: 0x001405AF
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x001423BE File Offset: 0x001405BE
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x001423CB File Offset: 0x001405CB
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x0600508A RID: 20618 RVA: 0x001423E0 File Offset: 0x001405E0
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw DrdaException.ParametersSourceIndex(parameterName, this, this.ItemType);
			}
			return this.InnerList[num];
		}

		// Token: 0x0600508B RID: 20619 RVA: 0x00142414 File Offset: 0x00140614
		private static int IndexOf(IEnumerable items, string parameterName)
		{
			if (items != null)
			{
				int num = 0;
				foreach (object obj in items)
				{
					DrdaParameter drdaParameter = (DrdaParameter)obj;
					if (DrdaParameterCollection.SrcCompare(parameterName, drdaParameter.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in items)
				{
					DrdaParameter drdaParameter2 = (DrdaParameter)obj2;
					if (DrdaParameterCollection.DstCompare(parameterName, drdaParameter2.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x001424E0 File Offset: 0x001406E0
		private static int SrcCompare(string strA, string strB)
		{
			if (!(strA == strB))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x001424EE File Offset: 0x001406EE
		private static int DstCompare(string strA, string strB)
		{
			return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x00142503 File Offset: 0x00140703
		public override int IndexOf(string parameterName)
		{
			return DrdaParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x00142514 File Offset: 0x00140714
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				List<DrdaParameter> innerList = this.InnerList;
				if (innerList != null)
				{
					int count = innerList.Count;
					for (int i = 0; i < count; i++)
					{
						if (value == innerList[i])
						{
							return i;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x00142555 File Offset: 0x00140755
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (DrdaParameter)value);
			this.InnerList.Insert(index, (DrdaParameter)value);
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x00142583 File Offset: 0x00140783
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw DrdaException.ParametersMappingIndex(index, this);
			}
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x0014259C File Offset: 0x0014079C
		public override void Remove(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			if (this != ((DrdaParameter)value).CompareExchangeParent(null, this))
			{
				throw DrdaException.CollectionRemoveInvalidObject(this.ItemType, this);
			}
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x001425E7 File Offset: 0x001407E7
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x00142600 File Offset: 0x00140800
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int num = this.CheckName(parameterName);
			this.RemoveIndex(num);
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x00142624 File Offset: 0x00140824
		private void RemoveIndex(int index)
		{
			List<DrdaParameter> innerList = this.InnerList;
			DrdaParameter drdaParameter = innerList[index];
			innerList.RemoveAt(index);
			drdaParameter.ResetParent();
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x0014264C File Offset: 0x0014084C
		private void Replace(int index, object newValue)
		{
			List<DrdaParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			DrdaParameter drdaParameter = innerList[index];
			innerList[index] = (DrdaParameter)newValue;
			drdaParameter.ResetParent();
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x00142688 File Offset: 0x00140888
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x001426A0 File Offset: 0x001408A0
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.OnChange();
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw DrdaException.ParametersSourceIndex(parameterName, this, this.ItemType);
			}
			this.Replace(num, value);
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x001426D8 File Offset: 0x001408D8
		private void Validate(int index, object value)
		{
			if (value == null)
			{
				throw DrdaException.ParameterNull("value", this, this.ItemType);
			}
			object obj = ((DrdaParameter)value).CompareExchangeParent(this, null);
			if (obj != null && this != obj)
			{
				throw DrdaException.ParametersIsNotParent(this.ItemType, ((DrdaParameter)value).ParameterName, this);
			}
			string text = ((DrdaParameter)value).ParameterName;
			if (text == null || text.Length == 0)
			{
				index = 1;
				do
				{
					text = "P" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				((DrdaParameter)value).ParameterName = text;
			}
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x00142773 File Offset: 0x00140973
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw DrdaException.ParameterNull("value", this, this.ItemType);
			}
			if (!this.ItemType.IsInstanceOfType(value))
			{
				throw DrdaException.InvalidParameterType(this, this.ItemType, value);
			}
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x001427A8 File Offset: 0x001409A8
		internal bool WriteParameters(ISqlStatement statement, string commandText)
		{
			Trace.MessageTrace("WriteParameters(): Count = {0}", this.Count);
			bool flag = false;
			bool flag2 = false;
			if (this._requiresBinding && this._initialCount == 0)
			{
				if (this._initialCount == 0)
				{
					this._initialCount = this.Count;
				}
				this._parameterPositions = new ArrayList();
				List<int> list = new List<int>();
				int num = 0;
				for (int i = 0; i < this._initialCount; i++)
				{
					if (this[i].ParameterName.Length > 0 && this[i].ParameterName[0] == '@')
					{
						int num2 = 0;
						while (num2 != -1)
						{
							num2 = commandText.IndexOf(this[i].ParameterName, num2 + 1);
							if (num2 != -1)
							{
								char c;
								if (commandText.Length != num2 + this[i].ParameterName.Length)
								{
									c = commandText[num2 + this[i].ParameterName.Length];
								}
								else
								{
									c = ' ';
								}
								if (char.IsWhiteSpace(c) || c == ',' || c == ')')
								{
									flag2 = true;
									this._parameterPositions.Add(new DrdaParameterPosition(num2, this[i]));
									list.Add(num2);
									num++;
									if (this[i].DrdaType == DrdaType.Xml || this[i].DrdaType == DrdaType.CLOB || this[i].DrdaType == DrdaType.BLOB || this[i].DrdaType == DrdaType.DBCLOB)
									{
										this[i].Binding.IsLob = true;
										flag = true;
									}
								}
							}
						}
					}
				}
				for (int j = 0; j < num; j++)
				{
					int num3 = list[j];
					DrdaParameterPosition drdaParameterPosition = (DrdaParameterPosition)this._parameterPositions[j];
					int num4 = j - 1;
					while (num4 >= 0 && num3 < list[num4])
					{
						list[num4 + 1] = list[num4];
						this._parameterPositions.RemoveAt(num4 + 1);
						this._parameterPositions.Insert(num4 + 1, this._parameterPositions[num4]);
						num4--;
						this._parameterPositions.RemoveAt(num4 + 1);
						list[num4 + 1] = num3;
						this._parameterPositions.Insert(num4 + 1, drdaParameterPosition);
					}
				}
				if (!flag2)
				{
					for (int i = 0; i < this.Count; i++)
					{
						this._parameterPositions.Add(new DrdaParameterPosition(i, this[i]));
					}
				}
				bool flag3 = true;
				for (int i = 0; i < this._parameterPositions.Count; i++)
				{
					DrdaParameterPosition drdaParameterPosition2 = (DrdaParameterPosition)this._parameterPositions[i];
					DrdaParameter parameter = drdaParameterPosition2.Parameter;
					flag3 = parameter.Binding.Initialize(statement.Requester.UseHIS2013Constants) && flag3;
					parameter.Binding.CopyValueTo(drdaParameterPosition2.Binding);
				}
				this._requiresBinding = !flag3;
				Trace.MessageTrace("WriteParameters(): created binding");
				Trace.MessageTrace("WriteParameters(): Begin writing values");
				for (int i = 0; i < this._parameterPositions.Count; i++)
				{
					DrdaParameterPosition drdaParameterPosition3 = (DrdaParameterPosition)this._parameterPositions[i];
					drdaParameterPosition3.Parameter.Binding.CopyValueTo(drdaParameterPosition3.Binding);
				}
				if (flag2)
				{
					this.Clear();
					this._initialCount = this._parameterPositions.Count;
					for (int k = 0; k < this._parameterPositions.Count; k++)
					{
						DrdaParameter parameter2 = ((DrdaParameterPosition)this._parameterPositions[k]).Parameter;
						this.Add(parameter2);
					}
					this._parameterPositions.Clear();
				}
				Trace.MessageTrace("WriteParameters(): End writing values");
			}
			return flag;
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x00142B6C File Offset: 0x00140D6C
		internal void ReadParameters(ISqlStatement statement, string commandText)
		{
			Trace.MessageTrace("ReadParameters(): Count = {0}", this.Count);
			if (this.Count > 0)
			{
				IList<ISqlParameter> parameters = statement.Parameters;
				int count = parameters.Count;
				if (count > 0)
				{
					Trace.MessageTrace("ReadParameters(): Begin reading values");
					int num = 0;
					for (int i = 0; i < this._parameterPositions.Count; i++)
					{
						DrdaParameterPosition drdaParameterPosition = (DrdaParameterPosition)this._parameterPositions[i];
						DrdaParameter parameter = drdaParameterPosition.Parameter;
						if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
						{
							drdaParameterPosition.Binding.CopyValueTo(parameter.Binding);
							parameter.Binding.Value = parameters[i].Value;
							num++;
							if (num == count)
							{
								break;
							}
						}
					}
					Trace.MessageTrace("ReadParameters(): End reading values");
				}
			}
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x00142C44 File Offset: 0x00140E44
		internal void OnChange()
		{
			this._requiresBinding = true;
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x00142C4D File Offset: 0x00140E4D
		internal void PropertyChanging()
		{
			this.OnChange();
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x00142C58 File Offset: 0x00140E58
		internal List<ISqlParameter> ToSqlParameters()
		{
			if (this._items == null)
			{
				return null;
			}
			List<ISqlParameter> list = new List<ISqlParameter>();
			this._items.ForEach(delegate(DrdaParameter parameter)
			{
				list.Add(parameter.ToSqlParameter());
			});
			return list;
		}

		// Token: 0x04003F5D RID: 16221
		private bool _requiresBinding = true;

		// Token: 0x04003F5E RID: 16222
		private ArrayList _parameterPositions;

		// Token: 0x04003F5F RID: 16223
		private List<DrdaParameter> _items;

		// Token: 0x04003F60 RID: 16224
		private int _initialCount;
	}
}
