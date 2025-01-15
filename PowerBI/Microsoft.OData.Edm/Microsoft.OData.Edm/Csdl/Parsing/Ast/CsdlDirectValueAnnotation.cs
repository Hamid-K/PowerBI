using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E9 RID: 489
	internal class CsdlDirectValueAnnotation : CsdlElement
	{
		// Token: 0x06000D78 RID: 3448 RVA: 0x00025E21 File Offset: 0x00024021
		public CsdlDirectValueAnnotation(string namespaceName, string name, string value, bool isAttribute, CsdlLocation location)
			: base(location)
		{
			this.namespaceName = namespaceName;
			this.name = name;
			this.value = value;
			this.isAttribute = isAttribute;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00025E48 File Offset: 0x00024048
		public string NamespaceName
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00025E50 File Offset: 0x00024050
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00025E58 File Offset: 0x00024058
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00025E60 File Offset: 0x00024060
		public bool IsAttribute
		{
			get
			{
				return this.isAttribute;
			}
		}

		// Token: 0x0400076B RID: 1899
		private readonly string namespaceName;

		// Token: 0x0400076C RID: 1900
		private readonly string name;

		// Token: 0x0400076D RID: 1901
		private readonly string value;

		// Token: 0x0400076E RID: 1902
		private readonly bool isAttribute;
	}
}
