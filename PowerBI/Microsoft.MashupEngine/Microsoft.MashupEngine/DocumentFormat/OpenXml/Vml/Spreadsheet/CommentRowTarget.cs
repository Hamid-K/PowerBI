using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021EE RID: 8686
	[GeneratedCode("DomGen", "2.0")]
	internal class CommentRowTarget : OpenXmlLeafTextElement
	{
		// Token: 0x170037D7 RID: 14295
		// (get) Token: 0x0600DD22 RID: 56610 RVA: 0x002BD100 File Offset: 0x002BB300
		public override string LocalName
		{
			get
			{
				return "Row";
			}
		}

		// Token: 0x170037D8 RID: 14296
		// (get) Token: 0x0600DD23 RID: 56611 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037D9 RID: 14297
		// (get) Token: 0x0600DD24 RID: 56612 RVA: 0x002BD107 File Offset: 0x002BB307
		internal override int ElementTypeId
		{
			get
			{
				return 12459;
			}
		}

		// Token: 0x0600DD25 RID: 56613 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD26 RID: 56614 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CommentRowTarget()
		{
		}

		// Token: 0x0600DD27 RID: 56615 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CommentRowTarget(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD28 RID: 56616 RVA: 0x002BD110 File Offset: 0x002BB310
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD29 RID: 56617 RVA: 0x002BD12B File Offset: 0x002BB32B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentRowTarget>(deep);
		}

		// Token: 0x04006CFE RID: 27902
		private const string tagName = "Row";

		// Token: 0x04006CFF RID: 27903
		private const byte tagNsId = 29;

		// Token: 0x04006D00 RID: 27904
		internal const int ElementTypeIdConst = 12459;
	}
}
