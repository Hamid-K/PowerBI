using System;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x0200038F RID: 911
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class XmlParentElementAttribute : Attribute
	{
		// Token: 0x06001E16 RID: 7702 RVA: 0x0007B163 File Offset: 0x00079363
		public XmlParentElementAttribute(string parentElmentName)
		{
			this.m_parentElementName = parentElmentName;
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x0007B172 File Offset: 0x00079372
		public string ParentElementName
		{
			get
			{
				return this.m_parentElementName;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0007B17A File Offset: 0x0007937A
		// (set) Token: 0x06001E19 RID: 7705 RVA: 0x0007B182 File Offset: 0x00079382
		public string XmlNameSpace
		{
			get
			{
				return this.m_elementNameSpace;
			}
			set
			{
				this.m_elementNameSpace = value;
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0007B18C File Offset: 0x0007938C
		public override bool Equals(object obj)
		{
			XmlParentElementAttribute xmlParentElementAttribute = obj as XmlParentElementAttribute;
			return xmlParentElementAttribute != null && this.ParentElementName == xmlParentElementAttribute.ParentElementName && this.XmlNameSpace == xmlParentElementAttribute.XmlNameSpace;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0007B1CC File Offset: 0x000793CC
		public override int GetHashCode()
		{
			int num = 0;
			if (this.m_parentElementName != null)
			{
				num ^= this.m_parentElementName.GetHashCode();
			}
			if (this.m_elementNameSpace != null)
			{
				num ^= this.m_elementNameSpace.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000CC0 RID: 3264
		private string m_parentElementName;

		// Token: 0x04000CC1 RID: 3265
		private string m_elementNameSpace;
	}
}
