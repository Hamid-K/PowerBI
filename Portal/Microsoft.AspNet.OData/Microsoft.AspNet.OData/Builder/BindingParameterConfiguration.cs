using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000128 RID: 296
	public class BindingParameterConfiguration : ParameterConfiguration
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x00029B94 File Offset: 0x00027D94
		public BindingParameterConfiguration(string name, IEdmTypeConfiguration parameterType)
			: base(name, parameterType)
		{
			EdmTypeKind edmTypeKind = parameterType.Kind;
			if (edmTypeKind == EdmTypeKind.Collection)
			{
				edmTypeKind = (parameterType as CollectionTypeConfiguration).ElementType.Kind;
			}
			if (edmTypeKind != EdmTypeKind.Entity)
			{
				throw Error.Argument("parameterType", SRResources.InvalidBindingParameterType, new object[] { parameterType.FullName });
			}
		}

		// Token: 0x0400033A RID: 826
		public const string DefaultBindingParameterName = "bindingParameter";
	}
}
