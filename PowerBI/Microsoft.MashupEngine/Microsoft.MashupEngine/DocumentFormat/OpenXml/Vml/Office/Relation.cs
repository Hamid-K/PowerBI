using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200221A RID: 8730
	[GeneratedCode("DomGen", "2.0")]
	internal class Relation : OpenXmlLeafElement
	{
		// Token: 0x17003929 RID: 14633
		// (get) Token: 0x0600DFFB RID: 57339 RVA: 0x002BF8C0 File Offset: 0x002BDAC0
		public override string LocalName
		{
			get
			{
				return "rel";
			}
		}

		// Token: 0x1700392A RID: 14634
		// (get) Token: 0x0600DFFC RID: 57340 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x1700392B RID: 14635
		// (get) Token: 0x0600DFFD RID: 57341 RVA: 0x002BF8C7 File Offset: 0x002BDAC7
		internal override int ElementTypeId
		{
			get
			{
				return 12423;
			}
		}

		// Token: 0x0600DFFE RID: 57342 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700392C RID: 14636
		// (get) Token: 0x0600DFFF RID: 57343 RVA: 0x002BF8CE File Offset: 0x002BDACE
		internal override string[] AttributeTagNames
		{
			get
			{
				return Relation.attributeTagNames;
			}
		}

		// Token: 0x1700392D RID: 14637
		// (get) Token: 0x0600E000 RID: 57344 RVA: 0x002BF8D5 File Offset: 0x002BDAD5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Relation.attributeNamespaceIds;
			}
		}

		// Token: 0x1700392E RID: 14638
		// (get) Token: 0x0600E001 RID: 57345 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600E002 RID: 57346 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700392F RID: 14639
		// (get) Token: 0x0600E003 RID: 57347 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E004 RID: 57348 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idsrc")]
		public StringValue SourceId
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

		// Token: 0x17003930 RID: 14640
		// (get) Token: 0x0600E005 RID: 57349 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E006 RID: 57350 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "iddest")]
		public StringValue DestinationId
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

		// Token: 0x17003931 RID: 14641
		// (get) Token: 0x0600E007 RID: 57351 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E008 RID: 57352 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idcntr")]
		public StringValue CenterShapeId
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

		// Token: 0x0600E00A RID: 57354 RVA: 0x002BF8DC File Offset: 0x002BDADC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "idsrc" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "iddest" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idcntr" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E00B RID: 57355 RVA: 0x002BF94B File Offset: 0x002BDB4B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Relation>(deep);
		}

		// Token: 0x0600E00C RID: 57356 RVA: 0x002BF954 File Offset: 0x002BDB54
		// Note: this type is marked as 'beforefieldinit'.
		static Relation()
		{
			byte[] array = new byte[4];
			array[0] = 26;
			Relation.attributeNamespaceIds = array;
		}

		// Token: 0x04006DC6 RID: 28102
		private const string tagName = "rel";

		// Token: 0x04006DC7 RID: 28103
		private const byte tagNsId = 27;

		// Token: 0x04006DC8 RID: 28104
		internal const int ElementTypeIdConst = 12423;

		// Token: 0x04006DC9 RID: 28105
		private static string[] attributeTagNames = new string[] { "ext", "idsrc", "iddest", "idcntr" };

		// Token: 0x04006DCA RID: 28106
		private static byte[] attributeNamespaceIds;
	}
}
