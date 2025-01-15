using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F5 RID: 501
	internal class CsdlReferentialConstraint : CsdlElementWithDocumentation
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x00024352 File Offset: 0x00022552
		public CsdlReferentialConstraint(string propertyName, string referencedPropertyName, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.propertyName = propertyName;
			this.referencedPropertyName = referencedPropertyName;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0002436B File Offset: 0x0002256B
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00024373 File Offset: 0x00022573
		public string ReferencedPropertyName
		{
			get
			{
				return this.referencedPropertyName;
			}
		}

		// Token: 0x0400072B RID: 1835
		private readonly string propertyName;

		// Token: 0x0400072C RID: 1836
		private readonly string referencedPropertyName;
	}
}
