using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200247C RID: 9340
	[GeneratedCode("DomGen", "2.0")]
	internal class MismatchedKeyMapCustomization : KeymapsType
	{
		// Token: 0x17005137 RID: 20791
		// (get) Token: 0x060113BE RID: 70590 RVA: 0x002EBF15 File Offset: 0x002EA115
		public override string LocalName
		{
			get
			{
				return "keymapsBad";
			}
		}

		// Token: 0x17005138 RID: 20792
		// (get) Token: 0x060113BF RID: 70591 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005139 RID: 20793
		// (get) Token: 0x060113C0 RID: 70592 RVA: 0x002EBF1C File Offset: 0x002EA11C
		internal override int ElementTypeId
		{
			get
			{
				return 12567;
			}
		}

		// Token: 0x060113C1 RID: 70593 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060113C2 RID: 70594 RVA: 0x002EBEE9 File Offset: 0x002EA0E9
		public MismatchedKeyMapCustomization()
		{
		}

		// Token: 0x060113C3 RID: 70595 RVA: 0x002EBEF1 File Offset: 0x002EA0F1
		public MismatchedKeyMapCustomization(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113C4 RID: 70596 RVA: 0x002EBEFA File Offset: 0x002EA0FA
		public MismatchedKeyMapCustomization(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113C5 RID: 70597 RVA: 0x002EBF03 File Offset: 0x002EA103
		public MismatchedKeyMapCustomization(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060113C6 RID: 70598 RVA: 0x002EBF23 File Offset: 0x002EA123
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MismatchedKeyMapCustomization>(deep);
		}

		// Token: 0x040078C9 RID: 30921
		private const string tagName = "keymapsBad";

		// Token: 0x040078CA RID: 30922
		private const byte tagNsId = 33;

		// Token: 0x040078CB RID: 30923
		internal const int ElementTypeIdConst = 12567;
	}
}
