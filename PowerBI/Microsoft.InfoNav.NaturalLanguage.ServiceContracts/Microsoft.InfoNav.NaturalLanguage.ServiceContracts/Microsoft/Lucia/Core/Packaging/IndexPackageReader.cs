using System;
using System.Threading;

namespace Microsoft.Lucia.Core.Packaging
{
	// Token: 0x02000170 RID: 368
	public abstract class IndexPackageReader : IDisposable
	{
		// Token: 0x06000727 RID: 1831
		public abstract bool TryReadVersion(out Version version);

		// Token: 0x06000728 RID: 1832
		public abstract void ExtractContentToDirectory(string path, CancellationToken cancellationToken);

		// Token: 0x06000729 RID: 1833 RVA: 0x0000C37D File Offset: 0x0000A57D
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000C386 File Offset: 0x0000A586
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
