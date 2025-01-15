using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DE RID: 478
	internal class CsdlDocumentation : CsdlElement
	{
		// Token: 0x06000CCB RID: 3275 RVA: 0x00023D00 File Offset: 0x00021F00
		public CsdlDocumentation(string summary, string longDescription, CsdlLocation location)
			: base(location)
		{
			this.summary = summary;
			this.longDescription = longDescription;
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00023D17 File Offset: 0x00021F17
		public string Summary
		{
			get
			{
				return this.summary;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00023D1F File Offset: 0x00021F1F
		public string LongDescription
		{
			get
			{
				return this.longDescription;
			}
		}

		// Token: 0x040006F6 RID: 1782
		private readonly string summary;

		// Token: 0x040006F7 RID: 1783
		private readonly string longDescription;
	}
}
