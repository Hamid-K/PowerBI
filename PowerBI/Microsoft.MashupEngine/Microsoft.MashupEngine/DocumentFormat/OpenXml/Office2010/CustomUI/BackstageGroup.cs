using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022E2 RID: 8930
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PrimaryItem), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TopItemsGroupControls), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BottomItemsGroupControls), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackstageGroup : OpenXmlCompositeElement
	{
		// Token: 0x170045EA RID: 17898
		// (get) Token: 0x0600FAA9 RID: 64169 RVA: 0x002C29FF File Offset: 0x002C0BFF
		public override string LocalName
		{
			get
			{
				return "group";
			}
		}

		// Token: 0x170045EB RID: 17899
		// (get) Token: 0x0600FAAA RID: 64170 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170045EC RID: 17900
		// (get) Token: 0x0600FAAB RID: 64171 RVA: 0x002D9E74 File Offset: 0x002D8074
		internal override int ElementTypeId
		{
			get
			{
				return 13075;
			}
		}

		// Token: 0x0600FAAC RID: 64172 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170045ED RID: 17901
		// (get) Token: 0x0600FAAD RID: 64173 RVA: 0x002D9E7B File Offset: 0x002D807B
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageGroup.attributeTagNames;
			}
		}

		// Token: 0x170045EE RID: 17902
		// (get) Token: 0x0600FAAE RID: 64174 RVA: 0x002D9E82 File Offset: 0x002D8082
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x170045EF RID: 17903
		// (get) Token: 0x0600FAAF RID: 64175 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FAB0 RID: 64176 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170045F0 RID: 17904
		// (get) Token: 0x0600FAB1 RID: 64177 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FAB2 RID: 64178 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170045F1 RID: 17905
		// (get) Token: 0x0600FAB3 RID: 64179 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FAB4 RID: 64180 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x170045F2 RID: 17906
		// (get) Token: 0x0600FAB5 RID: 64181 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FAB6 RID: 64182 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x170045F3 RID: 17907
		// (get) Token: 0x0600FAB7 RID: 64183 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FAB8 RID: 64184 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x170045F4 RID: 17908
		// (get) Token: 0x0600FAB9 RID: 64185 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FABA RID: 64186 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x170045F5 RID: 17909
		// (get) Token: 0x0600FABB RID: 64187 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FABC RID: 64188 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x170045F6 RID: 17910
		// (get) Token: 0x0600FABD RID: 64189 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FABE RID: 64190 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x170045F7 RID: 17911
		// (get) Token: 0x0600FABF RID: 64191 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FAC0 RID: 64192 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "label")]
		public StringValue Label
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170045F8 RID: 17912
		// (get) Token: 0x0600FAC1 RID: 64193 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FAC2 RID: 64194 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170045F9 RID: 17913
		// (get) Token: 0x0600FAC3 RID: 64195 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600FAC4 RID: 64196 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170045FA RID: 17914
		// (get) Token: 0x0600FAC5 RID: 64197 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FAC6 RID: 64198 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170045FB RID: 17915
		// (get) Token: 0x0600FAC7 RID: 64199 RVA: 0x002D9E89 File Offset: 0x002D8089
		// (set) Token: 0x0600FAC8 RID: 64200 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "style")]
		public EnumValue<StyleValues> Style
		{
			get
			{
				return (EnumValue<StyleValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170045FC RID: 17916
		// (get) Token: 0x0600FAC9 RID: 64201 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FACA RID: 64202 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getStyle")]
		public StringValue GetStyle
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x170045FD RID: 17917
		// (get) Token: 0x0600FACB RID: 64203 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600FACC RID: 64204 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "helperText")]
		public StringValue HelperText
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170045FE RID: 17918
		// (get) Token: 0x0600FACD RID: 64205 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FACE RID: 64206 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getHelperText")]
		public StringValue GetHelperText
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x170045FF RID: 17919
		// (get) Token: 0x0600FACF RID: 64207 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0600FAD0 RID: 64208 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17004600 RID: 17920
		// (get) Token: 0x0600FAD1 RID: 64209 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600FAD2 RID: 64210 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x0600FAD3 RID: 64211 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackstageGroup()
		{
		}

		// Token: 0x0600FAD4 RID: 64212 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackstageGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FAD5 RID: 64213 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackstageGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FAD6 RID: 64214 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackstageGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FAD7 RID: 64215 RVA: 0x002D9E9C File Offset: 0x002D809C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "primaryItem" == name)
			{
				return new PrimaryItem();
			}
			if (57 == namespaceId && "topItems" == name)
			{
				return new TopItemsGroupControls();
			}
			if (57 == namespaceId && "bottomItems" == name)
			{
				return new BottomItemsGroupControls();
			}
			return null;
		}

		// Token: 0x0600FAD8 RID: 64216 RVA: 0x002D9EF4 File Offset: 0x002D80F4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "style" == name)
			{
				return new EnumValue<StyleValues>();
			}
			if (namespaceId == 0 && "getStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "helperText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getHelperText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowLabel" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FAD9 RID: 64217 RVA: 0x002DA095 File Offset: 0x002D8295
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageGroup>(deep);
		}

		// Token: 0x0600FADA RID: 64218 RVA: 0x002DA0A0 File Offset: 0x002D82A0
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageGroup()
		{
			byte[] array = new byte[18];
			BackstageGroup.attributeNamespaceIds = array;
		}

		// Token: 0x04007197 RID: 29079
		private const string tagName = "group";

		// Token: 0x04007198 RID: 29080
		private const byte tagNsId = 57;

		// Token: 0x04007199 RID: 29081
		internal const int ElementTypeIdConst = 13075;

		// Token: 0x0400719A RID: 29082
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "label", "getLabel",
			"visible", "getVisible", "style", "getStyle", "helperText", "getHelperText", "showLabel", "getShowLabel"
		};

		// Token: 0x0400719B RID: 29083
		private static byte[] attributeNamespaceIds;
	}
}
