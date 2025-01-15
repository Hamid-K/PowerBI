using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028A1 RID: 10401
	[ChildElementInfo(typeof(EffectExtent))]
	[ChildElementInfo(typeof(Extent))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DocProperties))]
	[ChildElementInfo(typeof(NonVisualGraphicFrameDrawingProperties))]
	[ChildElementInfo(typeof(Graphic))]
	internal class Inline : OpenXmlCompositeElement
	{
		// Token: 0x17006838 RID: 26680
		// (get) Token: 0x0601476A RID: 83818 RVA: 0x003137CA File Offset: 0x003119CA
		public override string LocalName
		{
			get
			{
				return "inline";
			}
		}

		// Token: 0x17006839 RID: 26681
		// (get) Token: 0x0601476B RID: 83819 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x1700683A RID: 26682
		// (get) Token: 0x0601476C RID: 83820 RVA: 0x003137D1 File Offset: 0x003119D1
		internal override int ElementTypeId
		{
			get
			{
				return 10699;
			}
		}

		// Token: 0x0601476D RID: 83821 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700683B RID: 26683
		// (get) Token: 0x0601476E RID: 83822 RVA: 0x003137D8 File Offset: 0x003119D8
		internal override string[] AttributeTagNames
		{
			get
			{
				return Inline.attributeTagNames;
			}
		}

		// Token: 0x1700683C RID: 26684
		// (get) Token: 0x0601476F RID: 83823 RVA: 0x003137DF File Offset: 0x003119DF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Inline.attributeNamespaceIds;
			}
		}

		// Token: 0x1700683D RID: 26685
		// (get) Token: 0x06014770 RID: 83824 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06014771 RID: 83825 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "distT")]
		public UInt32Value DistanceFromTop
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700683E RID: 26686
		// (get) Token: 0x06014772 RID: 83826 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06014773 RID: 83827 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "distB")]
		public UInt32Value DistanceFromBottom
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

		// Token: 0x1700683F RID: 26687
		// (get) Token: 0x06014774 RID: 83828 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06014775 RID: 83829 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "distL")]
		public UInt32Value DistanceFromLeft
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

		// Token: 0x17006840 RID: 26688
		// (get) Token: 0x06014776 RID: 83830 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06014777 RID: 83831 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "distR")]
		public UInt32Value DistanceFromRight
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

		// Token: 0x17006841 RID: 26689
		// (get) Token: 0x06014778 RID: 83832 RVA: 0x002EB784 File Offset: 0x002E9984
		// (set) Token: 0x06014779 RID: 83833 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(51, "anchorId")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public HexBinaryValue AnchorId
		{
			get
			{
				return (HexBinaryValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17006842 RID: 26690
		// (get) Token: 0x0601477A RID: 83834 RVA: 0x003137E6 File Offset: 0x003119E6
		// (set) Token: 0x0601477B RID: 83835 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(51, "editId")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public HexBinaryValue EditId
		{
			get
			{
				return (HexBinaryValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0601477C RID: 83836 RVA: 0x00293ECF File Offset: 0x002920CF
		public Inline()
		{
		}

		// Token: 0x0601477D RID: 83837 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Inline(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601477E RID: 83838 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Inline(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601477F RID: 83839 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Inline(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014780 RID: 83840 RVA: 0x003137F8 File Offset: 0x003119F8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (16 == namespaceId && "extent" == name)
			{
				return new Extent();
			}
			if (16 == namespaceId && "effectExtent" == name)
			{
				return new EffectExtent();
			}
			if (16 == namespaceId && "docPr" == name)
			{
				return new DocProperties();
			}
			if (16 == namespaceId && "cNvGraphicFramePr" == name)
			{
				return new NonVisualGraphicFrameDrawingProperties();
			}
			if (10 == namespaceId && "graphic" == name)
			{
				return new Graphic();
			}
			return null;
		}

		// Token: 0x17006843 RID: 26691
		// (get) Token: 0x06014781 RID: 83841 RVA: 0x0031387E File Offset: 0x00311A7E
		internal override string[] ElementTagNames
		{
			get
			{
				return Inline.eleTagNames;
			}
		}

		// Token: 0x17006844 RID: 26692
		// (get) Token: 0x06014782 RID: 83842 RVA: 0x00313885 File Offset: 0x00311A85
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Inline.eleNamespaceIds;
			}
		}

		// Token: 0x17006845 RID: 26693
		// (get) Token: 0x06014783 RID: 83843 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006846 RID: 26694
		// (get) Token: 0x06014784 RID: 83844 RVA: 0x0031388C File Offset: 0x00311A8C
		// (set) Token: 0x06014785 RID: 83845 RVA: 0x00313895 File Offset: 0x00311A95
		public Extent Extent
		{
			get
			{
				return base.GetElement<Extent>(0);
			}
			set
			{
				base.SetElement<Extent>(0, value);
			}
		}

		// Token: 0x17006847 RID: 26695
		// (get) Token: 0x06014786 RID: 83846 RVA: 0x0031389F File Offset: 0x00311A9F
		// (set) Token: 0x06014787 RID: 83847 RVA: 0x003138A8 File Offset: 0x00311AA8
		public EffectExtent EffectExtent
		{
			get
			{
				return base.GetElement<EffectExtent>(1);
			}
			set
			{
				base.SetElement<EffectExtent>(1, value);
			}
		}

		// Token: 0x17006848 RID: 26696
		// (get) Token: 0x06014788 RID: 83848 RVA: 0x003138B2 File Offset: 0x00311AB2
		// (set) Token: 0x06014789 RID: 83849 RVA: 0x003138BB File Offset: 0x00311ABB
		public DocProperties DocProperties
		{
			get
			{
				return base.GetElement<DocProperties>(2);
			}
			set
			{
				base.SetElement<DocProperties>(2, value);
			}
		}

		// Token: 0x17006849 RID: 26697
		// (get) Token: 0x0601478A RID: 83850 RVA: 0x003138C5 File Offset: 0x00311AC5
		// (set) Token: 0x0601478B RID: 83851 RVA: 0x003138CE File Offset: 0x00311ACE
		public NonVisualGraphicFrameDrawingProperties NonVisualGraphicFrameDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualGraphicFrameDrawingProperties>(3);
			}
			set
			{
				base.SetElement<NonVisualGraphicFrameDrawingProperties>(3, value);
			}
		}

		// Token: 0x1700684A RID: 26698
		// (get) Token: 0x0601478C RID: 83852 RVA: 0x003138D8 File Offset: 0x00311AD8
		// (set) Token: 0x0601478D RID: 83853 RVA: 0x003138E1 File Offset: 0x00311AE1
		public Graphic Graphic
		{
			get
			{
				return base.GetElement<Graphic>(4);
			}
			set
			{
				base.SetElement<Graphic>(4, value);
			}
		}

		// Token: 0x0601478E RID: 83854 RVA: 0x003138EC File Offset: 0x00311AEC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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
			if (51 == namespaceId && "anchorId" == name)
			{
				return new HexBinaryValue();
			}
			if (51 == namespaceId && "editId" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601478F RID: 83855 RVA: 0x00313989 File Offset: 0x00311B89
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Inline>(deep);
		}

		// Token: 0x04008E3C RID: 36412
		private const string tagName = "inline";

		// Token: 0x04008E3D RID: 36413
		private const byte tagNsId = 16;

		// Token: 0x04008E3E RID: 36414
		internal const int ElementTypeIdConst = 10699;

		// Token: 0x04008E3F RID: 36415
		private static string[] attributeTagNames = new string[] { "distT", "distB", "distL", "distR", "anchorId", "editId" };

		// Token: 0x04008E40 RID: 36416
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 0, 51, 51 };

		// Token: 0x04008E41 RID: 36417
		private static readonly string[] eleTagNames = new string[] { "extent", "effectExtent", "docPr", "cNvGraphicFramePr", "graphic" };

		// Token: 0x04008E42 RID: 36418
		private static readonly byte[] eleNamespaceIds = new byte[] { 16, 16, 16, 16, 10 };
	}
}
