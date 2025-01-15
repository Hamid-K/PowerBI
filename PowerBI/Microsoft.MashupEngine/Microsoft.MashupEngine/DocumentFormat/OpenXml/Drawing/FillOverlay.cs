using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002718 RID: 10008
	[ChildElementInfo(typeof(NoFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	internal class FillOverlay : OpenXmlCompositeElement
	{
		// Token: 0x17005F1A RID: 24346
		// (get) Token: 0x0601329E RID: 78494 RVA: 0x0030459C File Offset: 0x0030279C
		public override string LocalName
		{
			get
			{
				return "fillOverlay";
			}
		}

		// Token: 0x17005F1B RID: 24347
		// (get) Token: 0x0601329F RID: 78495 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F1C RID: 24348
		// (get) Token: 0x060132A0 RID: 78496 RVA: 0x003045A3 File Offset: 0x003027A3
		internal override int ElementTypeId
		{
			get
			{
				return 10070;
			}
		}

		// Token: 0x060132A1 RID: 78497 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005F1D RID: 24349
		// (get) Token: 0x060132A2 RID: 78498 RVA: 0x003045AA File Offset: 0x003027AA
		internal override string[] AttributeTagNames
		{
			get
			{
				return FillOverlay.attributeTagNames;
			}
		}

		// Token: 0x17005F1E RID: 24350
		// (get) Token: 0x060132A3 RID: 78499 RVA: 0x003045B1 File Offset: 0x003027B1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FillOverlay.attributeNamespaceIds;
			}
		}

		// Token: 0x17005F1F RID: 24351
		// (get) Token: 0x060132A4 RID: 78500 RVA: 0x00304057 File Offset: 0x00302257
		// (set) Token: 0x060132A5 RID: 78501 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "blend")]
		public EnumValue<BlendModeValues> Blend
		{
			get
			{
				return (EnumValue<BlendModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060132A6 RID: 78502 RVA: 0x00293ECF File Offset: 0x002920CF
		public FillOverlay()
		{
		}

		// Token: 0x060132A7 RID: 78503 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FillOverlay(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060132A8 RID: 78504 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FillOverlay(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060132A9 RID: 78505 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FillOverlay(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060132AA RID: 78506 RVA: 0x003045B8 File Offset: 0x003027B8
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

		// Token: 0x17005F20 RID: 24352
		// (get) Token: 0x060132AB RID: 78507 RVA: 0x00304656 File Offset: 0x00302856
		internal override string[] ElementTagNames
		{
			get
			{
				return FillOverlay.eleTagNames;
			}
		}

		// Token: 0x17005F21 RID: 24353
		// (get) Token: 0x060132AC RID: 78508 RVA: 0x0030465D File Offset: 0x0030285D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FillOverlay.eleNamespaceIds;
			}
		}

		// Token: 0x17005F22 RID: 24354
		// (get) Token: 0x060132AD RID: 78509 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005F23 RID: 24355
		// (get) Token: 0x060132AE RID: 78510 RVA: 0x002E078C File Offset: 0x002DE98C
		// (set) Token: 0x060132AF RID: 78511 RVA: 0x002E0795 File Offset: 0x002DE995
		public NoFill NoFill
		{
			get
			{
				return base.GetElement<NoFill>(0);
			}
			set
			{
				base.SetElement<NoFill>(0, value);
			}
		}

		// Token: 0x17005F24 RID: 24356
		// (get) Token: 0x060132B0 RID: 78512 RVA: 0x002E079F File Offset: 0x002DE99F
		// (set) Token: 0x060132B1 RID: 78513 RVA: 0x002E07A8 File Offset: 0x002DE9A8
		public SolidFill SolidFill
		{
			get
			{
				return base.GetElement<SolidFill>(1);
			}
			set
			{
				base.SetElement<SolidFill>(1, value);
			}
		}

		// Token: 0x17005F25 RID: 24357
		// (get) Token: 0x060132B2 RID: 78514 RVA: 0x002E07B2 File Offset: 0x002DE9B2
		// (set) Token: 0x060132B3 RID: 78515 RVA: 0x002E07BB File Offset: 0x002DE9BB
		public GradientFill GradientFill
		{
			get
			{
				return base.GetElement<GradientFill>(2);
			}
			set
			{
				base.SetElement<GradientFill>(2, value);
			}
		}

		// Token: 0x17005F26 RID: 24358
		// (get) Token: 0x060132B4 RID: 78516 RVA: 0x002E07C5 File Offset: 0x002DE9C5
		// (set) Token: 0x060132B5 RID: 78517 RVA: 0x002E07CE File Offset: 0x002DE9CE
		public BlipFill BlipFill
		{
			get
			{
				return base.GetElement<BlipFill>(3);
			}
			set
			{
				base.SetElement<BlipFill>(3, value);
			}
		}

		// Token: 0x17005F27 RID: 24359
		// (get) Token: 0x060132B6 RID: 78518 RVA: 0x002E07D8 File Offset: 0x002DE9D8
		// (set) Token: 0x060132B7 RID: 78519 RVA: 0x002E07E1 File Offset: 0x002DE9E1
		public PatternFill PatternFill
		{
			get
			{
				return base.GetElement<PatternFill>(4);
			}
			set
			{
				base.SetElement<PatternFill>(4, value);
			}
		}

		// Token: 0x17005F28 RID: 24360
		// (get) Token: 0x060132B8 RID: 78520 RVA: 0x002E07EB File Offset: 0x002DE9EB
		// (set) Token: 0x060132B9 RID: 78521 RVA: 0x002E07F4 File Offset: 0x002DE9F4
		public GroupFill GroupFill
		{
			get
			{
				return base.GetElement<GroupFill>(5);
			}
			set
			{
				base.SetElement<GroupFill>(5, value);
			}
		}

		// Token: 0x060132BA RID: 78522 RVA: 0x00304074 File Offset: 0x00302274
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "blend" == name)
			{
				return new EnumValue<BlendModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060132BB RID: 78523 RVA: 0x00304664 File Offset: 0x00302864
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillOverlay>(deep);
		}

		// Token: 0x060132BC RID: 78524 RVA: 0x00304670 File Offset: 0x00302870
		// Note: this type is marked as 'beforefieldinit'.
		static FillOverlay()
		{
			byte[] array = new byte[1];
			FillOverlay.attributeNamespaceIds = array;
			FillOverlay.eleTagNames = new string[] { "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "grpFill" };
			FillOverlay.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x040084FB RID: 34043
		private const string tagName = "fillOverlay";

		// Token: 0x040084FC RID: 34044
		private const byte tagNsId = 10;

		// Token: 0x040084FD RID: 34045
		internal const int ElementTypeIdConst = 10070;

		// Token: 0x040084FE RID: 34046
		private static string[] attributeTagNames = new string[] { "blend" };

		// Token: 0x040084FF RID: 34047
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008500 RID: 34048
		private static readonly string[] eleTagNames;

		// Token: 0x04008501 RID: 34049
		private static readonly byte[] eleNamespaceIds;
	}
}
