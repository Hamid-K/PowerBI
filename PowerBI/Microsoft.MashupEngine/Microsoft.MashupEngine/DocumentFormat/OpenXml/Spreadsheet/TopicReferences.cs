using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C33 RID: 11315
	[GeneratedCode("DomGen", "2.0")]
	internal class TopicReferences : OpenXmlLeafElement
	{
		// Token: 0x17008102 RID: 33026
		// (get) Token: 0x06017EDE RID: 98014 RVA: 0x0030E261 File Offset: 0x0030C461
		public override string LocalName
		{
			get
			{
				return "tr";
			}
		}

		// Token: 0x17008103 RID: 33027
		// (get) Token: 0x06017EDF RID: 98015 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008104 RID: 33028
		// (get) Token: 0x06017EE0 RID: 98016 RVA: 0x0033CB12 File Offset: 0x0033AD12
		internal override int ElementTypeId
		{
			get
			{
				return 11297;
			}
		}

		// Token: 0x06017EE1 RID: 98017 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008105 RID: 33029
		// (get) Token: 0x06017EE2 RID: 98018 RVA: 0x0033CB19 File Offset: 0x0033AD19
		internal override string[] AttributeTagNames
		{
			get
			{
				return TopicReferences.attributeTagNames;
			}
		}

		// Token: 0x17008106 RID: 33030
		// (get) Token: 0x06017EE3 RID: 98019 RVA: 0x0033CB20 File Offset: 0x0033AD20
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TopicReferences.attributeNamespaceIds;
			}
		}

		// Token: 0x17008107 RID: 33031
		// (get) Token: 0x06017EE4 RID: 98020 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017EE5 RID: 98021 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public StringValue CellReference
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

		// Token: 0x17008108 RID: 33032
		// (get) Token: 0x06017EE6 RID: 98022 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017EE7 RID: 98023 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "s")]
		public UInt32Value SheetId
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06017EE9 RID: 98025 RVA: 0x0033CB27 File Offset: 0x0033AD27
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017EEA RID: 98026 RVA: 0x0033CB5D File Offset: 0x0033AD5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopicReferences>(deep);
		}

		// Token: 0x06017EEB RID: 98027 RVA: 0x0033CB68 File Offset: 0x0033AD68
		// Note: this type is marked as 'beforefieldinit'.
		static TopicReferences()
		{
			byte[] array = new byte[2];
			TopicReferences.attributeNamespaceIds = array;
		}

		// Token: 0x04009E31 RID: 40497
		private const string tagName = "tr";

		// Token: 0x04009E32 RID: 40498
		private const byte tagNsId = 22;

		// Token: 0x04009E33 RID: 40499
		internal const int ElementTypeIdConst = 11297;

		// Token: 0x04009E34 RID: 40500
		private static string[] attributeTagNames = new string[] { "r", "s" };

		// Token: 0x04009E35 RID: 40501
		private static byte[] attributeNamespaceIds;
	}
}
