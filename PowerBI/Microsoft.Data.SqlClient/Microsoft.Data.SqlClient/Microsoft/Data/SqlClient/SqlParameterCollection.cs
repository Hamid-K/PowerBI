using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200008D RID: 141
	[ListBindable(false)]
	public sealed class SqlParameterCollection : DbParameterCollection
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x00021B99 File Offset: 0x0001FD99
		internal SqlParameterCollection()
		{
		}

		// Token: 0x17000767 RID: 1895
		public SqlParameter this[int index]
		{
			get
			{
				return (SqlParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		// Token: 0x17000768 RID: 1896
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SqlParameter this[string parameterName]
		{
			get
			{
				return (SqlParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00021BD1 File Offset: 0x0001FDD1
		public SqlParameter Add(SqlParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00021BDC File Offset: 0x0001FDDC
		public SqlParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00021BEB File Offset: 0x0001FDEB
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType));
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00021BFA File Offset: 0x0001FDFA
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size));
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00021C0A File Offset: 0x0001FE0A
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size, string sourceColumn)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size, sourceColumn));
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00021C1C File Offset: 0x0001FE1C
		public void AddRange(SqlParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00021C25 File Offset: 0x0001FE25
		public override bool Contains(string value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00021C34 File Offset: 0x0001FE34
		public bool Contains(SqlParameter value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00021C43 File Offset: 0x0001FE43
		public void CopyTo(SqlParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00021C4D File Offset: 0x0001FE4D
		public int IndexOf(SqlParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00021C56 File Offset: 0x0001FE56
		public void Insert(int index, SqlParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00021C60 File Offset: 0x0001FE60
		public void Remove(SqlParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00021C69 File Offset: 0x0001FE69
		public override int Count
		{
			get
			{
				List<SqlParameter> items = this._items;
				if (items == null)
				{
					return 0;
				}
				return items.Count;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00021C7C File Offset: 0x0001FE7C
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x00021C89 File Offset: 0x0001FE89
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x00021C96 File Offset: 0x0001FE96
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00021CA3 File Offset: 0x0001FEA3
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00021CB0 File Offset: 0x0001FEB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.IsDirty = true;
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((SqlParameter)value);
			return this.Count - 1;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00021CE4 File Offset: 0x0001FEE4
		public override void AddRange(Array values)
		{
			this.IsDirty = true;
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			foreach (object obj in values)
			{
				this.ValidateType(obj);
			}
			foreach (object obj2 in values)
			{
				SqlParameter sqlParameter = (SqlParameter)obj2;
				this.Validate(-1, sqlParameter);
				this.InnerList.Add(sqlParameter);
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00021D9C File Offset: 0x0001FF9C
		public override void Clear()
		{
			this.IsDirty = true;
			List<SqlParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				foreach (SqlParameter sqlParameter in innerList)
				{
					sqlParameter.ResetParent();
				}
				innerList.Clear();
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00021E00 File Offset: 0x00020000
		public override bool Contains(object value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00021E0F File Offset: 0x0002000F
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00021E1E File Offset: 0x0002001E
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00021E2C File Offset: 0x0002002C
		public override int IndexOf(string parameterName)
		{
			List<SqlParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				int num = 0;
				foreach (SqlParameter sqlParameter in innerList)
				{
					if (parameterName == sqlParameter.ParameterName)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (SqlParameter sqlParameter2 in innerList)
				{
					if (CultureInfo.CurrentCulture.CompareInfo.Compare(parameterName, sqlParameter2.ParameterName, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00021EFC File Offset: 0x000200FC
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				List<SqlParameter> innerList = this.InnerList;
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

		// Token: 0x06000B97 RID: 2967 RVA: 0x00021F3D File Offset: 0x0002013D
		public override void Insert(int index, object value)
		{
			this.IsDirty = true;
			this.ValidateType(value);
			this.Validate(-1, (SqlParameter)value);
			this.InnerList.Insert(index, (SqlParameter)value);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00021F6C File Offset: 0x0002016C
		public override void Remove(object value)
		{
			this.IsDirty = true;
			this.ValidateType(value);
			int num = this.IndexOf(value);
			if (num != -1)
			{
				this.RemoveIndex(num);
				return;
			}
			if (((SqlParameter)value).CompareExchangeParent(null, this) != this)
			{
				throw ADP.CollectionRemoveInvalidObject(typeof(SqlParameter), this);
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00021FBC File Offset: 0x000201BC
		public override void RemoveAt(int index)
		{
			this.IsDirty = true;
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00021FD4 File Offset: 0x000201D4
		public override void RemoveAt(string parameterName)
		{
			this.IsDirty = true;
			int num = this.CheckName(parameterName);
			this.RemoveIndex(num);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00021FF7 File Offset: 0x000201F7
		protected override void SetParameter(int index, DbParameter value)
		{
			this.IsDirty = true;
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00022010 File Offset: 0x00020210
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.IsDirty = true;
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, typeof(SqlParameter));
			}
			this.Replace(num, value);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002204A File Offset: 0x0002024A
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00022060 File Offset: 0x00020260
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, typeof(SqlParameter));
			}
			return this.InnerList[num];
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00022098 File Offset: 0x00020298
		private List<SqlParameter> InnerList
		{
			get
			{
				List<SqlParameter> list = this._items;
				if (list == null)
				{
					list = new List<SqlParameter>();
					this._items = list;
				}
				return list;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x000220BD File Offset: 0x000202BD
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x000220C5 File Offset: 0x000202C5
		internal bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				this._isDirty = value;
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000220CE File Offset: 0x000202CE
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ParametersMappingIndex(index, this);
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000220E8 File Offset: 0x000202E8
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, typeof(SqlParameter));
			}
			return num;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00022114 File Offset: 0x00020314
		private void RemoveIndex(int index)
		{
			List<SqlParameter> innerList = this.InnerList;
			SqlParameter sqlParameter = innerList[index];
			innerList.RemoveAt(index);
			sqlParameter.ResetParent();
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00022140 File Offset: 0x00020340
		private void Replace(int index, object newValue)
		{
			List<SqlParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			SqlParameter sqlParameter = innerList[index];
			innerList[index] = (SqlParameter)newValue;
			sqlParameter.ResetParent();
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00022180 File Offset: 0x00020380
		private void Validate(int index, object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, typeof(SqlParameter));
			}
			object obj = ((SqlParameter)value).CompareExchangeParent(this, null);
			if (obj != null)
			{
				if (this != obj)
				{
					throw ADP.ParametersIsNotParent(typeof(SqlParameter), this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.ParametersIsParent(typeof(SqlParameter), this);
				}
			}
			string text = ((SqlParameter)value).ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				do
				{
					text = "Parameter" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				((SqlParameter)value).ParameterName = text;
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00022230 File Offset: 0x00020430
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, typeof(SqlParameter));
			}
			if (!typeof(SqlParameter).IsInstanceOfType(value))
			{
				throw ADP.InvalidParameterType(this, typeof(SqlParameter), value);
			}
		}

		// Token: 0x040002FD RID: 765
		private List<SqlParameter> _items;

		// Token: 0x040002FE RID: 766
		private bool _isDirty;
	}
}
