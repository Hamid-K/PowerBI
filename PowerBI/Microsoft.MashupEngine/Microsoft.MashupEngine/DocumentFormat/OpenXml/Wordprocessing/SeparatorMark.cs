using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E68 RID: 11880
	[GeneratedCode("DomGen", "2.0")]
	internal class SeparatorMark : EmptyType
	{
		// Token: 0x17008A79 RID: 35449
		// (get) Token: 0x060193D3 RID: 103379 RVA: 0x002CF519 File Offset: 0x002CD719
		public override string LocalName
		{
			get
			{
				return "separator";
			}
		}

		// Token: 0x17008A7A RID: 35450
		// (get) Token: 0x060193D4 RID: 103380 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A7B RID: 35451
		// (get) Token: 0x060193D5 RID: 103381 RVA: 0x00347B11 File Offset: 0x00345D11
		internal override int ElementTypeId
		{
			get
			{
				return 11559;
			}
		}

		// Token: 0x060193D6 RID: 103382 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193D8 RID: 103384 RVA: 0x00347B18 File Offset: 0x00345D18
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SeparatorMark>(deep);
		}

		// Token: 0x0400A7D0 RID: 42960
		private const string tagName = "separator";

		// Token: 0x0400A7D1 RID: 42961
		private const byte tagNsId = 23;

		// Token: 0x0400A7D2 RID: 42962
		internal const int ElementTypeIdConst = 11559;
	}
}
