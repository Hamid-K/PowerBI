using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CC4 RID: 11460
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CacheSourceExtension))]
	internal class CacheSourceExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17008504 RID: 34052
		// (get) Token: 0x06018878 RID: 100472 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17008505 RID: 34053
		// (get) Token: 0x06018879 RID: 100473 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008506 RID: 34054
		// (get) Token: 0x0601887A RID: 100474 RVA: 0x0034236F File Offset: 0x0034056F
		internal override int ElementTypeId
		{
			get
			{
				return 11440;
			}
		}

		// Token: 0x0601887B RID: 100475 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601887C RID: 100476 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheSourceExtensionList()
		{
		}

		// Token: 0x0601887D RID: 100477 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheSourceExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601887E RID: 100478 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheSourceExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601887F RID: 100479 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheSourceExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018880 RID: 100480 RVA: 0x00342376 File Offset: 0x00340576
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new CacheSourceExtension();
			}
			return null;
		}

		// Token: 0x06018881 RID: 100481 RVA: 0x00342391 File Offset: 0x00340591
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheSourceExtensionList>(deep);
		}

		// Token: 0x0400A0B5 RID: 41141
		private const string tagName = "extLst";

		// Token: 0x0400A0B6 RID: 41142
		private const byte tagNsId = 22;

		// Token: 0x0400A0B7 RID: 41143
		internal const int ElementTypeIdConst = 11440;
	}
}
