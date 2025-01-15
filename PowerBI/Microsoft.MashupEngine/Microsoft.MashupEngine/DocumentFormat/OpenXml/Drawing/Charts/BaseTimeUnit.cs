using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025F8 RID: 9720
	[GeneratedCode("DomGen", "2.0")]
	internal class BaseTimeUnit : TimeUnitType
	{
		// Token: 0x17005967 RID: 22887
		// (get) Token: 0x060125DF RID: 75231 RVA: 0x002FA41F File Offset: 0x002F861F
		public override string LocalName
		{
			get
			{
				return "baseTimeUnit";
			}
		}

		// Token: 0x17005968 RID: 22888
		// (get) Token: 0x060125E0 RID: 75232 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005969 RID: 22889
		// (get) Token: 0x060125E1 RID: 75233 RVA: 0x002FA426 File Offset: 0x002F8626
		internal override int ElementTypeId
		{
			get
			{
				return 10565;
			}
		}

		// Token: 0x060125E2 RID: 75234 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060125E4 RID: 75236 RVA: 0x002FA435 File Offset: 0x002F8635
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BaseTimeUnit>(deep);
		}

		// Token: 0x04007F42 RID: 32578
		private const string tagName = "baseTimeUnit";

		// Token: 0x04007F43 RID: 32579
		private const byte tagNsId = 11;

		// Token: 0x04007F44 RID: 32580
		internal const int ElementTypeIdConst = 10565;
	}
}
