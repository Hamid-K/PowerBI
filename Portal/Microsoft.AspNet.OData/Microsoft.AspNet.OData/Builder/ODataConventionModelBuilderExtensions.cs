using System;
using System.ComponentModel;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200011E RID: 286
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ODataConventionModelBuilderExtensions
	{
		// Token: 0x060009D7 RID: 2519 RVA: 0x00028945 File Offset: 0x00026B45
		public static ODataConventionModelBuilder EnableLowerCamelCase(this ODataConventionModelBuilder builder)
		{
			if (builder == null)
			{
				throw Error.ArgumentNull("builder");
			}
			return builder.EnableLowerCamelCase(NameResolverOptions.ProcessReflectedPropertyNames | NameResolverOptions.ProcessDataMemberAttributePropertyNames | NameResolverOptions.ProcessExplicitPropertyNames);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002895C File Offset: 0x00026B5C
		public static ODataConventionModelBuilder EnableLowerCamelCase(this ODataConventionModelBuilder builder, NameResolverOptions options)
		{
			if (builder == null)
			{
				throw Error.ArgumentNull("builder");
			}
			builder.OnModelCreating = (Action<ODataConventionModelBuilder>)Delegate.Combine(builder.OnModelCreating, new Action<ODataConventionModelBuilder>(new LowerCamelCaser(options).ApplyLowerCamelCase));
			return builder;
		}
	}
}
