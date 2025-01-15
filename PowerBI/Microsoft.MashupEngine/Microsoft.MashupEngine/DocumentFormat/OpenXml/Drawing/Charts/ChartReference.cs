using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200256B RID: 9579
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartReference : RelationshipIdType
	{
		// Token: 0x170055C5 RID: 21957
		// (get) Token: 0x06011DCC RID: 73164 RVA: 0x002AC9FE File Offset: 0x002AABFE
		public override string LocalName
		{
			get
			{
				return "chart";
			}
		}

		// Token: 0x170055C6 RID: 21958
		// (get) Token: 0x06011DCD RID: 73165 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055C7 RID: 21959
		// (get) Token: 0x06011DCE RID: 73166 RVA: 0x002F331C File Offset: 0x002F151C
		internal override int ElementTypeId
		{
			get
			{
				return 10388;
			}
		}

		// Token: 0x06011DCF RID: 73167 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011DD1 RID: 73169 RVA: 0x002F332B File Offset: 0x002F152B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartReference>(deep);
		}

		// Token: 0x04007CF2 RID: 31986
		private const string tagName = "chart";

		// Token: 0x04007CF3 RID: 31987
		private const byte tagNsId = 11;

		// Token: 0x04007CF4 RID: 31988
		internal const int ElementTypeIdConst = 10388;
	}
}
