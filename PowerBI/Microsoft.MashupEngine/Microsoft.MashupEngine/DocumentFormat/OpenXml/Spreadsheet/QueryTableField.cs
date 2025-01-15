using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B92 RID: 11154
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class QueryTableField : OpenXmlCompositeElement
	{
		// Token: 0x17007B04 RID: 31492
		// (get) Token: 0x060171F8 RID: 94712 RVA: 0x003330A7 File Offset: 0x003312A7
		public override string LocalName
		{
			get
			{
				return "queryTableField";
			}
		}

		// Token: 0x17007B05 RID: 31493
		// (get) Token: 0x060171F9 RID: 94713 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B06 RID: 31494
		// (get) Token: 0x060171FA RID: 94714 RVA: 0x003330AE File Offset: 0x003312AE
		internal override int ElementTypeId
		{
			get
			{
				return 11133;
			}
		}

		// Token: 0x060171FB RID: 94715 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007B07 RID: 31495
		// (get) Token: 0x060171FC RID: 94716 RVA: 0x003330B5 File Offset: 0x003312B5
		internal override string[] AttributeTagNames
		{
			get
			{
				return QueryTableField.attributeTagNames;
			}
		}

		// Token: 0x17007B08 RID: 31496
		// (get) Token: 0x060171FD RID: 94717 RVA: 0x003330BC File Offset: 0x003312BC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return QueryTableField.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B09 RID: 31497
		// (get) Token: 0x060171FE RID: 94718 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060171FF RID: 94719 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007B0A RID: 31498
		// (get) Token: 0x06017200 RID: 94720 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017201 RID: 94721 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17007B0B RID: 31499
		// (get) Token: 0x06017202 RID: 94722 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017203 RID: 94723 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "dataBound")]
		public BooleanValue DataBound
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007B0C RID: 31500
		// (get) Token: 0x06017204 RID: 94724 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017205 RID: 94725 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "rowNumbers")]
		public BooleanValue RowNumbers
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007B0D RID: 31501
		// (get) Token: 0x06017206 RID: 94726 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017207 RID: 94727 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "fillFormulas")]
		public BooleanValue FillFormulas
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007B0E RID: 31502
		// (get) Token: 0x06017208 RID: 94728 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017209 RID: 94729 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "clipped")]
		public BooleanValue Clipped
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

		// Token: 0x17007B0F RID: 31503
		// (get) Token: 0x0601720A RID: 94730 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x0601720B RID: 94731 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "tableColumnId")]
		public UInt32Value TableColumnId
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x0601720C RID: 94732 RVA: 0x00293ECF File Offset: 0x002920CF
		public QueryTableField()
		{
		}

		// Token: 0x0601720D RID: 94733 RVA: 0x00293ED7 File Offset: 0x002920D7
		public QueryTableField(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601720E RID: 94734 RVA: 0x00293EE0 File Offset: 0x002920E0
		public QueryTableField(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601720F RID: 94735 RVA: 0x00293EE9 File Offset: 0x002920E9
		public QueryTableField(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017210 RID: 94736 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007B10 RID: 31504
		// (get) Token: 0x06017211 RID: 94737 RVA: 0x003330C3 File Offset: 0x003312C3
		internal override string[] ElementTagNames
		{
			get
			{
				return QueryTableField.eleTagNames;
			}
		}

		// Token: 0x17007B11 RID: 31505
		// (get) Token: 0x06017212 RID: 94738 RVA: 0x003330CA File Offset: 0x003312CA
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return QueryTableField.eleNamespaceIds;
			}
		}

		// Token: 0x17007B12 RID: 31506
		// (get) Token: 0x06017213 RID: 94739 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007B13 RID: 31507
		// (get) Token: 0x06017214 RID: 94740 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x06017215 RID: 94741 RVA: 0x00332911 File Offset: 0x00330B11
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06017216 RID: 94742 RVA: 0x003330D4 File Offset: 0x003312D4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dataBound" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rowNumbers" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fillFormulas" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "clipped" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "tableColumnId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017217 RID: 94743 RVA: 0x00333183 File Offset: 0x00331383
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QueryTableField>(deep);
		}

		// Token: 0x06017218 RID: 94744 RVA: 0x0033318C File Offset: 0x0033138C
		// Note: this type is marked as 'beforefieldinit'.
		static QueryTableField()
		{
			byte[] array = new byte[7];
			QueryTableField.attributeNamespaceIds = array;
			QueryTableField.eleTagNames = new string[] { "extLst" };
			QueryTableField.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009B23 RID: 39715
		private const string tagName = "queryTableField";

		// Token: 0x04009B24 RID: 39716
		private const byte tagNsId = 22;

		// Token: 0x04009B25 RID: 39717
		internal const int ElementTypeIdConst = 11133;

		// Token: 0x04009B26 RID: 39718
		private static string[] attributeTagNames = new string[] { "id", "name", "dataBound", "rowNumbers", "fillFormulas", "clipped", "tableColumnId" };

		// Token: 0x04009B27 RID: 39719
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009B28 RID: 39720
		private static readonly string[] eleTagNames;

		// Token: 0x04009B29 RID: 39721
		private static readonly byte[] eleNamespaceIds;
	}
}
