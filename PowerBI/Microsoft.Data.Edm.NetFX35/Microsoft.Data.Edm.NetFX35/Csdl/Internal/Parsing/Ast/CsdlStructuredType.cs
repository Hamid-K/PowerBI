using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000134 RID: 308
	internal abstract class CsdlStructuredType : CsdlElementWithDocumentation
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0000F45B File Offset: 0x0000D65B
		protected CsdlStructuredType(IEnumerable<CsdlProperty> properties, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.properties = new List<CsdlProperty>(properties);
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000F471 File Offset: 0x0000D671
		public IEnumerable<CsdlProperty> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x0400031D RID: 797
		protected List<CsdlProperty> properties;
	}
}
