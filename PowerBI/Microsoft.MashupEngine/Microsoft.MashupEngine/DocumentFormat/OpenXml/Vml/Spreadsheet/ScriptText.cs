using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021E8 RID: 8680
	[GeneratedCode("DomGen", "2.0")]
	internal class ScriptText : OpenXmlLeafTextElement
	{
		// Token: 0x170037C5 RID: 14277
		// (get) Token: 0x0600DCF2 RID: 56562 RVA: 0x002BCFC8 File Offset: 0x002BB1C8
		public override string LocalName
		{
			get
			{
				return "ScriptText";
			}
		}

		// Token: 0x170037C6 RID: 14278
		// (get) Token: 0x0600DCF3 RID: 56563 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037C7 RID: 14279
		// (get) Token: 0x0600DCF4 RID: 56564 RVA: 0x002BCFCF File Offset: 0x002BB1CF
		internal override int ElementTypeId
		{
			get
			{
				return 12499;
			}
		}

		// Token: 0x0600DCF5 RID: 56565 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCF6 RID: 56566 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScriptText()
		{
		}

		// Token: 0x0600DCF7 RID: 56567 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScriptText(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCF8 RID: 56568 RVA: 0x002BCFD8 File Offset: 0x002BB1D8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCF9 RID: 56569 RVA: 0x002BCFF3 File Offset: 0x002BB1F3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScriptText>(deep);
		}

		// Token: 0x04006CEC RID: 27884
		private const string tagName = "ScriptText";

		// Token: 0x04006CED RID: 27885
		private const byte tagNsId = 29;

		// Token: 0x04006CEE RID: 27886
		internal const int ElementTypeIdConst = 12499;
	}
}
