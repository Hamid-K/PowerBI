using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002414 RID: 9236
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TupleSetRow), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TupleSetRows : OpenXmlCompositeElement
	{
		// Token: 0x17004F05 RID: 20229
		// (get) Token: 0x06010EA5 RID: 69285 RVA: 0x002E89AD File Offset: 0x002E6BAD
		public override string LocalName
		{
			get
			{
				return "rows";
			}
		}

		// Token: 0x17004F06 RID: 20230
		// (get) Token: 0x06010EA6 RID: 69286 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F07 RID: 20231
		// (get) Token: 0x06010EA7 RID: 69287 RVA: 0x002E89B4 File Offset: 0x002E6BB4
		internal override int ElementTypeId
		{
			get
			{
				return 12954;
			}
		}

		// Token: 0x06010EA8 RID: 69288 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010EA9 RID: 69289 RVA: 0x00293ECF File Offset: 0x002920CF
		public TupleSetRows()
		{
		}

		// Token: 0x06010EAA RID: 69290 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TupleSetRows(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010EAB RID: 69291 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TupleSetRows(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010EAC RID: 69292 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TupleSetRows(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010EAD RID: 69293 RVA: 0x002E89BB File Offset: 0x002E6BBB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "row" == name)
			{
				return new TupleSetRow();
			}
			return null;
		}

		// Token: 0x06010EAE RID: 69294 RVA: 0x002E89D6 File Offset: 0x002E6BD6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TupleSetRows>(deep);
		}

		// Token: 0x040076DD RID: 30429
		private const string tagName = "rows";

		// Token: 0x040076DE RID: 30430
		private const byte tagNsId = 53;

		// Token: 0x040076DF RID: 30431
		internal const int ElementTypeIdConst = 12954;
	}
}
