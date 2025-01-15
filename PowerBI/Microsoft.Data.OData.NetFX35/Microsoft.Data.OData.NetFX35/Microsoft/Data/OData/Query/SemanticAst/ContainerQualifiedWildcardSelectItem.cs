using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000067 RID: 103
	public sealed class ContainerQualifiedWildcardSelectItem : SelectItem
	{
		// Token: 0x0600027F RID: 639 RVA: 0x0000A131 File Offset: 0x00008331
		public ContainerQualifiedWildcardSelectItem(IEdmEntityContainer container)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityContainer>(container, "container");
			this.container = container;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000A14B File Offset: 0x0000834B
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x040000A8 RID: 168
		private readonly IEdmEntityContainer container;
	}
}
