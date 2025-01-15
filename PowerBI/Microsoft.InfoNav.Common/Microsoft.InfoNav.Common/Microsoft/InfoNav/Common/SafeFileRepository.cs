using System;
using System.Collections.Concurrent;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200006E RID: 110
	public sealed class SafeFileRepository
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public SafeFileRepository()
		{
			this._repository = new ConcurrentDictionary<string, SafeFile>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		public SafeFile GetOrLoadFile(string filePath)
		{
			return this._repository.GetOrAdd(filePath, new SafeFile(filePath));
		}

		// Token: 0x040000E0 RID: 224
		private readonly ConcurrentDictionary<string, SafeFile> _repository;
	}
}
