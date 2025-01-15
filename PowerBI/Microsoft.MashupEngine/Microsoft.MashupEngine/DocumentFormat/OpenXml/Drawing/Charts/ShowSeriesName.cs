using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200250D RID: 9485
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowSeriesName : BooleanType
	{
		// Token: 0x17005440 RID: 21568
		// (get) Token: 0x06011A7B RID: 72315 RVA: 0x002F1254 File Offset: 0x002EF454
		public override string LocalName
		{
			get
			{
				return "showSerName";
			}
		}

		// Token: 0x17005441 RID: 21569
		// (get) Token: 0x06011A7C RID: 72316 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005442 RID: 21570
		// (get) Token: 0x06011A7D RID: 72317 RVA: 0x002F125B File Offset: 0x002EF45B
		internal override int ElementTypeId
		{
			get
			{
				return 10349;
			}
		}

		// Token: 0x06011A7E RID: 72318 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A80 RID: 72320 RVA: 0x002F1262 File Offset: 0x002EF462
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowSeriesName>(deep);
		}

		// Token: 0x04007BBB RID: 31675
		private const string tagName = "showSerName";

		// Token: 0x04007BBC RID: 31676
		private const byte tagNsId = 11;

		// Token: 0x04007BBD RID: 31677
		internal const int ElementTypeIdConst = 10349;
	}
}
