using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F36 RID: 12086
	[GeneratedCode("DomGen", "2.0")]
	internal class HelpText : OpenXmlLeafElement
	{
		// Token: 0x17008F7F RID: 36735
		// (get) Token: 0x06019ED1 RID: 106193 RVA: 0x00359EFE File Offset: 0x003580FE
		public override string LocalName
		{
			get
			{
				return "helpText";
			}
		}

		// Token: 0x17008F80 RID: 36736
		// (get) Token: 0x06019ED2 RID: 106194 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F81 RID: 36737
		// (get) Token: 0x06019ED3 RID: 106195 RVA: 0x00359F05 File Offset: 0x00358105
		internal override int ElementTypeId
		{
			get
			{
				return 11731;
			}
		}

		// Token: 0x06019ED4 RID: 106196 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008F82 RID: 36738
		// (get) Token: 0x06019ED5 RID: 106197 RVA: 0x00359F0C File Offset: 0x0035810C
		internal override string[] AttributeTagNames
		{
			get
			{
				return HelpText.attributeTagNames;
			}
		}

		// Token: 0x17008F83 RID: 36739
		// (get) Token: 0x06019ED6 RID: 106198 RVA: 0x00359F13 File Offset: 0x00358113
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HelpText.attributeNamespaceIds;
			}
		}

		// Token: 0x17008F84 RID: 36740
		// (get) Token: 0x06019ED7 RID: 106199 RVA: 0x00359F1A File Offset: 0x0035811A
		// (set) Token: 0x06019ED8 RID: 106200 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "type")]
		public EnumValue<InfoTextValues> Type
		{
			get
			{
				return (EnumValue<InfoTextValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008F85 RID: 36741
		// (get) Token: 0x06019ED9 RID: 106201 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019EDA RID: 106202 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x06019EDC RID: 106204 RVA: 0x00359F29 File Offset: 0x00358129
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<InfoTextValues>();
			}
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019EDD RID: 106205 RVA: 0x00359F63 File Offset: 0x00358163
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HelpText>(deep);
		}

		// Token: 0x0400AAED RID: 43757
		private const string tagName = "helpText";

		// Token: 0x0400AAEE RID: 43758
		private const byte tagNsId = 23;

		// Token: 0x0400AAEF RID: 43759
		internal const int ElementTypeIdConst = 11731;

		// Token: 0x0400AAF0 RID: 43760
		private static string[] attributeTagNames = new string[] { "type", "val" };

		// Token: 0x0400AAF1 RID: 43761
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
