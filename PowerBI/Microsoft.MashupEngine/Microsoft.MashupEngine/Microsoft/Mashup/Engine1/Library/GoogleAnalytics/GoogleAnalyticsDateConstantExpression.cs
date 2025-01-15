using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AF8 RID: 2808
	internal sealed class GoogleAnalyticsDateConstantExpression : GoogleAnalyticsConstantExpression
	{
		// Token: 0x06004DEA RID: 19946 RVA: 0x00102946 File Offset: 0x00100B46
		public GoogleAnalyticsDateConstantExpression(DateTime dateValue)
		{
			this.value = dateValue;
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x06004DEB RID: 19947 RVA: 0x00102955 File Offset: 0x00100B55
		public DateTime AsDateTime
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x06004DEC RID: 19948 RVA: 0x00102960 File Offset: 0x00100B60
		public override string QueryString
		{
			get
			{
				return this.value.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x06004DED RID: 19949 RVA: 0x00002139 File Offset: 0x00000339
		public override GoogleAnalyticsDataType DataType
		{
			get
			{
				return GoogleAnalyticsDataType.Date;
			}
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x00102985 File Offset: 0x00100B85
		protected override Value GenerateV2Filter()
		{
			return DateTimeValue.New(this.value);
		}

		// Token: 0x040029CE RID: 10702
		private readonly DateTime value;
	}
}
