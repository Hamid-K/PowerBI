using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A18 RID: 10776
	[ChildElementInfo(typeof(CommonBehavior))]
	[ChildElementInfo(typeof(TimeAnimateValueList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Animate : OpenXmlCompositeElement
	{
		// Token: 0x17006FF1 RID: 28657
		// (get) Token: 0x06015934 RID: 88372 RVA: 0x00320BAC File Offset: 0x0031EDAC
		public override string LocalName
		{
			get
			{
				return "anim";
			}
		}

		// Token: 0x17006FF2 RID: 28658
		// (get) Token: 0x06015935 RID: 88373 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FF3 RID: 28659
		// (get) Token: 0x06015936 RID: 88374 RVA: 0x00320BB3 File Offset: 0x0031EDB3
		internal override int ElementTypeId
		{
			get
			{
				return 12202;
			}
		}

		// Token: 0x06015937 RID: 88375 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006FF4 RID: 28660
		// (get) Token: 0x06015938 RID: 88376 RVA: 0x00320BBA File Offset: 0x0031EDBA
		internal override string[] AttributeTagNames
		{
			get
			{
				return Animate.attributeTagNames;
			}
		}

		// Token: 0x17006FF5 RID: 28661
		// (get) Token: 0x06015939 RID: 88377 RVA: 0x00320BC1 File Offset: 0x0031EDC1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Animate.attributeNamespaceIds;
			}
		}

		// Token: 0x17006FF6 RID: 28662
		// (get) Token: 0x0601593A RID: 88378 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601593B RID: 88379 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "by")]
		public StringValue By
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

		// Token: 0x17006FF7 RID: 28663
		// (get) Token: 0x0601593C RID: 88380 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601593D RID: 88381 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "from")]
		public StringValue From
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

		// Token: 0x17006FF8 RID: 28664
		// (get) Token: 0x0601593E RID: 88382 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601593F RID: 88383 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "to")]
		public StringValue To
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17006FF9 RID: 28665
		// (get) Token: 0x06015940 RID: 88384 RVA: 0x00320BC8 File Offset: 0x0031EDC8
		// (set) Token: 0x06015941 RID: 88385 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "calcmode")]
		public EnumValue<AnimateBehaviorCalculateModeValues> CalculationMode
		{
			get
			{
				return (EnumValue<AnimateBehaviorCalculateModeValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006FFA RID: 28666
		// (get) Token: 0x06015942 RID: 88386 RVA: 0x00320BD7 File Offset: 0x0031EDD7
		// (set) Token: 0x06015943 RID: 88387 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "valueType")]
		public EnumValue<AnimateBehaviorValues> ValueType
		{
			get
			{
				return (EnumValue<AnimateBehaviorValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17006FFB RID: 28667
		// (get) Token: 0x06015944 RID: 88388 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x06015945 RID: 88389 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(49, "bounceEnd")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Int32Value BounceEnd
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x06015946 RID: 88390 RVA: 0x00293ECF File Offset: 0x002920CF
		public Animate()
		{
		}

		// Token: 0x06015947 RID: 88391 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Animate(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015948 RID: 88392 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Animate(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015949 RID: 88393 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Animate(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601594A RID: 88394 RVA: 0x00320BE6 File Offset: 0x0031EDE6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cBhvr" == name)
			{
				return new CommonBehavior();
			}
			if (24 == namespaceId && "tavLst" == name)
			{
				return new TimeAnimateValueList();
			}
			return null;
		}

		// Token: 0x17006FFC RID: 28668
		// (get) Token: 0x0601594B RID: 88395 RVA: 0x00320C19 File Offset: 0x0031EE19
		internal override string[] ElementTagNames
		{
			get
			{
				return Animate.eleTagNames;
			}
		}

		// Token: 0x17006FFD RID: 28669
		// (get) Token: 0x0601594C RID: 88396 RVA: 0x00320C20 File Offset: 0x0031EE20
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Animate.eleNamespaceIds;
			}
		}

		// Token: 0x17006FFE RID: 28670
		// (get) Token: 0x0601594D RID: 88397 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006FFF RID: 28671
		// (get) Token: 0x0601594E RID: 88398 RVA: 0x00320C27 File Offset: 0x0031EE27
		// (set) Token: 0x0601594F RID: 88399 RVA: 0x00320C30 File Offset: 0x0031EE30
		public CommonBehavior CommonBehavior
		{
			get
			{
				return base.GetElement<CommonBehavior>(0);
			}
			set
			{
				base.SetElement<CommonBehavior>(0, value);
			}
		}

		// Token: 0x17007000 RID: 28672
		// (get) Token: 0x06015950 RID: 88400 RVA: 0x00320C3A File Offset: 0x0031EE3A
		// (set) Token: 0x06015951 RID: 88401 RVA: 0x00320C43 File Offset: 0x0031EE43
		public TimeAnimateValueList TimeAnimateValueList
		{
			get
			{
				return base.GetElement<TimeAnimateValueList>(1);
			}
			set
			{
				base.SetElement<TimeAnimateValueList>(1, value);
			}
		}

		// Token: 0x06015952 RID: 88402 RVA: 0x00320C50 File Offset: 0x0031EE50
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "by" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "from" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "to" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "calcmode" == name)
			{
				return new EnumValue<AnimateBehaviorCalculateModeValues>();
			}
			if (namespaceId == 0 && "valueType" == name)
			{
				return new EnumValue<AnimateBehaviorValues>();
			}
			if (49 == namespaceId && "bounceEnd" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015953 RID: 88403 RVA: 0x00320CEB File Offset: 0x0031EEEB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Animate>(deep);
		}

		// Token: 0x040093F3 RID: 37875
		private const string tagName = "anim";

		// Token: 0x040093F4 RID: 37876
		private const byte tagNsId = 24;

		// Token: 0x040093F5 RID: 37877
		internal const int ElementTypeIdConst = 12202;

		// Token: 0x040093F6 RID: 37878
		private static string[] attributeTagNames = new string[] { "by", "from", "to", "calcmode", "valueType", "bounceEnd" };

		// Token: 0x040093F7 RID: 37879
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 0, 0, 49 };

		// Token: 0x040093F8 RID: 37880
		private static readonly string[] eleTagNames = new string[] { "cBhvr", "tavLst" };

		// Token: 0x040093F9 RID: 37881
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24 };
	}
}
