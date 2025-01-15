using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A9E RID: 10910
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeTree : GroupShapeType
	{
		// Token: 0x17007430 RID: 29744
		// (get) Token: 0x0601629E RID: 90782 RVA: 0x002DF986 File Offset: 0x002DDB86
		public override string LocalName
		{
			get
			{
				return "spTree";
			}
		}

		// Token: 0x17007431 RID: 29745
		// (get) Token: 0x0601629F RID: 90783 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007432 RID: 29746
		// (get) Token: 0x060162A0 RID: 90784 RVA: 0x003272F6 File Offset: 0x003254F6
		internal override int ElementTypeId
		{
			get
			{
				return 12324;
			}
		}

		// Token: 0x060162A1 RID: 90785 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060162A2 RID: 90786 RVA: 0x003272FD File Offset: 0x003254FD
		public ShapeTree()
		{
		}

		// Token: 0x060162A3 RID: 90787 RVA: 0x00327305 File Offset: 0x00325505
		public ShapeTree(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162A4 RID: 90788 RVA: 0x0032730E File Offset: 0x0032550E
		public ShapeTree(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162A5 RID: 90789 RVA: 0x00327317 File Offset: 0x00325517
		public ShapeTree(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060162A6 RID: 90790 RVA: 0x00327320 File Offset: 0x00325520
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeTree>(deep);
		}

		// Token: 0x04009680 RID: 38528
		private const string tagName = "spTree";

		// Token: 0x04009681 RID: 38529
		private const byte tagNsId = 24;

		// Token: 0x04009682 RID: 38530
		internal const int ElementTypeIdConst = 12324;
	}
}
