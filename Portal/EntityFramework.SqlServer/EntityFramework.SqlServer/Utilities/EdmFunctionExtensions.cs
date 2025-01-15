using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200001C RID: 28
	internal static class EdmFunctionExtensions
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		internal static bool IsCSpace(this EdmFunction function)
		{
			MetadataProperty metadataProperty = function.MetadataProperties.FirstOrDefault((MetadataProperty p) => p.Name == "DataSpace");
			return metadataProperty != null && (DataSpace)metadataProperty.Value == DataSpace.CSpace;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000EAC7 File Offset: 0x0000CCC7
		internal static bool IsCanonicalFunction(this EdmFunction function)
		{
			return function.IsCSpace() && function.NamespaceName == "Edm";
		}
	}
}
