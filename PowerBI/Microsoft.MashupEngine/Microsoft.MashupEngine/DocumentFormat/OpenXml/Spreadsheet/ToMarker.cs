using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C43 RID: 11331
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ToMarker : MarkerType
	{
		// Token: 0x170081AC RID: 33196
		// (get) Token: 0x06018049 RID: 98377 RVA: 0x002FCA83 File Offset: 0x002FAC83
		public override string LocalName
		{
			get
			{
				return "to";
			}
		}

		// Token: 0x170081AD RID: 33197
		// (get) Token: 0x0601804A RID: 98378 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081AE RID: 33198
		// (get) Token: 0x0601804B RID: 98379 RVA: 0x0033DB8F File Offset: 0x0033BD8F
		internal override int ElementTypeId
		{
			get
			{
				return 11312;
			}
		}

		// Token: 0x0601804C RID: 98380 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601804D RID: 98381 RVA: 0x0033DB63 File Offset: 0x0033BD63
		public ToMarker()
		{
		}

		// Token: 0x0601804E RID: 98382 RVA: 0x0033DB6B File Offset: 0x0033BD6B
		public ToMarker(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601804F RID: 98383 RVA: 0x0033DB74 File Offset: 0x0033BD74
		public ToMarker(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018050 RID: 98384 RVA: 0x0033DB7D File Offset: 0x0033BD7D
		public ToMarker(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018051 RID: 98385 RVA: 0x0033DB96 File Offset: 0x0033BD96
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ToMarker>(deep);
		}

		// Token: 0x04009E82 RID: 40578
		private const string tagName = "to";

		// Token: 0x04009E83 RID: 40579
		private const byte tagNsId = 22;

		// Token: 0x04009E84 RID: 40580
		internal const int ElementTypeIdConst = 11312;
	}
}
