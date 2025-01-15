using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200018A RID: 394
	internal class CsdlPropertyReference : CsdlElement
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x00011A92 File Offset: 0x0000FC92
		public CsdlPropertyReference(string propertyName, CsdlLocation location)
			: base(location)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00011AA2 File Offset: 0x0000FCA2
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x040003D6 RID: 982
		private readonly string propertyName;
	}
}
