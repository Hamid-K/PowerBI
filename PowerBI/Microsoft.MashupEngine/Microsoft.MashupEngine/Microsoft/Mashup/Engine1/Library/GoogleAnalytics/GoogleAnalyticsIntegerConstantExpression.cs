using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AF6 RID: 2806
	internal sealed class GoogleAnalyticsIntegerConstantExpression : GoogleAnalyticsConstantExpression
	{
		// Token: 0x06004DE2 RID: 19938 RVA: 0x001028A1 File Offset: 0x00100AA1
		public GoogleAnalyticsIntegerConstantExpression(int integerValue)
		{
			this.value = integerValue;
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x06004DE3 RID: 19939 RVA: 0x001028B0 File Offset: 0x00100AB0
		public override string QueryString
		{
			get
			{
				return this.value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x06004DE4 RID: 19940 RVA: 0x0000244F File Offset: 0x0000064F
		public override GoogleAnalyticsDataType DataType
		{
			get
			{
				return GoogleAnalyticsDataType.Integer;
			}
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x001028D0 File Offset: 0x00100AD0
		protected override Value GenerateV2Filter()
		{
			return RecordValue.New(Keys.New("int64Value"), new Value[] { NumberValue.New(this.value) });
		}

		// Token: 0x040029CC RID: 10700
		private readonly int value;
	}
}
