using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000071 RID: 113
	public class EdmReference : IEdmReference, IEdmElement
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x0000C024 File Offset: 0x0000A224
		public EdmReference(Uri uri)
		{
			this.uri = uri;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000C049 File Offset: 0x0000A249
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000C051 File Offset: 0x0000A251
		public IEnumerable<IEdmInclude> Includes
		{
			get
			{
				return this.includes;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000C059 File Offset: 0x0000A259
		public IEnumerable<IEdmIncludeAnnotations> IncludeAnnotations
		{
			get
			{
				return this.includeAnnotations;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000C061 File Offset: 0x0000A261
		public void AddInclude(IEdmInclude edmInclude)
		{
			this.includes.Add(edmInclude);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000C06F File Offset: 0x0000A26F
		public void AddIncludeAnnotations(IEdmIncludeAnnotations edmIncludeAnnotations)
		{
			this.includeAnnotations.Add(edmIncludeAnnotations);
		}

		// Token: 0x040000F9 RID: 249
		private Uri uri;

		// Token: 0x040000FA RID: 250
		private List<IEdmInclude> includes = new List<IEdmInclude>();

		// Token: 0x040000FB RID: 251
		private List<IEdmIncludeAnnotations> includeAnnotations = new List<IEdmIncludeAnnotations>();
	}
}
