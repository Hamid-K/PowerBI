using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000106 RID: 262
	public class EdmReference : IEdmReference, IEdmElement
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0000D5AE File Offset: 0x0000B7AE
		public EdmReference(string uri)
		{
			this.uri = uri;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000D5D3 File Offset: 0x0000B7D3
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000D5DB File Offset: 0x0000B7DB
		public IEnumerable<IEdmInclude> Includes
		{
			get
			{
				return this.includes;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000D5E3 File Offset: 0x0000B7E3
		public IEnumerable<IEdmIncludeAnnotations> IncludeAnnotations
		{
			get
			{
				return this.includeAnnotations;
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000D5EB File Offset: 0x0000B7EB
		public void AddInclude(IEdmInclude edmInclude)
		{
			this.includes.Add(edmInclude);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000D5F9 File Offset: 0x0000B7F9
		public void AddIncludeAnnotations(IEdmIncludeAnnotations edmIncludeAnnotations)
		{
			this.includeAnnotations.Add(edmIncludeAnnotations);
		}

		// Token: 0x040001F3 RID: 499
		private string uri;

		// Token: 0x040001F4 RID: 500
		private List<IEdmInclude> includes = new List<IEdmInclude>();

		// Token: 0x040001F5 RID: 501
		private List<IEdmIncludeAnnotations> includeAnnotations = new List<IEdmIncludeAnnotations>();
	}
}
