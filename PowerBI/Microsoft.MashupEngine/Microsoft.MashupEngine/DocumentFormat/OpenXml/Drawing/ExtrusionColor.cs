using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200273F RID: 10047
	[GeneratedCode("DomGen", "2.0")]
	internal class ExtrusionColor : ColorType
	{
		// Token: 0x1700605B RID: 24667
		// (get) Token: 0x06013551 RID: 79185 RVA: 0x002EEDCD File Offset: 0x002ECFCD
		public override string LocalName
		{
			get
			{
				return "extrusionClr";
			}
		}

		// Token: 0x1700605C RID: 24668
		// (get) Token: 0x06013552 RID: 79186 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700605D RID: 24669
		// (get) Token: 0x06013553 RID: 79187 RVA: 0x003062D6 File Offset: 0x003044D6
		internal override int ElementTypeId
		{
			get
			{
				return 10203;
			}
		}

		// Token: 0x06013554 RID: 79188 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013555 RID: 79189 RVA: 0x003062AA File Offset: 0x003044AA
		public ExtrusionColor()
		{
		}

		// Token: 0x06013556 RID: 79190 RVA: 0x003062B2 File Offset: 0x003044B2
		public ExtrusionColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013557 RID: 79191 RVA: 0x003062BB File Offset: 0x003044BB
		public ExtrusionColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013558 RID: 79192 RVA: 0x003062C4 File Offset: 0x003044C4
		public ExtrusionColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013559 RID: 79193 RVA: 0x003062DD File Offset: 0x003044DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtrusionColor>(deep);
		}

		// Token: 0x040085AB RID: 34219
		private const string tagName = "extrusionClr";

		// Token: 0x040085AC RID: 34220
		private const byte tagNsId = 10;

		// Token: 0x040085AD RID: 34221
		internal const int ElementTypeIdConst = 10203;
	}
}
