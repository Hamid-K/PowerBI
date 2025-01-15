using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000E0 RID: 224
	internal abstract class BinderBase
	{
		// Token: 0x06000B58 RID: 2904 RVA: 0x0001BD0F File Offset: 0x00019F0F
		protected BinderBase(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			this.bindMethod = bindMethod;
			this.state = state;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0001BD3D File Offset: 0x00019F3D
		protected ODataUriResolver Resolver
		{
			get
			{
				return this.state.Configuration.Resolver;
			}
		}

		// Token: 0x04000685 RID: 1669
		protected MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x04000686 RID: 1670
		protected BindingState state;
	}
}
