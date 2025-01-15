using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025C8 RID: 9672
	[GeneratedCode("DomGen", "2.0")]
	internal class SideWall : SurfaceType
	{
		// Token: 0x170057B1 RID: 22449
		// (get) Token: 0x06012225 RID: 74277 RVA: 0x002F5EF6 File Offset: 0x002F40F6
		public override string LocalName
		{
			get
			{
				return "sideWall";
			}
		}

		// Token: 0x170057B2 RID: 22450
		// (get) Token: 0x06012226 RID: 74278 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170057B3 RID: 22451
		// (get) Token: 0x06012227 RID: 74279 RVA: 0x002F5EFD File Offset: 0x002F40FD
		internal override int ElementTypeId
		{
			get
			{
				return 10498;
			}
		}

		// Token: 0x06012228 RID: 74280 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012229 RID: 74281 RVA: 0x002F5ECA File Offset: 0x002F40CA
		public SideWall()
		{
		}

		// Token: 0x0601222A RID: 74282 RVA: 0x002F5ED2 File Offset: 0x002F40D2
		public SideWall(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601222B RID: 74283 RVA: 0x002F5EDB File Offset: 0x002F40DB
		public SideWall(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601222C RID: 74284 RVA: 0x002F5EE4 File Offset: 0x002F40E4
		public SideWall(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601222D RID: 74285 RVA: 0x002F5F04 File Offset: 0x002F4104
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SideWall>(deep);
		}

		// Token: 0x04007E5E RID: 32350
		private const string tagName = "sideWall";

		// Token: 0x04007E5F RID: 32351
		private const byte tagNsId = 11;

		// Token: 0x04007E60 RID: 32352
		internal const int ElementTypeIdConst = 10498;
	}
}
