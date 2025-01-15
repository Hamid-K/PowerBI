using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002661 RID: 9825
	[ChildElementInfo(typeof(Connection))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConnectionList : OpenXmlCompositeElement
	{
		// Token: 0x17005BB1 RID: 23473
		// (get) Token: 0x06012B1F RID: 76575 RVA: 0x002FE1FE File Offset: 0x002FC3FE
		public override string LocalName
		{
			get
			{
				return "cxnLst";
			}
		}

		// Token: 0x17005BB2 RID: 23474
		// (get) Token: 0x06012B20 RID: 76576 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BB3 RID: 23475
		// (get) Token: 0x06012B21 RID: 76577 RVA: 0x002FE205 File Offset: 0x002FC405
		internal override int ElementTypeId
		{
			get
			{
				return 10642;
			}
		}

		// Token: 0x06012B22 RID: 76578 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012B23 RID: 76579 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConnectionList()
		{
		}

		// Token: 0x06012B24 RID: 76580 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConnectionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B25 RID: 76581 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConnectionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B26 RID: 76582 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConnectionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012B27 RID: 76583 RVA: 0x002FE20C File Offset: 0x002FC40C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "cxn" == name)
			{
				return new Connection();
			}
			return null;
		}

		// Token: 0x06012B28 RID: 76584 RVA: 0x002FE227 File Offset: 0x002FC427
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectionList>(deep);
		}

		// Token: 0x0400813D RID: 33085
		private const string tagName = "cxnLst";

		// Token: 0x0400813E RID: 33086
		private const byte tagNsId = 14;

		// Token: 0x0400813F RID: 33087
		internal const int ElementTypeIdConst = 10642;
	}
}
