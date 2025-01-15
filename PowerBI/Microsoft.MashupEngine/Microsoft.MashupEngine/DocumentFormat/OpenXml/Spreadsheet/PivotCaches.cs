using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C5C RID: 11356
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotCache))]
	internal class PivotCaches : OpenXmlCompositeElement
	{
		// Token: 0x17008264 RID: 33380
		// (get) Token: 0x06018204 RID: 98820 RVA: 0x002E5D09 File Offset: 0x002E3F09
		public override string LocalName
		{
			get
			{
				return "pivotCaches";
			}
		}

		// Token: 0x17008265 RID: 33381
		// (get) Token: 0x06018205 RID: 98821 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008266 RID: 33382
		// (get) Token: 0x06018206 RID: 98822 RVA: 0x0033EC0D File Offset: 0x0033CE0D
		internal override int ElementTypeId
		{
			get
			{
				return 11337;
			}
		}

		// Token: 0x06018207 RID: 98823 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018208 RID: 98824 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotCaches()
		{
		}

		// Token: 0x06018209 RID: 98825 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotCaches(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601820A RID: 98826 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotCaches(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601820B RID: 98827 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotCaches(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601820C RID: 98828 RVA: 0x002E5D17 File Offset: 0x002E3F17
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotCache" == name)
			{
				return new PivotCache();
			}
			return null;
		}

		// Token: 0x0601820D RID: 98829 RVA: 0x0033EC14 File Offset: 0x0033CE14
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotCaches>(deep);
		}

		// Token: 0x04009EF3 RID: 40691
		private const string tagName = "pivotCaches";

		// Token: 0x04009EF4 RID: 40692
		private const byte tagNsId = 22;

		// Token: 0x04009EF5 RID: 40693
		internal const int ElementTypeIdConst = 11337;
	}
}
