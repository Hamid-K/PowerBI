using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028BE RID: 10430
	[GeneratedCode("DomGen", "2.0")]
	internal class BookTitle : OpenXmlLeafTextElement
	{
		// Token: 0x170068E2 RID: 26850
		// (get) Token: 0x060148E4 RID: 84196 RVA: 0x0031496C File Offset: 0x00312B6C
		public override string LocalName
		{
			get
			{
				return "BookTitle";
			}
		}

		// Token: 0x170068E3 RID: 26851
		// (get) Token: 0x060148E5 RID: 84197 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068E4 RID: 26852
		// (get) Token: 0x060148E6 RID: 84198 RVA: 0x00314973 File Offset: 0x00312B73
		internal override int ElementTypeId
		{
			get
			{
				return 10784;
			}
		}

		// Token: 0x060148E7 RID: 84199 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060148E8 RID: 84200 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public BookTitle()
		{
		}

		// Token: 0x060148E9 RID: 84201 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public BookTitle(string text)
			: base(text)
		{
		}

		// Token: 0x060148EA RID: 84202 RVA: 0x0031497C File Offset: 0x00312B7C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060148EB RID: 84203 RVA: 0x00314997 File Offset: 0x00312B97
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BookTitle>(deep);
		}

		// Token: 0x04008EC6 RID: 36550
		private const string tagName = "BookTitle";

		// Token: 0x04008EC7 RID: 36551
		private const byte tagNsId = 9;

		// Token: 0x04008EC8 RID: 36552
		internal const int ElementTypeIdConst = 10784;
	}
}
