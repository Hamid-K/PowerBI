using System;
using System.Security.Permissions;
using System.Xml.XPath;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000C8 RID: 200
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class AttributeRuleBase : ProcessingRule
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00025351 File Offset: 0x00023551
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00025359 File Offset: 0x00023559
		public IXPathNavigable AttributeFragment
		{
			get
			{
				return this.m_attributeFragment;
			}
			set
			{
				this.m_attributeFragment = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00025362 File Offset: 0x00023562
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x0002536A File Offset: 0x0002356A
		public IXPathNavigable FolderFragment
		{
			get
			{
				return this.m_folderFragment;
			}
			set
			{
				this.m_folderFragment = value;
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00025374 File Offset: 0x00023574
		internal override bool LoadXmlElement(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.NamespaceURI == "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling")
			{
				string localName = xr.LocalName;
				if (localName == "Attribute")
				{
					this.m_attributeFragment = xr.ReadFragment();
					return true;
				}
				if (localName == "FieldFolder")
				{
					this.m_folderFragment = xr.ReadFragment();
					return true;
				}
			}
			return base.LoadXmlElement(xr, objectFactory);
		}

		// Token: 0x040004A6 RID: 1190
		private IXPathNavigable m_attributeFragment;

		// Token: 0x040004A7 RID: 1191
		private IXPathNavigable m_folderFragment;
	}
}
