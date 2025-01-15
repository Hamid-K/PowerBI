using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000177 RID: 375
	internal class CsdlDirectValueAnnotation : CsdlElement
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x000116D5 File Offset: 0x0000F8D5
		public CsdlDirectValueAnnotation(string namespaceName, string name, string value, bool isAttribute, CsdlLocation location)
			: base(location)
		{
			this.namespaceName = namespaceName;
			this.name = name;
			this.value = value;
			this.isAttribute = isAttribute;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000116FC File Offset: 0x0000F8FC
		public string NamespaceName
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00011704 File Offset: 0x0000F904
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001170C File Offset: 0x0000F90C
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00011714 File Offset: 0x0000F914
		public bool IsAttribute
		{
			get
			{
				return this.isAttribute;
			}
		}

		// Token: 0x040003AC RID: 940
		private readonly string namespaceName;

		// Token: 0x040003AD RID: 941
		private readonly string name;

		// Token: 0x040003AE RID: 942
		private readonly string value;

		// Token: 0x040003AF RID: 943
		private readonly bool isAttribute;
	}
}
