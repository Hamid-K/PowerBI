using System;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Parsers;

namespace Microsoft.OData.Core.UriParser.Binders
{
	// Token: 0x020001C3 RID: 451
	internal abstract class BinderBase
	{
		// Token: 0x060010E2 RID: 4322 RVA: 0x0003AE05 File Offset: 0x00039005
		protected BinderBase(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			this.bindMethod = bindMethod;
			this.state = state;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0003AE31 File Offset: 0x00039031
		protected ODataUriResolver Resolver
		{
			get
			{
				return this.state.Configuration.Resolver;
			}
		}

		// Token: 0x04000776 RID: 1910
		protected MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x04000777 RID: 1911
		protected BindingState state;
	}
}
