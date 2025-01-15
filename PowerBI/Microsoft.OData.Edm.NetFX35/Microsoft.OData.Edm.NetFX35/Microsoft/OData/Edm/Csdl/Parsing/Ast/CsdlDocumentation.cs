using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200017D RID: 381
	internal class CsdlDocumentation : CsdlElement
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x000117F0 File Offset: 0x0000F9F0
		public CsdlDocumentation(string summary, string longDescription, CsdlLocation location)
			: base(location)
		{
			this.summary = summary;
			this.longDescription = longDescription;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00011807 File Offset: 0x0000FA07
		public string Summary
		{
			get
			{
				return this.summary;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001180F File Offset: 0x0000FA0F
		public string LongDescription
		{
			get
			{
				return this.longDescription;
			}
		}

		// Token: 0x040003B9 RID: 953
		private readonly string summary;

		// Token: 0x040003BA RID: 954
		private readonly string longDescription;
	}
}
