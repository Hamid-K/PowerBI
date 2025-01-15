using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F64 RID: 12132
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GridColumn))]
	[ChildElementInfo(typeof(TableGridChange))]
	internal class TableGrid : OpenXmlCompositeElement
	{
		// Token: 0x170090B2 RID: 37042
		// (get) Token: 0x0601A17B RID: 106875 RVA: 0x0030E22F File Offset: 0x0030C42F
		public override string LocalName
		{
			get
			{
				return "tblGrid";
			}
		}

		// Token: 0x170090B3 RID: 37043
		// (get) Token: 0x0601A17C RID: 106876 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090B4 RID: 37044
		// (get) Token: 0x0601A17D RID: 106877 RVA: 0x0035D675 File Offset: 0x0035B875
		internal override int ElementTypeId
		{
			get
			{
				return 11790;
			}
		}

		// Token: 0x0601A17E RID: 106878 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A17F RID: 106879 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableGrid()
		{
		}

		// Token: 0x0601A180 RID: 106880 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableGrid(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A181 RID: 106881 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableGrid(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A182 RID: 106882 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableGrid(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A183 RID: 106883 RVA: 0x0035D67C File Offset: 0x0035B87C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "gridCol" == name)
			{
				return new GridColumn();
			}
			if (23 == namespaceId && "tblGridChange" == name)
			{
				return new TableGridChange();
			}
			return null;
		}

		// Token: 0x0601A184 RID: 106884 RVA: 0x0035D6AF File Offset: 0x0035B8AF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableGrid>(deep);
		}

		// Token: 0x0400ABB9 RID: 43961
		private const string tagName = "tblGrid";

		// Token: 0x0400ABBA RID: 43962
		private const byte tagNsId = 23;

		// Token: 0x0400ABBB RID: 43963
		internal const int ElementTypeIdConst = 11790;
	}
}
