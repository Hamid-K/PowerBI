using System;
using System.IO;
using System.Threading;
using Microsoft.Lucia.Core;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200004F RID: 79
	public interface IDataIndexReader
	{
		// Token: 0x0600025E RID: 606
		bool TryRead(Stream stream, IDatabaseContext databaseContext, CancellationToken token, out OpenDataIndexResult result);
	}
}
