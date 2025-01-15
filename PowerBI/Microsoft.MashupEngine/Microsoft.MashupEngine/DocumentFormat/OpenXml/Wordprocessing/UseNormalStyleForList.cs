using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E13 RID: 11795
	[GeneratedCode("DomGen", "2.0")]
	internal class UseNormalStyleForList : OnOffType
	{
		// Token: 0x170088D5 RID: 35029
		// (get) Token: 0x0601906A RID: 102506 RVA: 0x003458C2 File Offset: 0x00343AC2
		public override string LocalName
		{
			get
			{
				return "useNormalStyleForList";
			}
		}

		// Token: 0x170088D6 RID: 35030
		// (get) Token: 0x0601906B RID: 102507 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088D7 RID: 35031
		// (get) Token: 0x0601906C RID: 102508 RVA: 0x003458C9 File Offset: 0x00343AC9
		internal override int ElementTypeId
		{
			get
			{
				return 12105;
			}
		}

		// Token: 0x0601906D RID: 102509 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601906F RID: 102511 RVA: 0x003458D0 File Offset: 0x00343AD0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UseNormalStyleForList>(deep);
		}

		// Token: 0x0400A6A4 RID: 42660
		private const string tagName = "useNormalStyleForList";

		// Token: 0x0400A6A5 RID: 42661
		private const byte tagNsId = 23;

		// Token: 0x0400A6A6 RID: 42662
		internal const int ElementTypeIdConst = 12105;
	}
}
