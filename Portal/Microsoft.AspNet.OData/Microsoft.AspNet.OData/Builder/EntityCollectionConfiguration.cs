using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200012E RID: 302
	public class EntityCollectionConfiguration<TEntityType> : CollectionTypeConfiguration
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x0002AAAD File Offset: 0x00028CAD
		internal EntityCollectionConfiguration(EntityTypeConfiguration elementType)
			: base(elementType, typeof(IEnumerable<TEntityType>))
		{
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002AAC0 File Offset: 0x00028CC0
		public ActionConfiguration Action(string name)
		{
			ActionConfiguration actionConfiguration = base.ModelBuilder.Action(name);
			actionConfiguration.SetBindingParameter("bindingParameter", this);
			return actionConfiguration;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002AADB File Offset: 0x00028CDB
		public FunctionConfiguration Function(string name)
		{
			FunctionConfiguration functionConfiguration = base.ModelBuilder.Function(name);
			functionConfiguration.SetBindingParameter("bindingParameter", this);
			return functionConfiguration;
		}
	}
}
