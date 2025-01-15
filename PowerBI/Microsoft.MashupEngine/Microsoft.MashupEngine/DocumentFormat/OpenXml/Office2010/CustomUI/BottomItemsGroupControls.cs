using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x02002300 RID: 8960
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BottomItemsGroupControls : GroupControlsType
	{
		// Token: 0x1700475F RID: 18271
		// (get) Token: 0x0600FDDD RID: 64989 RVA: 0x002DCAE4 File Offset: 0x002DACE4
		public override string LocalName
		{
			get
			{
				return "bottomItems";
			}
		}

		// Token: 0x17004760 RID: 18272
		// (get) Token: 0x0600FDDE RID: 64990 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004761 RID: 18273
		// (get) Token: 0x0600FDDF RID: 64991 RVA: 0x002DCAEB File Offset: 0x002DACEB
		internal override int ElementTypeId
		{
			get
			{
				return 13102;
			}
		}

		// Token: 0x0600FDE0 RID: 64992 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FDE1 RID: 64993 RVA: 0x002DCAB8 File Offset: 0x002DACB8
		public BottomItemsGroupControls()
		{
		}

		// Token: 0x0600FDE2 RID: 64994 RVA: 0x002DCAC0 File Offset: 0x002DACC0
		public BottomItemsGroupControls(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FDE3 RID: 64995 RVA: 0x002DCAC9 File Offset: 0x002DACC9
		public BottomItemsGroupControls(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FDE4 RID: 64996 RVA: 0x002DCAD2 File Offset: 0x002DACD2
		public BottomItemsGroupControls(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FDE5 RID: 64997 RVA: 0x002DCAF2 File Offset: 0x002DACF2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BottomItemsGroupControls>(deep);
		}

		// Token: 0x04007214 RID: 29204
		private const string tagName = "bottomItems";

		// Token: 0x04007215 RID: 29205
		private const byte tagNsId = 57;

		// Token: 0x04007216 RID: 29206
		internal const int ElementTypeIdConst = 13102;
	}
}
