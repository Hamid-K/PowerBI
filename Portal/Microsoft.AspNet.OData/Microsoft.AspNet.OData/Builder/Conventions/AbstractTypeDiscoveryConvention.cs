using System;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x0200014D RID: 333
	internal class AbstractTypeDiscoveryConvention : IEdmTypeConvention, IConvention
	{
		// Token: 0x06000C44 RID: 3140 RVA: 0x0002FF44 File Offset: 0x0002E144
		public void Apply(IEdmTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model)
		{
			StructuralTypeConfiguration structuralTypeConfiguration = edmTypeConfiguration as StructuralTypeConfiguration;
			if (structuralTypeConfiguration != null && structuralTypeConfiguration.IsAbstract == null)
			{
				structuralTypeConfiguration.IsAbstract = new bool?(TypeHelper.IsAbstract(structuralTypeConfiguration.ClrType));
			}
		}
	}
}
