using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200298D RID: 10637
	[GeneratedCode("DomGen", "2.0")]
	internal class SeparatorChar : CharType
	{
		// Token: 0x17006CBD RID: 27837
		// (get) Token: 0x0601520D RID: 86541 RVA: 0x0031B992 File Offset: 0x00319B92
		public override string LocalName
		{
			get
			{
				return "sepChr";
			}
		}

		// Token: 0x17006CBE RID: 27838
		// (get) Token: 0x0601520E RID: 86542 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CBF RID: 27839
		// (get) Token: 0x0601520F RID: 86543 RVA: 0x0031B999 File Offset: 0x00319B99
		internal override int ElementTypeId
		{
			get
			{
				return 10890;
			}
		}

		// Token: 0x06015210 RID: 86544 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015212 RID: 86546 RVA: 0x0031B9A0 File Offset: 0x00319BA0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SeparatorChar>(deep);
		}

		// Token: 0x040091B8 RID: 37304
		private const string tagName = "sepChr";

		// Token: 0x040091B9 RID: 37305
		private const byte tagNsId = 21;

		// Token: 0x040091BA RID: 37306
		internal const int ElementTypeIdConst = 10890;
	}
}
