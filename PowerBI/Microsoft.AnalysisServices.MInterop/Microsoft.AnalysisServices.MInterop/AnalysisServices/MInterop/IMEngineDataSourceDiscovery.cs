using System;
using System.Collections.Generic;
using Microsoft.Data.Mashup;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000016 RID: 22
	internal interface IMEngineDataSourceDiscovery
	{
		// Token: 0x06000051 RID: 81
		IEnumerable<MashupDiscovery> FindReferencedDataSources(string memberName);

		// Token: 0x06000052 RID: 82
		ISet<MEngineLibrarySymbol> FindReferencedLibrarySymbols(string memberName);
	}
}
