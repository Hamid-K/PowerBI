using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003A RID: 58
	public class EdmIncludeAnnotations : IEdmIncludeAnnotations
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00003FC9 File Offset: 0x000021C9
		public EdmIncludeAnnotations(string termNamespace, string qualifier, string targetNamespace)
		{
			this.termNamespace = termNamespace;
			this.qualifier = qualifier;
			this.targetNamespace = targetNamespace;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003FE6 File Offset: 0x000021E6
		public string TermNamespace
		{
			get
			{
				return this.termNamespace;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00003FEE File Offset: 0x000021EE
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003FF6 File Offset: 0x000021F6
		public string TargetNamespace
		{
			get
			{
				return this.targetNamespace;
			}
		}

		// Token: 0x04000066 RID: 102
		private readonly string termNamespace;

		// Token: 0x04000067 RID: 103
		private readonly string qualifier;

		// Token: 0x04000068 RID: 104
		private readonly string targetNamespace;
	}
}
