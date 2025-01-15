using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BE6 RID: 11238
	[GeneratedCode("DomGen", "2.0")]
	internal class WebPublishItem : OpenXmlLeafElement
	{
		// Token: 0x17007E01 RID: 32257
		// (get) Token: 0x06017851 RID: 96337 RVA: 0x00337DA4 File Offset: 0x00335FA4
		public override string LocalName
		{
			get
			{
				return "webPublishItem";
			}
		}

		// Token: 0x17007E02 RID: 32258
		// (get) Token: 0x06017852 RID: 96338 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E03 RID: 32259
		// (get) Token: 0x06017853 RID: 96339 RVA: 0x00337DAB File Offset: 0x00335FAB
		internal override int ElementTypeId
		{
			get
			{
				return 11210;
			}
		}

		// Token: 0x06017854 RID: 96340 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E04 RID: 32260
		// (get) Token: 0x06017855 RID: 96341 RVA: 0x00337DB2 File Offset: 0x00335FB2
		internal override string[] AttributeTagNames
		{
			get
			{
				return WebPublishItem.attributeTagNames;
			}
		}

		// Token: 0x17007E05 RID: 32261
		// (get) Token: 0x06017856 RID: 96342 RVA: 0x00337DB9 File Offset: 0x00335FB9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WebPublishItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E06 RID: 32262
		// (get) Token: 0x06017857 RID: 96343 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017858 RID: 96344 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
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

		// Token: 0x17007E07 RID: 32263
		// (get) Token: 0x06017859 RID: 96345 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601785A RID: 96346 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "divId")]
		public StringValue DivId
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

		// Token: 0x17007E08 RID: 32264
		// (get) Token: 0x0601785B RID: 96347 RVA: 0x00337DC0 File Offset: 0x00335FC0
		// (set) Token: 0x0601785C RID: 96348 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "sourceType")]
		public EnumValue<WebSourceValues> SourceType
		{
			get
			{
				return (EnumValue<WebSourceValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007E09 RID: 32265
		// (get) Token: 0x0601785D RID: 96349 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601785E RID: 96350 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sourceRef")]
		public StringValue SourceRef
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

		// Token: 0x17007E0A RID: 32266
		// (get) Token: 0x0601785F RID: 96351 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06017860 RID: 96352 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "sourceObject")]
		public StringValue SourceObject
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

		// Token: 0x17007E0B RID: 32267
		// (get) Token: 0x06017861 RID: 96353 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06017862 RID: 96354 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "destinationFile")]
		public StringValue DestinationFile
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

		// Token: 0x17007E0C RID: 32268
		// (get) Token: 0x06017863 RID: 96355 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06017864 RID: 96356 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "title")]
		public StringValue Title
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

		// Token: 0x17007E0D RID: 32269
		// (get) Token: 0x06017865 RID: 96357 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017866 RID: 96358 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "autoRepublish")]
		public BooleanValue AutoRepublish
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x06017868 RID: 96360 RVA: 0x00337DD0 File Offset: 0x00335FD0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "divId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sourceType" == name)
			{
				return new EnumValue<WebSourceValues>();
			}
			if (namespaceId == 0 && "sourceRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sourceObject" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "destinationFile" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "title" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "autoRepublish" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017869 RID: 96361 RVA: 0x00337E95 File Offset: 0x00336095
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WebPublishItem>(deep);
		}

		// Token: 0x0601786A RID: 96362 RVA: 0x00337EA0 File Offset: 0x003360A0
		// Note: this type is marked as 'beforefieldinit'.
		static WebPublishItem()
		{
			byte[] array = new byte[8];
			WebPublishItem.attributeNamespaceIds = array;
		}

		// Token: 0x04009C9E RID: 40094
		private const string tagName = "webPublishItem";

		// Token: 0x04009C9F RID: 40095
		private const byte tagNsId = 22;

		// Token: 0x04009CA0 RID: 40096
		internal const int ElementTypeIdConst = 11210;

		// Token: 0x04009CA1 RID: 40097
		private static string[] attributeTagNames = new string[] { "id", "divId", "sourceType", "sourceRef", "sourceObject", "destinationFile", "title", "autoRepublish" };

		// Token: 0x04009CA2 RID: 40098
		private static byte[] attributeNamespaceIds;
	}
}
