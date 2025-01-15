using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A35 RID: 10805
	[ChildElementInfo(typeof(CommonTimeNode))]
	[ChildElementInfo(typeof(TargetElement))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AttributeNameList))]
	internal class CommonBehavior : OpenXmlCompositeElement
	{
		// Token: 0x170070ED RID: 28909
		// (get) Token: 0x06015B5B RID: 88923 RVA: 0x0032240B File Offset: 0x0032060B
		public override string LocalName
		{
			get
			{
				return "cBhvr";
			}
		}

		// Token: 0x170070EE RID: 28910
		// (get) Token: 0x06015B5C RID: 88924 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070EF RID: 28911
		// (get) Token: 0x06015B5D RID: 88925 RVA: 0x00322412 File Offset: 0x00320612
		internal override int ElementTypeId
		{
			get
			{
				return 12225;
			}
		}

		// Token: 0x06015B5E RID: 88926 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170070F0 RID: 28912
		// (get) Token: 0x06015B5F RID: 88927 RVA: 0x00322419 File Offset: 0x00320619
		internal override string[] AttributeTagNames
		{
			get
			{
				return CommonBehavior.attributeTagNames;
			}
		}

		// Token: 0x170070F1 RID: 28913
		// (get) Token: 0x06015B60 RID: 88928 RVA: 0x00322420 File Offset: 0x00320620
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CommonBehavior.attributeNamespaceIds;
			}
		}

		// Token: 0x170070F2 RID: 28914
		// (get) Token: 0x06015B61 RID: 88929 RVA: 0x00322427 File Offset: 0x00320627
		// (set) Token: 0x06015B62 RID: 88930 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "additive")]
		public EnumValue<BehaviorAdditiveValues> Additive
		{
			get
			{
				return (EnumValue<BehaviorAdditiveValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170070F3 RID: 28915
		// (get) Token: 0x06015B63 RID: 88931 RVA: 0x00322436 File Offset: 0x00320636
		// (set) Token: 0x06015B64 RID: 88932 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "accumulate")]
		public EnumValue<BehaviorAccumulateValues> Accumulate
		{
			get
			{
				return (EnumValue<BehaviorAccumulateValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170070F4 RID: 28916
		// (get) Token: 0x06015B65 RID: 88933 RVA: 0x00322445 File Offset: 0x00320645
		// (set) Token: 0x06015B66 RID: 88934 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "xfrmType")]
		public EnumValue<BehaviorTransformValues> TransformType
		{
			get
			{
				return (EnumValue<BehaviorTransformValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170070F5 RID: 28917
		// (get) Token: 0x06015B67 RID: 88935 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06015B68 RID: 88936 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "from")]
		public StringValue From
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170070F6 RID: 28918
		// (get) Token: 0x06015B69 RID: 88937 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06015B6A RID: 88938 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "to")]
		public StringValue To
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170070F7 RID: 28919
		// (get) Token: 0x06015B6B RID: 88939 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06015B6C RID: 88940 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "by")]
		public StringValue By
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170070F8 RID: 28920
		// (get) Token: 0x06015B6D RID: 88941 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06015B6E RID: 88942 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "rctx")]
		public StringValue RuntimeContext
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170070F9 RID: 28921
		// (get) Token: 0x06015B6F RID: 88943 RVA: 0x00322454 File Offset: 0x00320654
		// (set) Token: 0x06015B70 RID: 88944 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "override")]
		public EnumValue<BehaviorOverrideValues> Override
		{
			get
			{
				return (EnumValue<BehaviorOverrideValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x06015B71 RID: 88945 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommonBehavior()
		{
		}

		// Token: 0x06015B72 RID: 88946 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommonBehavior(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B73 RID: 88947 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommonBehavior(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B74 RID: 88948 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommonBehavior(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015B75 RID: 88949 RVA: 0x00322464 File Offset: 0x00320664
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cTn" == name)
			{
				return new CommonTimeNode();
			}
			if (24 == namespaceId && "tgtEl" == name)
			{
				return new TargetElement();
			}
			if (24 == namespaceId && "attrNameLst" == name)
			{
				return new AttributeNameList();
			}
			return null;
		}

		// Token: 0x170070FA RID: 28922
		// (get) Token: 0x06015B76 RID: 88950 RVA: 0x003224BA File Offset: 0x003206BA
		internal override string[] ElementTagNames
		{
			get
			{
				return CommonBehavior.eleTagNames;
			}
		}

		// Token: 0x170070FB RID: 28923
		// (get) Token: 0x06015B77 RID: 88951 RVA: 0x003224C1 File Offset: 0x003206C1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CommonBehavior.eleNamespaceIds;
			}
		}

		// Token: 0x170070FC RID: 28924
		// (get) Token: 0x06015B78 RID: 88952 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170070FD RID: 28925
		// (get) Token: 0x06015B79 RID: 88953 RVA: 0x0032095E File Offset: 0x0031EB5E
		// (set) Token: 0x06015B7A RID: 88954 RVA: 0x00320967 File Offset: 0x0031EB67
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

		// Token: 0x170070FE RID: 28926
		// (get) Token: 0x06015B7B RID: 88955 RVA: 0x003224C8 File Offset: 0x003206C8
		// (set) Token: 0x06015B7C RID: 88956 RVA: 0x003224D1 File Offset: 0x003206D1
		public TargetElement TargetElement
		{
			get
			{
				return base.GetElement<TargetElement>(1);
			}
			set
			{
				base.SetElement<TargetElement>(1, value);
			}
		}

		// Token: 0x170070FF RID: 28927
		// (get) Token: 0x06015B7D RID: 88957 RVA: 0x003224DB File Offset: 0x003206DB
		// (set) Token: 0x06015B7E RID: 88958 RVA: 0x003224E4 File Offset: 0x003206E4
		public AttributeNameList AttributeNameList
		{
			get
			{
				return base.GetElement<AttributeNameList>(2);
			}
			set
			{
				base.SetElement<AttributeNameList>(2, value);
			}
		}

		// Token: 0x06015B7F RID: 88959 RVA: 0x003224F0 File Offset: 0x003206F0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "additive" == name)
			{
				return new EnumValue<BehaviorAdditiveValues>();
			}
			if (namespaceId == 0 && "accumulate" == name)
			{
				return new EnumValue<BehaviorAccumulateValues>();
			}
			if (namespaceId == 0 && "xfrmType" == name)
			{
				return new EnumValue<BehaviorTransformValues>();
			}
			if (namespaceId == 0 && "from" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "to" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "by" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "rctx" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "override" == name)
			{
				return new EnumValue<BehaviorOverrideValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015B80 RID: 88960 RVA: 0x003225B5 File Offset: 0x003207B5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommonBehavior>(deep);
		}

		// Token: 0x06015B81 RID: 88961 RVA: 0x003225C0 File Offset: 0x003207C0
		// Note: this type is marked as 'beforefieldinit'.
		static CommonBehavior()
		{
			byte[] array = new byte[8];
			CommonBehavior.attributeNamespaceIds = array;
			CommonBehavior.eleTagNames = new string[] { "cTn", "tgtEl", "attrNameLst" };
			CommonBehavior.eleNamespaceIds = new byte[] { 24, 24, 24 };
		}

		// Token: 0x04009480 RID: 38016
		private const string tagName = "cBhvr";

		// Token: 0x04009481 RID: 38017
		private const byte tagNsId = 24;

		// Token: 0x04009482 RID: 38018
		internal const int ElementTypeIdConst = 12225;

		// Token: 0x04009483 RID: 38019
		private static string[] attributeTagNames = new string[] { "additive", "accumulate", "xfrmType", "from", "to", "by", "rctx", "override" };

		// Token: 0x04009484 RID: 38020
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009485 RID: 38021
		private static readonly string[] eleTagNames;

		// Token: 0x04009486 RID: 38022
		private static readonly byte[] eleNamespaceIds;
	}
}
