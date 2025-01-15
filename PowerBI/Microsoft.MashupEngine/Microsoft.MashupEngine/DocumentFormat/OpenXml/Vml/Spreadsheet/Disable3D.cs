using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021D3 RID: 8659
	[GeneratedCode("DomGen", "2.0")]
	internal class Disable3D : OpenXmlLeafTextElement
	{
		// Token: 0x17003786 RID: 14214
		// (get) Token: 0x0600DC4A RID: 56394 RVA: 0x002BCB84 File Offset: 0x002BAD84
		public override string LocalName
		{
			get
			{
				return "NoThreeD";
			}
		}

		// Token: 0x17003787 RID: 14215
		// (get) Token: 0x0600DC4B RID: 56395 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003788 RID: 14216
		// (get) Token: 0x0600DC4C RID: 56396 RVA: 0x002BCB8B File Offset: 0x002BAD8B
		internal override int ElementTypeId
		{
			get
			{
				return 12482;
			}
		}

		// Token: 0x0600DC4D RID: 56397 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC4E RID: 56398 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Disable3D()
		{
		}

		// Token: 0x0600DC4F RID: 56399 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Disable3D(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC50 RID: 56400 RVA: 0x002BCB94 File Offset: 0x002BAD94
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC51 RID: 56401 RVA: 0x002BCBAF File Offset: 0x002BADAF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Disable3D>(deep);
		}

		// Token: 0x04006CAD RID: 27821
		private const string tagName = "NoThreeD";

		// Token: 0x04006CAE RID: 27822
		private const byte tagNsId = 29;

		// Token: 0x04006CAF RID: 27823
		internal const int ElementTypeIdConst = 12482;
	}
}
