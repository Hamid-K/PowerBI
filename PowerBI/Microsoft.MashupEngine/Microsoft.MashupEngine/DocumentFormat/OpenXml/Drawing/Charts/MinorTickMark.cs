using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002557 RID: 9559
	[GeneratedCode("DomGen", "2.0")]
	internal class MinorTickMark : TickMarkType
	{
		// Token: 0x1700557B RID: 21883
		// (get) Token: 0x06011D2A RID: 73002 RVA: 0x002F2D4E File Offset: 0x002F0F4E
		public override string LocalName
		{
			get
			{
				return "minorTickMark";
			}
		}

		// Token: 0x1700557C RID: 21884
		// (get) Token: 0x06011D2B RID: 73003 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700557D RID: 21885
		// (get) Token: 0x06011D2C RID: 73004 RVA: 0x002F2D55 File Offset: 0x002F0F55
		internal override int ElementTypeId
		{
			get
			{
				return 10381;
			}
		}

		// Token: 0x06011D2D RID: 73005 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D2F RID: 73007 RVA: 0x002F2D5C File Offset: 0x002F0F5C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MinorTickMark>(deep);
		}

		// Token: 0x04007CB2 RID: 31922
		private const string tagName = "minorTickMark";

		// Token: 0x04007CB3 RID: 31923
		private const byte tagNsId = 11;

		// Token: 0x04007CB4 RID: 31924
		internal const int ElementTypeIdConst = 10381;
	}
}
