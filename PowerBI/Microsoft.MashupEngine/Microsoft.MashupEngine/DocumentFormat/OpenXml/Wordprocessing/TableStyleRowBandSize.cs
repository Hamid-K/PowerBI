using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F06 RID: 12038
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyleRowBandSize : UnsignedDecimalNumberMax3Type
	{
		// Token: 0x17008DEB RID: 36331
		// (get) Token: 0x06019B29 RID: 105257 RVA: 0x003540A4 File Offset: 0x003522A4
		public override string LocalName
		{
			get
			{
				return "tblStyleRowBandSize";
			}
		}

		// Token: 0x17008DEC RID: 36332
		// (get) Token: 0x06019B2A RID: 105258 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DED RID: 36333
		// (get) Token: 0x06019B2B RID: 105259 RVA: 0x003540AB File Offset: 0x003522AB
		internal override int ElementTypeId
		{
			get
			{
				return 11675;
			}
		}

		// Token: 0x06019B2C RID: 105260 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019B2E RID: 105262 RVA: 0x003540BA File Offset: 0x003522BA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleRowBandSize>(deep);
		}

		// Token: 0x0400AA2C RID: 43564
		private const string tagName = "tblStyleRowBandSize";

		// Token: 0x0400AA2D RID: 43565
		private const byte tagNsId = 23;

		// Token: 0x0400AA2E RID: 43566
		internal const int ElementTypeIdConst = 11675;
	}
}
