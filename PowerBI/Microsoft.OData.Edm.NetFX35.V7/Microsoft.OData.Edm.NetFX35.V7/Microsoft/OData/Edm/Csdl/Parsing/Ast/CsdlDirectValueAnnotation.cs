using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DA RID: 474
	internal class CsdlDirectValueAnnotation : CsdlElement
	{
		// Token: 0x06000CC3 RID: 3267 RVA: 0x00023C5E File Offset: 0x00021E5E
		public CsdlDirectValueAnnotation(string namespaceName, string name, string value, bool isAttribute, CsdlLocation location)
			: base(location)
		{
			this.namespaceName = namespaceName;
			this.name = name;
			this.value = value;
			this.isAttribute = isAttribute;
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00023C85 File Offset: 0x00021E85
		public string NamespaceName
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00023C8D File Offset: 0x00021E8D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00023C95 File Offset: 0x00021E95
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00023C9D File Offset: 0x00021E9D
		public bool IsAttribute
		{
			get
			{
				return this.isAttribute;
			}
		}

		// Token: 0x040006F2 RID: 1778
		private readonly string namespaceName;

		// Token: 0x040006F3 RID: 1779
		private readonly string name;

		// Token: 0x040006F4 RID: 1780
		private readonly string value;

		// Token: 0x040006F5 RID: 1781
		private readonly bool isAttribute;
	}
}
