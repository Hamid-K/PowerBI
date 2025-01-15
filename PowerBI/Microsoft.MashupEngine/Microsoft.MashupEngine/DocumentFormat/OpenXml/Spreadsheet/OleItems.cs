using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C25 RID: 11301
	[ChildElementInfo(typeof(OleItem), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OleItem))]
	internal class OleItems : OpenXmlCompositeElement
	{
		// Token: 0x1700808F RID: 32911
		// (get) Token: 0x06017DDD RID: 97757 RVA: 0x0033BFA6 File Offset: 0x0033A1A6
		public override string LocalName
		{
			get
			{
				return "oleItems";
			}
		}

		// Token: 0x17008090 RID: 32912
		// (get) Token: 0x06017DDE RID: 97758 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008091 RID: 32913
		// (get) Token: 0x06017DDF RID: 97759 RVA: 0x0033BFAD File Offset: 0x0033A1AD
		internal override int ElementTypeId
		{
			get
			{
				return 11282;
			}
		}

		// Token: 0x06017DE0 RID: 97760 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017DE1 RID: 97761 RVA: 0x00293ECF File Offset: 0x002920CF
		public OleItems()
		{
		}

		// Token: 0x06017DE2 RID: 97762 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OleItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017DE3 RID: 97763 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OleItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017DE4 RID: 97764 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OleItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017DE5 RID: 97765 RVA: 0x0033BFB4 File Offset: 0x0033A1B4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "oleItem" == name)
			{
				return new OleItem();
			}
			if (53 == namespaceId && "oleItem" == name)
			{
				return new OleItem();
			}
			return null;
		}

		// Token: 0x06017DE6 RID: 97766 RVA: 0x0033BFE7 File Offset: 0x0033A1E7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleItems>(deep);
		}

		// Token: 0x04009DEA RID: 40426
		private const string tagName = "oleItems";

		// Token: 0x04009DEB RID: 40427
		private const byte tagNsId = 22;

		// Token: 0x04009DEC RID: 40428
		internal const int ElementTypeIdConst = 11282;
	}
}
