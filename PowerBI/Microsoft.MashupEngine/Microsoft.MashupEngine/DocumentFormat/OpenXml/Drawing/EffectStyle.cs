using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002773 RID: 10099
	[ChildElementInfo(typeof(Scene3DType))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(EffectDag))]
	internal class EffectStyle : OpenXmlCompositeElement
	{
		// Token: 0x17006164 RID: 24932
		// (get) Token: 0x060137C0 RID: 79808 RVA: 0x003078F8 File Offset: 0x00305AF8
		public override string LocalName
		{
			get
			{
				return "effectStyle";
			}
		}

		// Token: 0x17006165 RID: 24933
		// (get) Token: 0x060137C1 RID: 79809 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006166 RID: 24934
		// (get) Token: 0x060137C2 RID: 79810 RVA: 0x003078FF File Offset: 0x00305AFF
		internal override int ElementTypeId
		{
			get
			{
				return 10139;
			}
		}

		// Token: 0x060137C3 RID: 79811 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060137C4 RID: 79812 RVA: 0x00293ECF File Offset: 0x002920CF
		public EffectStyle()
		{
		}

		// Token: 0x060137C5 RID: 79813 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EffectStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137C6 RID: 79814 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EffectStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137C7 RID: 79815 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EffectStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060137C8 RID: 79816 RVA: 0x00307908 File Offset: 0x00305B08
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "effectLst" == name)
			{
				return new EffectList();
			}
			if (10 == namespaceId && "effectDag" == name)
			{
				return new EffectDag();
			}
			if (10 == namespaceId && "scene3d" == name)
			{
				return new Scene3DType();
			}
			if (10 == namespaceId && "sp3d" == name)
			{
				return new Shape3DType();
			}
			return null;
		}

		// Token: 0x060137C9 RID: 79817 RVA: 0x00307976 File Offset: 0x00305B76
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EffectStyle>(deep);
		}

		// Token: 0x0400866B RID: 34411
		private const string tagName = "effectStyle";

		// Token: 0x0400866C RID: 34412
		private const byte tagNsId = 10;

		// Token: 0x0400866D RID: 34413
		internal const int ElementTypeIdConst = 10139;
	}
}
