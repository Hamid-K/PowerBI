using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002417 RID: 9239
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TupleSetRowItem : OpenXmlLeafElement
	{
		// Token: 0x17004F12 RID: 20242
		// (get) Token: 0x06010EC7 RID: 69319 RVA: 0x002E8A96 File Offset: 0x002E6C96
		public override string LocalName
		{
			get
			{
				return "rowItem";
			}
		}

		// Token: 0x17004F13 RID: 20243
		// (get) Token: 0x06010EC8 RID: 69320 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F14 RID: 20244
		// (get) Token: 0x06010EC9 RID: 69321 RVA: 0x002E8A9D File Offset: 0x002E6C9D
		internal override int ElementTypeId
		{
			get
			{
				return 12957;
			}
		}

		// Token: 0x06010ECA RID: 69322 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F15 RID: 20245
		// (get) Token: 0x06010ECB RID: 69323 RVA: 0x002E8AA4 File Offset: 0x002E6CA4
		internal override string[] AttributeTagNames
		{
			get
			{
				return TupleSetRowItem.attributeTagNames;
			}
		}

		// Token: 0x17004F16 RID: 20246
		// (get) Token: 0x06010ECC RID: 69324 RVA: 0x002E8AAB File Offset: 0x002E6CAB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TupleSetRowItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F17 RID: 20247
		// (get) Token: 0x06010ECD RID: 69325 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010ECE RID: 69326 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "u")]
		public StringValue UniqueName
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

		// Token: 0x17004F18 RID: 20248
		// (get) Token: 0x06010ECF RID: 69327 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010ED0 RID: 69328 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "d")]
		public StringValue DisplayName
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

		// Token: 0x06010ED2 RID: 69330 RVA: 0x002E8AB2 File Offset: 0x002E6CB2
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "u" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "d" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010ED3 RID: 69331 RVA: 0x002E8AE8 File Offset: 0x002E6CE8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TupleSetRowItem>(deep);
		}

		// Token: 0x06010ED4 RID: 69332 RVA: 0x002E8AF4 File Offset: 0x002E6CF4
		// Note: this type is marked as 'beforefieldinit'.
		static TupleSetRowItem()
		{
			byte[] array = new byte[2];
			TupleSetRowItem.attributeNamespaceIds = array;
		}

		// Token: 0x040076E8 RID: 30440
		private const string tagName = "rowItem";

		// Token: 0x040076E9 RID: 30441
		private const byte tagNsId = 53;

		// Token: 0x040076EA RID: 30442
		internal const int ElementTypeIdConst = 12957;

		// Token: 0x040076EB RID: 30443
		private static string[] attributeTagNames = new string[] { "u", "d" };

		// Token: 0x040076EC RID: 30444
		private static byte[] attributeNamespaceIds;
	}
}
