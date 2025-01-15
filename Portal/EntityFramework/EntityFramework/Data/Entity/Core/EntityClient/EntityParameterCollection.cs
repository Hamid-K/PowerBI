using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x020005DA RID: 1498
	public sealed class EntityParameterCollection : DbParameterCollection
	{
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06004865 RID: 18533 RVA: 0x00101CE7 File Offset: 0x000FFEE7
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

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06004866 RID: 18534 RVA: 0x00101D00 File Offset: 0x000FFF00
		private List<EntityParameter> InnerList
		{
			get
			{
				List<EntityParameter> list = this._items;
				if (list == null)
				{
					list = new List<EntityParameter>();
					this._items = list;
				}
				return list;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06004867 RID: 18535 RVA: 0x00101D25 File Offset: 0x000FFF25
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06004868 RID: 18536 RVA: 0x00101D32 File Offset: 0x000FFF32
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06004869 RID: 18537 RVA: 0x00101D3F File Offset: 0x000FFF3F
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x0600486A RID: 18538 RVA: 0x00101D4C File Offset: 0x000FFF4C
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x0600486B RID: 18539 RVA: 0x00101D59 File Offset: 0x000FFF59
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			Check.NotNull<object>(value, "value");
			EntityParameterCollection.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((EntityParameter)value);
			return this.Count - 1;
		}

		// Token: 0x0600486C RID: 18540 RVA: 0x00101D94 File Offset: 0x000FFF94
		public override void AddRange(Array values)
		{
			this.OnChange();
			Check.NotNull<Array>(values, "values");
			foreach (object obj in values)
			{
				EntityParameterCollection.ValidateType(obj);
			}
			foreach (object obj2 in values)
			{
				EntityParameter entityParameter = (EntityParameter)obj2;
				this.Validate(-1, entityParameter);
				this.InnerList.Add(entityParameter);
			}
		}

		// Token: 0x0600486D RID: 18541 RVA: 0x00101E44 File Offset: 0x00100044
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw new IndexOutOfRangeException(Strings.EntityParameterCollectionInvalidParameterName(parameterName));
			}
			return num;
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x00101E60 File Offset: 0x00100060
		public override void Clear()
		{
			this.OnChange();
			List<EntityParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				foreach (EntityParameter entityParameter in innerList)
				{
					entityParameter.ResetParent();
				}
				innerList.Clear();
			}
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x00101EC4 File Offset: 0x001000C4
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x00101ED3 File Offset: 0x001000D3
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x00101EE2 File Offset: 0x001000E2
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x00101EEF File Offset: 0x001000EF
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x00101F04 File Offset: 0x00100104
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw new IndexOutOfRangeException(Strings.EntityParameterCollectionInvalidParameterName(parameterName));
			}
			return this.InnerList[num];
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x00101F38 File Offset: 0x00100138
		private static int IndexOf(IEnumerable items, string parameterName)
		{
			if (items != null)
			{
				int num = 0;
				foreach (object obj in items)
				{
					EntityParameter entityParameter = (EntityParameter)obj;
					if (EntityUtil.SrcCompare(parameterName, entityParameter.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in items)
				{
					EntityParameter entityParameter2 = (EntityParameter)obj2;
					if (EntityUtil.DstCompare(parameterName, entityParameter2.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x00102004 File Offset: 0x00100204
		public override int IndexOf(string parameterName)
		{
			return EntityParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x00102014 File Offset: 0x00100214
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				EntityParameterCollection.ValidateType(value);
				List<EntityParameter> innerList = this.InnerList;
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

		// Token: 0x06004877 RID: 18551 RVA: 0x00102054 File Offset: 0x00100254
		public override void Insert(int index, object value)
		{
			this.OnChange();
			Check.NotNull<object>(value, "value");
			EntityParameterCollection.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Insert(index, (EntityParameter)value);
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x00102088 File Offset: 0x00100288
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw new IndexOutOfRangeException(Strings.EntityParameterCollectionInvalidIndex(index.ToString(CultureInfo.InvariantCulture), this.Count.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x001020CC File Offset: 0x001002CC
		public override void Remove(object value)
		{
			this.OnChange();
			Check.NotNull<object>(value, "value");
			EntityParameterCollection.ValidateType(value);
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			if (this != ((EntityParameter)value).CompareExchangeParent(null, this))
			{
				throw new ArgumentException(Strings.EntityParameterCollectionRemoveInvalidObject);
			}
		}

		// Token: 0x0600487A RID: 18554 RVA: 0x00102120 File Offset: 0x00100320
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x0600487B RID: 18555 RVA: 0x00102138 File Offset: 0x00100338
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int num = this.CheckName(parameterName);
			this.RemoveIndex(num);
		}

		// Token: 0x0600487C RID: 18556 RVA: 0x0010215C File Offset: 0x0010035C
		private void RemoveIndex(int index)
		{
			List<EntityParameter> innerList = this.InnerList;
			EntityParameter entityParameter = innerList[index];
			innerList.RemoveAt(index);
			entityParameter.ResetParent();
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x00102184 File Offset: 0x00100384
		private void Replace(int index, object newValue)
		{
			List<EntityParameter> innerList = this.InnerList;
			EntityParameterCollection.ValidateType(newValue);
			this.Validate(index, newValue);
			EntityParameter entityParameter = innerList[index];
			innerList[index] = (EntityParameter)newValue;
			entityParameter.ResetParent();
		}

		// Token: 0x0600487E RID: 18558 RVA: 0x001021BF File Offset: 0x001003BF
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x0600487F RID: 18559 RVA: 0x001021D8 File Offset: 0x001003D8
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.OnChange();
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw new IndexOutOfRangeException(Strings.EntityParameterCollectionInvalidParameterName(parameterName));
			}
			this.Replace(num, value);
		}

		// Token: 0x06004880 RID: 18560 RVA: 0x0010220C File Offset: 0x0010040C
		private void Validate(int index, object value)
		{
			Check.NotNull<object>(value, "value");
			EntityParameter entityParameter = (EntityParameter)value;
			object obj = entityParameter.CompareExchangeParent(this, null);
			if (obj != null)
			{
				if (this != obj)
				{
					throw new ArgumentException(Strings.EntityParameterContainedByAnotherCollection);
				}
				if (index != this.IndexOf(value))
				{
					throw new ArgumentException(Strings.EntityParameterContainedByAnotherCollection);
				}
			}
			string text = entityParameter.ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				do
				{
					text = "Parameter" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				entityParameter.ParameterName = text;
			}
		}

		// Token: 0x06004881 RID: 18561 RVA: 0x0010229B File Offset: 0x0010049B
		private static void ValidateType(object value)
		{
			Check.NotNull<object>(value, "value");
			if (!EntityParameterCollection._itemType.IsInstanceOfType(value))
			{
				throw new InvalidCastException(Strings.InvalidEntityParameterType(value.GetType().Name));
			}
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x001022CC File Offset: 0x001004CC
		internal EntityParameterCollection()
		{
		}

		// Token: 0x17000E45 RID: 3653
		public EntityParameter this[int index]
		{
			get
			{
				return (EntityParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		// Token: 0x17000E46 RID: 3654
		public EntityParameter this[string parameterName]
		{
			get
			{
				return (EntityParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06004887 RID: 18567 RVA: 0x00102304 File Offset: 0x00100504
		internal bool IsDirty
		{
			get
			{
				if (this._isDirty)
				{
					return true;
				}
				using (IEnumerator enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((EntityParameter)enumerator.Current).IsDirty)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x00102368 File Offset: 0x00100568
		public EntityParameter Add(EntityParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x00102374 File Offset: 0x00100574
		public EntityParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new EntityParameter
			{
				ParameterName = parameterName,
				Value = value
			});
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x0010239C File Offset: 0x0010059C
		public EntityParameter Add(string parameterName, DbType dbType)
		{
			return this.Add(new EntityParameter(parameterName, dbType));
		}

		// Token: 0x0600488B RID: 18571 RVA: 0x001023AB File Offset: 0x001005AB
		public EntityParameter Add(string parameterName, DbType dbType, int size)
		{
			return this.Add(new EntityParameter(parameterName, dbType, size));
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x001023BB File Offset: 0x001005BB
		public void AddRange(EntityParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x0600488D RID: 18573 RVA: 0x001023C4 File Offset: 0x001005C4
		public override bool Contains(string parameterName)
		{
			return this.IndexOf(parameterName) != -1;
		}

		// Token: 0x0600488E RID: 18574 RVA: 0x001023D3 File Offset: 0x001005D3
		public void CopyTo(EntityParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x0600488F RID: 18575 RVA: 0x001023DD File Offset: 0x001005DD
		public int IndexOf(EntityParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x001023E6 File Offset: 0x001005E6
		public void Insert(int index, EntityParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x001023F0 File Offset: 0x001005F0
		private void OnChange()
		{
			this._isDirty = true;
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x001023F9 File Offset: 0x001005F9
		public void Remove(EntityParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x00102404 File Offset: 0x00100604
		internal void ResetIsDirty()
		{
			this._isDirty = false;
			foreach (object obj in this)
			{
				((EntityParameter)obj).ResetIsDirty();
			}
		}

		// Token: 0x040019B3 RID: 6579
		private List<EntityParameter> _items;

		// Token: 0x040019B4 RID: 6580
		private static readonly Type _itemType = typeof(EntityParameter);

		// Token: 0x040019B5 RID: 6581
		private bool _isDirty;
	}
}
