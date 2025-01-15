using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BB3 RID: 11187
	[GeneratedCode("DomGen", "2.0")]
	internal class RevisionCustomView : OpenXmlLeafElement
	{
		// Token: 0x17007BBC RID: 31676
		// (get) Token: 0x06017388 RID: 95112 RVA: 0x0033415B File Offset: 0x0033235B
		public override string LocalName
		{
			get
			{
				return "rcv";
			}
		}

		// Token: 0x17007BBD RID: 31677
		// (get) Token: 0x06017389 RID: 95113 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007BBE RID: 31678
		// (get) Token: 0x0601738A RID: 95114 RVA: 0x00334162 File Offset: 0x00332362
		internal override int ElementTypeId
		{
			get
			{
				return 11158;
			}
		}

		// Token: 0x0601738B RID: 95115 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007BBF RID: 31679
		// (get) Token: 0x0601738C RID: 95116 RVA: 0x00334169 File Offset: 0x00332369
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionCustomView.attributeTagNames;
			}
		}

		// Token: 0x17007BC0 RID: 31680
		// (get) Token: 0x0601738D RID: 95117 RVA: 0x00334170 File Offset: 0x00332370
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionCustomView.attributeNamespaceIds;
			}
		}

		// Token: 0x17007BC1 RID: 31681
		// (get) Token: 0x0601738E RID: 95118 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601738F RID: 95119 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "guid")]
		public StringValue Guid
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

		// Token: 0x17007BC2 RID: 31682
		// (get) Token: 0x06017390 RID: 95120 RVA: 0x00334177 File Offset: 0x00332377
		// (set) Token: 0x06017391 RID: 95121 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "action")]
		public EnumValue<RevisionActionValues> Action
		{
			get
			{
				return (EnumValue<RevisionActionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06017393 RID: 95123 RVA: 0x00334186 File Offset: 0x00332386
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "guid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "action" == name)
			{
				return new EnumValue<RevisionActionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017394 RID: 95124 RVA: 0x003341BC File Offset: 0x003323BC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionCustomView>(deep);
		}

		// Token: 0x06017395 RID: 95125 RVA: 0x003341C8 File Offset: 0x003323C8
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionCustomView()
		{
			byte[] array = new byte[2];
			RevisionCustomView.attributeNamespaceIds = array;
		}

		// Token: 0x04009BA0 RID: 39840
		private const string tagName = "rcv";

		// Token: 0x04009BA1 RID: 39841
		private const byte tagNsId = 22;

		// Token: 0x04009BA2 RID: 39842
		internal const int ElementTypeIdConst = 11158;

		// Token: 0x04009BA3 RID: 39843
		private static string[] attributeTagNames = new string[] { "guid", "action" };

		// Token: 0x04009BA4 RID: 39844
		private static byte[] attributeNamespaceIds;
	}
}
