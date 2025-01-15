using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027F5 RID: 10229
	[ChildElementInfo(typeof(GridColumn))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableGrid : OpenXmlCompositeElement
	{
		// Token: 0x170064F3 RID: 25843
		// (get) Token: 0x06013FBC RID: 81852 RVA: 0x0030E22F File Offset: 0x0030C42F
		public override string LocalName
		{
			get
			{
				return "tblGrid";
			}
		}

		// Token: 0x170064F4 RID: 25844
		// (get) Token: 0x06013FBD RID: 81853 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170064F5 RID: 25845
		// (get) Token: 0x06013FBE RID: 81854 RVA: 0x0030E236 File Offset: 0x0030C436
		internal override int ElementTypeId
		{
			get
			{
				return 10265;
			}
		}

		// Token: 0x06013FBF RID: 81855 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013FC0 RID: 81856 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableGrid()
		{
		}

		// Token: 0x06013FC1 RID: 81857 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableGrid(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FC2 RID: 81858 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableGrid(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FC3 RID: 81859 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableGrid(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013FC4 RID: 81860 RVA: 0x0030E23D File Offset: 0x0030C43D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "gridCol" == name)
			{
				return new GridColumn();
			}
			return null;
		}

		// Token: 0x06013FC5 RID: 81861 RVA: 0x0030E258 File Offset: 0x0030C458
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableGrid>(deep);
		}

		// Token: 0x04008882 RID: 34946
		private const string tagName = "tblGrid";

		// Token: 0x04008883 RID: 34947
		private const byte tagNsId = 10;

		// Token: 0x04008884 RID: 34948
		internal const int ElementTypeIdConst = 10265;
	}
}
