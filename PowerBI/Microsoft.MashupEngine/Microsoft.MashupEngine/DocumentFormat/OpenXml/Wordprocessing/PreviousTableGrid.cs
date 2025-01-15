using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x0200300F RID: 12303
	[ChildElementInfo(typeof(GridColumn))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PreviousTableGrid : OpenXmlCompositeElement
	{
		// Token: 0x17009672 RID: 38514
		// (get) Token: 0x0601ADBC RID: 110012 RVA: 0x0030E22F File Offset: 0x0030C42F
		public override string LocalName
		{
			get
			{
				return "tblGrid";
			}
		}

		// Token: 0x17009673 RID: 38515
		// (get) Token: 0x0601ADBD RID: 110013 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009674 RID: 38516
		// (get) Token: 0x0601ADBE RID: 110014 RVA: 0x00368818 File Offset: 0x00366A18
		internal override int ElementTypeId
		{
			get
			{
				return 12161;
			}
		}

		// Token: 0x0601ADBF RID: 110015 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601ADC0 RID: 110016 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreviousTableGrid()
		{
		}

		// Token: 0x0601ADC1 RID: 110017 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreviousTableGrid(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ADC2 RID: 110018 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreviousTableGrid(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ADC3 RID: 110019 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreviousTableGrid(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601ADC4 RID: 110020 RVA: 0x0036881F File Offset: 0x00366A1F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "gridCol" == name)
			{
				return new GridColumn();
			}
			return null;
		}

		// Token: 0x0601ADC5 RID: 110021 RVA: 0x0036883A File Offset: 0x00366A3A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreviousTableGrid>(deep);
		}

		// Token: 0x0400AE93 RID: 44691
		private const string tagName = "tblGrid";

		// Token: 0x0400AE94 RID: 44692
		private const byte tagNsId = 23;

		// Token: 0x0400AE95 RID: 44693
		internal const int ElementTypeIdConst = 12161;
	}
}
