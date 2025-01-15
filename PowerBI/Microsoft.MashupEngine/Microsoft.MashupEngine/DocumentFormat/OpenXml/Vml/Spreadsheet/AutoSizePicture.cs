using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021C3 RID: 8643
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoSizePicture : OpenXmlLeafTextElement
	{
		// Token: 0x17003756 RID: 14166
		// (get) Token: 0x0600DBCA RID: 56266 RVA: 0x002BC844 File Offset: 0x002BAA44
		public override string LocalName
		{
			get
			{
				return "AutoPict";
			}
		}

		// Token: 0x17003757 RID: 14167
		// (get) Token: 0x0600DBCB RID: 56267 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003758 RID: 14168
		// (get) Token: 0x0600DBCC RID: 56268 RVA: 0x002BC84B File Offset: 0x002BAA4B
		internal override int ElementTypeId
		{
			get
			{
				return 12446;
			}
		}

		// Token: 0x0600DBCD RID: 56269 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBCE RID: 56270 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public AutoSizePicture()
		{
		}

		// Token: 0x0600DBCF RID: 56271 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public AutoSizePicture(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBD0 RID: 56272 RVA: 0x002BC854 File Offset: 0x002BAA54
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBD1 RID: 56273 RVA: 0x002BC86F File Offset: 0x002BAA6F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoSizePicture>(deep);
		}

		// Token: 0x04006C7D RID: 27773
		private const string tagName = "AutoPict";

		// Token: 0x04006C7E RID: 27774
		private const byte tagNsId = 29;

		// Token: 0x04006C7F RID: 27775
		internal const int ElementTypeIdConst = 12446;
	}
}
