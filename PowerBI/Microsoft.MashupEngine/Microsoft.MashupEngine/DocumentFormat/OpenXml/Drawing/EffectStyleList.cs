using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002776 RID: 10102
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EffectStyle))]
	internal class EffectStyleList : OpenXmlCompositeElement
	{
		// Token: 0x1700616D RID: 24941
		// (get) Token: 0x060137DE RID: 79838 RVA: 0x00307A69 File Offset: 0x00305C69
		public override string LocalName
		{
			get
			{
				return "effectStyleLst";
			}
		}

		// Token: 0x1700616E RID: 24942
		// (get) Token: 0x060137DF RID: 79839 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700616F RID: 24943
		// (get) Token: 0x060137E0 RID: 79840 RVA: 0x00307A70 File Offset: 0x00305C70
		internal override int ElementTypeId
		{
			get
			{
				return 10142;
			}
		}

		// Token: 0x060137E1 RID: 79841 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060137E2 RID: 79842 RVA: 0x00293ECF File Offset: 0x002920CF
		public EffectStyleList()
		{
		}

		// Token: 0x060137E3 RID: 79843 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EffectStyleList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137E4 RID: 79844 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EffectStyleList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137E5 RID: 79845 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EffectStyleList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060137E6 RID: 79846 RVA: 0x00307A77 File Offset: 0x00305C77
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "effectStyle" == name)
			{
				return new EffectStyle();
			}
			return null;
		}

		// Token: 0x060137E7 RID: 79847 RVA: 0x00307A92 File Offset: 0x00305C92
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EffectStyleList>(deep);
		}

		// Token: 0x04008674 RID: 34420
		private const string tagName = "effectStyleLst";

		// Token: 0x04008675 RID: 34421
		private const byte tagNsId = 10;

		// Token: 0x04008676 RID: 34422
		internal const int ElementTypeIdConst = 10142;
	}
}
