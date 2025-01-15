using System;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Http;

namespace Microsoft.Mashup.Engine1.Library.SharePoint
{
	// Token: 0x02000412 RID: 1042
	internal static class SharePointUtil
	{
		// Token: 0x060023B5 RID: 9141 RVA: 0x00064E1B File Offset: 0x0006301B
		public static bool TryExtractSharePointCorrelationID(MashupHttpWebResponse errorResponse, out string guid)
		{
			if (errorResponse != null && errorResponse.Headers != null)
			{
				guid = errorResponse.Headers.Get("SPRequestGuid");
				if (guid != null)
				{
					return true;
				}
			}
			guid = string.Empty;
			return false;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00064E48 File Offset: 0x00063048
		public static void HandleResponseError(MashupHttpWebResponse response, IHostTrace trace)
		{
			string text;
			if (SharePointUtil.TryExtractSharePointCorrelationID(response, out text))
			{
				trace.Add("SPRequestGuid", text, false);
			}
		}
	}
}
