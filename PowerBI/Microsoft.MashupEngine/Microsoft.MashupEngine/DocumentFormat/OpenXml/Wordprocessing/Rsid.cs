using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F7A RID: 12154
	[GeneratedCode("DomGen", "2.0")]
	internal class Rsid : LongHexNumberType
	{
		// Token: 0x1700912C RID: 37164
		// (get) Token: 0x0601A27C RID: 107132 RVA: 0x0035E207 File Offset: 0x0035C407
		public override string LocalName
		{
			get
			{
				return "rsid";
			}
		}

		// Token: 0x1700912D RID: 37165
		// (get) Token: 0x0601A27D RID: 107133 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700912E RID: 37166
		// (get) Token: 0x0601A27E RID: 107134 RVA: 0x0035E20E File Offset: 0x0035C40E
		internal override int ElementTypeId
		{
			get
			{
				return 11830;
			}
		}

		// Token: 0x0601A27F RID: 107135 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A281 RID: 107137 RVA: 0x0035E215 File Offset: 0x0035C415
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Rsid>(deep);
		}

		// Token: 0x0400AC12 RID: 44050
		private const string tagName = "rsid";

		// Token: 0x0400AC13 RID: 44051
		private const byte tagNsId = 23;

		// Token: 0x0400AC14 RID: 44052
		internal const int ElementTypeIdConst = 11830;
	}
}
