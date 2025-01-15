using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002644 RID: 9796
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(TransformGroup))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Scene3DType))]
	internal class GroupShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005AEC RID: 23276
		// (get) Token: 0x0601293C RID: 76092 RVA: 0x002DF441 File Offset: 0x002DD641
		public override string LocalName
		{
			get
			{
				return "grpSpPr";
			}
		}

		// Token: 0x17005AED RID: 23277
		// (get) Token: 0x0601293D RID: 76093 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005AEE RID: 23278
		// (get) Token: 0x0601293E RID: 76094 RVA: 0x002FCBCD File Offset: 0x002FADCD
		internal override int ElementTypeId
		{
			get
			{
				return 10614;
			}
		}

		// Token: 0x0601293F RID: 76095 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005AEF RID: 23279
		// (get) Token: 0x06012940 RID: 76096 RVA: 0x002FCBD4 File Offset: 0x002FADD4
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x17005AF0 RID: 23280
		// (get) Token: 0x06012941 RID: 76097 RVA: 0x002FCBDB File Offset: 0x002FADDB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17005AF1 RID: 23281
		// (get) Token: 0x06012942 RID: 76098 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06012943 RID: 76099 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012944 RID: 76100 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupShapeProperties()
		{
		}

		// Token: 0x06012945 RID: 76101 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012946 RID: 76102 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012947 RID: 76103 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012948 RID: 76104 RVA: 0x002FCBE4 File Offset: 0x002FADE4
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

		// Token: 0x17005AF2 RID: 23282
		// (get) Token: 0x06012949 RID: 76105 RVA: 0x002FCCFA File Offset: 0x002FAEFA
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17005AF3 RID: 23283
		// (get) Token: 0x0601294A RID: 76106 RVA: 0x002FCD01 File Offset: 0x002FAF01
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005AF4 RID: 23284
		// (get) Token: 0x0601294B RID: 76107 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005AF5 RID: 23285
		// (get) Token: 0x0601294C RID: 76108 RVA: 0x002DF584 File Offset: 0x002DD784
		// (set) Token: 0x0601294D RID: 76109 RVA: 0x002DF58D File Offset: 0x002DD78D
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

		// Token: 0x0601294E RID: 76110 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601294F RID: 76111 RVA: 0x002FCD08 File Offset: 0x002FAF08
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShapeProperties>(deep);
		}

		// Token: 0x06012950 RID: 76112 RVA: 0x002FCD14 File Offset: 0x002FAF14
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

		// Token: 0x040080BF RID: 32959
		private const string tagName = "grpSpPr";

		// Token: 0x040080C0 RID: 32960
		private const byte tagNsId = 12;

		// Token: 0x040080C1 RID: 32961
		internal const int ElementTypeIdConst = 10614;

		// Token: 0x040080C2 RID: 32962
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x040080C3 RID: 32963
		private static byte[] attributeNamespaceIds;

		// Token: 0x040080C4 RID: 32964
		private static readonly string[] eleTagNames;

		// Token: 0x040080C5 RID: 32965
		private static readonly byte[] eleNamespaceIds;
	}
}
