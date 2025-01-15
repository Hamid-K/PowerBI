using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F8C RID: 12172
	[GeneratedCode("DomGen", "2.0")]
	internal class Width : TwipsMeasureType
	{
		// Token: 0x170091BC RID: 37308
		// (get) Token: 0x0601A3B3 RID: 107443 RVA: 0x002F2F1C File Offset: 0x002F111C
		public override string LocalName
		{
			get
			{
				return "w";
			}
		}

		// Token: 0x170091BD RID: 37309
		// (get) Token: 0x0601A3B4 RID: 107444 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091BE RID: 37310
		// (get) Token: 0x0601A3B5 RID: 107445 RVA: 0x0035F540 File Offset: 0x0035D740
		internal override int ElementTypeId
		{
			get
			{
				return 11856;
			}
		}

		// Token: 0x0601A3B6 RID: 107446 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A3B8 RID: 107448 RVA: 0x0035F54F File Offset: 0x0035D74F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Width>(deep);
		}

		// Token: 0x0400AC53 RID: 44115
		private const string tagName = "w";

		// Token: 0x0400AC54 RID: 44116
		private const byte tagNsId = 23;

		// Token: 0x0400AC55 RID: 44117
		internal const int ElementTypeIdConst = 11856;
	}
}
