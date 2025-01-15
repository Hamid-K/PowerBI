using System;
using System.Threading;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.Lucia.Core.Packaging
{
	// Token: 0x0200016F RID: 367
	public abstract class DataIndexPackageWriter : IndexPackageWriter
	{
		// Token: 0x06000725 RID: 1829
		public abstract void WriteMetadata(DataIndexMetadata metadata, CancellationToken cancellationToken);
	}
}
