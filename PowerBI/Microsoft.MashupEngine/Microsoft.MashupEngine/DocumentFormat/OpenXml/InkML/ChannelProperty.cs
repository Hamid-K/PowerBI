using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200308D RID: 12429
	[GeneratedCode("DomGen", "2.0")]
	internal class ChannelProperty : OpenXmlLeafElement
	{
		// Token: 0x17009787 RID: 38791
		// (get) Token: 0x0601B03B RID: 110651 RVA: 0x0036AB8F File Offset: 0x00368D8F
		public override string LocalName
		{
			get
			{
				return "channelProperty";
			}
		}

		// Token: 0x17009788 RID: 38792
		// (get) Token: 0x0601B03C RID: 110652 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009789 RID: 38793
		// (get) Token: 0x0601B03D RID: 110653 RVA: 0x0036AB96 File Offset: 0x00368D96
		internal override int ElementTypeId
		{
			get
			{
				return 12650;
			}
		}

		// Token: 0x0601B03E RID: 110654 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700978A RID: 38794
		// (get) Token: 0x0601B03F RID: 110655 RVA: 0x0036AB9D File Offset: 0x00368D9D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ChannelProperty.attributeTagNames;
			}
		}

		// Token: 0x1700978B RID: 38795
		// (get) Token: 0x0601B040 RID: 110656 RVA: 0x0036ABA4 File Offset: 0x00368DA4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ChannelProperty.attributeNamespaceIds;
			}
		}

		// Token: 0x1700978C RID: 38796
		// (get) Token: 0x0601B041 RID: 110657 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B042 RID: 110658 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "channel")]
		public StringValue Channel
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

		// Token: 0x1700978D RID: 38797
		// (get) Token: 0x0601B043 RID: 110659 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B044 RID: 110660 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700978E RID: 38798
		// (get) Token: 0x0601B045 RID: 110661 RVA: 0x0036A087 File Offset: 0x00368287
		// (set) Token: 0x0601B046 RID: 110662 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "value")]
		public DecimalValue Value
		{
			get
			{
				return (DecimalValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700978F RID: 38799
		// (get) Token: 0x0601B047 RID: 110663 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601B048 RID: 110664 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "units")]
		public StringValue Units
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

		// Token: 0x0601B04A RID: 110666 RVA: 0x0036ABAC File Offset: 0x00368DAC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "channel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "value" == name)
			{
				return new DecimalValue();
			}
			if (namespaceId == 0 && "units" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B04B RID: 110667 RVA: 0x0036AC19 File Offset: 0x00368E19
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChannelProperty>(deep);
		}

		// Token: 0x0601B04C RID: 110668 RVA: 0x0036AC24 File Offset: 0x00368E24
		// Note: this type is marked as 'beforefieldinit'.
		static ChannelProperty()
		{
			byte[] array = new byte[4];
			ChannelProperty.attributeNamespaceIds = array;
		}

		// Token: 0x0400B28E RID: 45710
		private const string tagName = "channelProperty";

		// Token: 0x0400B28F RID: 45711
		private const byte tagNsId = 43;

		// Token: 0x0400B290 RID: 45712
		internal const int ElementTypeIdConst = 12650;

		// Token: 0x0400B291 RID: 45713
		private static string[] attributeTagNames = new string[] { "channel", "name", "value", "units" };

		// Token: 0x0400B292 RID: 45714
		private static byte[] attributeNamespaceIds;
	}
}
