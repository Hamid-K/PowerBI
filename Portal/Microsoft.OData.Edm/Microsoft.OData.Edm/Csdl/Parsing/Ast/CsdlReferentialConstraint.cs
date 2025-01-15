using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000202 RID: 514
	internal class CsdlReferentialConstraint : CsdlElement
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0002648B File Offset: 0x0002468B
		public CsdlReferentialConstraint(string propertyName, string referencedPropertyName, CsdlLocation location)
			: base(location)
		{
			this.propertyName = propertyName;
			this.referencedPropertyName = referencedPropertyName;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x000264A2 File Offset: 0x000246A2
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000264AA File Offset: 0x000246AA
		public string ReferencedPropertyName
		{
			get
			{
				return this.referencedPropertyName;
			}
		}

		// Token: 0x040007A1 RID: 1953
		private readonly string propertyName;

		// Token: 0x040007A2 RID: 1954
		private readonly string referencedPropertyName;
	}
}
