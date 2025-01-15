using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002837 RID: 10295
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(TransformGroup))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class VisualGroupShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006627 RID: 26151
		// (get) Token: 0x060142E5 RID: 82661 RVA: 0x002DF441 File Offset: 0x002DD641
		public override string LocalName
		{
			get
			{
				return "grpSpPr";
			}
		}

		// Token: 0x17006628 RID: 26152
		// (get) Token: 0x060142E6 RID: 82662 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006629 RID: 26153
		// (get) Token: 0x060142E7 RID: 82663 RVA: 0x00310039 File Offset: 0x0030E239
		internal override int ElementTypeId
		{
			get
			{
				return 10331;
			}
		}

		// Token: 0x060142E8 RID: 82664 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700662A RID: 26154
		// (get) Token: 0x060142E9 RID: 82665 RVA: 0x00310040 File Offset: 0x0030E240
		internal override string[] AttributeTagNames
		{
			get
			{
				return VisualGroupShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x1700662B RID: 26155
		// (get) Token: 0x060142EA RID: 82666 RVA: 0x00310047 File Offset: 0x0030E247
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VisualGroupShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700662C RID: 26156
		// (get) Token: 0x060142EB RID: 82667 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x060142EC RID: 82668 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bwMode")]
		public EnumValue<BlackWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackWhiteModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060142ED RID: 82669 RVA: 0x00293ECF File Offset: 0x002920CF
		public VisualGroupShapeProperties()
		{
		}

		// Token: 0x060142EE RID: 82670 RVA: 0x00293ED7 File Offset: 0x002920D7
		public VisualGroupShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060142EF RID: 82671 RVA: 0x00293EE0 File Offset: 0x002920E0
		public VisualGroupShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060142F0 RID: 82672 RVA: 0x00293EE9 File Offset: 0x002920E9
		public VisualGroupShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060142F1 RID: 82673 RVA: 0x00310050 File Offset: 0x0030E250
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "xfrm" == name)
			{
				return new TransformGroup();
			}
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
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700662D RID: 26157
		// (get) Token: 0x060142F2 RID: 82674 RVA: 0x00310166 File Offset: 0x0030E366
		internal override string[] ElementTagNames
		{
			get
			{
				return VisualGroupShapeProperties.eleTagNames;
			}
		}

		// Token: 0x1700662E RID: 26158
		// (get) Token: 0x060142F3 RID: 82675 RVA: 0x0031016D File Offset: 0x0030E36D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return VisualGroupShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700662F RID: 26159
		// (get) Token: 0x060142F4 RID: 82676 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006630 RID: 26160
		// (get) Token: 0x060142F5 RID: 82677 RVA: 0x002DF584 File Offset: 0x002DD784
		// (set) Token: 0x060142F6 RID: 82678 RVA: 0x002DF58D File Offset: 0x002DD78D
		public TransformGroup TransformGroup
		{
			get
			{
				return base.GetElement<TransformGroup>(0);
			}
			set
			{
				base.SetElement<TransformGroup>(0, value);
			}
		}

		// Token: 0x060142F7 RID: 82679 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060142F8 RID: 82680 RVA: 0x00310174 File Offset: 0x0030E374
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VisualGroupShapeProperties>(deep);
		}

		// Token: 0x060142F9 RID: 82681 RVA: 0x00310180 File Offset: 0x0030E380
		// Note: this type is marked as 'beforefieldinit'.
		static VisualGroupShapeProperties()
		{
			byte[] array = new byte[1];
			VisualGroupShapeProperties.attributeNamespaceIds = array;
			VisualGroupShapeProperties.eleTagNames = new string[]
			{
				"xfrm", "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "grpFill", "effectLst", "effectDag", "scene3d",
				"extLst"
			};
			VisualGroupShapeProperties.eleNamespaceIds = new byte[]
			{
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10
			};
		}

		// Token: 0x04008969 RID: 35177
		private const string tagName = "grpSpPr";

		// Token: 0x0400896A RID: 35178
		private const byte tagNsId = 10;

		// Token: 0x0400896B RID: 35179
		internal const int ElementTypeIdConst = 10331;

		// Token: 0x0400896C RID: 35180
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x0400896D RID: 35181
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400896E RID: 35182
		private static readonly string[] eleTagNames;

		// Token: 0x0400896F RID: 35183
		private static readonly byte[] eleNamespaceIds;
	}
}
