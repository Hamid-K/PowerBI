using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021F0 RID: 8688
	[GeneratedCode("DomGen", "2.0")]
	internal class InputValidationType : OpenXmlLeafTextElement
	{
		// Token: 0x170037DD RID: 14301
		// (get) Token: 0x0600DD32 RID: 56626 RVA: 0x002BD168 File Offset: 0x002BB368
		public override string LocalName
		{
			get
			{
				return "VTEdit";
			}
		}

		// Token: 0x170037DE RID: 14302
		// (get) Token: 0x0600DD33 RID: 56627 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037DF RID: 14303
		// (get) Token: 0x0600DD34 RID: 56628 RVA: 0x002BD16F File Offset: 0x002BB36F
		internal override int ElementTypeId
		{
			get
			{
				return 12464;
			}
		}

		// Token: 0x0600DD35 RID: 56629 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD36 RID: 56630 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public InputValidationType()
		{
		}

		// Token: 0x0600DD37 RID: 56631 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public InputValidationType(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD38 RID: 56632 RVA: 0x002BD178 File Offset: 0x002BB378
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD39 RID: 56633 RVA: 0x002BD193 File Offset: 0x002BB393
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InputValidationType>(deep);
		}

		// Token: 0x04006D04 RID: 27908
		private const string tagName = "VTEdit";

		// Token: 0x04006D05 RID: 27909
		private const byte tagNsId = 29;

		// Token: 0x04006D06 RID: 27910
		internal const int ElementTypeIdConst = 12464;
	}
}
