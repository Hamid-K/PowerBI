using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021C4 RID: 8644
	[GeneratedCode("DomGen", "2.0")]
	internal class LockText : OpenXmlLeafTextElement
	{
		// Token: 0x17003759 RID: 14169
		// (get) Token: 0x0600DBD2 RID: 56274 RVA: 0x002BC878 File Offset: 0x002BAA78
		public override string LocalName
		{
			get
			{
				return "LockText";
			}
		}

		// Token: 0x1700375A RID: 14170
		// (get) Token: 0x0600DBD3 RID: 56275 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700375B RID: 14171
		// (get) Token: 0x0600DBD4 RID: 56276 RVA: 0x002BC87F File Offset: 0x002BAA7F
		internal override int ElementTypeId
		{
			get
			{
				return 12450;
			}
		}

		// Token: 0x0600DBD5 RID: 56277 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBD6 RID: 56278 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public LockText()
		{
		}

		// Token: 0x0600DBD7 RID: 56279 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public LockText(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBD8 RID: 56280 RVA: 0x002BC888 File Offset: 0x002BAA88
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBD9 RID: 56281 RVA: 0x002BC8A3 File Offset: 0x002BAAA3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LockText>(deep);
		}

		// Token: 0x04006C80 RID: 27776
		private const string tagName = "LockText";

		// Token: 0x04006C81 RID: 27777
		private const byte tagNsId = 29;

		// Token: 0x04006C82 RID: 27778
		internal const int ElementTypeIdConst = 12450;
	}
}
