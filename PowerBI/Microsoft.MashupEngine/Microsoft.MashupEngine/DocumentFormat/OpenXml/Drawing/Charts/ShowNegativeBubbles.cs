using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200252F RID: 9519
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowNegativeBubbles : BooleanType
	{
		// Token: 0x170054A6 RID: 21670
		// (get) Token: 0x06011B47 RID: 72519 RVA: 0x002F1554 File Offset: 0x002EF754
		public override string LocalName
		{
			get
			{
				return "showNegBubbles";
			}
		}

		// Token: 0x170054A7 RID: 21671
		// (get) Token: 0x06011B48 RID: 72520 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054A8 RID: 21672
		// (get) Token: 0x06011B49 RID: 72521 RVA: 0x002F155B File Offset: 0x002EF75B
		internal override int ElementTypeId
		{
			get
			{
				return 10534;
			}
		}

		// Token: 0x06011B4A RID: 72522 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B4C RID: 72524 RVA: 0x002F1562 File Offset: 0x002EF762
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowNegativeBubbles>(deep);
		}

		// Token: 0x04007C21 RID: 31777
		private const string tagName = "showNegBubbles";

		// Token: 0x04007C22 RID: 31778
		private const byte tagNsId = 11;

		// Token: 0x04007C23 RID: 31779
		internal const int ElementTypeIdConst = 10534;
	}
}
