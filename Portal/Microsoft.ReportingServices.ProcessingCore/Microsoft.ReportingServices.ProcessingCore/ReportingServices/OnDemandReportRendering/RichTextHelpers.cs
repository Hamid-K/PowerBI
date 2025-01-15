using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200031C RID: 796
	internal sealed class RichTextHelpers
	{
		// Token: 0x06001D8E RID: 7566 RVA: 0x00074438 File Offset: 0x00072638
		internal static MarkupType TranslateMarkupType(string value)
		{
			if (ReportProcessing.CompareWithInvariantCulture(value, "None", false) == 0)
			{
				return MarkupType.None;
			}
			if (ReportProcessing.CompareWithInvariantCulture(value, "HTML", false) == 0)
			{
				return MarkupType.HTML;
			}
			return MarkupType.None;
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0007445B File Offset: 0x0007265B
		internal static ListStyle TranslateListStyle(string value)
		{
			if (ReportProcessing.CompareWithInvariantCulture(value, "None", false) == 0)
			{
				return ListStyle.None;
			}
			if (ReportProcessing.CompareWithInvariantCulture(value, "Numbered", false) == 0)
			{
				return ListStyle.Numbered;
			}
			if (ReportProcessing.CompareWithInvariantCulture(value, "Bulleted", false) == 0)
			{
				return ListStyle.Bulleted;
			}
			return ListStyle.None;
		}
	}
}
