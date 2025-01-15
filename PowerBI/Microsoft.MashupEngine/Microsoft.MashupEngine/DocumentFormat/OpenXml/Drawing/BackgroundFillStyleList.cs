using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002777 RID: 10103
	[ChildElementInfo(typeof(GroupFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	internal class BackgroundFillStyleList : OpenXmlCompositeElement
	{
		// Token: 0x17006170 RID: 24944
		// (get) Token: 0x060137E8 RID: 79848 RVA: 0x00307A9B File Offset: 0x00305C9B
		public override string LocalName
		{
			get
			{
				return "bgFillStyleLst";
			}
		}

		// Token: 0x17006171 RID: 24945
		// (get) Token: 0x060137E9 RID: 79849 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006172 RID: 24946
		// (get) Token: 0x060137EA RID: 79850 RVA: 0x00307AA2 File Offset: 0x00305CA2
		internal override int ElementTypeId
		{
			get
			{
				return 10143;
			}
		}

		// Token: 0x060137EB RID: 79851 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060137EC RID: 79852 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackgroundFillStyleList()
		{
		}

		// Token: 0x060137ED RID: 79853 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackgroundFillStyleList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137EE RID: 79854 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackgroundFillStyleList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137EF RID: 79855 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackgroundFillStyleList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060137F0 RID: 79856 RVA: 0x00307AAC File Offset: 0x00305CAC
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

		// Token: 0x060137F1 RID: 79857 RVA: 0x00307B4A File Offset: 0x00305D4A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackgroundFillStyleList>(deep);
		}

		// Token: 0x04008677 RID: 34423
		private const string tagName = "bgFillStyleLst";

		// Token: 0x04008678 RID: 34424
		private const byte tagNsId = 10;

		// Token: 0x04008679 RID: 34425
		internal const int ElementTypeIdConst = 10143;
	}
}
