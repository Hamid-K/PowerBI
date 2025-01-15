using System;
using System.Collections.Generic;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x0200004E RID: 78
	public class EmptyModelMetadataProvider : AssociatedMetadataProvider<ModelMetadata>
	{
		// Token: 0x06000226 RID: 550 RVA: 0x000069C3 File Offset: 0x00004BC3
		protected override ModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
		{
			return new ModelMetadata(this, containerType, null, modelType, propertyName);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000069D0 File Offset: 0x00004BD0
		protected override ModelMetadata CreateMetadataFromPrototype(ModelMetadata prototype, Func<object> modelAccessor)
		{
			return new ModelMetadata(this, prototype.ContainerType, modelAccessor, prototype.ModelType, prototype.PropertyName);
		}
	}
}
