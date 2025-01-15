using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200241B RID: 9243
	[ChildElementInfo(typeof(NegativeBorderColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FillColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BorderColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NegativeFillColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ConditionalFormattingValueObject), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BarAxisColor), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DataBar : OpenXmlCompositeElement
	{
		// Token: 0x17004F28 RID: 20264
		// (get) Token: 0x06010EFB RID: 69371 RVA: 0x002E8C59 File Offset: 0x002E6E59
		public override string LocalName
		{
			get
			{
				return "dataBar";
			}
		}

		// Token: 0x17004F29 RID: 20265
		// (get) Token: 0x06010EFC RID: 69372 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F2A RID: 20266
		// (get) Token: 0x06010EFD RID: 69373 RVA: 0x002E8C60 File Offset: 0x002E6E60
		internal override int ElementTypeId
		{
			get
			{
				return 12961;
			}
		}

		// Token: 0x06010EFE RID: 69374 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F2B RID: 20267
		// (get) Token: 0x06010EFF RID: 69375 RVA: 0x002E8C67 File Offset: 0x002E6E67
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataBar.attributeTagNames;
			}
		}

		// Token: 0x17004F2C RID: 20268
		// (get) Token: 0x06010F00 RID: 69376 RVA: 0x002E8C6E File Offset: 0x002E6E6E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataBar.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F2D RID: 20269
		// (get) Token: 0x06010F01 RID: 69377 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010F02 RID: 69378 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "minLength")]
		public UInt32Value MinLength
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

		// Token: 0x17004F2E RID: 20270
		// (get) Token: 0x06010F03 RID: 69379 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010F04 RID: 69380 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "maxLength")]
		public UInt32Value MaxLength
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

		// Token: 0x17004F2F RID: 20271
		// (get) Token: 0x06010F05 RID: 69381 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010F06 RID: 69382 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showValue")]
		public BooleanValue ShowValue
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

		// Token: 0x17004F30 RID: 20272
		// (get) Token: 0x06010F07 RID: 69383 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010F08 RID: 69384 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "border")]
		public BooleanValue Border
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

		// Token: 0x17004F31 RID: 20273
		// (get) Token: 0x06010F09 RID: 69385 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06010F0A RID: 69386 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "gradient")]
		public BooleanValue Gradient
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

		// Token: 0x17004F32 RID: 20274
		// (get) Token: 0x06010F0B RID: 69387 RVA: 0x002E8C75 File Offset: 0x002E6E75
		// (set) Token: 0x06010F0C RID: 69388 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "direction")]
		public EnumValue<DataBarDirectionValues> Direction
		{
			get
			{
				return (EnumValue<DataBarDirectionValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004F33 RID: 20275
		// (get) Token: 0x06010F0D RID: 69389 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06010F0E RID: 69390 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "negativeBarColorSameAsPositive")]
		public BooleanValue NegativeBarColorSameAsPositive
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17004F34 RID: 20276
		// (get) Token: 0x06010F0F RID: 69391 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06010F10 RID: 69392 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "negativeBarBorderColorSameAsPositive")]
		public BooleanValue NegativeBarBorderColorSameAsPositive
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004F35 RID: 20277
		// (get) Token: 0x06010F11 RID: 69393 RVA: 0x002E8C84 File Offset: 0x002E6E84
		// (set) Token: 0x06010F12 RID: 69394 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "axisPosition")]
		public EnumValue<DataBarAxisPositionValues> AxisPosition
		{
			get
			{
				return (EnumValue<DataBarAxisPositionValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x06010F13 RID: 69395 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataBar()
		{
		}

		// Token: 0x06010F14 RID: 69396 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataBar(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F15 RID: 69397 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataBar(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F16 RID: 69398 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataBar(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010F17 RID: 69399 RVA: 0x002E8C94 File Offset: 0x002E6E94
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "cfvo" == name)
			{
				return new ConditionalFormattingValueObject();
			}
			if (53 == namespaceId && "fillColor" == name)
			{
				return new FillColor();
			}
			if (53 == namespaceId && "borderColor" == name)
			{
				return new BorderColor();
			}
			if (53 == namespaceId && "negativeFillColor" == name)
			{
				return new NegativeFillColor();
			}
			if (53 == namespaceId && "negativeBorderColor" == name)
			{
				return new NegativeBorderColor();
			}
			if (53 == namespaceId && "axisColor" == name)
			{
				return new BarAxisColor();
			}
			return null;
		}

		// Token: 0x06010F18 RID: 69400 RVA: 0x002E8D34 File Offset: 0x002E6F34
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "minLength" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "maxLength" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "showValue" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "border" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "gradient" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "direction" == name)
			{
				return new EnumValue<DataBarDirectionValues>();
			}
			if (namespaceId == 0 && "negativeBarColorSameAsPositive" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "negativeBarBorderColorSameAsPositive" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "axisPosition" == name)
			{
				return new EnumValue<DataBarAxisPositionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010F19 RID: 69401 RVA: 0x002E8E0F File Offset: 0x002E700F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataBar>(deep);
		}

		// Token: 0x06010F1A RID: 69402 RVA: 0x002E8E18 File Offset: 0x002E7018
		// Note: this type is marked as 'beforefieldinit'.
		static DataBar()
		{
			byte[] array = new byte[9];
			DataBar.attributeNamespaceIds = array;
		}

		// Token: 0x040076FA RID: 30458
		private const string tagName = "dataBar";

		// Token: 0x040076FB RID: 30459
		private const byte tagNsId = 53;

		// Token: 0x040076FC RID: 30460
		internal const int ElementTypeIdConst = 12961;

		// Token: 0x040076FD RID: 30461
		private static string[] attributeTagNames = new string[] { "minLength", "maxLength", "showValue", "border", "gradient", "direction", "negativeBarColorSameAsPositive", "negativeBarBorderColorSameAsPositive", "axisPosition" };

		// Token: 0x040076FE RID: 30462
		private static byte[] attributeNamespaceIds;
	}
}
