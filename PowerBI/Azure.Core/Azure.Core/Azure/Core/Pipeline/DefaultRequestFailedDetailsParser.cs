using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000093 RID: 147
	internal class DefaultRequestFailedDetailsParser : RequestFailedDetailsParser
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000E6D1 File Offset: 0x0000C8D1
		[NullableContext(1)]
		public override bool TryParse(Response response, [Nullable(2)] out ResponseError error, [Nullable(new byte[] { 2, 1, 1 })] out IDictionary<string, string> data)
		{
			return RequestFailedException.TryExtractErrorContent(response, out error, out data);
		}
	}
}
