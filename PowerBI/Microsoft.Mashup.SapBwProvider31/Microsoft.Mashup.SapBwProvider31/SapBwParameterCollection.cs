using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200002F RID: 47
	public sealed class SapBwParameterCollection : DbParameterCollection, IEnumerable<SapBwParameter>, IEnumerable
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A767 File Offset: 0x00008967
		private SapBwParameterCollection.SapBwParameters Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new SapBwParameterCollection.SapBwParameters();
				}
				return this.items;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A782 File Offset: 0x00008982
		public override int Count
		{
			get
			{
				if (this.items == null)
				{
					return 0;
				}
				return this.items.Count;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A799 File Offset: 0x00008999
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.Items.InnerList).IsFixedSize;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A7B0 File Offset: 0x000089B0
		public override bool IsReadOnly
		{
			get
			{
				return this.Items.InnerList.IsReadOnly;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A7C2 File Offset: 0x000089C2
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.Items.InnerList).IsSynchronized;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000A7D9 File Offset: 0x000089D9
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.Items.InnerList).SyncRoot;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public override int Add(object value)
		{
			this.Items.Add(this.Validate(value, true));
			return this.Count - 1;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A810 File Offset: 0x00008A10
		public override void AddRange(Array values)
		{
			this.CheckArray(values);
			foreach (object obj in values)
			{
				SapBwParameter sapBwParameter = (SapBwParameter)obj;
				this.Items.SetParameter(sapBwParameter);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A870 File Offset: 0x00008A70
		private void CheckArray(Array values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			foreach (object obj in values)
			{
				this.Validate(obj, true);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		public override void Clear()
		{
			if (this.items != null)
			{
				this.items.Clear();
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A8E5 File Offset: 0x00008AE5
		public override bool Contains(object value)
		{
			return this.Items.Contains(this.Validate(value, false));
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A8FA File Offset: 0x00008AFA
		public override void CopyTo(Array array, int index)
		{
			this.CheckArray(array);
			((ICollection)this.Items.InnerList).CopyTo(array, index);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A91A File Offset: 0x00008B1A
		public override IEnumerator GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A927 File Offset: 0x00008B27
		protected override DbParameter GetParameter(int index)
		{
			this.CheckRange(index);
			return this.Items[index];
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A93C File Offset: 0x00008B3C
		public bool TryGetValue(string parameterName, out object value)
		{
			SapBwParameter sapBwParameter;
			if (this.Items.TryGetValue(parameterName, out sapBwParameter) && sapBwParameter.Value != null)
			{
				value = sapBwParameter.Value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A970 File Offset: 0x00008B70
		public bool GetBoolValueOrDefault(string parameterName, bool defaultValue)
		{
			object obj;
			if (this.TryGetValue(parameterName, out obj) && obj is bool)
			{
				return (bool)obj;
			}
			return defaultValue;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A998 File Offset: 0x00008B98
		protected override DbParameter GetParameter(string parameterName)
		{
			SapBwParameter sapBwParameter;
			if (this.Items.TryGetValue(parameterName, out sapBwParameter))
			{
				return sapBwParameter;
			}
			throw new SapBwException(Resources.ParameterNotFound(parameterName));
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A9C7 File Offset: 0x00008BC7
		public override int IndexOf(string parameterName)
		{
			return this.Items.IndexOf((SapBwParameter)this.GetParameter(parameterName));
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public override int IndexOf(object value)
		{
			return this.Items.IndexOf(this.Validate(value, false));
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A9F5 File Offset: 0x00008BF5
		public override void Insert(int index, object value)
		{
			this.Items.Insert(index, this.Validate(value, true));
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000AA0B File Offset: 0x00008C0B
		private void CheckRange(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000AA20 File Offset: 0x00008C20
		public override void Remove(object value)
		{
			this.Items.Remove(this.Validate(value, false));
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000AA36 File Offset: 0x00008C36
		public override void RemoveAt(int index)
		{
			this.CheckRange(index);
			this.Items.RemoveAt(index);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000AA4B File Offset: 0x00008C4B
		public override void RemoveAt(string parameterName)
		{
			if (!this.Items.Contains(parameterName))
			{
				throw new IndexOutOfRangeException();
			}
			this.Items.Remove(parameterName);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000AA6E File Offset: 0x00008C6E
		protected override void SetParameter(int index, DbParameter value)
		{
			this.CheckRange(index);
			this.Items[index] = this.Validate(value, true);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000AA8C File Offset: 0x00008C8C
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			if (string.IsNullOrEmpty(parameterName))
			{
				throw new ArgumentNullException("parameterName");
			}
			SapBwParameter sapBwParameter = this.Validate(value, false);
			if (string.IsNullOrEmpty(sapBwParameter.ParameterName))
			{
				sapBwParameter.ParameterName = parameterName;
			}
			else if (parameterName != sapBwParameter.ParameterName)
			{
				throw new SapBwException(Resources.ParameterNameMismatch(parameterName, sapBwParameter.ParameterName));
			}
			this.Items.SetParameter(sapBwParameter);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000AAFC File Offset: 0x00008CFC
		private SapBwParameter Validate(object value, bool checkName = false)
		{
			if (value == null)
			{
				throw new SapBwException(Resources.NullParameter);
			}
			if (!(value is SapBwParameter))
			{
				throw new SapBwException(Resources.InvalidParameterType);
			}
			SapBwParameter sapBwParameter = (SapBwParameter)value;
			if (checkName)
			{
				if (string.IsNullOrEmpty(sapBwParameter.ParameterName))
				{
					throw new SapBwException(Resources.NullParameterName);
				}
				if (this.Items.Contains(sapBwParameter.ParameterName))
				{
					throw new SapBwException(Resources.DuplicateParameterName(sapBwParameter.ParameterName));
				}
			}
			return sapBwParameter;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000AB85 File Offset: 0x00008D85
		public override bool Contains(string value)
		{
			return this.Items.Contains(value);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000AB93 File Offset: 0x00008D93
		IEnumerator<SapBwParameter> IEnumerable<SapBwParameter>.GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x040001AE RID: 430
		private SapBwParameterCollection.SapBwParameters items;

		// Token: 0x02000072 RID: 114
		private class SapBwParameters : KeyedCollection<string, SapBwParameter>
		{
			// Token: 0x060003EC RID: 1004 RVA: 0x0000F8BE File Offset: 0x0000DABE
			public SapBwParameters()
				: base(StringComparer.OrdinalIgnoreCase)
			{
			}

			// Token: 0x060003ED RID: 1005 RVA: 0x0000F8CB File Offset: 0x0000DACB
			protected override string GetKeyForItem(SapBwParameter item)
			{
				return item.ParameterName;
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000F8D3 File Offset: 0x0000DAD3
			public IList<SapBwParameter> InnerList
			{
				get
				{
					return base.Items;
				}
			}

			// Token: 0x060003EF RID: 1007 RVA: 0x0000F8DB File Offset: 0x0000DADB
			public bool TryGetValue(string parameterName, out SapBwParameter parameter)
			{
				return base.Dictionary.TryGetValue(parameterName, out parameter);
			}

			// Token: 0x060003F0 RID: 1008 RVA: 0x0000F8EC File Offset: 0x0000DAEC
			public void SetParameter(SapBwParameter parameter)
			{
				SapBwParameter sapBwParameter;
				if (base.Dictionary.TryGetValue(parameter.ParameterName, out sapBwParameter))
				{
					this.SetItem(base.Items.IndexOf(sapBwParameter), parameter);
					return;
				}
				base.Add(parameter);
			}
		}
	}
}
