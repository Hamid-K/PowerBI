using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021E4 RID: 8676
	[GeneratedCode("DomGen", "2.0")]
	internal class DropStyle : OpenXmlLeafTextElement
	{
		// Token: 0x170037B9 RID: 14265
		// (get) Token: 0x0600DCD2 RID: 56530 RVA: 0x002BCEF8 File Offset: 0x002BB0F8
		public override string LocalName
		{
			get
			{
				return "DropStyle";
			}
		}

		// Token: 0x170037BA RID: 14266
		// (get) Token: 0x0600DCD3 RID: 56531 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037BB RID: 14267
		// (get) Token: 0x0600DCD4 RID: 56532 RVA: 0x002BCEFF File Offset: 0x002BB0FF
		internal override int ElementTypeId
		{
			get
			{
				return 12476;
			}
		}

		// Token: 0x0600DCD5 RID: 56533 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCD6 RID: 56534 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DropStyle()
		{
		}

		// Token: 0x0600DCD7 RID: 56535 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DropStyle(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCD8 RID: 56536 RVA: 0x002BCF08 File Offset: 0x002BB108
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCD9 RID: 56537 RVA: 0x002BCF23 File Offset: 0x002BB123
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropStyle>(deep);
		}

		// Token: 0x04006CE0 RID: 27872
		private const string tagName = "DropStyle";

		// Token: 0x04006CE1 RID: 27873
		private const byte tagNsId = 29;

		// Token: 0x04006CE2 RID: 27874
		internal const int ElementTypeIdConst = 12476;
	}
}
