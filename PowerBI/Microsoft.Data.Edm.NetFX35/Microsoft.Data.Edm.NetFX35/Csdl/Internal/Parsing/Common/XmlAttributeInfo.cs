using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Common
{
	// Token: 0x02000156 RID: 342
	internal class XmlAttributeInfo
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x000111A2 File Offset: 0x0000F3A2
		internal XmlAttributeInfo(string attrName, string attrValue, CsdlLocation attrLocation)
		{
			this.name = attrName;
			this.attributeValue = attrValue;
			this.location = attrLocation;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000111BF File Offset: 0x0000F3BF
		private XmlAttributeInfo()
		{
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x000111C7 File Offset: 0x0000F3C7
		internal bool IsMissing
		{
			get
			{
				return object.ReferenceEquals(XmlAttributeInfo.Missing, this);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x000111D4 File Offset: 0x0000F3D4
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x000111DC File Offset: 0x0000F3DC
		internal bool IsUsed { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x000111E5 File Offset: 0x0000F3E5
		internal CsdlLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x000111ED File Offset: 0x0000F3ED
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x000111F5 File Offset: 0x0000F3F5
		internal string Value
		{
			get
			{
				return this.attributeValue;
			}
		}

		// Token: 0x0400037E RID: 894
		internal static readonly XmlAttributeInfo Missing = new XmlAttributeInfo();

		// Token: 0x0400037F RID: 895
		private readonly string name;

		// Token: 0x04000380 RID: 896
		private readonly string attributeValue;

		// Token: 0x04000381 RID: 897
		private readonly CsdlLocation location;
	}
}
