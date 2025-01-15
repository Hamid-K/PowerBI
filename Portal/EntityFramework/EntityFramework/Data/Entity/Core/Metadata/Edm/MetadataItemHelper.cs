using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DA RID: 1242
	internal static class MetadataItemHelper
	{
		// Token: 0x06003DBF RID: 15807 RVA: 0x000CCF70 File Offset: 0x000CB170
		public static bool IsInvalid(MetadataItem instance)
		{
			MetadataProperty metadataProperty;
			return instance.MetadataProperties.TryGetValue("EdmSchemaInvalid", false, out metadataProperty) && metadataProperty != null && (bool)metadataProperty.Value;
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000CCFA2 File Offset: 0x000CB1A2
		public static bool HasSchemaErrors(MetadataItem instance)
		{
			return instance.MetadataProperties.Contains("EdmSchemaErrors");
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000CCFB4 File Offset: 0x000CB1B4
		public static IEnumerable<EdmSchemaError> GetSchemaErrors(MetadataItem instance)
		{
			MetadataProperty metadataProperty;
			if (!instance.MetadataProperties.TryGetValue("EdmSchemaErrors", false, out metadataProperty) || metadataProperty == null)
			{
				return Enumerable.Empty<EdmSchemaError>();
			}
			return (IEnumerable<EdmSchemaError>)metadataProperty.Value;
		}

		// Token: 0x04001507 RID: 5383
		internal const string SchemaErrorsMetadataPropertyName = "EdmSchemaErrors";

		// Token: 0x04001508 RID: 5384
		internal const string SchemaInvalidMetadataPropertyName = "EdmSchemaInvalid";
	}
}
