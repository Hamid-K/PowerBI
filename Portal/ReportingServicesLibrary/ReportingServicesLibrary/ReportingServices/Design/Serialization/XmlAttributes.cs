using System;
using System.Reflection;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x02000397 RID: 919
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class XmlAttributes : XmlAttributes
	{
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0007C4CF File Offset: 0x0007A6CF
		// (set) Token: 0x06001E4D RID: 7757 RVA: 0x0007C4D7 File Offset: 0x0007A6D7
		public XmlParentElementAttribute XmlParentElement
		{
			get
			{
				return this.m_xmlParentElement;
			}
			set
			{
				this.m_xmlParentElement = value;
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0007C4E0 File Offset: 0x0007A6E0
		public XmlAttributes()
		{
			base.XmlDefaultValue = null;
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0007C4F0 File Offset: 0x0007A6F0
		public XmlAttributes(ICustomAttributeProvider provider)
			: base(provider)
		{
			object[] customAttributes = provider.GetCustomAttributes(typeof(XmlParentElementAttribute), false);
			if (customAttributes.Length != 0)
			{
				this.m_xmlParentElement = (XmlParentElementAttribute)customAttributes[0];
			}
		}

		// Token: 0x04000CD7 RID: 3287
		private XmlParentElementAttribute m_xmlParentElement;
	}
}
