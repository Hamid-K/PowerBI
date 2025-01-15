using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200017A RID: 378
	internal abstract class AttributeEdmTypeConvention<TEdmTypeConfiguration> : AttributeConvention, IEdmTypeConvention, IConvention where TEdmTypeConfiguration : class, IEdmTypeConfiguration
	{
		// Token: 0x06000CAE RID: 3246 RVA: 0x00031DDD File Offset: 0x0002FFDD
		protected AttributeEdmTypeConvention(Func<Attribute, bool> attributeFilter, bool allowMultiple)
			: base(attributeFilter, allowMultiple)
		{
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00031EE4 File Offset: 0x000300E4
		public void Apply(IEdmTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model)
		{
			TEdmTypeConfiguration tedmTypeConfiguration = edmTypeConfiguration as TEdmTypeConfiguration;
			if (tedmTypeConfiguration != null)
			{
				this.Apply(tedmTypeConfiguration, model);
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00031F10 File Offset: 0x00030110
		public void Apply(TEdmTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model)
		{
			if (edmTypeConfiguration == null)
			{
				throw Error.ArgumentNull("edmTypeConfiguration");
			}
			foreach (Attribute attribute in base.GetAttributes(TypeHelper.AsMemberInfo(edmTypeConfiguration.ClrType)))
			{
				this.Apply(edmTypeConfiguration, model, attribute);
			}
		}

		// Token: 0x06000CB1 RID: 3249
		public abstract void Apply(TEdmTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model, Attribute attribute);
	}
}
