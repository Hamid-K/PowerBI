using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000052 RID: 82
	internal class EdmDocumentation : IEdmDocumentation
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000A6F9 File Offset: 0x000088F9
		public EdmDocumentation(string summary, string description)
		{
			this.summary = summary;
			this.description = description;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000A70F File Offset: 0x0000890F
		public string Summary
		{
			get
			{
				return this.summary;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000A717 File Offset: 0x00008917
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x040000A8 RID: 168
		private readonly string summary;

		// Token: 0x040000A9 RID: 169
		private readonly string description;
	}
}
