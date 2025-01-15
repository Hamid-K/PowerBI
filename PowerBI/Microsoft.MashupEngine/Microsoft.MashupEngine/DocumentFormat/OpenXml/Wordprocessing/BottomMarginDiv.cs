using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FC0 RID: 12224
	[GeneratedCode("DomGen", "2.0")]
	internal class BottomMarginDiv : SignedTwipsMeasureType
	{
		// Token: 0x170093D7 RID: 37847
		// (get) Token: 0x0601A815 RID: 108565 RVA: 0x003632D1 File Offset: 0x003614D1
		public override string LocalName
		{
			get
			{
				return "marBottom";
			}
		}

		// Token: 0x170093D8 RID: 37848
		// (get) Token: 0x0601A816 RID: 108566 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093D9 RID: 37849
		// (get) Token: 0x0601A817 RID: 108567 RVA: 0x003632D8 File Offset: 0x003614D8
		internal override int ElementTypeId
		{
			get
			{
				return 11932;
			}
		}

		// Token: 0x0601A818 RID: 108568 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A81A RID: 108570 RVA: 0x003632DF File Offset: 0x003614DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BottomMarginDiv>(deep);
		}

		// Token: 0x0400AD43 RID: 44355
		private const string tagName = "marBottom";

		// Token: 0x0400AD44 RID: 44356
		private const byte tagNsId = 23;

		// Token: 0x0400AD45 RID: 44357
		internal const int ElementTypeIdConst = 11932;
	}
}
