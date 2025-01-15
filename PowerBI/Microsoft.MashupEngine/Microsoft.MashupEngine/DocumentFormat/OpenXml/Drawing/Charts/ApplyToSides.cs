using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002522 RID: 9506
	[GeneratedCode("DomGen", "2.0")]
	internal class ApplyToSides : BooleanType
	{
		// Token: 0x1700547F RID: 21631
		// (get) Token: 0x06011AF9 RID: 72441 RVA: 0x002F1437 File Offset: 0x002EF637
		public override string LocalName
		{
			get
			{
				return "applyToSides";
			}
		}

		// Token: 0x17005480 RID: 21632
		// (get) Token: 0x06011AFA RID: 72442 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005481 RID: 21633
		// (get) Token: 0x06011AFB RID: 72443 RVA: 0x002F143E File Offset: 0x002EF63E
		internal override int ElementTypeId
		{
			get
			{
				return 10470;
			}
		}

		// Token: 0x06011AFC RID: 72444 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AFE RID: 72446 RVA: 0x002F1445 File Offset: 0x002EF645
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplyToSides>(deep);
		}

		// Token: 0x04007BFA RID: 31738
		private const string tagName = "applyToSides";

		// Token: 0x04007BFB RID: 31739
		private const byte tagNsId = 11;

		// Token: 0x04007BFC RID: 31740
		internal const int ElementTypeIdConst = 10470;
	}
}
