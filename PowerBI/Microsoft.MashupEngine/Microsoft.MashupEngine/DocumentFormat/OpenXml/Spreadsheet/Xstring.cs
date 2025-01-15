using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C2A RID: 11306
	[GeneratedCode("DomGen", "2.0")]
	internal class Xstring : OpenXmlLeafTextElement
	{
		// Token: 0x170080BA RID: 32954
		// (get) Token: 0x06017E3A RID: 97850 RVA: 0x002F33CF File Offset: 0x002F15CF
		public override string LocalName
		{
			get
			{
				return "v";
			}
		}

		// Token: 0x170080BB RID: 32955
		// (get) Token: 0x06017E3B RID: 97851 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080BC RID: 32956
		// (get) Token: 0x06017E3C RID: 97852 RVA: 0x0033C36B File Offset: 0x0033A56B
		internal override int ElementTypeId
		{
			get
			{
				return 11287;
			}
		}

		// Token: 0x06017E3D RID: 97853 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017E3E RID: 97854 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Xstring()
		{
		}

		// Token: 0x06017E3F RID: 97855 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Xstring(string text)
			: base(text)
		{
		}

		// Token: 0x06017E40 RID: 97856 RVA: 0x0033C374 File Offset: 0x0033A574
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06017E41 RID: 97857 RVA: 0x0033C38F File Offset: 0x0033A58F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Xstring>(deep);
		}

		// Token: 0x04009E07 RID: 40455
		private const string tagName = "v";

		// Token: 0x04009E08 RID: 40456
		private const byte tagNsId = 22;

		// Token: 0x04009E09 RID: 40457
		internal const int ElementTypeIdConst = 11287;
	}
}
