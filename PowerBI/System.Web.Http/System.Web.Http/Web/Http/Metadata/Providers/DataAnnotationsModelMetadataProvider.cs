using System;
using System.Collections.Generic;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x0200004D RID: 77
	public class DataAnnotationsModelMetadataProvider : AssociatedMetadataProvider<CachedDataAnnotationsModelMetadata>
	{
		// Token: 0x06000223 RID: 547 RVA: 0x000069A5 File Offset: 0x00004BA5
		protected override CachedDataAnnotationsModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
		{
			return new CachedDataAnnotationsModelMetadata(this, containerType, modelType, propertyName, attributes);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000069B2 File Offset: 0x00004BB2
		protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
		{
			return new CachedDataAnnotationsModelMetadata(prototype, modelAccessor);
		}
	}
}
