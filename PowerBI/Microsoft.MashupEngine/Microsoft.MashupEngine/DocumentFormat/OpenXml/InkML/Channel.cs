using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200308B RID: 12427
	[ChildElementInfo(typeof(Mapping))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Channel : OpenXmlCompositeElement
	{
		// Token: 0x17009776 RID: 38774
		// (get) Token: 0x0601B011 RID: 110609 RVA: 0x0036A98B File Offset: 0x00368B8B
		public override string LocalName
		{
			get
			{
				return "channel";
			}
		}

		// Token: 0x17009777 RID: 38775
		// (get) Token: 0x0601B012 RID: 110610 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009778 RID: 38776
		// (get) Token: 0x0601B013 RID: 110611 RVA: 0x0036A992 File Offset: 0x00368B92
		internal override int ElementTypeId
		{
			get
			{
				return 12648;
			}
		}

		// Token: 0x0601B014 RID: 110612 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009779 RID: 38777
		// (get) Token: 0x0601B015 RID: 110613 RVA: 0x0036A999 File Offset: 0x00368B99
		internal override string[] AttributeTagNames
		{
			get
			{
				return Channel.attributeTagNames;
			}
		}

		// Token: 0x1700977A RID: 38778
		// (get) Token: 0x0601B016 RID: 110614 RVA: 0x0036A9A0 File Offset: 0x00368BA0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Channel.attributeNamespaceIds;
			}
		}

		// Token: 0x1700977B RID: 38779
		// (get) Token: 0x0601B017 RID: 110615 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B018 RID: 110616 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(1, "id")]
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

		// Token: 0x1700977C RID: 38780
		// (get) Token: 0x0601B019 RID: 110617 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B01A RID: 110618 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x1700977D RID: 38781
		// (get) Token: 0x0601B01B RID: 110619 RVA: 0x0036A9A7 File Offset: 0x00368BA7
		// (set) Token: 0x0601B01C RID: 110620 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "type")]
		public EnumValue<ChannelDataTypeValues> Type
		{
			get
			{
				return (EnumValue<ChannelDataTypeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700977E RID: 38782
		// (get) Token: 0x0601B01D RID: 110621 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601B01E RID: 110622 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "default")]
		public StringValue Default
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

		// Token: 0x1700977F RID: 38783
		// (get) Token: 0x0601B01F RID: 110623 RVA: 0x0036A9B6 File Offset: 0x00368BB6
		// (set) Token: 0x0601B020 RID: 110624 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "min")]
		public DecimalValue Min
		{
			get
			{
				return (DecimalValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17009780 RID: 38784
		// (get) Token: 0x0601B021 RID: 110625 RVA: 0x0036A9C5 File Offset: 0x00368BC5
		// (set) Token: 0x0601B022 RID: 110626 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "max")]
		public DecimalValue Max
		{
			get
			{
				return (DecimalValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17009781 RID: 38785
		// (get) Token: 0x0601B023 RID: 110627 RVA: 0x0036A9D4 File Offset: 0x00368BD4
		// (set) Token: 0x0601B024 RID: 110628 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "orientation")]
		public EnumValue<ChannelValueOrientationValues> Orientation
		{
			get
			{
				return (EnumValue<ChannelValueOrientationValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17009782 RID: 38786
		// (get) Token: 0x0601B025 RID: 110629 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0601B026 RID: 110630 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "respectTo")]
		public StringValue RespectTo
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

		// Token: 0x17009783 RID: 38787
		// (get) Token: 0x0601B027 RID: 110631 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601B028 RID: 110632 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "units")]
		public StringValue Units
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

		// Token: 0x0601B029 RID: 110633 RVA: 0x00293ECF File Offset: 0x002920CF
		public Channel()
		{
		}

		// Token: 0x0601B02A RID: 110634 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Channel(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B02B RID: 110635 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Channel(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B02C RID: 110636 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Channel(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B02D RID: 110637 RVA: 0x0036A9E3 File Offset: 0x00368BE3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "mapping" == name)
			{
				return new Mapping();
			}
			return null;
		}

		// Token: 0x0601B02E RID: 110638 RVA: 0x0036AA00 File Offset: 0x00368C00
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ChannelDataTypeValues>();
			}
			if (namespaceId == 0 && "default" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "min" == name)
			{
				return new DecimalValue();
			}
			if (namespaceId == 0 && "max" == name)
			{
				return new DecimalValue();
			}
			if (namespaceId == 0 && "orientation" == name)
			{
				return new EnumValue<ChannelValueOrientationValues>();
			}
			if (namespaceId == 0 && "respectTo" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "units" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B02F RID: 110639 RVA: 0x0036AADC File Offset: 0x00368CDC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Channel>(deep);
		}

		// Token: 0x0601B030 RID: 110640 RVA: 0x0036AAE8 File Offset: 0x00368CE8
		// Note: this type is marked as 'beforefieldinit'.
		static Channel()
		{
			byte[] array = new byte[9];
			array[0] = 1;
			Channel.attributeNamespaceIds = array;
		}

		// Token: 0x0400B286 RID: 45702
		private const string tagName = "channel";

		// Token: 0x0400B287 RID: 45703
		private const byte tagNsId = 43;

		// Token: 0x0400B288 RID: 45704
		internal const int ElementTypeIdConst = 12648;

		// Token: 0x0400B289 RID: 45705
		private static string[] attributeTagNames = new string[] { "id", "name", "type", "default", "min", "max", "orientation", "respectTo", "units" };

		// Token: 0x0400B28A RID: 45706
		private static byte[] attributeNamespaceIds;
	}
}
