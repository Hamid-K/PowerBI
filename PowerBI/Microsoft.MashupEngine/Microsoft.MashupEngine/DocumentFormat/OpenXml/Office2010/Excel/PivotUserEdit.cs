using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Excel;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002425 RID: 9253
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Formula))]
	[ChildElementInfo(typeof(PivotEditValue), FileFormatVersions.Office2010)]
	internal class PivotUserEdit : OpenXmlCompositeElement
	{
		// Token: 0x17004F7C RID: 20348
		// (get) Token: 0x06010FC0 RID: 69568 RVA: 0x002E956C File Offset: 0x002E776C
		public override string LocalName
		{
			get
			{
				return "userEdit";
			}
		}

		// Token: 0x17004F7D RID: 20349
		// (get) Token: 0x06010FC1 RID: 69569 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F7E RID: 20350
		// (get) Token: 0x06010FC2 RID: 69570 RVA: 0x002E9573 File Offset: 0x002E7773
		internal override int ElementTypeId
		{
			get
			{
				return 12977;
			}
		}

		// Token: 0x06010FC3 RID: 69571 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010FC4 RID: 69572 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotUserEdit()
		{
		}

		// Token: 0x06010FC5 RID: 69573 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotUserEdit(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010FC6 RID: 69574 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotUserEdit(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010FC7 RID: 69575 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotUserEdit(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010FC8 RID: 69576 RVA: 0x002E957A File Offset: 0x002E777A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (32 == namespaceId && "f" == name)
			{
				return new Formula();
			}
			if (53 == namespaceId && "editValue" == name)
			{
				return new PivotEditValue();
			}
			return null;
		}

		// Token: 0x17004F7F RID: 20351
		// (get) Token: 0x06010FC9 RID: 69577 RVA: 0x002E95AD File Offset: 0x002E77AD
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotUserEdit.eleTagNames;
			}
		}

		// Token: 0x17004F80 RID: 20352
		// (get) Token: 0x06010FCA RID: 69578 RVA: 0x002E95B4 File Offset: 0x002E77B4
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotUserEdit.eleNamespaceIds;
			}
		}

		// Token: 0x17004F81 RID: 20353
		// (get) Token: 0x06010FCB RID: 69579 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17004F82 RID: 20354
		// (get) Token: 0x06010FCC RID: 69580 RVA: 0x002E7D10 File Offset: 0x002E5F10
		// (set) Token: 0x06010FCD RID: 69581 RVA: 0x002E7D19 File Offset: 0x002E5F19
		public Formula Formula
		{
			get
			{
				return base.GetElement<Formula>(0);
			}
			set
			{
				base.SetElement<Formula>(0, value);
			}
		}

		// Token: 0x17004F83 RID: 20355
		// (get) Token: 0x06010FCE RID: 69582 RVA: 0x002E95BB File Offset: 0x002E77BB
		// (set) Token: 0x06010FCF RID: 69583 RVA: 0x002E95C4 File Offset: 0x002E77C4
		public PivotEditValue PivotEditValue
		{
			get
			{
				return base.GetElement<PivotEditValue>(1);
			}
			set
			{
				base.SetElement<PivotEditValue>(1, value);
			}
		}

		// Token: 0x06010FD0 RID: 69584 RVA: 0x002E95CE File Offset: 0x002E77CE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotUserEdit>(deep);
		}

		// Token: 0x0400772A RID: 30506
		private const string tagName = "userEdit";

		// Token: 0x0400772B RID: 30507
		private const byte tagNsId = 53;

		// Token: 0x0400772C RID: 30508
		internal const int ElementTypeIdConst = 12977;

		// Token: 0x0400772D RID: 30509
		private static readonly string[] eleTagNames = new string[] { "f", "editValue" };

		// Token: 0x0400772E RID: 30510
		private static readonly byte[] eleNamespaceIds = new byte[] { 32, 53 };
	}
}
