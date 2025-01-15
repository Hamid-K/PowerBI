using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021C6 RID: 8646
	[GeneratedCode("DomGen", "2.0")]
	internal class SecretEdit : OpenXmlLeafTextElement
	{
		// Token: 0x1700375F RID: 14175
		// (get) Token: 0x0600DBE2 RID: 56290 RVA: 0x002BC8E0 File Offset: 0x002BAAE0
		public override string LocalName
		{
			get
			{
				return "SecretEdit";
			}
		}

		// Token: 0x17003760 RID: 14176
		// (get) Token: 0x0600DBE3 RID: 56291 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003761 RID: 14177
		// (get) Token: 0x0600DBE4 RID: 56292 RVA: 0x002BC8E7 File Offset: 0x002BAAE7
		internal override int ElementTypeId
		{
			get
			{
				return 12452;
			}
		}

		// Token: 0x0600DBE5 RID: 56293 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBE6 RID: 56294 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public SecretEdit()
		{
		}

		// Token: 0x0600DBE7 RID: 56295 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public SecretEdit(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBE8 RID: 56296 RVA: 0x002BC8F0 File Offset: 0x002BAAF0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBE9 RID: 56297 RVA: 0x002BC90B File Offset: 0x002BAB0B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SecretEdit>(deep);
		}

		// Token: 0x04006C86 RID: 27782
		private const string tagName = "SecretEdit";

		// Token: 0x04006C87 RID: 27783
		private const byte tagNsId = 29;

		// Token: 0x04006C88 RID: 27784
		internal const int ElementTypeIdConst = 12452;
	}
}
