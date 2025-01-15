using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001FB RID: 507
	internal class EdmDocumentation : IEdmDocumentation
	{
		// Token: 0x06000BDE RID: 3038 RVA: 0x00021AEB File Offset: 0x0001FCEB
		public EdmDocumentation(string summary, string description)
		{
			this.summary = summary;
			this.description = description;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00021B01 File Offset: 0x0001FD01
		public string Summary
		{
			get
			{
				return this.summary;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00021B09 File Offset: 0x0001FD09
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x04000571 RID: 1393
		private readonly string summary;

		// Token: 0x04000572 RID: 1394
		private readonly string description;
	}
}
