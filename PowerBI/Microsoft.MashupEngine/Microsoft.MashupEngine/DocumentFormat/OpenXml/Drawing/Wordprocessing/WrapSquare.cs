using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x0200289D RID: 10397
	[ChildElementInfo(typeof(EffectExtent))]
	[GeneratedCode("DomGen", "2.0")]
	internal class WrapSquare : OpenXmlCompositeElement
	{
		// Token: 0x17006807 RID: 26631
		// (get) Token: 0x06014704 RID: 83716 RVA: 0x00313389 File Offset: 0x00311589
		public override string LocalName
		{
			get
			{
				return "wrapSquare";
			}
		}

		// Token: 0x17006808 RID: 26632
		// (get) Token: 0x06014705 RID: 83717 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17006809 RID: 26633
		// (get) Token: 0x06014706 RID: 83718 RVA: 0x00313390 File Offset: 0x00311590
		internal override int ElementTypeId
		{
			get
			{
				return 10695;
			}
		}

		// Token: 0x06014707 RID: 83719 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700680A RID: 26634
		// (get) Token: 0x06014708 RID: 83720 RVA: 0x00313397 File Offset: 0x00311597
		internal override string[] AttributeTagNames
		{
			get
			{
				return WrapSquare.attributeTagNames;
			}
		}

		// Token: 0x1700680B RID: 26635
		// (get) Token: 0x06014709 RID: 83721 RVA: 0x0031339E File Offset: 0x0031159E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WrapSquare.attributeNamespaceIds;
			}
		}

		// Token: 0x1700680C RID: 26636
		// (get) Token: 0x0601470A RID: 83722 RVA: 0x003133A5 File Offset: 0x003115A5
		// (set) Token: 0x0601470B RID: 83723 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "wrapText")]
		public EnumValue<WrapTextValues> WrapText
		{
			get
			{
				return (EnumValue<WrapTextValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700680D RID: 26637
		// (get) Token: 0x0601470C RID: 83724 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601470D RID: 83725 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "distT")]
		public UInt32Value DistanceFromTop
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700680E RID: 26638
		// (get) Token: 0x0601470E RID: 83726 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x0601470F RID: 83727 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "distB")]
		public UInt32Value DistanceFromBottom
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700680F RID: 26639
		// (get) Token: 0x06014710 RID: 83728 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06014711 RID: 83729 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "distL")]
		public UInt32Value DistanceFromLeft
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006810 RID: 26640
		// (get) Token: 0x06014712 RID: 83730 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06014713 RID: 83731 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "distR")]
		public UInt32Value DistanceFromRight
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06014714 RID: 83732 RVA: 0x00293ECF File Offset: 0x002920CF
		public WrapSquare()
		{
		}

		// Token: 0x06014715 RID: 83733 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WrapSquare(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014716 RID: 83734 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WrapSquare(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014717 RID: 83735 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WrapSquare(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014718 RID: 83736 RVA: 0x003133B4 File Offset: 0x003115B4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (16 == namespaceId && "effectExtent" == name)
			{
				return new EffectExtent();
			}
			return null;
		}

		// Token: 0x17006811 RID: 26641
		// (get) Token: 0x06014719 RID: 83737 RVA: 0x003133CF File Offset: 0x003115CF
		internal override string[] ElementTagNames
		{
			get
			{
				return WrapSquare.eleTagNames;
			}
		}

		// Token: 0x17006812 RID: 26642
		// (get) Token: 0x0601471A RID: 83738 RVA: 0x003133D6 File Offset: 0x003115D6
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WrapSquare.eleNamespaceIds;
			}
		}

		// Token: 0x17006813 RID: 26643
		// (get) Token: 0x0601471B RID: 83739 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006814 RID: 26644
		// (get) Token: 0x0601471C RID: 83740 RVA: 0x003133DD File Offset: 0x003115DD
		// (set) Token: 0x0601471D RID: 83741 RVA: 0x003133E6 File Offset: 0x003115E6
		public EffectExtent EffectExtent
		{
			get
			{
				return base.GetElement<EffectExtent>(0);
			}
			set
			{
				base.SetElement<EffectExtent>(0, value);
			}
		}

		// Token: 0x0601471E RID: 83742 RVA: 0x003133F0 File Offset: 0x003115F0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "wrapText" == name)
			{
				return new EnumValue<WrapTextValues>();
			}
			if (namespaceId == 0 && "distT" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "distB" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "distL" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "distR" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601471F RID: 83743 RVA: 0x00313473 File Offset: 0x00311673
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WrapSquare>(deep);
		}

		// Token: 0x06014720 RID: 83744 RVA: 0x0031347C File Offset: 0x0031167C
		// Note: this type is marked as 'beforefieldinit'.
		static WrapSquare()
		{
			byte[] array = new byte[5];
			WrapSquare.attributeNamespaceIds = array;
			WrapSquare.eleTagNames = new string[] { "effectExtent" };
			WrapSquare.eleNamespaceIds = new byte[] { 16 };
		}

		// Token: 0x04008E20 RID: 36384
		private const string tagName = "wrapSquare";

		// Token: 0x04008E21 RID: 36385
		private const byte tagNsId = 16;

		// Token: 0x04008E22 RID: 36386
		internal const int ElementTypeIdConst = 10695;

		// Token: 0x04008E23 RID: 36387
		private static string[] attributeTagNames = new string[] { "wrapText", "distT", "distB", "distL", "distR" };

		// Token: 0x04008E24 RID: 36388
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008E25 RID: 36389
		private static readonly string[] eleTagNames;

		// Token: 0x04008E26 RID: 36390
		private static readonly byte[] eleNamespaceIds;
	}
}
