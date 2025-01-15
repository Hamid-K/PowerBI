using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001C0 RID: 448
	internal class XmlAttributeInfo
	{
		// Token: 0x06000CE7 RID: 3303 RVA: 0x00025636 File Offset: 0x00023836
		internal XmlAttributeInfo(string attrName, string attrValue, CsdlLocation attrLocation)
		{
			this.name = attrName;
			this.attributeValue = attrValue;
			this.location = attrLocation;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00002050 File Offset: 0x00000250
		private XmlAttributeInfo()
		{
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00025653 File Offset: 0x00023853
		internal bool IsMissing
		{
			get
			{
				return XmlAttributeInfo.Missing == this;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0002565D File Offset: 0x0002385D
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00025665 File Offset: 0x00023865
		internal bool IsUsed { get; set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0002566E File Offset: 0x0002386E
		internal CsdlLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00025676 File Offset: 0x00023876
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0002567E File Offset: 0x0002387E
		internal string Value
		{
			get
			{
				return this.attributeValue;
			}
		}

		// Token: 0x0400072F RID: 1839
		internal static readonly XmlAttributeInfo Missing = new XmlAttributeInfo();

		// Token: 0x04000730 RID: 1840
		private readonly string name;

		// Token: 0x04000731 RID: 1841
		private readonly string attributeValue;

		// Token: 0x04000732 RID: 1842
		private readonly CsdlLocation location;
	}
}
