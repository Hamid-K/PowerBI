using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BAF RID: 11183
	[GeneratedCode("DomGen", "2.0")]
	internal class PhoneticProperties : OpenXmlLeafElement
	{
		// Token: 0x17007B89 RID: 31625
		// (get) Token: 0x06017319 RID: 95001 RVA: 0x00333B3E File Offset: 0x00331D3E
		public override string LocalName
		{
			get
			{
				return "phoneticPr";
			}
		}

		// Token: 0x17007B8A RID: 31626
		// (get) Token: 0x0601731A RID: 95002 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B8B RID: 31627
		// (get) Token: 0x0601731B RID: 95003 RVA: 0x00333B45 File Offset: 0x00331D45
		internal override int ElementTypeId
		{
			get
			{
				return 11154;
			}
		}

		// Token: 0x0601731C RID: 95004 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007B8C RID: 31628
		// (get) Token: 0x0601731D RID: 95005 RVA: 0x00333B4C File Offset: 0x00331D4C
		internal override string[] AttributeTagNames
		{
			get
			{
				return PhoneticProperties.attributeTagNames;
			}
		}

		// Token: 0x17007B8D RID: 31629
		// (get) Token: 0x0601731E RID: 95006 RVA: 0x00333B53 File Offset: 0x00331D53
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PhoneticProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B8E RID: 31630
		// (get) Token: 0x0601731F RID: 95007 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017320 RID: 95008 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fontId")]
		public UInt32Value FontId
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

		// Token: 0x17007B8F RID: 31631
		// (get) Token: 0x06017321 RID: 95009 RVA: 0x00333B5A File Offset: 0x00331D5A
		// (set) Token: 0x06017322 RID: 95010 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public EnumValue<PhoneticValues> Type
		{
			get
			{
				return (EnumValue<PhoneticValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007B90 RID: 31632
		// (get) Token: 0x06017323 RID: 95011 RVA: 0x00333B69 File Offset: 0x00331D69
		// (set) Token: 0x06017324 RID: 95012 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "alignment")]
		public EnumValue<PhoneticAlignmentValues> Alignment
		{
			get
			{
				return (EnumValue<PhoneticAlignmentValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06017326 RID: 95014 RVA: 0x00333B78 File Offset: 0x00331D78
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fontId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<PhoneticValues>();
			}
			if (namespaceId == 0 && "alignment" == name)
			{
				return new EnumValue<PhoneticAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017327 RID: 95015 RVA: 0x00333BCF File Offset: 0x00331DCF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PhoneticProperties>(deep);
		}

		// Token: 0x06017328 RID: 95016 RVA: 0x00333BD8 File Offset: 0x00331DD8
		// Note: this type is marked as 'beforefieldinit'.
		static PhoneticProperties()
		{
			byte[] array = new byte[3];
			PhoneticProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009B8A RID: 39818
		private const string tagName = "phoneticPr";

		// Token: 0x04009B8B RID: 39819
		private const byte tagNsId = 22;

		// Token: 0x04009B8C RID: 39820
		internal const int ElementTypeIdConst = 11154;

		// Token: 0x04009B8D RID: 39821
		private static string[] attributeTagNames = new string[] { "fontId", "type", "alignment" };

		// Token: 0x04009B8E RID: 39822
		private static byte[] attributeNamespaceIds;
	}
}
