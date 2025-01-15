using System;
using System.Collections.Generic;
using System.Threading;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000071 RID: 113
	internal class ManagedIdentityRequestFailedDetailsParser : RequestFailedDetailsParser
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0000B5B0 File Offset: 0x000097B0
		public override bool TryParse(Response response, out ResponseError error, out IDictionary<string, string> data)
		{
			data = new Dictionary<string, string>();
			error = null;
			bool flag;
			try
			{
				string text = response.Content.ToString();
				if (text == null || !text.StartsWith("{", StringComparison.OrdinalIgnoreCase))
				{
					flag = false;
				}
				else
				{
					string text2 = ManagedIdentitySource.GetMessageFromResponse(response, false, CancellationToken.None).EnsureCompleted<string>();
					error = new ResponseError(null, text2);
					flag = true;
				}
			}
			catch
			{
				error = new ResponseError(null, "Managed Identity response was not in the expected format. See the inner exception for details.");
				flag = true;
			}
			return flag;
		}
	}
}
