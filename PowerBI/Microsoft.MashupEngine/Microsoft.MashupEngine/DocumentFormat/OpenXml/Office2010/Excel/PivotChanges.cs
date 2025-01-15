using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002421 RID: 9249
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PivotChange), FileFormatVersions.Office2010)]
	internal class PivotChanges : OpenXmlCompositeElement
	{
		// Token: 0x17004F63 RID: 20323
		// (get) Token: 0x06010F80 RID: 69504 RVA: 0x002E9325 File Offset: 0x002E7525
		public override string LocalName
		{
			get
			{
				return "pivotChanges";
			}
		}

		// Token: 0x17004F64 RID: 20324
		// (get) Token: 0x06010F81 RID: 69505 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F65 RID: 20325
		// (get) Token: 0x06010F82 RID: 69506 RVA: 0x002E932C File Offset: 0x002E752C
		internal override int ElementTypeId
		{
			get
			{
				return 12973;
			}
		}

		// Token: 0x06010F83 RID: 69507 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010F84 RID: 69508 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotChanges()
		{
		}

		// Token: 0x06010F85 RID: 69509 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotChanges(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F86 RID: 69510 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotChanges(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F87 RID: 69511 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotChanges(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010F88 RID: 69512 RVA: 0x002E9333 File Offset: 0x002E7533
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "pivotChange" == name)
			{
				return new PivotChange();
			}
			return null;
		}

		// Token: 0x06010F89 RID: 69513 RVA: 0x002E934E File Offset: 0x002E754E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotChanges>(deep);
		}

		// Token: 0x04007718 RID: 30488
		private const string tagName = "pivotChanges";

		// Token: 0x04007719 RID: 30489
		private const byte tagNsId = 53;

		// Token: 0x0400771A RID: 30490
		internal const int ElementTypeIdConst = 12973;
	}
}
