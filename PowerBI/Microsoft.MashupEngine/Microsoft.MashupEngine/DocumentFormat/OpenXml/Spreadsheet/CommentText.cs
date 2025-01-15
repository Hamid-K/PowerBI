using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B96 RID: 11158
	[GeneratedCode("DomGen", "2.0")]
	internal class CommentText : RstType
	{
		// Token: 0x17007B1E RID: 31518
		// (get) Token: 0x06017236 RID: 94774 RVA: 0x00321E60 File Offset: 0x00320060
		public override string LocalName
		{
			get
			{
				return "text";
			}
		}

		// Token: 0x17007B1F RID: 31519
		// (get) Token: 0x06017237 RID: 94775 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B20 RID: 31520
		// (get) Token: 0x06017238 RID: 94776 RVA: 0x00333345 File Offset: 0x00331545
		internal override int ElementTypeId
		{
			get
			{
				return 11462;
			}
		}

		// Token: 0x06017239 RID: 94777 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601723A RID: 94778 RVA: 0x00333302 File Offset: 0x00331502
		public CommentText()
		{
		}

		// Token: 0x0601723B RID: 94779 RVA: 0x0033330A File Offset: 0x0033150A
		public CommentText(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601723C RID: 94780 RVA: 0x00333313 File Offset: 0x00331513
		public CommentText(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601723D RID: 94781 RVA: 0x0033331C File Offset: 0x0033151C
		public CommentText(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601723E RID: 94782 RVA: 0x0033334C File Offset: 0x0033154C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentText>(deep);
		}

		// Token: 0x04009B32 RID: 39730
		private const string tagName = "text";

		// Token: 0x04009B33 RID: 39731
		private const byte tagNsId = 22;

		// Token: 0x04009B34 RID: 39732
		internal const int ElementTypeIdConst = 11462;
	}
}
