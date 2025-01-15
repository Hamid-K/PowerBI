using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002764 RID: 10084
	[ChildElementInfo(typeof(AlphaFloor))]
	[ChildElementInfo(typeof(Grayscale))]
	[ChildElementInfo(typeof(ColorReplacement))]
	[ChildElementInfo(typeof(AlphaBiLevel))]
	[ChildElementInfo(typeof(AlphaCeiling))]
	[ChildElementInfo(typeof(FillOverlay))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AlphaModulationEffect))]
	[ChildElementInfo(typeof(AlphaModulationFixed))]
	[ChildElementInfo(typeof(AlphaReplace))]
	[ChildElementInfo(typeof(BiLevel))]
	[ChildElementInfo(typeof(Blur))]
	[ChildElementInfo(typeof(ColorChange))]
	[ChildElementInfo(typeof(Duotone))]
	[ChildElementInfo(typeof(AlphaInverse))]
	[ChildElementInfo(typeof(Hsl))]
	[ChildElementInfo(typeof(LuminanceEffect))]
	[ChildElementInfo(typeof(TintEffect))]
	[ChildElementInfo(typeof(BlipExtensionList))]
	internal class Blip : OpenXmlCompositeElement
	{
		// Token: 0x17006101 RID: 24833
		// (get) Token: 0x060136DB RID: 79579 RVA: 0x00306E80 File Offset: 0x00305080
		public override string LocalName
		{
			get
			{
				return "blip";
			}
		}

		// Token: 0x17006102 RID: 24834
		// (get) Token: 0x060136DC RID: 79580 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006103 RID: 24835
		// (get) Token: 0x060136DD RID: 79581 RVA: 0x00306E87 File Offset: 0x00305087
		internal override int ElementTypeId
		{
			get
			{
				return 10121;
			}
		}

		// Token: 0x060136DE RID: 79582 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006104 RID: 24836
		// (get) Token: 0x060136DF RID: 79583 RVA: 0x00306E8E File Offset: 0x0030508E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Blip.attributeTagNames;
			}
		}

		// Token: 0x17006105 RID: 24837
		// (get) Token: 0x060136E0 RID: 79584 RVA: 0x00306E95 File Offset: 0x00305095
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Blip.attributeNamespaceIds;
			}
		}

		// Token: 0x17006106 RID: 24838
		// (get) Token: 0x060136E1 RID: 79585 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060136E2 RID: 79586 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "embed")]
		public StringValue Embed
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006107 RID: 24839
		// (get) Token: 0x060136E3 RID: 79587 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060136E4 RID: 79588 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "link")]
		public StringValue Link
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006108 RID: 24840
		// (get) Token: 0x060136E5 RID: 79589 RVA: 0x00306E9C File Offset: 0x0030509C
		// (set) Token: 0x060136E6 RID: 79590 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "cstate")]
		public EnumValue<BlipCompressionValues> CompressionState
		{
			get
			{
				return (EnumValue<BlipCompressionValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060136E7 RID: 79591 RVA: 0x00293ECF File Offset: 0x002920CF
		public Blip()
		{
		}

		// Token: 0x060136E8 RID: 79592 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Blip(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060136E9 RID: 79593 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Blip(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060136EA RID: 79594 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Blip(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060136EB RID: 79595 RVA: 0x00306EAC File Offset: 0x003050AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "alphaBiLevel" == name)
			{
				return new AlphaBiLevel();
			}
			if (10 == namespaceId && "alphaCeiling" == name)
			{
				return new AlphaCeiling();
			}
			if (10 == namespaceId && "alphaFloor" == name)
			{
				return new AlphaFloor();
			}
			if (10 == namespaceId && "alphaInv" == name)
			{
				return new AlphaInverse();
			}
			if (10 == namespaceId && "alphaMod" == name)
			{
				return new AlphaModulationEffect();
			}
			if (10 == namespaceId && "alphaModFix" == name)
			{
				return new AlphaModulationFixed();
			}
			if (10 == namespaceId && "alphaRepl" == name)
			{
				return new AlphaReplace();
			}
			if (10 == namespaceId && "biLevel" == name)
			{
				return new BiLevel();
			}
			if (10 == namespaceId && "blur" == name)
			{
				return new Blur();
			}
			if (10 == namespaceId && "clrChange" == name)
			{
				return new ColorChange();
			}
			if (10 == namespaceId && "clrRepl" == name)
			{
				return new ColorReplacement();
			}
			if (10 == namespaceId && "duotone" == name)
			{
				return new Duotone();
			}
			if (10 == namespaceId && "fillOverlay" == name)
			{
				return new FillOverlay();
			}
			if (10 == namespaceId && "grayscl" == name)
			{
				return new Grayscale();
			}
			if (10 == namespaceId && "hsl" == name)
			{
				return new Hsl();
			}
			if (10 == namespaceId && "lum" == name)
			{
				return new LuminanceEffect();
			}
			if (10 == namespaceId && "tint" == name)
			{
				return new TintEffect();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new BlipExtensionList();
			}
			return null;
		}

		// Token: 0x060136EC RID: 79596 RVA: 0x0030706C File Offset: 0x0030526C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "embed" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "link" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cstate" == name)
			{
				return new EnumValue<BlipCompressionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060136ED RID: 79597 RVA: 0x003070C7 File Offset: 0x003052C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Blip>(deep);
		}

		// Token: 0x060136EE RID: 79598 RVA: 0x003070D0 File Offset: 0x003052D0
		// Note: this type is marked as 'beforefieldinit'.
		static Blip()
		{
			byte[] array = new byte[3];
			array[0] = 19;
			array[1] = 19;
			Blip.attributeNamespaceIds = array;
		}

		// Token: 0x0400862E RID: 34350
		private const string tagName = "blip";

		// Token: 0x0400862F RID: 34351
		private const byte tagNsId = 10;

		// Token: 0x04008630 RID: 34352
		internal const int ElementTypeIdConst = 10121;

		// Token: 0x04008631 RID: 34353
		private static string[] attributeTagNames = new string[] { "embed", "link", "cstate" };

		// Token: 0x04008632 RID: 34354
		private static byte[] attributeNamespaceIds;
	}
}
