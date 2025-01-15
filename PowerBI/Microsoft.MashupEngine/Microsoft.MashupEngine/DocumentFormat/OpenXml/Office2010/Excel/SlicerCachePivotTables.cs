using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002437 RID: 9271
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SlicerCachePivotTable), FileFormatVersions.Office2010)]
	internal class SlicerCachePivotTables : OpenXmlCompositeElement
	{
		// Token: 0x17005027 RID: 20519
		// (get) Token: 0x06011137 RID: 69943 RVA: 0x002EA5A5 File Offset: 0x002E87A5
		public override string LocalName
		{
			get
			{
				return "pivotTables";
			}
		}

		// Token: 0x17005028 RID: 20520
		// (get) Token: 0x06011138 RID: 69944 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005029 RID: 20521
		// (get) Token: 0x06011139 RID: 69945 RVA: 0x002EA5AC File Offset: 0x002E87AC
		internal override int ElementTypeId
		{
			get
			{
				return 12995;
			}
		}

		// Token: 0x0601113A RID: 69946 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601113B RID: 69947 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlicerCachePivotTables()
		{
		}

		// Token: 0x0601113C RID: 69948 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlicerCachePivotTables(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601113D RID: 69949 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlicerCachePivotTables(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601113E RID: 69950 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlicerCachePivotTables(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601113F RID: 69951 RVA: 0x002EA5B3 File Offset: 0x002E87B3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "pivotTable" == name)
			{
				return new SlicerCachePivotTable();
			}
			return null;
		}

		// Token: 0x06011140 RID: 69952 RVA: 0x002EA5CE File Offset: 0x002E87CE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerCachePivotTables>(deep);
		}

		// Token: 0x0400778A RID: 30602
		private const string tagName = "pivotTables";

		// Token: 0x0400778B RID: 30603
		private const byte tagNsId = 53;

		// Token: 0x0400778C RID: 30604
		internal const int ElementTypeIdConst = 12995;
	}
}
