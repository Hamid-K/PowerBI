using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021C7 RID: 8647
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultButton : OpenXmlLeafTextElement
	{
		// Token: 0x17003762 RID: 14178
		// (get) Token: 0x0600DBEA RID: 56298 RVA: 0x002BC914 File Offset: 0x002BAB14
		public override string LocalName
		{
			get
			{
				return "Default";
			}
		}

		// Token: 0x17003763 RID: 14179
		// (get) Token: 0x0600DBEB RID: 56299 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003764 RID: 14180
		// (get) Token: 0x0600DBEC RID: 56300 RVA: 0x002BC91B File Offset: 0x002BAB1B
		internal override int ElementTypeId
		{
			get
			{
				return 12453;
			}
		}

		// Token: 0x0600DBED RID: 56301 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBEE RID: 56302 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DefaultButton()
		{
		}

		// Token: 0x0600DBEF RID: 56303 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DefaultButton(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBF0 RID: 56304 RVA: 0x002BC924 File Offset: 0x002BAB24
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBF1 RID: 56305 RVA: 0x002BC93F File Offset: 0x002BAB3F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultButton>(deep);
		}

		// Token: 0x04006C89 RID: 27785
		private const string tagName = "Default";

		// Token: 0x04006C8A RID: 27786
		private const byte tagNsId = 29;

		// Token: 0x04006C8B RID: 27787
		internal const int ElementTypeIdConst = 12453;
	}
}
