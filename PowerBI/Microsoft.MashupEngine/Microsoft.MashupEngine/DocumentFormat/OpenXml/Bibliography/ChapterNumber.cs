using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028C2 RID: 10434
	[GeneratedCode("DomGen", "2.0")]
	internal class ChapterNumber : OpenXmlLeafTextElement
	{
		// Token: 0x170068EE RID: 26862
		// (get) Token: 0x06014904 RID: 84228 RVA: 0x00314A3C File Offset: 0x00312C3C
		public override string LocalName
		{
			get
			{
				return "ChapterNumber";
			}
		}

		// Token: 0x170068EF RID: 26863
		// (get) Token: 0x06014905 RID: 84229 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068F0 RID: 26864
		// (get) Token: 0x06014906 RID: 84230 RVA: 0x00314A43 File Offset: 0x00312C43
		internal override int ElementTypeId
		{
			get
			{
				return 10788;
			}
		}

		// Token: 0x06014907 RID: 84231 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014908 RID: 84232 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ChapterNumber()
		{
		}

		// Token: 0x06014909 RID: 84233 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ChapterNumber(string text)
			: base(text)
		{
		}

		// Token: 0x0601490A RID: 84234 RVA: 0x00314A4C File Offset: 0x00312C4C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601490B RID: 84235 RVA: 0x00314A67 File Offset: 0x00312C67
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChapterNumber>(deep);
		}

		// Token: 0x04008ED2 RID: 36562
		private const string tagName = "ChapterNumber";

		// Token: 0x04008ED3 RID: 36563
		private const byte tagNsId = 9;

		// Token: 0x04008ED4 RID: 36564
		internal const int ElementTypeIdConst = 10788;
	}
}
