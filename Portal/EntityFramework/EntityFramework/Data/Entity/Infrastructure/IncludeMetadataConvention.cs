using System;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000252 RID: 594
	[Obsolete("The IncludeMetadataConvention is no longer used. EdmMetadata is not included in the model. <see cref=\"EdmModelDiffer\" /> is now used to detect changes in the model.")]
	public class IncludeMetadataConvention : Convention
	{
		// Token: 0x06001EBE RID: 7870 RVA: 0x000558DA File Offset: 0x00053ADA
		internal virtual void Apply(ModelConfiguration modelConfiguration)
		{
			Check.NotNull<ModelConfiguration>(modelConfiguration, "modelConfiguration");
			EdmMetadataContext.ConfigureEdmMetadata(modelConfiguration);
		}
	}
}
