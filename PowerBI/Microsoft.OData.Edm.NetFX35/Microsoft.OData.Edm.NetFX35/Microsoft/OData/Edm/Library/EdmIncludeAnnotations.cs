using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000105 RID: 261
	public class EdmIncludeAnnotations : IEdmIncludeAnnotations
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x0000D579 File Offset: 0x0000B779
		public EdmIncludeAnnotations(string termNamespace, string qualifier, string targetNamespace)
		{
			this.termNamespace = termNamespace;
			this.qualifier = qualifier;
			this.targetNamespace = targetNamespace;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000D596 File Offset: 0x0000B796
		public string TermNamespace
		{
			get
			{
				return this.termNamespace;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000D59E File Offset: 0x0000B79E
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
		public string TargetNamespace
		{
			get
			{
				return this.targetNamespace;
			}
		}

		// Token: 0x040001F0 RID: 496
		private readonly string termNamespace;

		// Token: 0x040001F1 RID: 497
		private readonly string qualifier;

		// Token: 0x040001F2 RID: 498
		private readonly string targetNamespace;
	}
}
