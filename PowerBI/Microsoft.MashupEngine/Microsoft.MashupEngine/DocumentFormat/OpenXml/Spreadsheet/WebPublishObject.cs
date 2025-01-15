using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C35 RID: 11317
	[GeneratedCode("DomGen", "2.0")]
	internal class WebPublishObject : OpenXmlLeafElement
	{
		// Token: 0x17008110 RID: 33040
		// (get) Token: 0x06017EFA RID: 98042 RVA: 0x0033CC38 File Offset: 0x0033AE38
		public override string LocalName
		{
			get
			{
				return "webPublishObject";
			}
		}

		// Token: 0x17008111 RID: 33041
		// (get) Token: 0x06017EFB RID: 98043 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008112 RID: 33042
		// (get) Token: 0x06017EFC RID: 98044 RVA: 0x0033CC3F File Offset: 0x0033AE3F
		internal override int ElementTypeId
		{
			get
			{
				return 11299;
			}
		}

		// Token: 0x06017EFD RID: 98045 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008113 RID: 33043
		// (get) Token: 0x06017EFE RID: 98046 RVA: 0x0033CC46 File Offset: 0x0033AE46
		internal override string[] AttributeTagNames
		{
			get
			{
				return WebPublishObject.attributeTagNames;
			}
		}

		// Token: 0x17008114 RID: 33044
		// (get) Token: 0x06017EFF RID: 98047 RVA: 0x0033CC4D File Offset: 0x0033AE4D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WebPublishObject.attributeNamespaceIds;
			}
		}

		// Token: 0x17008115 RID: 33045
		// (get) Token: 0x06017F00 RID: 98048 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017F01 RID: 98049 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008116 RID: 33046
		// (get) Token: 0x06017F02 RID: 98050 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017F03 RID: 98051 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008117 RID: 33047
		// (get) Token: 0x06017F04 RID: 98052 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06017F05 RID: 98053 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "sourceObject")]
		public StringValue SourceObject
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

		// Token: 0x17008118 RID: 33048
		// (get) Token: 0x06017F06 RID: 98054 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06017F07 RID: 98055 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "destinationFile")]
		public StringValue DestinationFile
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

		// Token: 0x17008119 RID: 33049
		// (get) Token: 0x06017F08 RID: 98056 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06017F09 RID: 98057 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "title")]
		public StringValue Title
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

		// Token: 0x1700811A RID: 33050
		// (get) Token: 0x06017F0A RID: 98058 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017F0B RID: 98059 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "autoRepublish")]
		public BooleanValue AutoRepublish
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x06017F0D RID: 98061 RVA: 0x0033CC54 File Offset: 0x0033AE54
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

		// Token: 0x06017F0E RID: 98062 RVA: 0x0033CCED File Offset: 0x0033AEED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WebPublishObject>(deep);
		}

		// Token: 0x06017F0F RID: 98063 RVA: 0x0033CCF8 File Offset: 0x0033AEF8
		// Note: this type is marked as 'beforefieldinit'.
		static WebPublishObject()
		{
			byte[] array = new byte[6];
			WebPublishObject.attributeNamespaceIds = array;
		}

		// Token: 0x04009E3B RID: 40507
		private const string tagName = "webPublishObject";

		// Token: 0x04009E3C RID: 40508
		private const byte tagNsId = 22;

		// Token: 0x04009E3D RID: 40509
		internal const int ElementTypeIdConst = 11299;

		// Token: 0x04009E3E RID: 40510
		private static string[] attributeTagNames = new string[] { "id", "divId", "sourceObject", "destinationFile", "title", "autoRepublish" };

		// Token: 0x04009E3F RID: 40511
		private static byte[] attributeNamespaceIds;
	}
}
