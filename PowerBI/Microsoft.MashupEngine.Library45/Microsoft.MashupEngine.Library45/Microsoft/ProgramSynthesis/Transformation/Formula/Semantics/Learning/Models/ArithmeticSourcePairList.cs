using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016C4 RID: 5828
	public class ArithmeticSourcePairList : ReadOnlyListBase<ArithmeticSourcePair>
	{
		// Token: 0x0600C24C RID: 49740 RVA: 0x0029DC5B File Offset: 0x0029BE5B
		public ArithmeticSourcePairList()
			: base(new ArithmeticSourcePair[0])
		{
		}

		// Token: 0x0600C24D RID: 49741 RVA: 0x0029DC69 File Offset: 0x0029BE69
		public ArithmeticSourcePairList(IEnumerable<ArithmeticSourcePair> items)
			: base(items)
		{
		}

		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x0600C24E RID: 49742 RVA: 0x0029DC74 File Offset: 0x0029BE74
		public decimal[] DistinctLeftValues
		{
			get
			{
				decimal[] array;
				if ((array = this._distinctLeftValues) == null)
				{
					array = (this._distinctLeftValues = this.LeftValues.Distinct<decimal>().ToArray<decimal>());
				}
				return array;
			}
		}

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x0600C24F RID: 49743 RVA: 0x0029DCA4 File Offset: 0x0029BEA4
		public decimal[] DistinctRightValues
		{
			get
			{
				decimal[] array;
				if ((array = this._distinctRightValues) == null)
				{
					array = (this._distinctRightValues = this.RightValues.Distinct<decimal>().ToArray<decimal>());
				}
				return array;
			}
		}

		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x0600C250 RID: 49744 RVA: 0x0029DCD4 File Offset: 0x0029BED4
		public decimal[] LeftValues
		{
			get
			{
				decimal[] array;
				if ((array = this._leftValues) == null)
				{
					array = (this._leftValues = base.Items.Select((ArithmeticSourcePair i) => i.Left.Value).ToArray<decimal>());
				}
				return array;
			}
		}

		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x0600C251 RID: 49745 RVA: 0x0029DD24 File Offset: 0x0029BF24
		public decimal[] RightValues
		{
			get
			{
				decimal[] array;
				if ((array = this._rightValues) == null)
				{
					array = (this._rightValues = base.Items.Select((ArithmeticSourcePair i) => i.Right.Value).ToArray<decimal>());
				}
				return array;
			}
		}

		// Token: 0x04004B5B RID: 19291
		private decimal[] _distinctLeftValues;

		// Token: 0x04004B5C RID: 19292
		private decimal[] _distinctRightValues;

		// Token: 0x04004B5D RID: 19293
		private decimal[] _leftValues;

		// Token: 0x04004B5E RID: 19294
		private decimal[] _rightValues;
	}
}
