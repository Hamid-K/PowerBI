using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x02000197 RID: 407
	internal class XmlAttributeInfo
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x00013576 File Offset: 0x00011776
		internal XmlAttributeInfo(string attrName, string attrValue, CsdlLocation attrLocation)
		{
			this.name = attrName;
			this.attributeValue = attrValue;
			this.location = attrLocation;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00013593 File Offset: 0x00011793
		private XmlAttributeInfo()
		{
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001359B File Offset: 0x0001179B
		internal bool IsMissing
		{
			get
			{
				return object.ReferenceEquals(XmlAttributeInfo.Missing, this);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x000135A8 File Offset: 0x000117A8
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x000135B0 File Offset: 0x000117B0
		internal bool IsUsed { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x000135B9 File Offset: 0x000117B9
		internal CsdlLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000135C1 File Offset: 0x000117C1
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x000135C9 File Offset: 0x000117C9
		internal string Value
		{
			get
			{
				return this.attributeValue;
			}
		}

		// Token: 0x0400040E RID: 1038
		internal static readonly XmlAttributeInfo Missing = new XmlAttributeInfo();

		// Token: 0x0400040F RID: 1039
		private readonly string name;

		// Token: 0x04000410 RID: 1040
		private readonly string attributeValue;

		// Token: 0x04000411 RID: 1041
		private readonly CsdlLocation location;
	}
}
