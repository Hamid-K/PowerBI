using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200290E RID: 10510
	[ChildElementInfo(typeof(VTUnsignedShort))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(VTDouble))]
	[ChildElementInfo(typeof(VTClipboardData))]
	[ChildElementInfo(typeof(VTByte))]
	[ChildElementInfo(typeof(VTShort))]
	[ChildElementInfo(typeof(VTInt32))]
	[ChildElementInfo(typeof(VTInt64))]
	[ChildElementInfo(typeof(VTUnsignedByte))]
	[ChildElementInfo(typeof(VTUnsignedInt32))]
	[ChildElementInfo(typeof(VTUnsignedInt64))]
	[ChildElementInfo(typeof(VTFloat))]
	[ChildElementInfo(typeof(Variant))]
	[ChildElementInfo(typeof(VTLPSTR))]
	[ChildElementInfo(typeof(VTLPWSTR))]
	[ChildElementInfo(typeof(VTBString))]
	[ChildElementInfo(typeof(VTDate))]
	[ChildElementInfo(typeof(VTFileTime))]
	[ChildElementInfo(typeof(VTBool))]
	[ChildElementInfo(typeof(VTCurrency))]
	[ChildElementInfo(typeof(VTError))]
	[ChildElementInfo(typeof(VTClassId))]
	internal class VTVector : OpenXmlCompositeElement
	{
		// Token: 0x17006A7C RID: 27260
		// (get) Token: 0x06014CD1 RID: 85201 RVA: 0x003177B8 File Offset: 0x003159B8
		public override string LocalName
		{
			get
			{
				return "vector";
			}
		}

		// Token: 0x17006A7D RID: 27261
		// (get) Token: 0x06014CD2 RID: 85202 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006A7E RID: 27262
		// (get) Token: 0x06014CD3 RID: 85203 RVA: 0x003177BF File Offset: 0x003159BF
		internal override int ElementTypeId
		{
			get
			{
				return 10964;
			}
		}

		// Token: 0x06014CD4 RID: 85204 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006A7F RID: 27263
		// (get) Token: 0x06014CD5 RID: 85205 RVA: 0x003177C6 File Offset: 0x003159C6
		internal override string[] AttributeTagNames
		{
			get
			{
				return VTVector.attributeTagNames;
			}
		}

		// Token: 0x17006A80 RID: 27264
		// (get) Token: 0x06014CD6 RID: 85206 RVA: 0x003177CD File Offset: 0x003159CD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VTVector.attributeNamespaceIds;
			}
		}

		// Token: 0x17006A81 RID: 27265
		// (get) Token: 0x06014CD7 RID: 85207 RVA: 0x003177D4 File Offset: 0x003159D4
		// (set) Token: 0x06014CD8 RID: 85208 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "baseType")]
		public EnumValue<VectorBaseValues> BaseType
		{
			get
			{
				return (EnumValue<VectorBaseValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006A82 RID: 27266
		// (get) Token: 0x06014CD9 RID: 85209 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06014CDA RID: 85210 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "size")]
		public UInt32Value Size
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

		// Token: 0x06014CDB RID: 85211 RVA: 0x00293ECF File Offset: 0x002920CF
		public VTVector()
		{
		}

		// Token: 0x06014CDC RID: 85212 RVA: 0x00293ED7 File Offset: 0x002920D7
		public VTVector(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014CDD RID: 85213 RVA: 0x00293EE0 File Offset: 0x002920E0
		public VTVector(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014CDE RID: 85214 RVA: 0x00293EE9 File Offset: 0x002920E9
		public VTVector(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014CDF RID: 85215 RVA: 0x003177E4 File Offset: 0x003159E4
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
			if (5 == namespaceId && "i8" == name)
			{
				return new VTInt64();
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
			if (5 == namespaceId && "ui8" == name)
			{
				return new VTUnsignedInt64();
			}
			if (5 == namespaceId && "r4" == name)
			{
				return new VTFloat();
			}
			if (5 == namespaceId && "r8" == name)
			{
				return new VTDouble();
			}
			if (5 == namespaceId && "lpstr" == name)
			{
				return new VTLPSTR();
			}
			if (5 == namespaceId && "lpwstr" == name)
			{
				return new VTLPWSTR();
			}
			if (5 == namespaceId && "bstr" == name)
			{
				return new VTBString();
			}
			if (5 == namespaceId && "date" == name)
			{
				return new VTDate();
			}
			if (5 == namespaceId && "filetime" == name)
			{
				return new VTFileTime();
			}
			if (5 == namespaceId && "bool" == name)
			{
				return new VTBool();
			}
			if (5 == namespaceId && "cy" == name)
			{
				return new VTCurrency();
			}
			if (5 == namespaceId && "error" == name)
			{
				return new VTError();
			}
			if (5 == namespaceId && "clsid" == name)
			{
				return new VTClassId();
			}
			if (5 == namespaceId && "cf" == name)
			{
				return new VTClipboardData();
			}
			return null;
		}

		// Token: 0x06014CE0 RID: 85216 RVA: 0x003179D5 File Offset: 0x00315BD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "baseType" == name)
			{
				return new EnumValue<VectorBaseValues>();
			}
			if (namespaceId == 0 && "size" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014CE1 RID: 85217 RVA: 0x00317A0B File Offset: 0x00315C0B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTVector>(deep);
		}

		// Token: 0x06014CE2 RID: 85218 RVA: 0x00317A14 File Offset: 0x00315C14
		// Note: this type is marked as 'beforefieldinit'.
		static VTVector()
		{
			byte[] array = new byte[2];
			VTVector.attributeNamespaceIds = array;
		}

		// Token: 0x04008FD8 RID: 36824
		private const string tagName = "vector";

		// Token: 0x04008FD9 RID: 36825
		private const byte tagNsId = 5;

		// Token: 0x04008FDA RID: 36826
		internal const int ElementTypeIdConst = 10964;

		// Token: 0x04008FDB RID: 36827
		private static string[] attributeTagNames = new string[] { "baseType", "size" };

		// Token: 0x04008FDC RID: 36828
		private static byte[] attributeNamespaceIds;
	}
}
