using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Common
{
	// Token: 0x02000155 RID: 341
	internal class XmlAnnotationInfo
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x00011120 File Offset: 0x0000F320
		internal XmlAnnotationInfo(CsdlLocation location, string namespaceName, string name, string value, bool isAttribute)
		{
			this.Location = location;
			this.NamespaceName = namespaceName;
			this.Name = name;
			this.Value = value;
			this.IsAttribute = isAttribute;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001114D File Offset: 0x0000F34D
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00011155 File Offset: 0x0000F355
		internal string NamespaceName { get; private set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001115E File Offset: 0x0000F35E
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00011166 File Offset: 0x0000F366
		internal string Name { get; private set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001116F File Offset: 0x0000F36F
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00011177 File Offset: 0x0000F377
		internal CsdlLocation Location { get; private set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00011180 File Offset: 0x0000F380
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00011188 File Offset: 0x0000F388
		internal string Value { get; private set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00011191 File Offset: 0x0000F391
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00011199 File Offset: 0x0000F399
		internal bool IsAttribute { get; private set; }
	}
}
