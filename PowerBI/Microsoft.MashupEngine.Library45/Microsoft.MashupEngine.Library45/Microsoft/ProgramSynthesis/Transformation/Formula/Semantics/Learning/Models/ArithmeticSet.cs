using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016C3 RID: 5827
	public class ArithmeticSet : NumberSourceList
	{
		// Token: 0x0600C248 RID: 49736 RVA: 0x0029DBB7 File Offset: 0x0029BDB7
		public ArithmeticSet(IEnumerable<NumberSource> details)
			: base(details)
		{
		}

		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x0600C249 RID: 49737 RVA: 0x0029DBC0 File Offset: 0x0029BDC0
		public decimal? Average
		{
			get
			{
				decimal? average = this._average;
				if (average == null)
				{
					return this._average = Operators.Average(base.Values);
				}
				return average;
			}
		}

		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x0600C24A RID: 49738 RVA: 0x0029DBF4 File Offset: 0x0029BDF4
		public decimal? Product
		{
			get
			{
				decimal? product = this._product;
				if (product == null)
				{
					return this._product = Operators.Product(base.Values);
				}
				return product;
			}
		}

		// Token: 0x170020F8 RID: 8440
		// (get) Token: 0x0600C24B RID: 49739 RVA: 0x0029DC28 File Offset: 0x0029BE28
		public decimal? Sum
		{
			get
			{
				decimal? sum = this._sum;
				if (sum == null)
				{
					return this._sum = Operators.Sum(base.Values);
				}
				return sum;
			}
		}

		// Token: 0x04004B58 RID: 19288
		private decimal? _average;

		// Token: 0x04004B59 RID: 19289
		private decimal? _product;

		// Token: 0x04004B5A RID: 19290
		private decimal? _sum;
	}
}
