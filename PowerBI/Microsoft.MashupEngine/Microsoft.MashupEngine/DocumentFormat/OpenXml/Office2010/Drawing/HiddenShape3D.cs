using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200234C RID: 9036
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BevelTop))]
	[ChildElementInfo(typeof(BevelBottom))]
	[ChildElementInfo(typeof(ExtrusionColor))]
	[ChildElementInfo(typeof(ContourColor))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class HiddenShape3D : OpenXmlCompositeElement
	{
		// Token: 0x170049B7 RID: 18871
		// (get) Token: 0x06010304 RID: 66308 RVA: 0x002E0C98 File Offset: 0x002DEE98
		public override string LocalName
		{
			get
			{
				return "hiddenSp3d";
			}
		}

		// Token: 0x170049B8 RID: 18872
		// (get) Token: 0x06010305 RID: 66309 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x170049B9 RID: 18873
		// (get) Token: 0x06010306 RID: 66310 RVA: 0x002E0C9F File Offset: 0x002DEE9F
		internal override int ElementTypeId
		{
			get
			{
				return 12721;
			}
		}

		// Token: 0x06010307 RID: 66311 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170049BA RID: 18874
		// (get) Token: 0x06010308 RID: 66312 RVA: 0x002E0CA6 File Offset: 0x002DEEA6
		internal override string[] AttributeTagNames
		{
			get
			{
				return HiddenShape3D.attributeTagNames;
			}
		}

		// Token: 0x170049BB RID: 18875
		// (get) Token: 0x06010309 RID: 66313 RVA: 0x002E0CAD File Offset: 0x002DEEAD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HiddenShape3D.attributeNamespaceIds;
			}
		}

		// Token: 0x170049BC RID: 18876
		// (get) Token: 0x0601030A RID: 66314 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x0601030B RID: 66315 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "z")]
		public Int64Value Z
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170049BD RID: 18877
		// (get) Token: 0x0601030C RID: 66316 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x0601030D RID: 66317 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "extrusionH")]
		public Int64Value ExtrusionHeight
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170049BE RID: 18878
		// (get) Token: 0x0601030E RID: 66318 RVA: 0x002E0CD2 File Offset: 0x002DEED2
		// (set) Token: 0x0601030F RID: 66319 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "contourW")]
		public Int64Value ContourWidth
		{
			get
			{
				return (Int64Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170049BF RID: 18879
		// (get) Token: 0x06010310 RID: 66320 RVA: 0x002E0CE1 File Offset: 0x002DEEE1
		// (set) Token: 0x06010311 RID: 66321 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "prstMaterial")]
		public EnumValue<PresetMaterialTypeValues> PresetMaterial
		{
			get
			{
				return (EnumValue<PresetMaterialTypeValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06010312 RID: 66322 RVA: 0x00293ECF File Offset: 0x002920CF
		public HiddenShape3D()
		{
		}

		// Token: 0x06010313 RID: 66323 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HiddenShape3D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010314 RID: 66324 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HiddenShape3D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010315 RID: 66325 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HiddenShape3D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010316 RID: 66326 RVA: 0x002E0CF0 File Offset: 0x002DEEF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "bevelT" == name)
			{
				return new BevelTop();
			}
			if (10 == namespaceId && "bevelB" == name)
			{
				return new BevelBottom();
			}
			if (10 == namespaceId && "extrusionClr" == name)
			{
				return new ExtrusionColor();
			}
			if (10 == namespaceId && "contourClr" == name)
			{
				return new ContourColor();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170049C0 RID: 18880
		// (get) Token: 0x06010317 RID: 66327 RVA: 0x002E0D76 File Offset: 0x002DEF76
		internal override string[] ElementTagNames
		{
			get
			{
				return HiddenShape3D.eleTagNames;
			}
		}

		// Token: 0x170049C1 RID: 18881
		// (get) Token: 0x06010318 RID: 66328 RVA: 0x002E0D7D File Offset: 0x002DEF7D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HiddenShape3D.eleNamespaceIds;
			}
		}

		// Token: 0x170049C2 RID: 18882
		// (get) Token: 0x06010319 RID: 66329 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170049C3 RID: 18883
		// (get) Token: 0x0601031A RID: 66330 RVA: 0x002E0D84 File Offset: 0x002DEF84
		// (set) Token: 0x0601031B RID: 66331 RVA: 0x002E0D8D File Offset: 0x002DEF8D
		public BevelTop BevelTop
		{
			get
			{
				return base.GetElement<BevelTop>(0);
			}
			set
			{
				base.SetElement<BevelTop>(0, value);
			}
		}

		// Token: 0x170049C4 RID: 18884
		// (get) Token: 0x0601031C RID: 66332 RVA: 0x002E0D97 File Offset: 0x002DEF97
		// (set) Token: 0x0601031D RID: 66333 RVA: 0x002E0DA0 File Offset: 0x002DEFA0
		public BevelBottom BevelBottom
		{
			get
			{
				return base.GetElement<BevelBottom>(1);
			}
			set
			{
				base.SetElement<BevelBottom>(1, value);
			}
		}

		// Token: 0x170049C5 RID: 18885
		// (get) Token: 0x0601031E RID: 66334 RVA: 0x002E0DAA File Offset: 0x002DEFAA
		// (set) Token: 0x0601031F RID: 66335 RVA: 0x002E0DB3 File Offset: 0x002DEFB3
		public ExtrusionColor ExtrusionColor
		{
			get
			{
				return base.GetElement<ExtrusionColor>(2);
			}
			set
			{
				base.SetElement<ExtrusionColor>(2, value);
			}
		}

		// Token: 0x170049C6 RID: 18886
		// (get) Token: 0x06010320 RID: 66336 RVA: 0x002E0DBD File Offset: 0x002DEFBD
		// (set) Token: 0x06010321 RID: 66337 RVA: 0x002E0DC6 File Offset: 0x002DEFC6
		public ContourColor ContourColor
		{
			get
			{
				return base.GetElement<ContourColor>(3);
			}
			set
			{
				base.SetElement<ContourColor>(3, value);
			}
		}

		// Token: 0x170049C7 RID: 18887
		// (get) Token: 0x06010322 RID: 66338 RVA: 0x002E0DD0 File Offset: 0x002DEFD0
		// (set) Token: 0x06010323 RID: 66339 RVA: 0x002E0DD9 File Offset: 0x002DEFD9
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x06010324 RID: 66340 RVA: 0x002E0DE4 File Offset: 0x002DEFE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "z" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "extrusionH" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "contourW" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "prstMaterial" == name)
			{
				return new EnumValue<PresetMaterialTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010325 RID: 66341 RVA: 0x002E0E51 File Offset: 0x002DF051
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HiddenShape3D>(deep);
		}

		// Token: 0x06010326 RID: 66342 RVA: 0x002E0E5C File Offset: 0x002DF05C
		// Note: this type is marked as 'beforefieldinit'.
		static HiddenShape3D()
		{
			byte[] array = new byte[4];
			HiddenShape3D.attributeNamespaceIds = array;
			HiddenShape3D.eleTagNames = new string[] { "bevelT", "bevelB", "extrusionClr", "contourClr", "extLst" };
			HiddenShape3D.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10 };
		}

		// Token: 0x04007377 RID: 29559
		private const string tagName = "hiddenSp3d";

		// Token: 0x04007378 RID: 29560
		private const byte tagNsId = 48;

		// Token: 0x04007379 RID: 29561
		internal const int ElementTypeIdConst = 12721;

		// Token: 0x0400737A RID: 29562
		private static string[] attributeTagNames = new string[] { "z", "extrusionH", "contourW", "prstMaterial" };

		// Token: 0x0400737B RID: 29563
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400737C RID: 29564
		private static readonly string[] eleTagNames;

		// Token: 0x0400737D RID: 29565
		private static readonly byte[] eleNamespaceIds;
	}
}
