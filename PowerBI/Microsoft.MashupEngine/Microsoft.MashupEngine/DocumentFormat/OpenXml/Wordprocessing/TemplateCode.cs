using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F7C RID: 12156
	[GeneratedCode("DomGen", "2.0")]
	internal class TemplateCode : LongHexNumberType
	{
		// Token: 0x17009132 RID: 37170
		// (get) Token: 0x0601A288 RID: 107144 RVA: 0x00322A54 File Offset: 0x00320C54
		public override string LocalName
		{
			get
			{
				return "tmpl";
			}
		}

		// Token: 0x17009133 RID: 37171
		// (get) Token: 0x0601A289 RID: 107145 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009134 RID: 37172
		// (get) Token: 0x0601A28A RID: 107146 RVA: 0x0035E235 File Offset: 0x0035C435
		internal override int ElementTypeId
		{
			get
			{
				return 11876;
			}
		}

		// Token: 0x0601A28B RID: 107147 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A28D RID: 107149 RVA: 0x0035E23C File Offset: 0x0035C43C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TemplateCode>(deep);
		}

		// Token: 0x0400AC18 RID: 44056
		private const string tagName = "tmpl";

		// Token: 0x0400AC19 RID: 44057
		private const byte tagNsId = 23;

		// Token: 0x0400AC1A RID: 44058
		internal const int ElementTypeIdConst = 11876;
	}
}
