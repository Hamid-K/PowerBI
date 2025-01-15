using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AF5 RID: 2805
	internal sealed class GoogleAnalyticsDoubleConstantExpression : GoogleAnalyticsConstantExpression
	{
		// Token: 0x06004DDE RID: 19934 RVA: 0x0010284A File Offset: 0x00100A4A
		public GoogleAnalyticsDoubleConstantExpression(double doubleValue)
		{
			this.value = doubleValue;
		}

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x06004DDF RID: 19935 RVA: 0x0010285C File Offset: 0x00100A5C
		public override string QueryString
		{
			get
			{
				return this.value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x06004DE0 RID: 19936 RVA: 0x000023C4 File Offset: 0x000005C4
		public override GoogleAnalyticsDataType DataType
		{
			get
			{
				return GoogleAnalyticsDataType.Float;
			}
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x0010287C File Offset: 0x00100A7C
		protected override Value GenerateV2Filter()
		{
			return RecordValue.New(Keys.New("doubleValue"), new Value[] { NumberValue.New(this.value) });
		}

		// Token: 0x040029CB RID: 10699
		private readonly double value;
	}
}
