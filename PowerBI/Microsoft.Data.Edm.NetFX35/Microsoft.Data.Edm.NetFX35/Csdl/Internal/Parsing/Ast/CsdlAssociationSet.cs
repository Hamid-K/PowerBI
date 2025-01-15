using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000131 RID: 305
	internal class CsdlAssociationSet : CsdlNamedElement
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x0000F3BB File Offset: 0x0000D5BB
		public CsdlAssociationSet(string name, string association, CsdlAssociationSetEnd end1, CsdlAssociationSetEnd end2, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.association = association;
			this.end1 = end1;
			this.end2 = end2;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000F3DE File Offset: 0x0000D5DE
		public string Association
		{
			get
			{
				return this.association;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000F3E6 File Offset: 0x0000D5E6
		public CsdlAssociationSetEnd End1
		{
			get
			{
				return this.end1;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000F3EE File Offset: 0x0000D5EE
		public CsdlAssociationSetEnd End2
		{
			get
			{
				return this.end2;
			}
		}

		// Token: 0x04000315 RID: 789
		private readonly string association;

		// Token: 0x04000316 RID: 790
		private readonly CsdlAssociationSetEnd end1;

		// Token: 0x04000317 RID: 791
		private readonly CsdlAssociationSetEnd end2;
	}
}
