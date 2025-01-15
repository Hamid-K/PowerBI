using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000178 RID: 376
	internal abstract class AttributeEdmPropertyConvention<TPropertyConfiguration> : AttributeConvention, IEdmPropertyConvention<TPropertyConfiguration>, IEdmPropertyConvention, IConvention where TPropertyConfiguration : PropertyConfiguration
	{
		// Token: 0x06000CA7 RID: 3239 RVA: 0x00031DDD File Offset: 0x0002FFDD
		protected AttributeEdmPropertyConvention(Func<Attribute, bool> attributeFilter, bool allowMultiple)
			: base(attributeFilter, allowMultiple)
		{
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00031DE8 File Offset: 0x0002FFE8
		public void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (structuralTypeConfiguration == null)
			{
				throw Error.ArgumentNull("structuralTypeConfiguration");
			}
			TPropertyConfiguration tpropertyConfiguration = edmProperty as TPropertyConfiguration;
			if (tpropertyConfiguration != null)
			{
				this.Apply(tpropertyConfiguration, structuralTypeConfiguration, model);
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00031E30 File Offset: 0x00030030
		public void Apply(TPropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (structuralTypeConfiguration == null)
			{
				throw Error.ArgumentNull("structuralTypeConfiguration");
			}
			foreach (Attribute attribute in base.GetAttributes(edmProperty.PropertyInfo))
			{
				this.Apply(edmProperty, structuralTypeConfiguration, attribute, model);
			}
		}

		// Token: 0x06000CAA RID: 3242
		public abstract void Apply(TPropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model);
	}
}
