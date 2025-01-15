using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200255D RID: 9565
	[GeneratedCode("DomGen", "2.0")]
	internal class Top : DoubleType
	{
		// Token: 0x17005593 RID: 21907
		// (get) Token: 0x06011D5B RID: 73051 RVA: 0x002F2F05 File Offset: 0x002F1105
		public override string LocalName
		{
			get
			{
				return "y";
			}
		}

		// Token: 0x17005594 RID: 21908
		// (get) Token: 0x06011D5C RID: 73052 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005595 RID: 21909
		// (get) Token: 0x06011D5D RID: 73053 RVA: 0x002F2F0C File Offset: 0x002F110C
		internal override int ElementTypeId
		{
			get
			{
				return 10412;
			}
		}

		// Token: 0x06011D5E RID: 73054 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D60 RID: 73056 RVA: 0x002F2F13 File Offset: 0x002F1113
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Top>(deep);
		}

		// Token: 0x04007CC7 RID: 31943
		private const string tagName = "y";

		// Token: 0x04007CC8 RID: 31944
		private const byte tagNsId = 11;

		// Token: 0x04007CC9 RID: 31945
		internal const int ElementTypeIdConst = 10412;
	}
}
