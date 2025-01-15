using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x0200307E RID: 12414
	[GeneratedCode("DomGen", "2.0")]
	internal class EndPoint : OpenXmlCompositeElement
	{
		// Token: 0x17009722 RID: 38690
		// (get) Token: 0x0601AF4A RID: 110410 RVA: 0x00369E5F File Offset: 0x0036805F
		public override string LocalName
		{
			get
			{
				return "endpoint";
			}
		}

		// Token: 0x17009723 RID: 38691
		// (get) Token: 0x0601AF4B RID: 110411 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x17009724 RID: 38692
		// (get) Token: 0x0601AF4C RID: 110412 RVA: 0x00369E66 File Offset: 0x00368066
		internal override int ElementTypeId
		{
			get
			{
				return 12683;
			}
		}

		// Token: 0x0601AF4D RID: 110413 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009725 RID: 38693
		// (get) Token: 0x0601AF4E RID: 110414 RVA: 0x00369E6D File Offset: 0x0036806D
		internal override string[] AttributeTagNames
		{
			get
			{
				return EndPoint.attributeTagNames;
			}
		}

		// Token: 0x17009726 RID: 38694
		// (get) Token: 0x0601AF4F RID: 110415 RVA: 0x00369E74 File Offset: 0x00368074
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EndPoint.attributeNamespaceIds;
			}
		}

		// Token: 0x17009727 RID: 38695
		// (get) Token: 0x0601AF50 RID: 110416 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AF51 RID: 110417 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17009728 RID: 38696
		// (get) Token: 0x0601AF52 RID: 110418 RVA: 0x00369E7B File Offset: 0x0036807B
		// (set) Token: 0x0601AF53 RID: 110419 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(44, "endpoint-role")]
		public EnumValue<EndPointRoleValues> EndpointRole
		{
			get
			{
				return (EnumValue<EndPointRoleValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009729 RID: 38697
		// (get) Token: 0x0601AF54 RID: 110420 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601AF55 RID: 110421 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(44, "endpoint-address")]
		public StringValue EndPointAddress
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

		// Token: 0x1700972A RID: 38698
		// (get) Token: 0x0601AF56 RID: 110422 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601AF57 RID: 110423 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(44, "message-id")]
		public StringValue MessageId
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

		// Token: 0x1700972B RID: 38699
		// (get) Token: 0x0601AF58 RID: 110424 RVA: 0x002C92DB File Offset: 0x002C74DB
		// (set) Token: 0x0601AF59 RID: 110425 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(44, "port-num")]
		public IntegerValue PortNumber
		{
			get
			{
				return (IntegerValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700972C RID: 38700
		// (get) Token: 0x0601AF5A RID: 110426 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0601AF5B RID: 110427 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(44, "port-type")]
		public StringValue PortType
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

		// Token: 0x1700972D RID: 38701
		// (get) Token: 0x0601AF5C RID: 110428 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0601AF5D RID: 110429 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(44, "endpoint-pair-ref")]
		public StringValue EndpointPairRef
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

		// Token: 0x1700972E RID: 38702
		// (get) Token: 0x0601AF5E RID: 110430 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0601AF5F RID: 110431 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(44, "service-name")]
		public StringValue ServiceName
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700972F RID: 38703
		// (get) Token: 0x0601AF60 RID: 110432 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601AF61 RID: 110433 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(44, "media-type")]
		public StringValue MediaType
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17009730 RID: 38704
		// (get) Token: 0x0601AF62 RID: 110434 RVA: 0x00369E8A File Offset: 0x0036808A
		// (set) Token: 0x0601AF63 RID: 110435 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(44, "medium")]
		public ListValue<EnumValue<MediumValues>> Medium
		{
			get
			{
				return (ListValue<EnumValue<MediumValues>>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17009731 RID: 38705
		// (get) Token: 0x0601AF64 RID: 110436 RVA: 0x00369E9A File Offset: 0x0036809A
		// (set) Token: 0x0601AF65 RID: 110437 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(44, "mode")]
		public ListValue<StringValue> Mode
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x0601AF66 RID: 110438 RVA: 0x00293ECF File Offset: 0x002920CF
		public EndPoint()
		{
		}

		// Token: 0x0601AF67 RID: 110439 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EndPoint(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF68 RID: 110440 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EndPoint(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF69 RID: 110441 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EndPoint(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AF6A RID: 110442 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x0601AF6B RID: 110443 RVA: 0x00369EAC File Offset: 0x003680AC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "endpoint-role" == name)
			{
				return new EnumValue<EndPointRoleValues>();
			}
			if (44 == namespaceId && "endpoint-address" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "message-id" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "port-num" == name)
			{
				return new IntegerValue();
			}
			if (44 == namespaceId && "port-type" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "endpoint-pair-ref" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "service-name" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "media-type" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "medium" == name)
			{
				return new ListValue<EnumValue<MediumValues>>();
			}
			if (44 == namespaceId && "mode" == name)
			{
				return new ListValue<StringValue>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AF6C RID: 110444 RVA: 0x00369FC7 File Offset: 0x003681C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndPoint>(deep);
		}

		// Token: 0x0400B248 RID: 45640
		private const string tagName = "endpoint";

		// Token: 0x0400B249 RID: 45641
		private const byte tagNsId = 44;

		// Token: 0x0400B24A RID: 45642
		internal const int ElementTypeIdConst = 12683;

		// Token: 0x0400B24B RID: 45643
		private static string[] attributeTagNames = new string[]
		{
			"id", "endpoint-role", "endpoint-address", "message-id", "port-num", "port-type", "endpoint-pair-ref", "service-name", "media-type", "medium",
			"mode"
		};

		// Token: 0x0400B24C RID: 45644
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 44, 44, 44, 44, 44, 44, 44, 44, 44,
			44
		};
	}
}
