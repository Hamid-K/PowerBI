using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BC2 RID: 11202
	[GeneratedCode("DomGen", "2.0")]
	internal class OldCell : CellType
	{
		// Token: 0x17007C8B RID: 31883
		// (get) Token: 0x06017534 RID: 95540 RVA: 0x00335766 File Offset: 0x00333966
		public override string LocalName
		{
			get
			{
				return "oc";
			}
		}

		// Token: 0x17007C8C RID: 31884
		// (get) Token: 0x06017535 RID: 95541 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C8D RID: 31885
		// (get) Token: 0x06017536 RID: 95542 RVA: 0x0033576D File Offset: 0x0033396D
		internal override int ElementTypeId
		{
			get
			{
				return 11172;
			}
		}

		// Token: 0x06017537 RID: 95543 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017538 RID: 95544 RVA: 0x00335774 File Offset: 0x00333974
		public OldCell()
		{
		}

		// Token: 0x06017539 RID: 95545 RVA: 0x0033577C File Offset: 0x0033397C
		public OldCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601753A RID: 95546 RVA: 0x00335785 File Offset: 0x00333985
		public OldCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601753B RID: 95547 RVA: 0x0033578E File Offset: 0x0033398E
		public OldCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601753C RID: 95548 RVA: 0x00335797 File Offset: 0x00333997
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OldCell>(deep);
		}

		// Token: 0x04009BF2 RID: 39922
		private const string tagName = "oc";

		// Token: 0x04009BF3 RID: 39923
		private const byte tagNsId = 22;

		// Token: 0x04009BF4 RID: 39924
		internal const int ElementTypeIdConst = 11172;
	}
}
