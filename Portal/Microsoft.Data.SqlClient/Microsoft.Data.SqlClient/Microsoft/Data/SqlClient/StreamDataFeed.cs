using System;
using System.IO;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200008F RID: 143
	internal class StreamDataFeed : DataFeed
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002226F File Offset: 0x0002046F
		internal StreamDataFeed(Stream source)
		{
			this._source = source;
		}

		// Token: 0x040002FF RID: 767
		internal Stream _source;
	}
}
