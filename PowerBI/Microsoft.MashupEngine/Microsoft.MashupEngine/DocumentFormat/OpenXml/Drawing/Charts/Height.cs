using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200255F RID: 9567
	[GeneratedCode("DomGen", "2.0")]
	internal class Height : DoubleType
	{
		// Token: 0x17005599 RID: 21913
		// (get) Token: 0x06011D67 RID: 73063 RVA: 0x002C804C File Offset: 0x002C624C
		public override string LocalName
		{
			get
			{
				return "h";
			}
		}

		// Token: 0x1700559A RID: 21914
		// (get) Token: 0x06011D68 RID: 73064 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700559B RID: 21915
		// (get) Token: 0x06011D69 RID: 73065 RVA: 0x002F2F33 File Offset: 0x002F1133
		internal override int ElementTypeId
		{
			get
			{
				return 10414;
			}
		}

		// Token: 0x06011D6A RID: 73066 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D6C RID: 73068 RVA: 0x002F2F3A File Offset: 0x002F113A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Height>(deep);
		}

		// Token: 0x04007CCD RID: 31949
		private const string tagName = "h";

		// Token: 0x04007CCE RID: 31950
		private const byte tagNsId = 11;

		// Token: 0x04007CCF RID: 31951
		internal const int ElementTypeIdConst = 10414;
	}
}
