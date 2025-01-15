using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011F RID: 287
	internal abstract class BinderBase
	{
		// Token: 0x06000FB0 RID: 4016 RVA: 0x0002747F File Offset: 0x0002567F
		protected BinderBase(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			this.bindMethod = bindMethod;
			this.state = state;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x000274AD File Offset: 0x000256AD
		protected ODataUriResolver Resolver
		{
			get
			{
				return this.state.Configuration.Resolver;
			}
		}

		// Token: 0x04000797 RID: 1943
		protected MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x04000798 RID: 1944
		protected BindingState state;
	}
}
