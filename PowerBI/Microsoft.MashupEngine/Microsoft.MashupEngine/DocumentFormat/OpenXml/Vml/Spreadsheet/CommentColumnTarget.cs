using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021EF RID: 8687
	[GeneratedCode("DomGen", "2.0")]
	internal class CommentColumnTarget : OpenXmlLeafTextElement
	{
		// Token: 0x170037DA RID: 14298
		// (get) Token: 0x0600DD2A RID: 56618 RVA: 0x002BD134 File Offset: 0x002BB334
		public override string LocalName
		{
			get
			{
				return "Column";
			}
		}

		// Token: 0x170037DB RID: 14299
		// (get) Token: 0x0600DD2B RID: 56619 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037DC RID: 14300
		// (get) Token: 0x0600DD2C RID: 56620 RVA: 0x002BD13B File Offset: 0x002BB33B
		internal override int ElementTypeId
		{
			get
			{
				return 12460;
			}
		}

		// Token: 0x0600DD2D RID: 56621 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD2E RID: 56622 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CommentColumnTarget()
		{
		}

		// Token: 0x0600DD2F RID: 56623 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CommentColumnTarget(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD30 RID: 56624 RVA: 0x002BD144 File Offset: 0x002BB344
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD31 RID: 56625 RVA: 0x002BD15F File Offset: 0x002BB35F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentColumnTarget>(deep);
		}

		// Token: 0x04006D01 RID: 27905
		private const string tagName = "Column";

		// Token: 0x04006D02 RID: 27906
		private const byte tagNsId = 29;

		// Token: 0x04006D03 RID: 27907
		internal const int ElementTypeIdConst = 12460;
	}
}
