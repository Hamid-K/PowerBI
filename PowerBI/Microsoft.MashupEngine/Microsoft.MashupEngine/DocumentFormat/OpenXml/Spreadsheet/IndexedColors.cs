using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C10 RID: 11280
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColor))]
	internal class IndexedColors : OpenXmlCompositeElement
	{
		// Token: 0x17007FEE RID: 32750
		// (get) Token: 0x06017C6E RID: 97390 RVA: 0x0033B1E8 File Offset: 0x003393E8
		public override string LocalName
		{
			get
			{
				return "indexedColors";
			}
		}

		// Token: 0x17007FEF RID: 32751
		// (get) Token: 0x06017C6F RID: 97391 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007FF0 RID: 32752
		// (get) Token: 0x06017C70 RID: 97392 RVA: 0x0033B1EF File Offset: 0x003393EF
		internal override int ElementTypeId
		{
			get
			{
				return 11261;
			}
		}

		// Token: 0x06017C71 RID: 97393 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017C72 RID: 97394 RVA: 0x00293ECF File Offset: 0x002920CF
		public IndexedColors()
		{
		}

		// Token: 0x06017C73 RID: 97395 RVA: 0x00293ED7 File Offset: 0x002920D7
		public IndexedColors(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C74 RID: 97396 RVA: 0x00293EE0 File Offset: 0x002920E0
		public IndexedColors(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C75 RID: 97397 RVA: 0x00293EE9 File Offset: 0x002920E9
		public IndexedColors(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017C76 RID: 97398 RVA: 0x0033B1F6 File Offset: 0x003393F6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "rgbColor" == name)
			{
				return new RgbColor();
			}
			return null;
		}

		// Token: 0x06017C77 RID: 97399 RVA: 0x0033B211 File Offset: 0x00339411
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IndexedColors>(deep);
		}

		// Token: 0x04009D83 RID: 40323
		private const string tagName = "indexedColors";

		// Token: 0x04009D84 RID: 40324
		private const byte tagNsId = 22;

		// Token: 0x04009D85 RID: 40325
		internal const int ElementTypeIdConst = 11261;
	}
}
