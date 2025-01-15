using System;
using System.Globalization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200040B RID: 1035
	internal class UnaryComparisonFunc : UnaryFunc
	{
		// Token: 0x0600240A RID: 9226 RVA: 0x0006E7D0 File Offset: 0x0006C9D0
		public UnaryComparisonFunc(ComparisonFunc cmp, string operand)
		{
			this.m_cmp = cmp;
			this.m_operand = operand;
			this.m_isLong = false;
			try
			{
				this.m_longValue = Convert.ToInt64(operand, CultureInfo.InvariantCulture);
				this.m_isLong = true;
			}
			catch (FormatException)
			{
			}
			catch (InvalidCastException)
			{
			}
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0006E834 File Offset: 0x0006CA34
		protected override object UnaryInvoke(object arg)
		{
			if (arg == null)
			{
				return this.m_cmp.Compare(1);
			}
			if (this.m_isLong)
			{
				try
				{
					long num = Convert.ToInt64(arg, CultureInfo.InvariantCulture);
					return this.m_cmp.Compare(this.m_longValue.CompareTo(num));
				}
				catch (FormatException)
				{
				}
				catch (InvalidCastException)
				{
				}
			}
			return this.m_cmp.Compare(string.Compare(this.m_operand, arg.ToString(), StringComparison.Ordinal));
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0006E8D0 File Offset: 0x0006CAD0
		public override string ToString()
		{
			return this.m_cmp.ToString() + ":" + this.m_operand;
		}

		// Token: 0x0400164B RID: 5707
		private ComparisonFunc m_cmp;

		// Token: 0x0400164C RID: 5708
		private string m_operand;

		// Token: 0x0400164D RID: 5709
		private long m_longValue;

		// Token: 0x0400164E RID: 5710
		private bool m_isLong;
	}
}
