using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016D4 RID: 5844
	public abstract class InputValueList<TDetail, TValue> : IReadOnlyList<TDetail>, IReadOnlyCollection<TDetail>, IEnumerable<TDetail>, IEnumerable where TDetail : InputValueDetail<TValue>
	{
		// Token: 0x0600C2F2 RID: 49906 RVA: 0x002A011D File Offset: 0x0029E31D
		protected InputValueList()
		{
			this._items = new TDetail[0];
		}

		// Token: 0x0600C2F3 RID: 49907 RVA: 0x002A0131 File Offset: 0x0029E331
		protected InputValueList(IEnumerable<TDetail> details)
		{
			this._items = details.ToReadOnlyList<TDetail>();
		}

		// Token: 0x17002128 RID: 8488
		public TDetail this[int index]
		{
			get
			{
				return this._items[index];
			}
			set
			{
				throw new InvalidOperationException("value");
			}
		}

		// Token: 0x17002129 RID: 8489
		// (get) Token: 0x0600C2F6 RID: 49910 RVA: 0x002A0160 File Offset: 0x0029E360
		public string[] ColumnNames
		{
			get
			{
				string[] array;
				if ((array = this._columnNames) == null)
				{
					array = (this._columnNames = this._items.Select((TDetail d) => d.ColumnName).ToArray<string>());
				}
				return array;
			}
		}

		// Token: 0x1700212A RID: 8490
		// (get) Token: 0x0600C2F7 RID: 49911 RVA: 0x002A01AF File Offset: 0x0029E3AF
		// (set) Token: 0x0600C2F8 RID: 49912 RVA: 0x002A0153 File Offset: 0x0029E353
		public int Count
		{
			get
			{
				return this._items.Count;
			}
			set
			{
				throw new InvalidOperationException("value");
			}
		}

		// Token: 0x1700212B RID: 8491
		// (get) Token: 0x0600C2F9 RID: 49913 RVA: 0x002A01BC File Offset: 0x0029E3BC
		public TValue[] DistinctValues
		{
			get
			{
				TValue[] array;
				if ((array = this._distinctValues) == null)
				{
					array = (this._distinctValues = this.Values.Distinct<TValue>().ToArray<TValue>());
				}
				return array;
			}
		}

		// Token: 0x1700212C RID: 8492
		// (get) Token: 0x0600C2FA RID: 49914 RVA: 0x002A01EC File Offset: 0x0029E3EC
		public TValue[] Values
		{
			get
			{
				TValue[] array;
				if ((array = this._values) == null)
				{
					array = (this._values = this._items.Select((TDetail d) => d.Value).ToArray<TValue>());
				}
				return array;
			}
		}

		// Token: 0x0600C2FB RID: 49915 RVA: 0x002A023B File Offset: 0x0029E43B
		public IEnumerator<TDetail> GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		// Token: 0x0600C2FC RID: 49916 RVA: 0x002A0248 File Offset: 0x0029E448
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04004BCF RID: 19407
		private string[] _columnNames;

		// Token: 0x04004BD0 RID: 19408
		private TValue[] _distinctValues;

		// Token: 0x04004BD1 RID: 19409
		private readonly IReadOnlyList<TDetail> _items;

		// Token: 0x04004BD2 RID: 19410
		private TValue[] _values;
	}
}
