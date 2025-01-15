using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A16 RID: 10774
	[ChildElementInfo(typeof(CommonTimeNode))]
	[ChildElementInfo(typeof(PreviousConditionList))]
	[ChildElementInfo(typeof(NextConditionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SequenceTimeNode : OpenXmlCompositeElement
	{
		// Token: 0x17006FDC RID: 28636
		// (get) Token: 0x06015907 RID: 88327 RVA: 0x003209B0 File Offset: 0x0031EBB0
		public override string LocalName
		{
			get
			{
				return "seq";
			}
		}

		// Token: 0x17006FDD RID: 28637
		// (get) Token: 0x06015908 RID: 88328 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FDE RID: 28638
		// (get) Token: 0x06015909 RID: 88329 RVA: 0x003209B7 File Offset: 0x0031EBB7
		internal override int ElementTypeId
		{
			get
			{
				return 12200;
			}
		}

		// Token: 0x0601590A RID: 88330 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006FDF RID: 28639
		// (get) Token: 0x0601590B RID: 88331 RVA: 0x003209BE File Offset: 0x0031EBBE
		internal override string[] AttributeTagNames
		{
			get
			{
				return SequenceTimeNode.attributeTagNames;
			}
		}

		// Token: 0x17006FE0 RID: 28640
		// (get) Token: 0x0601590C RID: 88332 RVA: 0x003209C5 File Offset: 0x0031EBC5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SequenceTimeNode.attributeNamespaceIds;
			}
		}

		// Token: 0x17006FE1 RID: 28641
		// (get) Token: 0x0601590D RID: 88333 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601590E RID: 88334 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "concurrent")]
		public BooleanValue Concurrent
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006FE2 RID: 28642
		// (get) Token: 0x0601590F RID: 88335 RVA: 0x003209CC File Offset: 0x0031EBCC
		// (set) Token: 0x06015910 RID: 88336 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "prevAc")]
		public EnumValue<PreviousActionValues> PreviousAction
		{
			get
			{
				return (EnumValue<PreviousActionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006FE3 RID: 28643
		// (get) Token: 0x06015911 RID: 88337 RVA: 0x003209DB File Offset: 0x0031EBDB
		// (set) Token: 0x06015912 RID: 88338 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "nextAc")]
		public EnumValue<NextActionValues> NextAction
		{
			get
			{
				return (EnumValue<NextActionValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06015913 RID: 88339 RVA: 0x00293ECF File Offset: 0x002920CF
		public SequenceTimeNode()
		{
		}

		// Token: 0x06015914 RID: 88340 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SequenceTimeNode(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015915 RID: 88341 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SequenceTimeNode(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015916 RID: 88342 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SequenceTimeNode(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015917 RID: 88343 RVA: 0x003209EC File Offset: 0x0031EBEC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cTn" == name)
			{
				return new CommonTimeNode();
			}
			if (24 == namespaceId && "prevCondLst" == name)
			{
				return new PreviousConditionList();
			}
			if (24 == namespaceId && "nextCondLst" == name)
			{
				return new NextConditionList();
			}
			return null;
		}

		// Token: 0x17006FE4 RID: 28644
		// (get) Token: 0x06015918 RID: 88344 RVA: 0x00320A42 File Offset: 0x0031EC42
		internal override string[] ElementTagNames
		{
			get
			{
				return SequenceTimeNode.eleTagNames;
			}
		}

		// Token: 0x17006FE5 RID: 28645
		// (get) Token: 0x06015919 RID: 88345 RVA: 0x00320A49 File Offset: 0x0031EC49
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SequenceTimeNode.eleNamespaceIds;
			}
		}

		// Token: 0x17006FE6 RID: 28646
		// (get) Token: 0x0601591A RID: 88346 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006FE7 RID: 28647
		// (get) Token: 0x0601591B RID: 88347 RVA: 0x0032095E File Offset: 0x0031EB5E
		// (set) Token: 0x0601591C RID: 88348 RVA: 0x00320967 File Offset: 0x0031EB67
		public CommonTimeNode CommonTimeNode
		{
			get
			{
				return base.GetElement<CommonTimeNode>(0);
			}
			set
			{
				base.SetElement<CommonTimeNode>(0, value);
			}
		}

		// Token: 0x17006FE8 RID: 28648
		// (get) Token: 0x0601591D RID: 88349 RVA: 0x00320A50 File Offset: 0x0031EC50
		// (set) Token: 0x0601591E RID: 88350 RVA: 0x00320A59 File Offset: 0x0031EC59
		public PreviousConditionList PreviousConditionList
		{
			get
			{
				return base.GetElement<PreviousConditionList>(1);
			}
			set
			{
				base.SetElement<PreviousConditionList>(1, value);
			}
		}

		// Token: 0x17006FE9 RID: 28649
		// (get) Token: 0x0601591F RID: 88351 RVA: 0x00320A63 File Offset: 0x0031EC63
		// (set) Token: 0x06015920 RID: 88352 RVA: 0x00320A6C File Offset: 0x0031EC6C
		public NextConditionList NextConditionList
		{
			get
			{
				return base.GetElement<NextConditionList>(2);
			}
			set
			{
				base.SetElement<NextConditionList>(2, value);
			}
		}

		// Token: 0x06015921 RID: 88353 RVA: 0x00320A78 File Offset: 0x0031EC78
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "concurrent" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "prevAc" == name)
			{
				return new EnumValue<PreviousActionValues>();
			}
			if (namespaceId == 0 && "nextAc" == name)
			{
				return new EnumValue<NextActionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015922 RID: 88354 RVA: 0x00320ACF File Offset: 0x0031ECCF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SequenceTimeNode>(deep);
		}

		// Token: 0x06015923 RID: 88355 RVA: 0x00320AD8 File Offset: 0x0031ECD8
		// Note: this type is marked as 'beforefieldinit'.
		static SequenceTimeNode()
		{
			byte[] array = new byte[3];
			SequenceTimeNode.attributeNamespaceIds = array;
			SequenceTimeNode.eleTagNames = new string[] { "cTn", "prevCondLst", "nextCondLst" };
			SequenceTimeNode.eleNamespaceIds = new byte[] { 24, 24, 24 };
		}

		// Token: 0x040093E7 RID: 37863
		private const string tagName = "seq";

		// Token: 0x040093E8 RID: 37864
		private const byte tagNsId = 24;

		// Token: 0x040093E9 RID: 37865
		internal const int ElementTypeIdConst = 12200;

		// Token: 0x040093EA RID: 37866
		private static string[] attributeTagNames = new string[] { "concurrent", "prevAc", "nextAc" };

		// Token: 0x040093EB RID: 37867
		private static byte[] attributeNamespaceIds;

		// Token: 0x040093EC RID: 37868
		private static readonly string[] eleTagNames;

		// Token: 0x040093ED RID: 37869
		private static readonly byte[] eleNamespaceIds;
	}
}
