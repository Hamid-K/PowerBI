using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DDA RID: 11738
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowXmlTags : OnOffType
	{
		// Token: 0x1700882A RID: 34858
		// (get) Token: 0x06018F14 RID: 102164 RVA: 0x003453A3 File Offset: 0x003435A3
		public override string LocalName
		{
			get
			{
				return "showXMLTags";
			}
		}

		// Token: 0x1700882B RID: 34859
		// (get) Token: 0x06018F15 RID: 102165 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700882C RID: 34860
		// (get) Token: 0x06018F16 RID: 102166 RVA: 0x003453AA File Offset: 0x003435AA
		internal override int ElementTypeId
		{
			get
			{
				return 12032;
			}
		}

		// Token: 0x06018F17 RID: 102167 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F19 RID: 102169 RVA: 0x003453B1 File Offset: 0x003435B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowXmlTags>(deep);
		}

		// Token: 0x0400A5F9 RID: 42489
		private const string tagName = "showXMLTags";

		// Token: 0x0400A5FA RID: 42490
		private const byte tagNsId = 23;

		// Token: 0x0400A5FB RID: 42491
		internal const int ElementTypeIdConst = 12032;
	}
}
