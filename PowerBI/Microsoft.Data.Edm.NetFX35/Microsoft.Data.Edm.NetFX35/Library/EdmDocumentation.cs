using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001CC RID: 460
	public class EdmDocumentation : IEdmDocumentation
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x0001FFF3 File Offset: 0x0001E1F3
		public EdmDocumentation(string summary, string description)
		{
			this.summary = summary;
			this.description = description;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00020009 File Offset: 0x0001E209
		public string Summary
		{
			get
			{
				return this.summary;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00020011 File Offset: 0x0001E211
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x0400051F RID: 1311
		private readonly string summary;

		// Token: 0x04000520 RID: 1312
		private readonly string description;
	}
}
