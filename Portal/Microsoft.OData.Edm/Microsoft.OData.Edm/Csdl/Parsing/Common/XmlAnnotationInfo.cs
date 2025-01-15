using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001BF RID: 447
	internal class XmlAnnotationInfo
	{
		// Token: 0x06000CDC RID: 3292 RVA: 0x000255B4 File Offset: 0x000237B4
		internal XmlAnnotationInfo(CsdlLocation location, string namespaceName, string name, string value, bool isAttribute)
		{
			this.Location = location;
			this.NamespaceName = namespaceName;
			this.Name = name;
			this.Value = value;
			this.IsAttribute = isAttribute;
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x000255E1 File Offset: 0x000237E1
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x000255E9 File Offset: 0x000237E9
		internal string NamespaceName { get; private set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x000255F2 File Offset: 0x000237F2
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x000255FA File Offset: 0x000237FA
		internal string Name { get; private set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00025603 File Offset: 0x00023803
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x0002560B File Offset: 0x0002380B
		internal CsdlLocation Location { get; private set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00025614 File Offset: 0x00023814
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x0002561C File Offset: 0x0002381C
		internal string Value { get; private set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00025625 File Offset: 0x00023825
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x0002562D File Offset: 0x0002382D
		internal bool IsAttribute { get; private set; }
	}
}
