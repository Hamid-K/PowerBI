using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025C9 RID: 9673
	[GeneratedCode("DomGen", "2.0")]
	internal class BackWall : SurfaceType
	{
		// Token: 0x170057B4 RID: 22452
		// (get) Token: 0x0601222E RID: 74286 RVA: 0x002F5F0D File Offset: 0x002F410D
		public override string LocalName
		{
			get
			{
				return "backWall";
			}
		}

		// Token: 0x170057B5 RID: 22453
		// (get) Token: 0x0601222F RID: 74287 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170057B6 RID: 22454
		// (get) Token: 0x06012230 RID: 74288 RVA: 0x002F5F14 File Offset: 0x002F4114
		internal override int ElementTypeId
		{
			get
			{
				return 10499;
			}
		}

		// Token: 0x06012231 RID: 74289 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012232 RID: 74290 RVA: 0x002F5ECA File Offset: 0x002F40CA
		public BackWall()
		{
		}

		// Token: 0x06012233 RID: 74291 RVA: 0x002F5ED2 File Offset: 0x002F40D2
		public BackWall(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012234 RID: 74292 RVA: 0x002F5EDB File Offset: 0x002F40DB
		public BackWall(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012235 RID: 74293 RVA: 0x002F5EE4 File Offset: 0x002F40E4
		public BackWall(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012236 RID: 74294 RVA: 0x002F5F1B File Offset: 0x002F411B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackWall>(deep);
		}

		// Token: 0x04007E61 RID: 32353
		private const string tagName = "backWall";

		// Token: 0x04007E62 RID: 32354
		private const byte tagNsId = 11;

		// Token: 0x04007E63 RID: 32355
		internal const int ElementTypeIdConst = 10499;
	}
}
