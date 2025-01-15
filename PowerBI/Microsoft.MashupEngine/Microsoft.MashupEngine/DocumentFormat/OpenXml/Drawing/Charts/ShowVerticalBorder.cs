using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002517 RID: 9495
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowVerticalBorder : BooleanType
	{
		// Token: 0x1700545E RID: 21598
		// (get) Token: 0x06011AB7 RID: 72375 RVA: 0x002F133A File Offset: 0x002EF53A
		public override string LocalName
		{
			get
			{
				return "showVertBorder";
			}
		}

		// Token: 0x1700545F RID: 21599
		// (get) Token: 0x06011AB8 RID: 72376 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005460 RID: 21600
		// (get) Token: 0x06011AB9 RID: 72377 RVA: 0x002F1341 File Offset: 0x002EF541
		internal override int ElementTypeId
		{
			get
			{
				return 10424;
			}
		}

		// Token: 0x06011ABA RID: 72378 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011ABC RID: 72380 RVA: 0x002F1348 File Offset: 0x002EF548
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowVerticalBorder>(deep);
		}

		// Token: 0x04007BD9 RID: 31705
		private const string tagName = "showVertBorder";

		// Token: 0x04007BDA RID: 31706
		private const byte tagNsId = 11;

		// Token: 0x04007BDB RID: 31707
		internal const int ElementTypeIdConst = 10424;
	}
}
