using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200250E RID: 9486
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowPercent : BooleanType
	{
		// Token: 0x17005443 RID: 21571
		// (get) Token: 0x06011A81 RID: 72321 RVA: 0x002F126B File Offset: 0x002EF46B
		public override string LocalName
		{
			get
			{
				return "showPercent";
			}
		}

		// Token: 0x17005444 RID: 21572
		// (get) Token: 0x06011A82 RID: 72322 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005445 RID: 21573
		// (get) Token: 0x06011A83 RID: 72323 RVA: 0x002F1272 File Offset: 0x002EF472
		internal override int ElementTypeId
		{
			get
			{
				return 10350;
			}
		}

		// Token: 0x06011A84 RID: 72324 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A86 RID: 72326 RVA: 0x002F1279 File Offset: 0x002EF479
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowPercent>(deep);
		}

		// Token: 0x04007BBE RID: 31678
		private const string tagName = "showPercent";

		// Token: 0x04007BBF RID: 31679
		private const byte tagNsId = 11;

		// Token: 0x04007BC0 RID: 31680
		internal const int ElementTypeIdConst = 10350;
	}
}
