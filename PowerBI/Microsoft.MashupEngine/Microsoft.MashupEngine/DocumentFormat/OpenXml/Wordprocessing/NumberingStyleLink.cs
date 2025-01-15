using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E86 RID: 11910
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingStyleLink : String253Type
	{
		// Token: 0x17008AFB RID: 35579
		// (get) Token: 0x060194E6 RID: 103654 RVA: 0x00348696 File Offset: 0x00346896
		public override string LocalName
		{
			get
			{
				return "numStyleLink";
			}
		}

		// Token: 0x17008AFC RID: 35580
		// (get) Token: 0x060194E7 RID: 103655 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AFD RID: 35581
		// (get) Token: 0x060194E8 RID: 103656 RVA: 0x0034869D File Offset: 0x0034689D
		internal override int ElementTypeId
		{
			get
			{
				return 11879;
			}
		}

		// Token: 0x060194E9 RID: 103657 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060194EB RID: 103659 RVA: 0x003486A4 File Offset: 0x003468A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingStyleLink>(deep);
		}

		// Token: 0x0400A838 RID: 43064
		private const string tagName = "numStyleLink";

		// Token: 0x0400A839 RID: 43065
		private const byte tagNsId = 23;

		// Token: 0x0400A83A RID: 43066
		internal const int ElementTypeIdConst = 11879;
	}
}
