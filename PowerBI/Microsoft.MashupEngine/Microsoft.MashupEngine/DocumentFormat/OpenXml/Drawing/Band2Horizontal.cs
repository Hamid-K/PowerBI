using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002807 RID: 10247
	[GeneratedCode("DomGen", "2.0")]
	internal class Band2Horizontal : TablePartStyleType
	{
		// Token: 0x17006544 RID: 25924
		// (get) Token: 0x06014096 RID: 82070 RVA: 0x0030EAA6 File Offset: 0x0030CCA6
		public override string LocalName
		{
			get
			{
				return "band2H";
			}
		}

		// Token: 0x17006545 RID: 25925
		// (get) Token: 0x06014097 RID: 82071 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006546 RID: 25926
		// (get) Token: 0x06014098 RID: 82072 RVA: 0x0030EAAD File Offset: 0x0030CCAD
		internal override int ElementTypeId
		{
			get
			{
				return 10282;
			}
		}

		// Token: 0x06014099 RID: 82073 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601409A RID: 82074 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public Band2Horizontal()
		{
		}

		// Token: 0x0601409B RID: 82075 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public Band2Horizontal(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601409C RID: 82076 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public Band2Horizontal(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601409D RID: 82077 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public Band2Horizontal(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601409E RID: 82078 RVA: 0x0030EAB4 File Offset: 0x0030CCB4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Band2Horizontal>(deep);
		}

		// Token: 0x040088BE RID: 35006
		private const string tagName = "band2H";

		// Token: 0x040088BF RID: 35007
		private const byte tagNsId = 10;

		// Token: 0x040088C0 RID: 35008
		internal const int ElementTypeIdConst = 10282;
	}
}
