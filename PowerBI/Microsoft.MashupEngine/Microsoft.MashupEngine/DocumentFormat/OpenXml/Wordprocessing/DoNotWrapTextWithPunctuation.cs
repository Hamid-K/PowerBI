using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E0E RID: 11790
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotWrapTextWithPunctuation : OnOffType
	{
		// Token: 0x170088C6 RID: 35014
		// (get) Token: 0x0601904C RID: 102476 RVA: 0x0034584F File Offset: 0x00343A4F
		public override string LocalName
		{
			get
			{
				return "doNotWrapTextWithPunct";
			}
		}

		// Token: 0x170088C7 RID: 35015
		// (get) Token: 0x0601904D RID: 102477 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088C8 RID: 35016
		// (get) Token: 0x0601904E RID: 102478 RVA: 0x00345856 File Offset: 0x00343A56
		internal override int ElementTypeId
		{
			get
			{
				return 12100;
			}
		}

		// Token: 0x0601904F RID: 102479 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019051 RID: 102481 RVA: 0x0034585D File Offset: 0x00343A5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotWrapTextWithPunctuation>(deep);
		}

		// Token: 0x0400A695 RID: 42645
		private const string tagName = "doNotWrapTextWithPunct";

		// Token: 0x0400A696 RID: 42646
		private const byte tagNsId = 23;

		// Token: 0x0400A697 RID: 42647
		internal const int ElementTypeIdConst = 12100;
	}
}
