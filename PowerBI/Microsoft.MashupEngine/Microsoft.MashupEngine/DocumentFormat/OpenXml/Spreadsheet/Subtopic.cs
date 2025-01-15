using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B42 RID: 11074
	[GeneratedCode("DomGen", "2.0")]
	internal class Subtopic : XstringType
	{
		// Token: 0x170077AD RID: 30637
		// (get) Token: 0x06016AB3 RID: 92851 RVA: 0x0032DAB0 File Offset: 0x0032BCB0
		public override string LocalName
		{
			get
			{
				return "stp";
			}
		}

		// Token: 0x170077AE RID: 30638
		// (get) Token: 0x06016AB4 RID: 92852 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077AF RID: 30639
		// (get) Token: 0x06016AB5 RID: 92853 RVA: 0x0032DAB7 File Offset: 0x0032BCB7
		internal override int ElementTypeId
		{
			get
			{
				return 11296;
			}
		}

		// Token: 0x06016AB6 RID: 92854 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016AB7 RID: 92855 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public Subtopic()
		{
		}

		// Token: 0x06016AB8 RID: 92856 RVA: 0x0032D835 File Offset: 0x0032BA35
		public Subtopic(string text)
			: base(text)
		{
		}

		// Token: 0x06016AB9 RID: 92857 RVA: 0x0032DAC0 File Offset: 0x0032BCC0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016ABA RID: 92858 RVA: 0x0032DADB File Offset: 0x0032BCDB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Subtopic>(deep);
		}

		// Token: 0x04009986 RID: 39302
		private const string tagName = "stp";

		// Token: 0x04009987 RID: 39303
		private const byte tagNsId = 22;

		// Token: 0x04009988 RID: 39304
		internal const int ElementTypeIdConst = 11296;
	}
}
