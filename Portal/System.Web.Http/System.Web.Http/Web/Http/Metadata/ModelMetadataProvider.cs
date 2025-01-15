using System;
using System.Collections.Generic;

namespace System.Web.Http.Metadata
{
	// Token: 0x02000048 RID: 72
	public abstract class ModelMetadataProvider
	{
		// Token: 0x060001F2 RID: 498
		public abstract IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType);

		// Token: 0x060001F3 RID: 499
		public abstract ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName);

		// Token: 0x060001F4 RID: 500
		public abstract ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType);
	}
}
