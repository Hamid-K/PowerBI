using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200289A RID: 10394
	[ChildElementInfo(typeof(Scene3DType))]
	[GeneratedCode("DomGen", "2.0")]
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
	internal class GroupShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170067FA RID: 26618
		// (get) Token: 0x060146E9 RID: 83689 RVA: 0x002DF441 File Offset: 0x002DD641
		public override string LocalName
		{
			get
			{
				return "grpSpPr";
			}
		}

		// Token: 0x170067FB RID: 26619
		// (get) Token: 0x060146EA RID: 83690 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067FC RID: 26620
		// (get) Token: 0x060146EB RID: 83691 RVA: 0x0031317D File Offset: 0x0031137D
		internal override int ElementTypeId
		{
			get
			{
				return 10755;
			}
		}

		// Token: 0x060146EC RID: 83692 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170067FD RID: 26621
		// (get) Token: 0x060146ED RID: 83693 RVA: 0x00313184 File Offset: 0x00311384
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x170067FE RID: 26622
		// (get) Token: 0x060146EE RID: 83694 RVA: 0x0031318B File Offset: 0x0031138B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170067FF RID: 26623
		// (get) Token: 0x060146EF RID: 83695 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x060146F0 RID: 83696 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060146F1 RID: 83697 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupShapeProperties()
		{
		}

		// Token: 0x060146F2 RID: 83698 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060146F3 RID: 83699 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060146F4 RID: 83700 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060146F5 RID: 83701 RVA: 0x00313194 File Offset: 0x00311394
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

		// Token: 0x17006800 RID: 26624
		// (get) Token: 0x060146F6 RID: 83702 RVA: 0x003132AA File Offset: 0x003114AA
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17006801 RID: 26625
		// (get) Token: 0x060146F7 RID: 83703 RVA: 0x003132B1 File Offset: 0x003114B1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006802 RID: 26626
		// (get) Token: 0x060146F8 RID: 83704 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006803 RID: 26627
		// (get) Token: 0x060146F9 RID: 83705 RVA: 0x002DF584 File Offset: 0x002DD784
		// (set) Token: 0x060146FA RID: 83706 RVA: 0x002DF58D File Offset: 0x002DD78D
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

		// Token: 0x060146FB RID: 83707 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060146FC RID: 83708 RVA: 0x003132B8 File Offset: 0x003114B8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShapeProperties>(deep);
		}

		// Token: 0x060146FD RID: 83709 RVA: 0x003132C4 File Offset: 0x003114C4
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

		// Token: 0x04008E12 RID: 36370
		private const string tagName = "grpSpPr";

		// Token: 0x04008E13 RID: 36371
		private const byte tagNsId = 18;

		// Token: 0x04008E14 RID: 36372
		internal const int ElementTypeIdConst = 10755;

		// Token: 0x04008E15 RID: 36373
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x04008E16 RID: 36374
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008E17 RID: 36375
		private static readonly string[] eleTagNames;

		// Token: 0x04008E18 RID: 36376
		private static readonly byte[] eleNamespaceIds;
	}
}
