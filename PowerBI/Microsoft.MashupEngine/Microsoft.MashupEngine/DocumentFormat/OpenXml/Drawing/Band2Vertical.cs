using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002809 RID: 10249
	[GeneratedCode("DomGen", "2.0")]
	internal class Band2Vertical : TablePartStyleType
	{
		// Token: 0x1700654A RID: 25930
		// (get) Token: 0x060140A8 RID: 82088 RVA: 0x0030EAD4 File Offset: 0x0030CCD4
		public override string LocalName
		{
			get
			{
				return "band2V";
			}
		}

		// Token: 0x1700654B RID: 25931
		// (get) Token: 0x060140A9 RID: 82089 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700654C RID: 25932
		// (get) Token: 0x060140AA RID: 82090 RVA: 0x0030EADB File Offset: 0x0030CCDB
		internal override int ElementTypeId
		{
			get
			{
				return 10284;
			}
		}

		// Token: 0x060140AB RID: 82091 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060140AC RID: 82092 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public Band2Vertical()
		{
		}

		// Token: 0x060140AD RID: 82093 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public Band2Vertical(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140AE RID: 82094 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public Band2Vertical(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140AF RID: 82095 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public Band2Vertical(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060140B0 RID: 82096 RVA: 0x0030EAE2 File Offset: 0x0030CCE2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Band2Vertical>(deep);
		}

		// Token: 0x040088C4 RID: 35012
		private const string tagName = "band2V";

		// Token: 0x040088C5 RID: 35013
		private const byte tagNsId = 10;

		// Token: 0x040088C6 RID: 35014
		internal const int ElementTypeIdConst = 10284;
	}
}
