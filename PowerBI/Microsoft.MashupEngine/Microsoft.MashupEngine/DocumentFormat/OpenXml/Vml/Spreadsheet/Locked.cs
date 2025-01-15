using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021BD RID: 8637
	[GeneratedCode("DomGen", "2.0")]
	internal class Locked : OpenXmlLeafTextElement
	{
		// Token: 0x17003744 RID: 14148
		// (get) Token: 0x0600DB9A RID: 56218 RVA: 0x002BC70C File Offset: 0x002BA90C
		public override string LocalName
		{
			get
			{
				return "Locked";
			}
		}

		// Token: 0x17003745 RID: 14149
		// (get) Token: 0x0600DB9B RID: 56219 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003746 RID: 14150
		// (get) Token: 0x0600DB9C RID: 56220 RVA: 0x002BC713 File Offset: 0x002BA913
		internal override int ElementTypeId
		{
			get
			{
				return 12440;
			}
		}

		// Token: 0x0600DB9D RID: 56221 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DB9E RID: 56222 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Locked()
		{
		}

		// Token: 0x0600DB9F RID: 56223 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Locked(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBA0 RID: 56224 RVA: 0x002BC71C File Offset: 0x002BA91C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBA1 RID: 56225 RVA: 0x002BC737 File Offset: 0x002BA937
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Locked>(deep);
		}

		// Token: 0x04006C6B RID: 27755
		private const string tagName = "Locked";

		// Token: 0x04006C6C RID: 27756
		private const byte tagNsId = 29;

		// Token: 0x04006C6D RID: 27757
		internal const int ElementTypeIdConst = 12440;
	}
}
