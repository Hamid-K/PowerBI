using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000257 RID: 599
	internal static class WriterUtils
	{
		// Token: 0x060012A3 RID: 4771 RVA: 0x000466DE File Offset: 0x000448DE
		internal static bool ShouldSkipProperty(this ProjectedPropertiesAnnotation projectedProperties, string propertyName)
		{
			return projectedProperties != null && (object.ReferenceEquals(ProjectedPropertiesAnnotation.EmptyProjectedPropertiesInstance, projectedProperties) || (!object.ReferenceEquals(ProjectedPropertiesAnnotation.AllProjectedPropertiesInstance, projectedProperties) && !projectedProperties.IsPropertyProjected(propertyName)));
		}
	}
}
