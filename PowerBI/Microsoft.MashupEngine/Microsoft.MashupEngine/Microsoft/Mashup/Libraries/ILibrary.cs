using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020CE RID: 8398
	public interface ILibrary
	{
		// Token: 0x17003171 RID: 12657
		// (get) Token: 0x0600CDC1 RID: 52673
		ILibraryProvider Provider { get; }

		// Token: 0x17003172 RID: 12658
		// (get) Token: 0x0600CDC2 RID: 52674
		string Identifier { get; }

		// Token: 0x17003173 RID: 12659
		// (get) Token: 0x0600CDC3 RID: 52675
		string Version { get; }

		// Token: 0x17003174 RID: 12660
		// (get) Token: 0x0600CDC4 RID: 52676
		byte[] Contents { get; }

		// Token: 0x17003175 RID: 12661
		// (get) Token: 0x0600CDC5 RID: 52677
		IEnumerable<KeyValuePair<string, byte[]>> Metadata { get; }

		// Token: 0x0600CDC6 RID: 52678
		bool TryGetMetadata(string metadataName, out byte[] metadata);

		// Token: 0x0600CDC7 RID: 52679
		bool TrySetMetadata(string metadataName, byte[] metadata);
	}
}
