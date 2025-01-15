using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AF7 RID: 2807
	internal sealed class GoogleAnalyticsStringConstantExpression : GoogleAnalyticsConstantExpression
	{
		// Token: 0x06004DE6 RID: 19942 RVA: 0x001028F5 File Offset: 0x00100AF5
		public GoogleAnalyticsStringConstantExpression(string stringValue)
		{
			this.value = stringValue;
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x06004DE7 RID: 19943 RVA: 0x00102904 File Offset: 0x00100B04
		public override string QueryString
		{
			get
			{
				return this.value.Replace("\\", "\\\\").Replace(";", "\\;").Replace(",", "\\,");
			}
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x06004DE8 RID: 19944 RVA: 0x000024ED File Offset: 0x000006ED
		public override GoogleAnalyticsDataType DataType
		{
			get
			{
				return GoogleAnalyticsDataType.String;
			}
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x00102939 File Offset: 0x00100B39
		protected override Value GenerateV2Filter()
		{
			return TextValue.New(this.value);
		}

		// Token: 0x040029CD RID: 10701
		private readonly string value;
	}
}
