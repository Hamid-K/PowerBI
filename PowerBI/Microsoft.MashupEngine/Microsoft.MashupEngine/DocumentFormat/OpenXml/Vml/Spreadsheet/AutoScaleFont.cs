using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021D9 RID: 8665
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoScaleFont : OpenXmlLeafTextElement
	{
		// Token: 0x17003798 RID: 14232
		// (get) Token: 0x0600DC7A RID: 56442 RVA: 0x002BCCBC File Offset: 0x002BAEBC
		public override string LocalName
		{
			get
			{
				return "AutoScale";
			}
		}

		// Token: 0x17003799 RID: 14233
		// (get) Token: 0x0600DC7B RID: 56443 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700379A RID: 14234
		// (get) Token: 0x0600DC7C RID: 56444 RVA: 0x002BCCC3 File Offset: 0x002BAEC3
		internal override int ElementTypeId
		{
			get
			{
				return 12496;
			}
		}

		// Token: 0x0600DC7D RID: 56445 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC7E RID: 56446 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public AutoScaleFont()
		{
		}

		// Token: 0x0600DC7F RID: 56447 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public AutoScaleFont(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC80 RID: 56448 RVA: 0x002BCCCC File Offset: 0x002BAECC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC81 RID: 56449 RVA: 0x002BCCE7 File Offset: 0x002BAEE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoScaleFont>(deep);
		}

		// Token: 0x04006CBF RID: 27839
		private const string tagName = "AutoScale";

		// Token: 0x04006CC0 RID: 27840
		private const byte tagNsId = 29;

		// Token: 0x04006CC1 RID: 27841
		internal const int ElementTypeIdConst = 12496;
	}
}
