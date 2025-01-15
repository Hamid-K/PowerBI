using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200004A RID: 74
	public interface IFeedResult
	{
		// Token: 0x06000145 RID: 325
		Stream BeginStreamingData(IEnumerable<KeyValuePair<string, string>> headers, int statusCode);
	}
}
