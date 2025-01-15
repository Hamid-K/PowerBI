using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E73 RID: 11891
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtContentCitation : EmptyType
	{
		// Token: 0x17008A9A RID: 35482
		// (get) Token: 0x06019415 RID: 103445 RVA: 0x00347BF9 File Offset: 0x00345DF9
		public override string LocalName
		{
			get
			{
				return "citation";
			}
		}

		// Token: 0x17008A9B RID: 35483
		// (get) Token: 0x06019416 RID: 103446 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A9C RID: 35484
		// (get) Token: 0x06019417 RID: 103447 RVA: 0x00347C00 File Offset: 0x00345E00
		internal override int ElementTypeId
		{
			get
			{
				return 12156;
			}
		}

		// Token: 0x06019418 RID: 103448 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601941A RID: 103450 RVA: 0x00347C07 File Offset: 0x00345E07
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentCitation>(deep);
		}

		// Token: 0x0400A7F1 RID: 42993
		private const string tagName = "citation";

		// Token: 0x0400A7F2 RID: 42994
		private const byte tagNsId = 23;

		// Token: 0x0400A7F3 RID: 42995
		internal const int ElementTypeIdConst = 12156;
	}
}
