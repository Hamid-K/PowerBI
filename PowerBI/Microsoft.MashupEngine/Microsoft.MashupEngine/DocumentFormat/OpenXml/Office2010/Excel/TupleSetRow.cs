using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002416 RID: 9238
	[ChildElementInfo(typeof(TupleSetRowItem), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TupleSetRow : OpenXmlCompositeElement
	{
		// Token: 0x17004F0F RID: 20239
		// (get) Token: 0x06010EBD RID: 69309 RVA: 0x002E3583 File Offset: 0x002E1783
		public override string LocalName
		{
			get
			{
				return "row";
			}
		}

		// Token: 0x17004F10 RID: 20240
		// (get) Token: 0x06010EBE RID: 69310 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F11 RID: 20241
		// (get) Token: 0x06010EBF RID: 69311 RVA: 0x002E8A6B File Offset: 0x002E6C6B
		internal override int ElementTypeId
		{
			get
			{
				return 12956;
			}
		}

		// Token: 0x06010EC0 RID: 69312 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010EC1 RID: 69313 RVA: 0x00293ECF File Offset: 0x002920CF
		public TupleSetRow()
		{
		}

		// Token: 0x06010EC2 RID: 69314 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TupleSetRow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010EC3 RID: 69315 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TupleSetRow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010EC4 RID: 69316 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TupleSetRow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010EC5 RID: 69317 RVA: 0x002E8A72 File Offset: 0x002E6C72
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "rowItem" == name)
			{
				return new TupleSetRowItem();
			}
			return null;
		}

		// Token: 0x06010EC6 RID: 69318 RVA: 0x002E8A8D File Offset: 0x002E6C8D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TupleSetRow>(deep);
		}

		// Token: 0x040076E5 RID: 30437
		private const string tagName = "row";

		// Token: 0x040076E6 RID: 30438
		private const byte tagNsId = 53;

		// Token: 0x040076E7 RID: 30439
		internal const int ElementTypeIdConst = 12956;
	}
}
