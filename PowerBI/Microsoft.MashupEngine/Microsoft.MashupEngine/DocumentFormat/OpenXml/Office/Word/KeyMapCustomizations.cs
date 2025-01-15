using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200247B RID: 9339
	[GeneratedCode("DomGen", "2.0")]
	internal class KeyMapCustomizations : KeymapsType
	{
		// Token: 0x17005134 RID: 20788
		// (get) Token: 0x060113B5 RID: 70581 RVA: 0x002EBEDB File Offset: 0x002EA0DB
		public override string LocalName
		{
			get
			{
				return "keymaps";
			}
		}

		// Token: 0x17005135 RID: 20789
		// (get) Token: 0x060113B6 RID: 70582 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005136 RID: 20790
		// (get) Token: 0x060113B7 RID: 70583 RVA: 0x002EBEE2 File Offset: 0x002EA0E2
		internal override int ElementTypeId
		{
			get
			{
				return 12566;
			}
		}

		// Token: 0x060113B8 RID: 70584 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060113B9 RID: 70585 RVA: 0x002EBEE9 File Offset: 0x002EA0E9
		public KeyMapCustomizations()
		{
		}

		// Token: 0x060113BA RID: 70586 RVA: 0x002EBEF1 File Offset: 0x002EA0F1
		public KeyMapCustomizations(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113BB RID: 70587 RVA: 0x002EBEFA File Offset: 0x002EA0FA
		public KeyMapCustomizations(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113BC RID: 70588 RVA: 0x002EBF03 File Offset: 0x002EA103
		public KeyMapCustomizations(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060113BD RID: 70589 RVA: 0x002EBF0C File Offset: 0x002EA10C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<KeyMapCustomizations>(deep);
		}

		// Token: 0x040078C6 RID: 30918
		private const string tagName = "keymaps";

		// Token: 0x040078C7 RID: 30919
		private const byte tagNsId = 33;

		// Token: 0x040078C8 RID: 30920
		internal const int ElementTypeIdConst = 12566;
	}
}
