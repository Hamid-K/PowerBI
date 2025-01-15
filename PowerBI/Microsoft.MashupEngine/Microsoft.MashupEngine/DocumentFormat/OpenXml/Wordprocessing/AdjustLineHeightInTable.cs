using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E03 RID: 11779
	[GeneratedCode("DomGen", "2.0")]
	internal class AdjustLineHeightInTable : OnOffType
	{
		// Token: 0x170088A5 RID: 34981
		// (get) Token: 0x0601900A RID: 102410 RVA: 0x00345752 File Offset: 0x00343952
		public override string LocalName
		{
			get
			{
				return "adjustLineHeightInTable";
			}
		}

		// Token: 0x170088A6 RID: 34982
		// (get) Token: 0x0601900B RID: 102411 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088A7 RID: 34983
		// (get) Token: 0x0601900C RID: 102412 RVA: 0x00345759 File Offset: 0x00343959
		internal override int ElementTypeId
		{
			get
			{
				return 12089;
			}
		}

		// Token: 0x0601900D RID: 102413 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601900F RID: 102415 RVA: 0x00345760 File Offset: 0x00343960
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AdjustLineHeightInTable>(deep);
		}

		// Token: 0x0400A674 RID: 42612
		private const string tagName = "adjustLineHeightInTable";

		// Token: 0x0400A675 RID: 42613
		private const byte tagNsId = 23;

		// Token: 0x0400A676 RID: 42614
		internal const int ElementTypeIdConst = 12089;
	}
}
