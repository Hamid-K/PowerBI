using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200297C RID: 10620
	[GeneratedCode("DomGen", "2.0")]
	internal class HideSuperArgument : OnOffType
	{
		// Token: 0x17006C7F RID: 27775
		// (get) Token: 0x0601518C RID: 86412 RVA: 0x0031B589 File Offset: 0x00319789
		public override string LocalName
		{
			get
			{
				return "supHide";
			}
		}

		// Token: 0x17006C80 RID: 27776
		// (get) Token: 0x0601518D RID: 86413 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C81 RID: 27777
		// (get) Token: 0x0601518E RID: 86414 RVA: 0x0031B590 File Offset: 0x00319790
		internal override int ElementTypeId
		{
			get
			{
				return 10925;
			}
		}

		// Token: 0x0601518F RID: 86415 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015191 RID: 86417 RVA: 0x0031B597 File Offset: 0x00319797
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HideSuperArgument>(deep);
		}

		// Token: 0x04009180 RID: 37248
		private const string tagName = "supHide";

		// Token: 0x04009181 RID: 37249
		private const byte tagNsId = 21;

		// Token: 0x04009182 RID: 37250
		internal const int ElementTypeIdConst = 10925;
	}
}
