using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000062 RID: 98
	public class EdmIncludeAnnotations : IEdmIncludeAnnotations
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000AF6C File Offset: 0x0000916C
		public EdmIncludeAnnotations(string termNamespace, string qualifier, string targetNamespace)
		{
			this.termNamespace = termNamespace;
			this.qualifier = qualifier;
			this.targetNamespace = targetNamespace;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000AF89 File Offset: 0x00009189
		public string TermNamespace
		{
			get
			{
				return this.termNamespace;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000AF91 File Offset: 0x00009191
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000AF99 File Offset: 0x00009199
		public string TargetNamespace
		{
			get
			{
				return this.targetNamespace;
			}
		}

		// Token: 0x040000C8 RID: 200
		private readonly string termNamespace;

		// Token: 0x040000C9 RID: 201
		private readonly string qualifier;

		// Token: 0x040000CA RID: 202
		private readonly string targetNamespace;
	}
}
