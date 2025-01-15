using System;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000048 RID: 72
	public class ResourceContext<TStructuredType> : ResourceContext
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00007C20 File Offset: 0x00005E20
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00007C2D File Offset: 0x00005E2D
		[Obsolete("Resource instance might not be available when the incoming uri has a $select. Use the EdmObject property instead.")]
		public new TStructuredType ResourceInstance
		{
			get
			{
				return (TStructuredType)((object)base.ResourceInstance);
			}
			set
			{
				base.ResourceInstance = value;
			}
		}
	}
}
