using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001B3 RID: 435
	internal class XmlAttributeInfo
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x0002346E File Offset: 0x0002166E
		internal XmlAttributeInfo(string attrName, string attrValue, CsdlLocation attrLocation)
		{
			this.name = attrName;
			this.attributeValue = attrValue;
			this.location = attrLocation;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00002088 File Offset: 0x00000288
		private XmlAttributeInfo()
		{
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002348B File Offset: 0x0002168B
		internal bool IsMissing
		{
			get
			{
				return XmlAttributeInfo.Missing == this;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00023495 File Offset: 0x00021695
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0002349D File Offset: 0x0002169D
		internal bool IsUsed { get; set; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x000234A6 File Offset: 0x000216A6
		internal CsdlLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x000234AE File Offset: 0x000216AE
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000234B6 File Offset: 0x000216B6
		internal string Value
		{
			get
			{
				return this.attributeValue;
			}
		}

		// Token: 0x040006B6 RID: 1718
		internal static readonly XmlAttributeInfo Missing = new XmlAttributeInfo();

		// Token: 0x040006B7 RID: 1719
		private readonly string name;

		// Token: 0x040006B8 RID: 1720
		private readonly string attributeValue;

		// Token: 0x040006B9 RID: 1721
		private readonly CsdlLocation location;
	}
}
