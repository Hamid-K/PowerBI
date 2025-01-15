using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001B2 RID: 434
	internal class XmlAnnotationInfo
	{
		// Token: 0x06000C2A RID: 3114 RVA: 0x000233EC File Offset: 0x000215EC
		internal XmlAnnotationInfo(CsdlLocation location, string namespaceName, string name, string value, bool isAttribute)
		{
			this.Location = location;
			this.NamespaceName = namespaceName;
			this.Name = name;
			this.Value = value;
			this.IsAttribute = isAttribute;
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x00023419 File Offset: 0x00021619
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x00023421 File Offset: 0x00021621
		internal string NamespaceName { get; private set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002342A File Offset: 0x0002162A
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x00023432 File Offset: 0x00021632
		internal string Name { get; private set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0002343B File Offset: 0x0002163B
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x00023443 File Offset: 0x00021643
		internal CsdlLocation Location { get; private set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002344C File Offset: 0x0002164C
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x00023454 File Offset: 0x00021654
		internal string Value { get; private set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002345D File Offset: 0x0002165D
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x00023465 File Offset: 0x00021665
		internal bool IsAttribute { get; private set; }
	}
}
