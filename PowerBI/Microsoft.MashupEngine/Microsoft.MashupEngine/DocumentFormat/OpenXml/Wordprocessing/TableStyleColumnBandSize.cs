using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F07 RID: 12039
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyleColumnBandSize : UnsignedDecimalNumberMax3Type
	{
		// Token: 0x17008DEE RID: 36334
		// (get) Token: 0x06019B2F RID: 105263 RVA: 0x003540C3 File Offset: 0x003522C3
		public override string LocalName
		{
			get
			{
				return "tblStyleColBandSize";
			}
		}

		// Token: 0x17008DEF RID: 36335
		// (get) Token: 0x06019B30 RID: 105264 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DF0 RID: 36336
		// (get) Token: 0x06019B31 RID: 105265 RVA: 0x003540CA File Offset: 0x003522CA
		internal override int ElementTypeId
		{
			get
			{
				return 11676;
			}
		}

		// Token: 0x06019B32 RID: 105266 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019B34 RID: 105268 RVA: 0x003540D1 File Offset: 0x003522D1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleColumnBandSize>(deep);
		}

		// Token: 0x0400AA2F RID: 43567
		private const string tagName = "tblStyleColBandSize";

		// Token: 0x0400AA30 RID: 43568
		private const byte tagNsId = 23;

		// Token: 0x0400AA31 RID: 43569
		internal const int ElementTypeIdConst = 11676;
	}
}
