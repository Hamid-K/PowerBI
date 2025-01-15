using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003C RID: 60
	public class EdmReference : IEdmReference, IEdmElement
	{
		// Token: 0x06000131 RID: 305 RVA: 0x000043DC File Offset: 0x000025DC
		public EdmReference(Uri uri)
		{
			this.uri = uri;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004401 File Offset: 0x00002601
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004409 File Offset: 0x00002609
		public IEnumerable<IEdmInclude> Includes
		{
			get
			{
				return this.includes;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004411 File Offset: 0x00002611
		public IEnumerable<IEdmIncludeAnnotations> IncludeAnnotations
		{
			get
			{
				return this.includeAnnotations;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004419 File Offset: 0x00002619
		public void AddInclude(IEdmInclude edmInclude)
		{
			this.includes.Add(edmInclude);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004427 File Offset: 0x00002627
		public void AddIncludeAnnotations(IEdmIncludeAnnotations edmIncludeAnnotations)
		{
			this.includeAnnotations.Add(edmIncludeAnnotations);
		}

		// Token: 0x0400006D RID: 109
		private Uri uri;

		// Token: 0x0400006E RID: 110
		private List<IEdmInclude> includes = new List<IEdmInclude>();

		// Token: 0x0400006F RID: 111
		private List<IEdmIncludeAnnotations> includeAnnotations = new List<IEdmIncludeAnnotations>();
	}
}
