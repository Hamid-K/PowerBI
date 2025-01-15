using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200007F RID: 127
	public interface ISchemaMetadataProvider
	{
		// Token: 0x06000240 RID: 576
		IReadOnlyDictionary<int, DependentSchemaContainer> GetDependentSchemaContainers(DependentSchema[] dependentSchemas, IConceptualSchema baseConceptualSchema, string databaseName);
	}
}
