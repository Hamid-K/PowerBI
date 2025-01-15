using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200290F RID: 10511
	[ChildElementInfo(typeof(VTShort))]
	[ChildElementInfo(typeof(VTBool))]
	[ChildElementInfo(typeof(VTError))]
	[ChildElementInfo(typeof(VTUnsignedInt32))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Variant))]
	[ChildElementInfo(typeof(VTByte))]
	[ChildElementInfo(typeof(VTInt32))]
	[ChildElementInfo(typeof(VTInteger))]
	[ChildElementInfo(typeof(VTUnsignedByte))]
	[ChildElementInfo(typeof(VTUnsignedShort))]
	[ChildElementInfo(typeof(VTUnsignedInteger))]
	[ChildElementInfo(typeof(VTFloat))]
	[ChildElementInfo(typeof(VTDouble))]
	[ChildElementInfo(typeof(VTDecimal))]
	[ChildElementInfo(typeof(VTBString))]
	[ChildElementInfo(typeof(VTDate))]
	[ChildElementInfo(typeof(VTCurrency))]
	internal class VTArray : OpenXmlCompositeElement
	{
		// Token: 0x17006A83 RID: 27267
		// (get) Token: 0x06014CE3 RID: 85219 RVA: 0x00317A4B File Offset: 0x00315C4B
		public override string LocalName
		{
			get
			{
				return "array";
			}
		}

		// Token: 0x17006A84 RID: 27268
		// (get) Token: 0x06014CE4 RID: 85220 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006A85 RID: 27269
		// (get) Token: 0x06014CE5 RID: 85221 RVA: 0x00317A52 File Offset: 0x00315C52
		internal override int ElementTypeId
		{
			get
			{
				return 10965;
			}
		}

		// Token: 0x06014CE6 RID: 85222 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006A86 RID: 27270
		// (get) Token: 0x06014CE7 RID: 85223 RVA: 0x00317A59 File Offset: 0x00315C59
		internal override string[] AttributeTagNames
		{
			get
			{
				return VTArray.attributeTagNames;
			}
		}

		// Token: 0x17006A87 RID: 27271
		// (get) Token: 0x06014CE8 RID: 85224 RVA: 0x00317A60 File Offset: 0x00315C60
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VTArray.attributeNamespaceIds;
			}
		}

		// Token: 0x17006A88 RID: 27272
		// (get) Token: 0x06014CE9 RID: 85225 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06014CEA RID: 85226 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "lBound")]
		public Int32Value LowerBounds
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006A89 RID: 27273
		// (get) Token: 0x06014CEB RID: 85227 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06014CEC RID: 85228 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "uBound")]
		public Int32Value UpperBounds
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006A8A RID: 27274
		// (get) Token: 0x06014CED RID: 85229 RVA: 0x00317A67 File Offset: 0x00315C67
		// (set) Token: 0x06014CEE RID: 85230 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "baseType")]
		public EnumValue<ArrayBaseValues> BaseType
		{
			get
			{
				return (EnumValue<ArrayBaseValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06014CEF RID: 85231 RVA: 0x00293ECF File Offset: 0x002920CF
		public VTArray()
		{
		}

		// Token: 0x06014CF0 RID: 85232 RVA: 0x00293ED7 File Offset: 0x002920D7
		public VTArray(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014CF1 RID: 85233 RVA: 0x00293EE0 File Offset: 0x002920E0
		public VTArray(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014CF2 RID: 85234 RVA: 0x00293EE9 File Offset: 0x002920E9
		public VTArray(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014CF3 RID: 85235 RVA: 0x00317A78 File Offset: 0x00315C78
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (5 == namespaceId && "variant" == name)
			{
				return new Variant();
			}
			if (5 == namespaceId && "i1" == name)
			{
				return new VTByte();
			}
			if (5 == namespaceId && "i2" == name)
			{
				return new VTShort();
			}
			if (5 == namespaceId && "i4" == name)
			{
				return new VTInt32();
			}
			if (5 == namespaceId && "int" == name)
			{
				return new VTInteger();
			}
			if (5 == namespaceId && "ui1" == name)
			{
				return new VTUnsignedByte();
			}
			if (5 == namespaceId && "ui2" == name)
			{
				return new VTUnsignedShort();
			}
			if (5 == namespaceId && "ui4" == name)
			{
				return new VTUnsignedInt32();
			}
			if (5 == namespaceId && "uint" == name)
			{
				return new VTUnsignedInteger();
			}
			if (5 == namespaceId && "r4" == name)
			{
				return new VTFloat();
			}
			if (5 == namespaceId && "r8" == name)
			{
				return new VTDouble();
			}
			if (5 == namespaceId && "decimal" == name)
			{
				return new VTDecimal();
			}
			if (5 == namespaceId && "bstr" == name)
			{
				return new VTBString();
			}
			if (5 == namespaceId && "date" == name)
			{
				return new VTDate();
			}
			if (5 == namespaceId && "bool" == name)
			{
				return new VTBool();
			}
			if (5 == namespaceId && "error" == name)
			{
				return new VTError();
			}
			if (5 == namespaceId && "cy" == name)
			{
				return new VTCurrency();
			}
			return null;
		}

		// Token: 0x06014CF4 RID: 85236 RVA: 0x00317C10 File Offset: 0x00315E10
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "lBound" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "uBound" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "baseType" == name)
			{
				return new EnumValue<ArrayBaseValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014CF5 RID: 85237 RVA: 0x00317C67 File Offset: 0x00315E67
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTArray>(deep);
		}

		// Token: 0x06014CF6 RID: 85238 RVA: 0x00317C70 File Offset: 0x00315E70
		// Note: this type is marked as 'beforefieldinit'.
		static VTArray()
		{
			byte[] array = new byte[3];
			VTArray.attributeNamespaceIds = array;
		}

		// Token: 0x04008FDD RID: 36829
		private const string tagName = "array";

		// Token: 0x04008FDE RID: 36830
		private const byte tagNsId = 5;

		// Token: 0x04008FDF RID: 36831
		internal const int ElementTypeIdConst = 10965;

		// Token: 0x04008FE0 RID: 36832
		private static string[] attributeTagNames = new string[] { "lBound", "uBound", "baseType" };

		// Token: 0x04008FE1 RID: 36833
		private static byte[] attributeNamespaceIds;
	}
}
