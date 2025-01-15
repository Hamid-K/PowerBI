using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A3A RID: 10810
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonTimeNode))]
	[ChildElementInfo(typeof(TargetElement))]
	internal class CommonMediaNode : OpenXmlCompositeElement
	{
		// Token: 0x17007115 RID: 28949
		// (get) Token: 0x06015BB7 RID: 89015 RVA: 0x0032287B File Offset: 0x00320A7B
		public override string LocalName
		{
			get
			{
				return "cMediaNode";
			}
		}

		// Token: 0x17007116 RID: 28950
		// (get) Token: 0x06015BB8 RID: 89016 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007117 RID: 28951
		// (get) Token: 0x06015BB9 RID: 89017 RVA: 0x00322882 File Offset: 0x00320A82
		internal override int ElementTypeId
		{
			get
			{
				return 12228;
			}
		}

		// Token: 0x06015BBA RID: 89018 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007118 RID: 28952
		// (get) Token: 0x06015BBB RID: 89019 RVA: 0x00322889 File Offset: 0x00320A89
		internal override string[] AttributeTagNames
		{
			get
			{
				return CommonMediaNode.attributeTagNames;
			}
		}

		// Token: 0x17007119 RID: 28953
		// (get) Token: 0x06015BBC RID: 89020 RVA: 0x00322890 File Offset: 0x00320A90
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CommonMediaNode.attributeNamespaceIds;
			}
		}

		// Token: 0x1700711A RID: 28954
		// (get) Token: 0x06015BBD RID: 89021 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06015BBE RID: 89022 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "vol")]
		public Int32Value Volume
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700711B RID: 28955
		// (get) Token: 0x06015BBF RID: 89023 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06015BC0 RID: 89024 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "mute")]
		public BooleanValue Mute
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700711C RID: 28956
		// (get) Token: 0x06015BC1 RID: 89025 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06015BC2 RID: 89026 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "numSld")]
		public UInt32Value SlideCount
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

		// Token: 0x1700711D RID: 28957
		// (get) Token: 0x06015BC3 RID: 89027 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06015BC4 RID: 89028 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showWhenStopped")]
		public BooleanValue ShowWhenStopped
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06015BC5 RID: 89029 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommonMediaNode()
		{
		}

		// Token: 0x06015BC6 RID: 89030 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommonMediaNode(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BC7 RID: 89031 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommonMediaNode(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BC8 RID: 89032 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommonMediaNode(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015BC9 RID: 89033 RVA: 0x00322897 File Offset: 0x00320A97
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
			return null;
		}

		// Token: 0x1700711E RID: 28958
		// (get) Token: 0x06015BCA RID: 89034 RVA: 0x003228CA File Offset: 0x00320ACA
		internal override string[] ElementTagNames
		{
			get
			{
				return CommonMediaNode.eleTagNames;
			}
		}

		// Token: 0x1700711F RID: 28959
		// (get) Token: 0x06015BCB RID: 89035 RVA: 0x003228D1 File Offset: 0x00320AD1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CommonMediaNode.eleNamespaceIds;
			}
		}

		// Token: 0x17007120 RID: 28960
		// (get) Token: 0x06015BCC RID: 89036 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007121 RID: 28961
		// (get) Token: 0x06015BCD RID: 89037 RVA: 0x0032095E File Offset: 0x0031EB5E
		// (set) Token: 0x06015BCE RID: 89038 RVA: 0x00320967 File Offset: 0x0031EB67
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

		// Token: 0x17007122 RID: 28962
		// (get) Token: 0x06015BCF RID: 89039 RVA: 0x003224C8 File Offset: 0x003206C8
		// (set) Token: 0x06015BD0 RID: 89040 RVA: 0x003224D1 File Offset: 0x003206D1
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

		// Token: 0x06015BD1 RID: 89041 RVA: 0x003228D8 File Offset: 0x00320AD8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "vol" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "mute" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "numSld" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "showWhenStopped" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015BD2 RID: 89042 RVA: 0x00322945 File Offset: 0x00320B45
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommonMediaNode>(deep);
		}

		// Token: 0x06015BD3 RID: 89043 RVA: 0x00322950 File Offset: 0x00320B50
		// Note: this type is marked as 'beforefieldinit'.
		static CommonMediaNode()
		{
			byte[] array = new byte[4];
			CommonMediaNode.attributeNamespaceIds = array;
			CommonMediaNode.eleTagNames = new string[] { "cTn", "tgtEl" };
			CommonMediaNode.eleNamespaceIds = new byte[] { 24, 24 };
		}

		// Token: 0x04009494 RID: 38036
		private const string tagName = "cMediaNode";

		// Token: 0x04009495 RID: 38037
		private const byte tagNsId = 24;

		// Token: 0x04009496 RID: 38038
		internal const int ElementTypeIdConst = 12228;

		// Token: 0x04009497 RID: 38039
		private static string[] attributeTagNames = new string[] { "vol", "mute", "numSld", "showWhenStopped" };

		// Token: 0x04009498 RID: 38040
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009499 RID: 38041
		private static readonly string[] eleTagNames;

		// Token: 0x0400949A RID: 38042
		private static readonly byte[] eleNamespaceIds;
	}
}
