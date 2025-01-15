using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002546 RID: 9542
	[GeneratedCode("DomGen", "2.0")]
	internal class FormatId : UnsignedIntegerType
	{
		// Token: 0x170054F7 RID: 21751
		// (get) Token: 0x06011C09 RID: 72713 RVA: 0x002F19E0 File Offset: 0x002EFBE0
		public override string LocalName
		{
			get
			{
				return "fmtId";
			}
		}

		// Token: 0x170054F8 RID: 21752
		// (get) Token: 0x06011C0A RID: 72714 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054F9 RID: 21753
		// (get) Token: 0x06011C0B RID: 72715 RVA: 0x002F19E7 File Offset: 0x002EFBE7
		internal override int ElementTypeId
		{
			get
			{
				return 10528;
			}
		}

		// Token: 0x06011C0C RID: 72716 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011C0E RID: 72718 RVA: 0x002F19EE File Offset: 0x002EFBEE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormatId>(deep);
		}

		// Token: 0x04007C68 RID: 31848
		private const string tagName = "fmtId";

		// Token: 0x04007C69 RID: 31849
		private const byte tagNsId = 11;

		// Token: 0x04007C6A RID: 31850
		internal const int ElementTypeIdConst = 10528;
	}
}
