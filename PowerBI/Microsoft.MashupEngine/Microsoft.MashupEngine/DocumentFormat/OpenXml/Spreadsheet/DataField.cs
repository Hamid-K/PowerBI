using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B75 RID: 11125
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DataFieldExtensionList))]
	internal class DataField : OpenXmlCompositeElement
	{
		// Token: 0x170079CF RID: 31183
		// (get) Token: 0x06016F60 RID: 94048 RVA: 0x002E60BE File Offset: 0x002E42BE
		public override string LocalName
		{
			get
			{
				return "dataField";
			}
		}

		// Token: 0x170079D0 RID: 31184
		// (get) Token: 0x06016F61 RID: 94049 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170079D1 RID: 31185
		// (get) Token: 0x06016F62 RID: 94050 RVA: 0x00331223 File Offset: 0x0032F423
		internal override int ElementTypeId
		{
			get
			{
				return 11105;
			}
		}

		// Token: 0x06016F63 RID: 94051 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170079D2 RID: 31186
		// (get) Token: 0x06016F64 RID: 94052 RVA: 0x0033122A File Offset: 0x0032F42A
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataField.attributeTagNames;
			}
		}

		// Token: 0x170079D3 RID: 31187
		// (get) Token: 0x06016F65 RID: 94053 RVA: 0x00331231 File Offset: 0x0032F431
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataField.attributeNamespaceIds;
			}
		}

		// Token: 0x170079D4 RID: 31188
		// (get) Token: 0x06016F66 RID: 94054 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016F67 RID: 94055 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x170079D5 RID: 31189
		// (get) Token: 0x06016F68 RID: 94056 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06016F69 RID: 94057 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fld")]
		public UInt32Value Field
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

		// Token: 0x170079D6 RID: 31190
		// (get) Token: 0x06016F6A RID: 94058 RVA: 0x00331238 File Offset: 0x0032F438
		// (set) Token: 0x06016F6B RID: 94059 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "subtotal")]
		public EnumValue<DataConsolidateFunctionValues> Subtotal
		{
			get
			{
				return (EnumValue<DataConsolidateFunctionValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170079D7 RID: 31191
		// (get) Token: 0x06016F6C RID: 94060 RVA: 0x00331247 File Offset: 0x0032F447
		// (set) Token: 0x06016F6D RID: 94061 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showDataAs")]
		public EnumValue<ShowDataAsValues> ShowDataAs
		{
			get
			{
				return (EnumValue<ShowDataAsValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170079D8 RID: 31192
		// (get) Token: 0x06016F6E RID: 94062 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x06016F6F RID: 94063 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "baseField")]
		public Int32Value BaseField
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

		// Token: 0x170079D9 RID: 31193
		// (get) Token: 0x06016F70 RID: 94064 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06016F71 RID: 94065 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "baseItem")]
		public UInt32Value BaseItem
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170079DA RID: 31194
		// (get) Token: 0x06016F72 RID: 94066 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06016F73 RID: 94067 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "numFmtId")]
		public UInt32Value NumberFormatId
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

		// Token: 0x06016F74 RID: 94068 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataField()
		{
		}

		// Token: 0x06016F75 RID: 94069 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataField(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016F76 RID: 94070 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataField(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016F77 RID: 94071 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataField(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016F78 RID: 94072 RVA: 0x00331256 File Offset: 0x0032F456
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new DataFieldExtensionList();
			}
			return null;
		}

		// Token: 0x170079DB RID: 31195
		// (get) Token: 0x06016F79 RID: 94073 RVA: 0x00331271 File Offset: 0x0032F471
		internal override string[] ElementTagNames
		{
			get
			{
				return DataField.eleTagNames;
			}
		}

		// Token: 0x170079DC RID: 31196
		// (get) Token: 0x06016F7A RID: 94074 RVA: 0x00331278 File Offset: 0x0032F478
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataField.eleNamespaceIds;
			}
		}

		// Token: 0x170079DD RID: 31197
		// (get) Token: 0x06016F7B RID: 94075 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170079DE RID: 31198
		// (get) Token: 0x06016F7C RID: 94076 RVA: 0x0033127F File Offset: 0x0032F47F
		// (set) Token: 0x06016F7D RID: 94077 RVA: 0x00331288 File Offset: 0x0032F488
		public DataFieldExtensionList DataFieldExtensionList
		{
			get
			{
				return base.GetElement<DataFieldExtensionList>(0);
			}
			set
			{
				base.SetElement<DataFieldExtensionList>(0, value);
			}
		}

		// Token: 0x06016F7E RID: 94078 RVA: 0x00331294 File Offset: 0x0032F494
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fld" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "subtotal" == name)
			{
				return new EnumValue<DataConsolidateFunctionValues>();
			}
			if (namespaceId == 0 && "showDataAs" == name)
			{
				return new EnumValue<ShowDataAsValues>();
			}
			if (namespaceId == 0 && "baseField" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "baseItem" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "numFmtId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016F7F RID: 94079 RVA: 0x00331343 File Offset: 0x0032F543
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataField>(deep);
		}

		// Token: 0x06016F80 RID: 94080 RVA: 0x0033134C File Offset: 0x0032F54C
		// Note: this type is marked as 'beforefieldinit'.
		static DataField()
		{
			byte[] array = new byte[7];
			DataField.attributeNamespaceIds = array;
			DataField.eleTagNames = new string[] { "extLst" };
			DataField.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009A87 RID: 39559
		private const string tagName = "dataField";

		// Token: 0x04009A88 RID: 39560
		private const byte tagNsId = 22;

		// Token: 0x04009A89 RID: 39561
		internal const int ElementTypeIdConst = 11105;

		// Token: 0x04009A8A RID: 39562
		private static string[] attributeTagNames = new string[] { "name", "fld", "subtotal", "showDataAs", "baseField", "baseItem", "numFmtId" };

		// Token: 0x04009A8B RID: 39563
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009A8C RID: 39564
		private static readonly string[] eleTagNames;

		// Token: 0x04009A8D RID: 39565
		private static readonly byte[] eleNamespaceIds;
	}
}
