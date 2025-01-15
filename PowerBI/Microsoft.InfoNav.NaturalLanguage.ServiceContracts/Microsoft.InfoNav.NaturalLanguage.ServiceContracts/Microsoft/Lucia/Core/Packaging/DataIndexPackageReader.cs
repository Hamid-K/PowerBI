using System;
using System.Threading;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.Lucia.Core.Packaging
{
	// Token: 0x0200016E RID: 366
	public abstract class DataIndexPackageReader : IndexPackageReader
	{
		// Token: 0x06000722 RID: 1826
		public abstract bool TryReadVersion(out DataIndexVersion version);

		// Token: 0x06000723 RID: 1827
		public abstract DataIndexMetadata ReadMetadata(CancellationToken cancellationToken);
	}
}
