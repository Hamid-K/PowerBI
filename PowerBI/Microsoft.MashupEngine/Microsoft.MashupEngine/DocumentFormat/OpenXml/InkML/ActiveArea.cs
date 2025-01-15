using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003091 RID: 12433
	[GeneratedCode("DomGen", "2.0")]
	internal class ActiveArea : OpenXmlLeafElement
	{
		// Token: 0x170097A3 RID: 38819
		// (get) Token: 0x0601B077 RID: 110711 RVA: 0x0036AE0F File Offset: 0x0036900F
		public override string LocalName
		{
			get
			{
				return "activeArea";
			}
		}

		// Token: 0x170097A4 RID: 38820
		// (get) Token: 0x0601B078 RID: 110712 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097A5 RID: 38821
		// (get) Token: 0x0601B079 RID: 110713 RVA: 0x0036AE16 File Offset: 0x00369016
		internal override int ElementTypeId
		{
			get
			{
				return 12654;
			}
		}

		// Token: 0x0601B07A RID: 110714 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170097A6 RID: 38822
		// (get) Token: 0x0601B07B RID: 110715 RVA: 0x0036AE1D File Offset: 0x0036901D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ActiveArea.attributeTagNames;
			}
		}

		// Token: 0x170097A7 RID: 38823
		// (get) Token: 0x0601B07C RID: 110716 RVA: 0x0036AE24 File Offset: 0x00369024
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ActiveArea.attributeNamespaceIds;
			}
		}

		// Token: 0x170097A8 RID: 38824
		// (get) Token: 0x0601B07D RID: 110717 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B07E RID: 110718 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "size")]
		public StringValue Size
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

		// Token: 0x170097A9 RID: 38825
		// (get) Token: 0x0601B07F RID: 110719 RVA: 0x0036A078 File Offset: 0x00368278
		// (set) Token: 0x0601B080 RID: 110720 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "height")]
		public DecimalValue Height
		{
			get
			{
				return (DecimalValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170097AA RID: 38826
		// (get) Token: 0x0601B081 RID: 110721 RVA: 0x0036A087 File Offset: 0x00368287
		// (set) Token: 0x0601B082 RID: 110722 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "width")]
		public DecimalValue Width
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

		// Token: 0x170097AB RID: 38827
		// (get) Token: 0x0601B083 RID: 110723 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601B084 RID: 110724 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x0601B086 RID: 110726 RVA: 0x0036AE2C File Offset: 0x0036902C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "size" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "height" == name)
			{
				return new DecimalValue();
			}
			if (namespaceId == 0 && "width" == name)
			{
				return new DecimalValue();
			}
			if (namespaceId == 0 && "units" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B087 RID: 110727 RVA: 0x0036AE99 File Offset: 0x00369099
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ActiveArea>(deep);
		}

		// Token: 0x0601B088 RID: 110728 RVA: 0x0036AEA4 File Offset: 0x003690A4
		// Note: this type is marked as 'beforefieldinit'.
		static ActiveArea()
		{
			byte[] array = new byte[4];
			ActiveArea.attributeNamespaceIds = array;
		}

		// Token: 0x0400B2A2 RID: 45730
		private const string tagName = "activeArea";

		// Token: 0x0400B2A3 RID: 45731
		private const byte tagNsId = 43;

		// Token: 0x0400B2A4 RID: 45732
		internal const int ElementTypeIdConst = 12654;

		// Token: 0x0400B2A5 RID: 45733
		private static string[] attributeTagNames = new string[] { "size", "height", "width", "units" };

		// Token: 0x0400B2A6 RID: 45734
		private static byte[] attributeNamespaceIds;
	}
}
