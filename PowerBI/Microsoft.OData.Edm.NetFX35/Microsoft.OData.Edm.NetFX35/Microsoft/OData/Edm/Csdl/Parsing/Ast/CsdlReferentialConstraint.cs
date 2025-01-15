using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200018B RID: 395
	internal class CsdlReferentialConstraint : CsdlElementWithDocumentation
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x00011AAA File Offset: 0x0000FCAA
		public CsdlReferentialConstraint(string propertyName, string referencedPropertyName, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.propertyName = propertyName;
			this.referencedPropertyName = referencedPropertyName;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00011AC3 File Offset: 0x0000FCC3
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00011ACB File Offset: 0x0000FCCB
		public string ReferencedPropertyName
		{
			get
			{
				return this.referencedPropertyName;
			}
		}

		// Token: 0x040003D7 RID: 983
		private readonly string propertyName;

		// Token: 0x040003D8 RID: 984
		private readonly string referencedPropertyName;
	}
}
