using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DD8 RID: 11736
	[GeneratedCode("DomGen", "2.0")]
	internal class SaveXmlDataOnly : OnOffType
	{
		// Token: 0x17008824 RID: 34852
		// (get) Token: 0x06018F08 RID: 102152 RVA: 0x00345375 File Offset: 0x00343575
		public override string LocalName
		{
			get
			{
				return "saveXmlDataOnly";
			}
		}

		// Token: 0x17008825 RID: 34853
		// (get) Token: 0x06018F09 RID: 102153 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008826 RID: 34854
		// (get) Token: 0x06018F0A RID: 102154 RVA: 0x0034537C File Offset: 0x0034357C
		internal override int ElementTypeId
		{
			get
			{
				return 12029;
			}
		}

		// Token: 0x06018F0B RID: 102155 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F0D RID: 102157 RVA: 0x00345383 File Offset: 0x00343583
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SaveXmlDataOnly>(deep);
		}

		// Token: 0x0400A5F3 RID: 42483
		private const string tagName = "saveXmlDataOnly";

		// Token: 0x0400A5F4 RID: 42484
		private const byte tagNsId = 23;

		// Token: 0x0400A5F5 RID: 42485
		internal const int ElementTypeIdConst = 12029;
	}
}
