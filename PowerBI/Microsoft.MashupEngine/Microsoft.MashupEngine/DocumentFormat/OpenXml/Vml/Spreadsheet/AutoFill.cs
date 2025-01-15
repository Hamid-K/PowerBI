using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021C1 RID: 8641
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoFill : OpenXmlLeafTextElement
	{
		// Token: 0x17003750 RID: 14160
		// (get) Token: 0x0600DBBA RID: 56250 RVA: 0x002BC7DC File Offset: 0x002BA9DC
		public override string LocalName
		{
			get
			{
				return "AutoFill";
			}
		}

		// Token: 0x17003751 RID: 14161
		// (get) Token: 0x0600DBBB RID: 56251 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003752 RID: 14162
		// (get) Token: 0x0600DBBC RID: 56252 RVA: 0x002BC7E3 File Offset: 0x002BA9E3
		internal override int ElementTypeId
		{
			get
			{
				return 12444;
			}
		}

		// Token: 0x0600DBBD RID: 56253 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBBE RID: 56254 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public AutoFill()
		{
		}

		// Token: 0x0600DBBF RID: 56255 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public AutoFill(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBC0 RID: 56256 RVA: 0x002BC7EC File Offset: 0x002BA9EC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBC1 RID: 56257 RVA: 0x002BC807 File Offset: 0x002BAA07
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoFill>(deep);
		}

		// Token: 0x04006C77 RID: 27767
		private const string tagName = "AutoFill";

		// Token: 0x04006C78 RID: 27768
		private const byte tagNsId = 29;

		// Token: 0x04006C79 RID: 27769
		internal const int ElementTypeIdConst = 12444;
	}
}
