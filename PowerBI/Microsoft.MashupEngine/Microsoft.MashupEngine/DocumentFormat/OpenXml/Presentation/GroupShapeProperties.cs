using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AA3 RID: 10915
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(EffectList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TransformGroup))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(Scene3DType))]
	internal class GroupShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007445 RID: 29765
		// (get) Token: 0x060162D8 RID: 90840 RVA: 0x002DF441 File Offset: 0x002DD641
		public override string LocalName
		{
			get
			{
				return "grpSpPr";
			}
		}

		// Token: 0x17007446 RID: 29766
		// (get) Token: 0x060162D9 RID: 90841 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007447 RID: 29767
		// (get) Token: 0x060162DA RID: 90842 RVA: 0x00327468 File Offset: 0x00325668
		internal override int ElementTypeId
		{
			get
			{
				return 12328;
			}
		}

		// Token: 0x060162DB RID: 90843 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007448 RID: 29768
		// (get) Token: 0x060162DC RID: 90844 RVA: 0x0032746F File Offset: 0x0032566F
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x17007449 RID: 29769
		// (get) Token: 0x060162DD RID: 90845 RVA: 0x00327476 File Offset: 0x00325676
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700744A RID: 29770
		// (get) Token: 0x060162DE RID: 90846 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x060162DF RID: 90847 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060162E0 RID: 90848 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupShapeProperties()
		{
		}

		// Token: 0x060162E1 RID: 90849 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162E2 RID: 90850 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162E3 RID: 90851 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060162E4 RID: 90852 RVA: 0x00327480 File Offset: 0x00325680
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

		// Token: 0x1700744B RID: 29771
		// (get) Token: 0x060162E5 RID: 90853 RVA: 0x00327596 File Offset: 0x00325796
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShapeProperties.eleTagNames;
			}
		}

		// Token: 0x1700744C RID: 29772
		// (get) Token: 0x060162E6 RID: 90854 RVA: 0x0032759D File Offset: 0x0032579D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700744D RID: 29773
		// (get) Token: 0x060162E7 RID: 90855 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700744E RID: 29774
		// (get) Token: 0x060162E8 RID: 90856 RVA: 0x002DF584 File Offset: 0x002DD784
		// (set) Token: 0x060162E9 RID: 90857 RVA: 0x002DF58D File Offset: 0x002DD78D
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

		// Token: 0x060162EA RID: 90858 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060162EB RID: 90859 RVA: 0x003275A4 File Offset: 0x003257A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShapeProperties>(deep);
		}

		// Token: 0x060162EC RID: 90860 RVA: 0x003275B0 File Offset: 0x003257B0
		// Note: this type is marked as 'beforefieldinit'.
		static GroupShapeProperties()
		{
			byte[] array = new byte[1];
			GroupShapeProperties.attributeNamespaceIds = array;
			GroupShapeProperties.eleTagNames = new string[]
			{
				"xfrm", "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "grpFill", "effectLst", "effectDag", "scene3d",
				"extLst"
			};
			GroupShapeProperties.eleNamespaceIds = new byte[]
			{
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10
			};
		}

		// Token: 0x04009691 RID: 38545
		private const string tagName = "grpSpPr";

		// Token: 0x04009692 RID: 38546
		private const byte tagNsId = 24;

		// Token: 0x04009693 RID: 38547
		internal const int ElementTypeIdConst = 12328;

		// Token: 0x04009694 RID: 38548
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x04009695 RID: 38549
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009696 RID: 38550
		private static readonly string[] eleTagNames;

		// Token: 0x04009697 RID: 38551
		private static readonly byte[] eleNamespaceIds;
	}
}
