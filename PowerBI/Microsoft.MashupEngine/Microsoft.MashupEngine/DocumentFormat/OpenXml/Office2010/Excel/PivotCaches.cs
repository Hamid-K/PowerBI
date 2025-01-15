using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023DA RID: 9178
	[ChildElementInfo(typeof(PivotCache))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class PivotCaches : OpenXmlCompositeElement
	{
		// Token: 0x17004D3B RID: 19771
		// (get) Token: 0x06010AB9 RID: 68281 RVA: 0x002E5D09 File Offset: 0x002E3F09
		public override string LocalName
		{
			get
			{
				return "pivotCaches";
			}
		}

		// Token: 0x17004D3C RID: 19772
		// (get) Token: 0x06010ABA RID: 68282 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D3D RID: 19773
		// (get) Token: 0x06010ABB RID: 68283 RVA: 0x002E5D10 File Offset: 0x002E3F10
		internal override int ElementTypeId
		{
			get
			{
				return 12904;
			}
		}

		// Token: 0x06010ABC RID: 68284 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010ABD RID: 68285 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotCaches()
		{
		}

		// Token: 0x06010ABE RID: 68286 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotCaches(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010ABF RID: 68287 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotCaches(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010AC0 RID: 68288 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotCaches(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010AC1 RID: 68289 RVA: 0x002E5D17 File Offset: 0x002E3F17
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotCache" == name)
			{
				return new PivotCache();
			}
			return null;
		}

		// Token: 0x06010AC2 RID: 68290 RVA: 0x002E5D32 File Offset: 0x002E3F32
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotCaches>(deep);
		}

		// Token: 0x040075D7 RID: 30167
		private const string tagName = "pivotCaches";

		// Token: 0x040075D8 RID: 30168
		private const byte tagNsId = 53;

		// Token: 0x040075D9 RID: 30169
		internal const int ElementTypeIdConst = 12904;
	}
}
