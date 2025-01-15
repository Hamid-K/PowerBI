using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002774 RID: 10100
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(GroupFill))]
	internal class FillStyleList : OpenXmlCompositeElement
	{
		// Token: 0x17006167 RID: 24935
		// (get) Token: 0x060137CA RID: 79818 RVA: 0x0030797F File Offset: 0x00305B7F
		public override string LocalName
		{
			get
			{
				return "fillStyleLst";
			}
		}

		// Token: 0x17006168 RID: 24936
		// (get) Token: 0x060137CB RID: 79819 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006169 RID: 24937
		// (get) Token: 0x060137CC RID: 79820 RVA: 0x00307986 File Offset: 0x00305B86
		internal override int ElementTypeId
		{
			get
			{
				return 10140;
			}
		}

		// Token: 0x060137CD RID: 79821 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060137CE RID: 79822 RVA: 0x00293ECF File Offset: 0x002920CF
		public FillStyleList()
		{
		}

		// Token: 0x060137CF RID: 79823 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FillStyleList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137D0 RID: 79824 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FillStyleList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137D1 RID: 79825 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FillStyleList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060137D2 RID: 79826 RVA: 0x00307990 File Offset: 0x00305B90
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "grpFill" == name)
			{
				return new GroupFill();
			}
			return null;
		}

		// Token: 0x060137D3 RID: 79827 RVA: 0x00307A2E File Offset: 0x00305C2E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillStyleList>(deep);
		}

		// Token: 0x0400866E RID: 34414
		private const string tagName = "fillStyleLst";

		// Token: 0x0400866F RID: 34415
		private const byte tagNsId = 10;

		// Token: 0x04008670 RID: 34416
		internal const int ElementTypeIdConst = 10140;
	}
}
