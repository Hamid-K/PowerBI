using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C42 RID: 11330
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class FromMarker : MarkerType
	{
		// Token: 0x170081A9 RID: 33193
		// (get) Token: 0x06018040 RID: 98368 RVA: 0x002FCA49 File Offset: 0x002FAC49
		public override string LocalName
		{
			get
			{
				return "from";
			}
		}

		// Token: 0x170081AA RID: 33194
		// (get) Token: 0x06018041 RID: 98369 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081AB RID: 33195
		// (get) Token: 0x06018042 RID: 98370 RVA: 0x0033DB5C File Offset: 0x0033BD5C
		internal override int ElementTypeId
		{
			get
			{
				return 11311;
			}
		}

		// Token: 0x06018043 RID: 98371 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06018044 RID: 98372 RVA: 0x0033DB63 File Offset: 0x0033BD63
		public FromMarker()
		{
		}

		// Token: 0x06018045 RID: 98373 RVA: 0x0033DB6B File Offset: 0x0033BD6B
		public FromMarker(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018046 RID: 98374 RVA: 0x0033DB74 File Offset: 0x0033BD74
		public FromMarker(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018047 RID: 98375 RVA: 0x0033DB7D File Offset: 0x0033BD7D
		public FromMarker(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018048 RID: 98376 RVA: 0x0033DB86 File Offset: 0x0033BD86
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FromMarker>(deep);
		}

		// Token: 0x04009E7F RID: 40575
		private const string tagName = "from";

		// Token: 0x04009E80 RID: 40576
		private const byte tagNsId = 22;

		// Token: 0x04009E81 RID: 40577
		internal const int ElementTypeIdConst = 11311;
	}
}
