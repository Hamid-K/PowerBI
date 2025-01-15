using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027D6 RID: 10198
	[ChildElementInfo(typeof(ConnectionSite))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConnectionSiteList : OpenXmlCompositeElement
	{
		// Token: 0x170063E4 RID: 25572
		// (get) Token: 0x06013D5D RID: 81245 RVA: 0x002FE1FE File Offset: 0x002FC3FE
		public override string LocalName
		{
			get
			{
				return "cxnLst";
			}
		}

		// Token: 0x170063E5 RID: 25573
		// (get) Token: 0x06013D5E RID: 81246 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063E6 RID: 25574
		// (get) Token: 0x06013D5F RID: 81247 RVA: 0x0030C23D File Offset: 0x0030A43D
		internal override int ElementTypeId
		{
			get
			{
				return 10231;
			}
		}

		// Token: 0x06013D60 RID: 81248 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013D61 RID: 81249 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConnectionSiteList()
		{
		}

		// Token: 0x06013D62 RID: 81250 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConnectionSiteList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D63 RID: 81251 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConnectionSiteList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D64 RID: 81252 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConnectionSiteList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013D65 RID: 81253 RVA: 0x0030C244 File Offset: 0x0030A444
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "cxn" == name)
			{
				return new ConnectionSite();
			}
			return null;
		}

		// Token: 0x06013D66 RID: 81254 RVA: 0x0030C25F File Offset: 0x0030A45F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectionSiteList>(deep);
		}

		// Token: 0x040087FF RID: 34815
		private const string tagName = "cxnLst";

		// Token: 0x04008800 RID: 34816
		private const byte tagNsId = 10;

		// Token: 0x04008801 RID: 34817
		internal const int ElementTypeIdConst = 10231;
	}
}
