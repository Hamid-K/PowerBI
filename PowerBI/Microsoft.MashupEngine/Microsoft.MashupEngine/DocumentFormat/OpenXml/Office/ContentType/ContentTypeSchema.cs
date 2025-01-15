using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.ContentType
{
	// Token: 0x020022B0 RID: 8880
	[GeneratedCode("DomGen", "2.0")]
	internal class ContentTypeSchema : OpenXmlCompositeElement
	{
		// Token: 0x17004147 RID: 16711
		// (get) Token: 0x0600F107 RID: 61703 RVA: 0x002D1089 File Offset: 0x002CF289
		public override string LocalName
		{
			get
			{
				return "contentTypeSchema";
			}
		}

		// Token: 0x17004148 RID: 16712
		// (get) Token: 0x0600F108 RID: 61704 RVA: 0x002D1090 File Offset: 0x002CF290
		internal override byte NamespaceId
		{
			get
			{
				return 38;
			}
		}

		// Token: 0x17004149 RID: 16713
		// (get) Token: 0x0600F109 RID: 61705 RVA: 0x002D1094 File Offset: 0x002CF294
		internal override int ElementTypeId
		{
			get
			{
				return 12634;
			}
		}

		// Token: 0x0600F10A RID: 61706 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700414A RID: 16714
		// (get) Token: 0x0600F10B RID: 61707 RVA: 0x002D109B File Offset: 0x002CF29B
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContentTypeSchema.attributeTagNames;
			}
		}

		// Token: 0x1700414B RID: 16715
		// (get) Token: 0x0600F10C RID: 61708 RVA: 0x002D10A2 File Offset: 0x002CF2A2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContentTypeSchema.attributeNamespaceIds;
			}
		}

		// Token: 0x1700414C RID: 16716
		// (get) Token: 0x0600F10D RID: 61709 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F10E RID: 61710 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(38, "_")]
		public StringValue UnderScore
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

		// Token: 0x1700414D RID: 16717
		// (get) Token: 0x0600F10F RID: 61711 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F110 RID: 61712 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(41, "_")]
		public StringValue ReservedAttributeString
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

		// Token: 0x1700414E RID: 16718
		// (get) Token: 0x0600F111 RID: 61713 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F112 RID: 61714 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(41, "contentTypeName")]
		public StringValue ContentTypeName
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

		// Token: 0x1700414F RID: 16719
		// (get) Token: 0x0600F113 RID: 61715 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F114 RID: 61716 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(41, "contentTypeID")]
		public StringValue ContentTypeID
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

		// Token: 0x17004150 RID: 16720
		// (get) Token: 0x0600F115 RID: 61717 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x0600F116 RID: 61718 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(41, "contentTypeVersion")]
		public Int32Value ContentTypeVersion
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17004151 RID: 16721
		// (get) Token: 0x0600F117 RID: 61719 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F118 RID: 61720 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(41, "contentTypeDescription")]
		public StringValue ContentTypeDescription
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

		// Token: 0x17004152 RID: 16722
		// (get) Token: 0x0600F119 RID: 61721 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F11A RID: 61722 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(41, "contentTypeScope")]
		public StringValue ContentTypeScope
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

		// Token: 0x17004153 RID: 16723
		// (get) Token: 0x0600F11B RID: 61723 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F11C RID: 61724 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(41, "versionID")]
		public StringValue VersionID
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

		// Token: 0x0600F11D RID: 61725 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContentTypeSchema()
		{
		}

		// Token: 0x0600F11E RID: 61726 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContentTypeSchema(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F11F RID: 61727 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContentTypeSchema(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F120 RID: 61728 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContentTypeSchema(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F121 RID: 61729 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x0600F122 RID: 61730 RVA: 0x002D10AC File Offset: 0x002CF2AC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (38 == namespaceId && "_" == name)
			{
				return new StringValue();
			}
			if (41 == namespaceId && "_" == name)
			{
				return new StringValue();
			}
			if (41 == namespaceId && "contentTypeName" == name)
			{
				return new StringValue();
			}
			if (41 == namespaceId && "contentTypeID" == name)
			{
				return new StringValue();
			}
			if (41 == namespaceId && "contentTypeVersion" == name)
			{
				return new Int32Value();
			}
			if (41 == namespaceId && "contentTypeDescription" == name)
			{
				return new StringValue();
			}
			if (41 == namespaceId && "contentTypeScope" == name)
			{
				return new StringValue();
			}
			if (41 == namespaceId && "versionID" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F123 RID: 61731 RVA: 0x002D1181 File Offset: 0x002CF381
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContentTypeSchema>(deep);
		}

		// Token: 0x040070AA RID: 28842
		private const string tagName = "contentTypeSchema";

		// Token: 0x040070AB RID: 28843
		private const byte tagNsId = 38;

		// Token: 0x040070AC RID: 28844
		internal const int ElementTypeIdConst = 12634;

		// Token: 0x040070AD RID: 28845
		private static string[] attributeTagNames = new string[] { "_", "_", "contentTypeName", "contentTypeID", "contentTypeVersion", "contentTypeDescription", "contentTypeScope", "versionID" };

		// Token: 0x040070AE RID: 28846
		private static byte[] attributeNamespaceIds = new byte[] { 38, 41, 41, 41, 41, 41, 41, 41 };
	}
}
