using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021FB RID: 8699
	[GeneratedCode("DomGen", "2.0")]
	internal class ClipboardFormat : OpenXmlLeafTextElement
	{
		// Token: 0x170037FE RID: 14334
		// (get) Token: 0x0600DD8A RID: 56714 RVA: 0x002BD3A4 File Offset: 0x002BB5A4
		public override string LocalName
		{
			get
			{
				return "CF";
			}
		}

		// Token: 0x170037FF RID: 14335
		// (get) Token: 0x0600DD8B RID: 56715 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003800 RID: 14336
		// (get) Token: 0x0600DD8C RID: 56716 RVA: 0x002BD3AB File Offset: 0x002BB5AB
		internal override int ElementTypeId
		{
			get
			{
				return 12493;
			}
		}

		// Token: 0x0600DD8D RID: 56717 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD8E RID: 56718 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ClipboardFormat()
		{
		}

		// Token: 0x0600DD8F RID: 56719 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ClipboardFormat(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD90 RID: 56720 RVA: 0x002BD3B4 File Offset: 0x002BB5B4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<ClipboardFormatValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD91 RID: 56721 RVA: 0x002BD3CF File Offset: 0x002BB5CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ClipboardFormat>(deep);
		}

		// Token: 0x04006D25 RID: 27941
		private const string tagName = "CF";

		// Token: 0x04006D26 RID: 27942
		private const byte tagNsId = 29;

		// Token: 0x04006D27 RID: 27943
		internal const int ElementTypeIdConst = 12493;
	}
}
