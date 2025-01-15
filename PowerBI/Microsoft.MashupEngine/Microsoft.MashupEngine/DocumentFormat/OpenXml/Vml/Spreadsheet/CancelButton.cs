using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021C9 RID: 8649
	[GeneratedCode("DomGen", "2.0")]
	internal class CancelButton : OpenXmlLeafTextElement
	{
		// Token: 0x17003768 RID: 14184
		// (get) Token: 0x0600DBFA RID: 56314 RVA: 0x002BC97C File Offset: 0x002BAB7C
		public override string LocalName
		{
			get
			{
				return "Cancel";
			}
		}

		// Token: 0x17003769 RID: 14185
		// (get) Token: 0x0600DBFB RID: 56315 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700376A RID: 14186
		// (get) Token: 0x0600DBFC RID: 56316 RVA: 0x002BC983 File Offset: 0x002BAB83
		internal override int ElementTypeId
		{
			get
			{
				return 12455;
			}
		}

		// Token: 0x0600DBFD RID: 56317 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBFE RID: 56318 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CancelButton()
		{
		}

		// Token: 0x0600DBFF RID: 56319 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CancelButton(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC00 RID: 56320 RVA: 0x002BC98C File Offset: 0x002BAB8C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC01 RID: 56321 RVA: 0x002BC9A7 File Offset: 0x002BABA7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CancelButton>(deep);
		}

		// Token: 0x04006C8F RID: 27791
		private const string tagName = "Cancel";

		// Token: 0x04006C90 RID: 27792
		private const byte tagNsId = 29;

		// Token: 0x04006C91 RID: 27793
		internal const int ElementTypeIdConst = 12455;
	}
}
