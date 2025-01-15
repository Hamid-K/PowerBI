using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003C2 RID: 962
	public sealed class Filter
	{
		// Token: 0x06001F0B RID: 7947 RVA: 0x000025F4 File Offset: 0x000007F4
		public Filter()
		{
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0007DF34 File Offset: 0x0007C134
		public Filter(string sExpr, Filter.Operators op, string sValue)
		{
			this.FilterExpression = new Expression(sExpr);
			this.Operator = op;
			if (sValue != null)
			{
				this.FilterValues = new ArrayList(1);
				this.FilterValues.Add(new Expression(sValue));
			}
		}

		// Token: 0x04000D7E RID: 3454
		public Expression FilterExpression;

		// Token: 0x04000D7F RID: 3455
		public Filter.Operators Operator;

		// Token: 0x04000D80 RID: 3456
		[XmlArrayItem("FilterValue", typeof(Expression))]
		public ArrayList FilterValues;

		// Token: 0x02000516 RID: 1302
		public enum Operators
		{
			// Token: 0x04001272 RID: 4722
			Equal,
			// Token: 0x04001273 RID: 4723
			Like,
			// Token: 0x04001274 RID: 4724
			NotEqual,
			// Token: 0x04001275 RID: 4725
			GreaterThan,
			// Token: 0x04001276 RID: 4726
			GreaterThanOrEqual,
			// Token: 0x04001277 RID: 4727
			LessThan,
			// Token: 0x04001278 RID: 4728
			LessThanOrEqual,
			// Token: 0x04001279 RID: 4729
			TopN,
			// Token: 0x0400127A RID: 4730
			BottomN,
			// Token: 0x0400127B RID: 4731
			TopPercent,
			// Token: 0x0400127C RID: 4732
			BottomPercent,
			// Token: 0x0400127D RID: 4733
			In,
			// Token: 0x0400127E RID: 4734
			Between
		}
	}
}
