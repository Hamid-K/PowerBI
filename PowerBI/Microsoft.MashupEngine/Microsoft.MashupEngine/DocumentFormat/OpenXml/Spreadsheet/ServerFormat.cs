using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B6C RID: 11116
	[GeneratedCode("DomGen", "2.0")]
	internal class ServerFormat : OpenXmlLeafElement
	{
		// Token: 0x1700792F RID: 31023
		// (get) Token: 0x06016E17 RID: 93719 RVA: 0x00330123 File Offset: 0x0032E323
		public override string LocalName
		{
			get
			{
				return "serverFormat";
			}
		}

		// Token: 0x17007930 RID: 31024
		// (get) Token: 0x06016E18 RID: 93720 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007931 RID: 31025
		// (get) Token: 0x06016E19 RID: 93721 RVA: 0x0033012A File Offset: 0x0032E32A
		internal override int ElementTypeId
		{
			get
			{
				return 11095;
			}
		}

		// Token: 0x06016E1A RID: 93722 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007932 RID: 31026
		// (get) Token: 0x06016E1B RID: 93723 RVA: 0x00330131 File Offset: 0x0032E331
		internal override string[] AttributeTagNames
		{
			get
			{
				return ServerFormat.attributeTagNames;
			}
		}

		// Token: 0x17007933 RID: 31027
		// (get) Token: 0x06016E1C RID: 93724 RVA: 0x00330138 File Offset: 0x0032E338
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ServerFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x17007934 RID: 31028
		// (get) Token: 0x06016E1D RID: 93725 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016E1E RID: 93726 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "culture")]
		public StringValue Culture
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

		// Token: 0x17007935 RID: 31029
		// (get) Token: 0x06016E1F RID: 93727 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016E20 RID: 93728 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "format")]
		public StringValue Format
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

		// Token: 0x06016E22 RID: 93730 RVA: 0x0033013F File Offset: 0x0032E33F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "culture" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "format" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016E23 RID: 93731 RVA: 0x00330175 File Offset: 0x0032E375
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ServerFormat>(deep);
		}

		// Token: 0x06016E24 RID: 93732 RVA: 0x00330180 File Offset: 0x0032E380
		// Note: this type is marked as 'beforefieldinit'.
		static ServerFormat()
		{
			byte[] array = new byte[2];
			ServerFormat.attributeNamespaceIds = array;
		}

		// Token: 0x04009A50 RID: 39504
		private const string tagName = "serverFormat";

		// Token: 0x04009A51 RID: 39505
		private const byte tagNsId = 22;

		// Token: 0x04009A52 RID: 39506
		internal const int ElementTypeIdConst = 11095;

		// Token: 0x04009A53 RID: 39507
		private static string[] attributeTagNames = new string[] { "culture", "format" };

		// Token: 0x04009A54 RID: 39508
		private static byte[] attributeNamespaceIds;
	}
}
