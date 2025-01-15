using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002810 RID: 10256
	[GeneratedCode("DomGen", "2.0")]
	internal class NortheastCell : TablePartStyleType
	{
		// Token: 0x1700655F RID: 25951
		// (get) Token: 0x060140E7 RID: 82151 RVA: 0x0030EB75 File Offset: 0x0030CD75
		public override string LocalName
		{
			get
			{
				return "neCell";
			}
		}

		// Token: 0x17006560 RID: 25952
		// (get) Token: 0x060140E8 RID: 82152 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006561 RID: 25953
		// (get) Token: 0x060140E9 RID: 82153 RVA: 0x0030EB7C File Offset: 0x0030CD7C
		internal override int ElementTypeId
		{
			get
			{
				return 10291;
			}
		}

		// Token: 0x060140EA RID: 82154 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060140EB RID: 82155 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public NortheastCell()
		{
		}

		// Token: 0x060140EC RID: 82156 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public NortheastCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140ED RID: 82157 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public NortheastCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140EE RID: 82158 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public NortheastCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060140EF RID: 82159 RVA: 0x0030EB83 File Offset: 0x0030CD83
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NortheastCell>(deep);
		}

		// Token: 0x040088D9 RID: 35033
		private const string tagName = "neCell";

		// Token: 0x040088DA RID: 35034
		private const byte tagNsId = 10;

		// Token: 0x040088DB RID: 35035
		internal const int ElementTypeIdConst = 10291;
	}
}
