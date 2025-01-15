using System;
using System.Threading;

namespace Microsoft.Lucia.Core.Packaging
{
	// Token: 0x02000171 RID: 369
	public abstract class IndexPackageWriter : IDisposable
	{
		// Token: 0x0600072C RID: 1836
		public abstract void WriteVersion(Version version);

		// Token: 0x0600072D RID: 1837
		public abstract void ImportContentFromDirectory(string path, CancellationToken cancellationToken);

		// Token: 0x0600072E RID: 1838 RVA: 0x0000C390 File Offset: 0x0000A590
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000C399 File Offset: 0x0000A599
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
