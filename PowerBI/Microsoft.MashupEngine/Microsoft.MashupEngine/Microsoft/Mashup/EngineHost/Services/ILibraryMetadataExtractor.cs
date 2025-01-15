using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Libraries;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019F1 RID: 6641
	public interface ILibraryMetadataExtractor
	{
		// Token: 0x17002AC9 RID: 10953
		// (get) Token: 0x0600A7FE RID: 43006
		IEnumerable<string> RequiredMetadata { get; }

		// Token: 0x0600A7FF RID: 43007
		IDictionary<string, byte[]> ExtractMetadata(ILibraryService libraryService, ILibrary library, IModule module, IRecordValue exports);
	}
}
