using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000130 RID: 304
	internal class CsdlAssociationEnd : CsdlNamedElement
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x0000F380 File Offset: 0x0000D580
		public CsdlAssociationEnd(string role, CsdlTypeReference type, EdmMultiplicity multiplicity, CsdlOnDelete onDelete, CsdlDocumentation documentation, CsdlLocation location)
			: base(role, documentation, location)
		{
			this.type = type;
			this.multiplicity = multiplicity;
			this.onDelete = onDelete;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000F3A3 File Offset: 0x0000D5A3
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000F3AB File Offset: 0x0000D5AB
		public EdmMultiplicity Multiplicity
		{
			get
			{
				return this.multiplicity;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000F3B3 File Offset: 0x0000D5B3
		public CsdlOnDelete OnDelete
		{
			get
			{
				return this.onDelete;
			}
		}

		// Token: 0x04000312 RID: 786
		private readonly CsdlTypeReference type;

		// Token: 0x04000313 RID: 787
		private readonly EdmMultiplicity multiplicity;

		// Token: 0x04000314 RID: 788
		private readonly CsdlOnDelete onDelete;
	}
}
