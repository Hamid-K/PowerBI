using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200220A RID: 8714
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LinkType))]
	[ChildElementInfo(typeof(LockedField))]
	[ChildElementInfo(typeof(FieldCodes))]
	internal class OleObject : OpenXmlCompositeElement
	{
		// Token: 0x170038AF RID: 14511
		// (get) Token: 0x0600DEF5 RID: 57077 RVA: 0x002BEC25 File Offset: 0x002BCE25
		public override string LocalName
		{
			get
			{
				return "OLEObject";
			}
		}

		// Token: 0x170038B0 RID: 14512
		// (get) Token: 0x0600DEF6 RID: 57078 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170038B1 RID: 14513
		// (get) Token: 0x0600DEF7 RID: 57079 RVA: 0x002BEC2C File Offset: 0x002BCE2C
		internal override int ElementTypeId
		{
			get
			{
				return 12408;
			}
		}

		// Token: 0x0600DEF8 RID: 57080 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170038B2 RID: 14514
		// (get) Token: 0x0600DEF9 RID: 57081 RVA: 0x002BEC33 File Offset: 0x002BCE33
		internal override string[] AttributeTagNames
		{
			get
			{
				return OleObject.attributeTagNames;
			}
		}

		// Token: 0x170038B3 RID: 14515
		// (get) Token: 0x0600DEFA RID: 57082 RVA: 0x002BEC3A File Offset: 0x002BCE3A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OleObject.attributeNamespaceIds;
			}
		}

		// Token: 0x170038B4 RID: 14516
		// (get) Token: 0x0600DEFB RID: 57083 RVA: 0x002BEC41 File Offset: 0x002BCE41
		// (set) Token: 0x0600DEFC RID: 57084 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "Type")]
		public EnumValue<OleValues> Type
		{
			get
			{
				return (EnumValue<OleValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170038B5 RID: 14517
		// (get) Token: 0x0600DEFD RID: 57085 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600DEFE RID: 57086 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ProgID")]
		public StringValue ProgId
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

		// Token: 0x170038B6 RID: 14518
		// (get) Token: 0x0600DEFF RID: 57087 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600DF00 RID: 57088 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ShapeID")]
		public StringValue ShapeId
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

		// Token: 0x170038B7 RID: 14519
		// (get) Token: 0x0600DF01 RID: 57089 RVA: 0x002BEC50 File Offset: 0x002BCE50
		// (set) Token: 0x0600DF02 RID: 57090 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "DrawAspect")]
		public EnumValue<OleDrawAspectValues> DrawAspect
		{
			get
			{
				return (EnumValue<OleDrawAspectValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170038B8 RID: 14520
		// (get) Token: 0x0600DF03 RID: 57091 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600DF04 RID: 57092 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "ObjectID")]
		public StringValue ObjectId
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

		// Token: 0x170038B9 RID: 14521
		// (get) Token: 0x0600DF05 RID: 57093 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600DF06 RID: 57094 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x170038BA RID: 14522
		// (get) Token: 0x0600DF07 RID: 57095 RVA: 0x002BEC5F File Offset: 0x002BCE5F
		// (set) Token: 0x0600DF08 RID: 57096 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "UpdateMode")]
		public EnumValue<OleUpdateModeValues> UpdateMode
		{
			get
			{
				return (EnumValue<OleUpdateModeValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x0600DF09 RID: 57097 RVA: 0x00293ECF File Offset: 0x002920CF
		public OleObject()
		{
		}

		// Token: 0x0600DF0A RID: 57098 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OleObject(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DF0B RID: 57099 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OleObject(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DF0C RID: 57100 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OleObject(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600DF0D RID: 57101 RVA: 0x002BEC70 File Offset: 0x002BCE70
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "LinkType" == name)
			{
				return new LinkType();
			}
			if (27 == namespaceId && "LockedField" == name)
			{
				return new LockedField();
			}
			if (27 == namespaceId && "FieldCodes" == name)
			{
				return new FieldCodes();
			}
			return null;
		}

		// Token: 0x170038BB RID: 14523
		// (get) Token: 0x0600DF0E RID: 57102 RVA: 0x002BECC6 File Offset: 0x002BCEC6
		internal override string[] ElementTagNames
		{
			get
			{
				return OleObject.eleTagNames;
			}
		}

		// Token: 0x170038BC RID: 14524
		// (get) Token: 0x0600DF0F RID: 57103 RVA: 0x002BECCD File Offset: 0x002BCECD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OleObject.eleNamespaceIds;
			}
		}

		// Token: 0x170038BD RID: 14525
		// (get) Token: 0x0600DF10 RID: 57104 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170038BE RID: 14526
		// (get) Token: 0x0600DF11 RID: 57105 RVA: 0x002BECD4 File Offset: 0x002BCED4
		// (set) Token: 0x0600DF12 RID: 57106 RVA: 0x002BECDD File Offset: 0x002BCEDD
		public LinkType LinkType
		{
			get
			{
				return base.GetElement<LinkType>(0);
			}
			set
			{
				base.SetElement<LinkType>(0, value);
			}
		}

		// Token: 0x170038BF RID: 14527
		// (get) Token: 0x0600DF13 RID: 57107 RVA: 0x002BECE7 File Offset: 0x002BCEE7
		// (set) Token: 0x0600DF14 RID: 57108 RVA: 0x002BECF0 File Offset: 0x002BCEF0
		public LockedField LockedField
		{
			get
			{
				return base.GetElement<LockedField>(1);
			}
			set
			{
				base.SetElement<LockedField>(1, value);
			}
		}

		// Token: 0x170038C0 RID: 14528
		// (get) Token: 0x0600DF15 RID: 57109 RVA: 0x002BECFA File Offset: 0x002BCEFA
		// (set) Token: 0x0600DF16 RID: 57110 RVA: 0x002BED03 File Offset: 0x002BCF03
		public FieldCodes FieldCodes
		{
			get
			{
				return base.GetElement<FieldCodes>(2);
			}
			set
			{
				base.SetElement<FieldCodes>(2, value);
			}
		}

		// Token: 0x0600DF17 RID: 57111 RVA: 0x002BED10 File Offset: 0x002BCF10
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "Type" == name)
			{
				return new EnumValue<OleValues>();
			}
			if (namespaceId == 0 && "ProgID" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ShapeID" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "DrawAspect" == name)
			{
				return new EnumValue<OleDrawAspectValues>();
			}
			if (namespaceId == 0 && "ObjectID" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "UpdateMode" == name)
			{
				return new EnumValue<OleUpdateModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DF18 RID: 57112 RVA: 0x002BEDC1 File Offset: 0x002BCFC1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleObject>(deep);
		}

		// Token: 0x0600DF19 RID: 57113 RVA: 0x002BEDCC File Offset: 0x002BCFCC
		// Note: this type is marked as 'beforefieldinit'.
		static OleObject()
		{
			byte[] array = new byte[7];
			array[5] = 19;
			OleObject.attributeNamespaceIds = array;
			OleObject.eleTagNames = new string[] { "LinkType", "LockedField", "FieldCodes" };
			OleObject.eleNamespaceIds = new byte[] { 27, 27, 27 };
		}

		// Token: 0x04006D81 RID: 28033
		private const string tagName = "OLEObject";

		// Token: 0x04006D82 RID: 28034
		private const byte tagNsId = 27;

		// Token: 0x04006D83 RID: 28035
		internal const int ElementTypeIdConst = 12408;

		// Token: 0x04006D84 RID: 28036
		private static string[] attributeTagNames = new string[] { "Type", "ProgID", "ShapeID", "DrawAspect", "ObjectID", "id", "UpdateMode" };

		// Token: 0x04006D85 RID: 28037
		private static byte[] attributeNamespaceIds;

		// Token: 0x04006D86 RID: 28038
		private static readonly string[] eleTagNames;

		// Token: 0x04006D87 RID: 28039
		private static readonly byte[] eleNamespaceIds;
	}
}
