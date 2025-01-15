using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200273E RID: 10046
	[GeneratedCode("DomGen", "2.0")]
	internal class BulletColor : ColorType
	{
		// Token: 0x17006058 RID: 24664
		// (get) Token: 0x06013548 RID: 79176 RVA: 0x0030629C File Offset: 0x0030449C
		public override string LocalName
		{
			get
			{
				return "buClr";
			}
		}

		// Token: 0x17006059 RID: 24665
		// (get) Token: 0x06013549 RID: 79177 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700605A RID: 24666
		// (get) Token: 0x0601354A RID: 79178 RVA: 0x003062A3 File Offset: 0x003044A3
		internal override int ElementTypeId
		{
			get
			{
				return 10103;
			}
		}

		// Token: 0x0601354B RID: 79179 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601354C RID: 79180 RVA: 0x003062AA File Offset: 0x003044AA
		public BulletColor()
		{
		}

		// Token: 0x0601354D RID: 79181 RVA: 0x003062B2 File Offset: 0x003044B2
		public BulletColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601354E RID: 79182 RVA: 0x003062BB File Offset: 0x003044BB
		public BulletColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601354F RID: 79183 RVA: 0x003062C4 File Offset: 0x003044C4
		public BulletColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013550 RID: 79184 RVA: 0x003062CD File Offset: 0x003044CD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BulletColor>(deep);
		}

		// Token: 0x040085A8 RID: 34216
		private const string tagName = "buClr";

		// Token: 0x040085A9 RID: 34217
		private const byte tagNsId = 10;

		// Token: 0x040085AA RID: 34218
		internal const int ElementTypeIdConst = 10103;
	}
}
