using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.PowerBI.ExploreHost.DocumentConversion
{
	// Token: 0x02000090 RID: 144
	public interface IExplorationConversionHandler
	{
		// Token: 0x060003B8 RID: 952
		string ConvertToExploration(Stream documentStream, string databaseID, Dictionary<string, string> workSheetNames = null, Dictionary<string, bool> workSheetNameToDataSourceMapping = null, bool convertFromRdlx = false);
	}
}
