using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DE5 RID: 11749
	[GeneratedCode("DomGen", "2.0")]
	internal class SpaceForUnderline : OnOffType
	{
		// Token: 0x1700884B RID: 34891
		// (get) Token: 0x06018F56 RID: 102230 RVA: 0x003454A0 File Offset: 0x003436A0
		public override string LocalName
		{
			get
			{
				return "spaceForUL";
			}
		}

		// Token: 0x1700884C RID: 34892
		// (get) Token: 0x06018F57 RID: 102231 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700884D RID: 34893
		// (get) Token: 0x06018F58 RID: 102232 RVA: 0x003454A7 File Offset: 0x003436A7
		internal override int ElementTypeId
		{
			get
			{
				return 12059;
			}
		}

		// Token: 0x06018F59 RID: 102233 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F5B RID: 102235 RVA: 0x003454AE File Offset: 0x003436AE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SpaceForUnderline>(deep);
		}

		// Token: 0x0400A61A RID: 42522
		private const string tagName = "spaceForUL";

		// Token: 0x0400A61B RID: 42523
		private const byte tagNsId = 23;

		// Token: 0x0400A61C RID: 42524
		internal const int ElementTypeIdConst = 12059;
	}
}
