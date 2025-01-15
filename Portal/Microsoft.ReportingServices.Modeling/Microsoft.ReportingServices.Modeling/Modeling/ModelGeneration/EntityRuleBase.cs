using System;
using System.Security.Permissions;
using System.Xml.XPath;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000D1 RID: 209
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class EntityRuleBase : ProcessingRule
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00026759 File Offset: 0x00024959
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x00026761 File Offset: 0x00024961
		public IXPathNavigable EntityFragment
		{
			get
			{
				return this.m_entityFragment;
			}
			set
			{
				this.m_entityFragment = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002676A File Offset: 0x0002496A
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x00026772 File Offset: 0x00024972
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

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002677C File Offset: 0x0002497C
		internal override bool LoadXmlElement(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.NamespaceURI == "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling")
			{
				string localName = xr.LocalName;
				if (localName == "Entity")
				{
					this.m_entityFragment = xr.ReadFragment();
					return true;
				}
				if (localName == "EntityFolder")
				{
					this.m_folderFragment = xr.ReadFragment();
					return true;
				}
			}
			return base.LoadXmlElement(xr, objectFactory);
		}

		// Token: 0x040004BA RID: 1210
		private IXPathNavigable m_entityFragment;

		// Token: 0x040004BB RID: 1211
		private IXPathNavigable m_folderFragment;
	}
}
