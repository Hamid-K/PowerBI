using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002814 RID: 10260
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x1700657B RID: 25979
		// (get) Token: 0x0601412A RID: 82218 RVA: 0x0030F03A File Offset: 0x0030D23A
		public override string LocalName
		{
			get
			{
				return "defPPr";
			}
		}

		// Token: 0x1700657C RID: 25980
		// (get) Token: 0x0601412B RID: 82219 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700657D RID: 25981
		// (get) Token: 0x0601412C RID: 82220 RVA: 0x0030F041 File Offset: 0x0030D241
		internal override int ElementTypeId
		{
			get
			{
				return 10296;
			}
		}

		// Token: 0x0601412D RID: 82221 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601412E RID: 82222 RVA: 0x0030F00E File Offset: 0x0030D20E
		public DefaultParagraphProperties()
		{
		}

		// Token: 0x0601412F RID: 82223 RVA: 0x0030F016 File Offset: 0x0030D216
		public DefaultParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014130 RID: 82224 RVA: 0x0030F01F File Offset: 0x0030D21F
		public DefaultParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014131 RID: 82225 RVA: 0x0030F028 File Offset: 0x0030D228
		public DefaultParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014132 RID: 82226 RVA: 0x0030F048 File Offset: 0x0030D248
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultParagraphProperties>(deep);
		}

		// Token: 0x040088E6 RID: 35046
		private const string tagName = "defPPr";

		// Token: 0x040088E7 RID: 35047
		private const byte tagNsId = 10;

		// Token: 0x040088E8 RID: 35048
		internal const int ElementTypeIdConst = 10296;
	}
}
