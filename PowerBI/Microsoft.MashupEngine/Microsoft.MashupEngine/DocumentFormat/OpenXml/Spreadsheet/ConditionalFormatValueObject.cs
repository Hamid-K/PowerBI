using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BE0 RID: 11232
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConditionalFormatValueObject : OpenXmlCompositeElement
	{
		// Token: 0x17007DC0 RID: 32192
		// (get) Token: 0x060177CA RID: 96202 RVA: 0x002E9180 File Offset: 0x002E7380
		public override string LocalName
		{
			get
			{
				return "cfvo";
			}
		}

		// Token: 0x17007DC1 RID: 32193
		// (get) Token: 0x060177CB RID: 96203 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007DC2 RID: 32194
		// (get) Token: 0x060177CC RID: 96204 RVA: 0x00337768 File Offset: 0x00335968
		internal override int ElementTypeId
		{
			get
			{
				return 11204;
			}
		}

		// Token: 0x060177CD RID: 96205 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007DC3 RID: 32195
		// (get) Token: 0x060177CE RID: 96206 RVA: 0x0033776F File Offset: 0x0033596F
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormatValueObject.attributeTagNames;
			}
		}

		// Token: 0x17007DC4 RID: 32196
		// (get) Token: 0x060177CF RID: 96207 RVA: 0x00337776 File Offset: 0x00335976
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormatValueObject.attributeNamespaceIds;
			}
		}

		// Token: 0x17007DC5 RID: 32197
		// (get) Token: 0x060177D0 RID: 96208 RVA: 0x0033777D File Offset: 0x0033597D
		// (set) Token: 0x060177D1 RID: 96209 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<ConditionalFormatValueObjectValues> Type
		{
			get
			{
				return (EnumValue<ConditionalFormatValueObjectValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007DC6 RID: 32198
		// (get) Token: 0x060177D2 RID: 96210 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060177D3 RID: 96211 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "val")]
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

		// Token: 0x17007DC7 RID: 32199
		// (get) Token: 0x060177D4 RID: 96212 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060177D5 RID: 96213 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "gte")]
		public BooleanValue GreaterThanOrEqual
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

		// Token: 0x060177D6 RID: 96214 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormatValueObject()
		{
		}

		// Token: 0x060177D7 RID: 96215 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormatValueObject(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060177D8 RID: 96216 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormatValueObject(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060177D9 RID: 96217 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormatValueObject(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060177DA RID: 96218 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007DC8 RID: 32200
		// (get) Token: 0x060177DB RID: 96219 RVA: 0x0033778C File Offset: 0x0033598C
		internal override string[] ElementTagNames
		{
			get
			{
				return ConditionalFormatValueObject.eleTagNames;
			}
		}

		// Token: 0x17007DC9 RID: 32201
		// (get) Token: 0x060177DC RID: 96220 RVA: 0x00337793 File Offset: 0x00335993
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ConditionalFormatValueObject.eleNamespaceIds;
			}
		}

		// Token: 0x17007DCA RID: 32202
		// (get) Token: 0x060177DD RID: 96221 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007DCB RID: 32203
		// (get) Token: 0x060177DE RID: 96222 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x060177DF RID: 96223 RVA: 0x00332911 File Offset: 0x00330B11
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

		// Token: 0x060177E0 RID: 96224 RVA: 0x0033779C File Offset: 0x0033599C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ConditionalFormatValueObjectValues>();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "gte" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060177E1 RID: 96225 RVA: 0x003377F3 File Offset: 0x003359F3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormatValueObject>(deep);
		}

		// Token: 0x060177E2 RID: 96226 RVA: 0x003377FC File Offset: 0x003359FC
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormatValueObject()
		{
			byte[] array = new byte[3];
			ConditionalFormatValueObject.attributeNamespaceIds = array;
			ConditionalFormatValueObject.eleTagNames = new string[] { "extLst" };
			ConditionalFormatValueObject.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009C7E RID: 40062
		private const string tagName = "cfvo";

		// Token: 0x04009C7F RID: 40063
		private const byte tagNsId = 22;

		// Token: 0x04009C80 RID: 40064
		internal const int ElementTypeIdConst = 11204;

		// Token: 0x04009C81 RID: 40065
		private static string[] attributeTagNames = new string[] { "type", "val", "gte" };

		// Token: 0x04009C82 RID: 40066
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009C83 RID: 40067
		private static readonly string[] eleTagNames;

		// Token: 0x04009C84 RID: 40068
		private static readonly byte[] eleNamespaceIds;
	}
}
