using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002461 RID: 9313
	[GeneratedCode("DomGen", "2.0")]
	internal class MacroKeyboardCustomization : MacroWllType
	{
		// Token: 0x170050B7 RID: 20663
		// (get) Token: 0x06011294 RID: 70292 RVA: 0x002EB2C8 File Offset: 0x002E94C8
		public override string LocalName
		{
			get
			{
				return "macro";
			}
		}

		// Token: 0x170050B8 RID: 20664
		// (get) Token: 0x06011295 RID: 70293 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050B9 RID: 20665
		// (get) Token: 0x06011296 RID: 70294 RVA: 0x002EB2CF File Offset: 0x002E94CF
		internal override int ElementTypeId
		{
			get
			{
				return 12542;
			}
		}

		// Token: 0x06011297 RID: 70295 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011299 RID: 70297 RVA: 0x002EB2DE File Offset: 0x002E94DE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MacroKeyboardCustomization>(deep);
		}

		// Token: 0x0400786E RID: 30830
		private const string tagName = "macro";

		// Token: 0x0400786F RID: 30831
		private const byte tagNsId = 33;

		// Token: 0x04007870 RID: 30832
		internal const int ElementTypeIdConst = 12542;
	}
}
