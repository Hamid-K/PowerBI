using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.VariantTypes;

namespace DocumentFormat.OpenXml.CustomProperties
{
	// Token: 0x02002909 RID: 10505
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(VTVector))]
	[ChildElementInfo(typeof(VTArray))]
	[ChildElementInfo(typeof(VTBlob))]
	[ChildElementInfo(typeof(VTOBlob))]
	[ChildElementInfo(typeof(VTEmpty))]
	[ChildElementInfo(typeof(VTNull))]
	[ChildElementInfo(typeof(VTByte))]
	[ChildElementInfo(typeof(VTShort))]
	[ChildElementInfo(typeof(VTInt32))]
	[ChildElementInfo(typeof(VTInt64))]
	[ChildElementInfo(typeof(VTInteger))]
	[ChildElementInfo(typeof(VTUnsignedByte))]
	[ChildElementInfo(typeof(VTUnsignedShort))]
	[ChildElementInfo(typeof(VTUnsignedInt32))]
	[ChildElementInfo(typeof(VTUnsignedInt64))]
	[ChildElementInfo(typeof(VTUnsignedInteger))]
	[ChildElementInfo(typeof(VTFloat))]
	[ChildElementInfo(typeof(VTDouble))]
	[ChildElementInfo(typeof(VTDecimal))]
	[ChildElementInfo(typeof(VTLPSTR))]
	[ChildElementInfo(typeof(VTLPWSTR))]
	[ChildElementInfo(typeof(VTBString))]
	[ChildElementInfo(typeof(VTDate))]
	[ChildElementInfo(typeof(VTFileTime))]
	[ChildElementInfo(typeof(VTBool))]
	[ChildElementInfo(typeof(VTCurrency))]
	[ChildElementInfo(typeof(VTError))]
	[ChildElementInfo(typeof(VTStreamData))]
	[ChildElementInfo(typeof(VTOStreamData))]
	[ChildElementInfo(typeof(VTStorage))]
	[ChildElementInfo(typeof(VTOStorage))]
	[ChildElementInfo(typeof(VTVStreamData))]
	[ChildElementInfo(typeof(VTClassId))]
	[ChildElementInfo(typeof(VTClipboardData))]
	internal class CustomDocumentProperty : OpenXmlCompositeElement
	{
		// Token: 0x17006A11 RID: 27153
		// (get) Token: 0x06014BF0 RID: 84976 RVA: 0x002C85F5 File Offset: 0x002C67F5
		public override string LocalName
		{
			get
			{
				return "property";
			}
		}

		// Token: 0x17006A12 RID: 27154
		// (get) Token: 0x06014BF1 RID: 84977 RVA: 0x0000244F File Offset: 0x0000064F
		internal override byte NamespaceId
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17006A13 RID: 27155
		// (get) Token: 0x06014BF2 RID: 84978 RVA: 0x00316691 File Offset: 0x00314891
		internal override int ElementTypeId
		{
			get
			{
				return 10838;
			}
		}

		// Token: 0x06014BF3 RID: 84979 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006A14 RID: 27156
		// (get) Token: 0x06014BF4 RID: 84980 RVA: 0x00316698 File Offset: 0x00314898
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomDocumentProperty.attributeTagNames;
			}
		}

		// Token: 0x17006A15 RID: 27157
		// (get) Token: 0x06014BF5 RID: 84981 RVA: 0x0031669F File Offset: 0x0031489F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomDocumentProperty.attributeNamespaceIds;
			}
		}

		// Token: 0x17006A16 RID: 27158
		// (get) Token: 0x06014BF6 RID: 84982 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06014BF7 RID: 84983 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fmtid")]
		public StringValue FormatId
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

		// Token: 0x17006A17 RID: 27159
		// (get) Token: 0x06014BF8 RID: 84984 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06014BF9 RID: 84985 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pid")]
		public Int32Value PropertyId
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

		// Token: 0x17006A18 RID: 27160
		// (get) Token: 0x06014BFA RID: 84986 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06014BFB RID: 84987 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17006A19 RID: 27161
		// (get) Token: 0x06014BFC RID: 84988 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06014BFD RID: 84989 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "linkTarget")]
		public StringValue LinkTarget
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

		// Token: 0x06014BFE RID: 84990 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomDocumentProperty()
		{
		}

		// Token: 0x06014BFF RID: 84991 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomDocumentProperty(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014C00 RID: 84992 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomDocumentProperty(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014C01 RID: 84993 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomDocumentProperty(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014C02 RID: 84994 RVA: 0x003166A8 File Offset: 0x003148A8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (5 == namespaceId && "vector" == name)
			{
				return new VTVector();
			}
			if (5 == namespaceId && "array" == name)
			{
				return new VTArray();
			}
			if (5 == namespaceId && "blob" == name)
			{
				return new VTBlob();
			}
			if (5 == namespaceId && "oblob" == name)
			{
				return new VTOBlob();
			}
			if (5 == namespaceId && "empty" == name)
			{
				return new VTEmpty();
			}
			if (5 == namespaceId && "null" == name)
			{
				return new VTNull();
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
			if (5 == namespaceId && "ui8" == name)
			{
				return new VTUnsignedInt64();
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
			if (5 == namespaceId && "stream" == name)
			{
				return new VTStreamData();
			}
			if (5 == namespaceId && "ostream" == name)
			{
				return new VTOStreamData();
			}
			if (5 == namespaceId && "storage" == name)
			{
				return new VTStorage();
			}
			if (5 == namespaceId && "ostorage" == name)
			{
				return new VTOStorage();
			}
			if (5 == namespaceId && "vstream" == name)
			{
				return new VTVStreamData();
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

		// Token: 0x17006A1A RID: 27162
		// (get) Token: 0x06014C03 RID: 84995 RVA: 0x003169C4 File Offset: 0x00314BC4
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomDocumentProperty.eleTagNames;
			}
		}

		// Token: 0x17006A1B RID: 27163
		// (get) Token: 0x06014C04 RID: 84996 RVA: 0x003169CB File Offset: 0x00314BCB
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomDocumentProperty.eleNamespaceIds;
			}
		}

		// Token: 0x17006A1C RID: 27164
		// (get) Token: 0x06014C05 RID: 84997 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17006A1D RID: 27165
		// (get) Token: 0x06014C06 RID: 84998 RVA: 0x003169D2 File Offset: 0x00314BD2
		// (set) Token: 0x06014C07 RID: 84999 RVA: 0x003169DB File Offset: 0x00314BDB
		public VTVector VTVector
		{
			get
			{
				return base.GetElement<VTVector>(0);
			}
			set
			{
				base.SetElement<VTVector>(0, value);
			}
		}

		// Token: 0x17006A1E RID: 27166
		// (get) Token: 0x06014C08 RID: 85000 RVA: 0x003169E5 File Offset: 0x00314BE5
		// (set) Token: 0x06014C09 RID: 85001 RVA: 0x003169EE File Offset: 0x00314BEE
		public VTArray VTArray
		{
			get
			{
				return base.GetElement<VTArray>(1);
			}
			set
			{
				base.SetElement<VTArray>(1, value);
			}
		}

		// Token: 0x17006A1F RID: 27167
		// (get) Token: 0x06014C0A RID: 85002 RVA: 0x003169F8 File Offset: 0x00314BF8
		// (set) Token: 0x06014C0B RID: 85003 RVA: 0x00316A01 File Offset: 0x00314C01
		public VTBlob VTBlob
		{
			get
			{
				return base.GetElement<VTBlob>(2);
			}
			set
			{
				base.SetElement<VTBlob>(2, value);
			}
		}

		// Token: 0x17006A20 RID: 27168
		// (get) Token: 0x06014C0C RID: 85004 RVA: 0x00316A0B File Offset: 0x00314C0B
		// (set) Token: 0x06014C0D RID: 85005 RVA: 0x00316A14 File Offset: 0x00314C14
		public VTOBlob VTOBlob
		{
			get
			{
				return base.GetElement<VTOBlob>(3);
			}
			set
			{
				base.SetElement<VTOBlob>(3, value);
			}
		}

		// Token: 0x17006A21 RID: 27169
		// (get) Token: 0x06014C0E RID: 85006 RVA: 0x00316A1E File Offset: 0x00314C1E
		// (set) Token: 0x06014C0F RID: 85007 RVA: 0x00316A27 File Offset: 0x00314C27
		public VTEmpty VTEmpty
		{
			get
			{
				return base.GetElement<VTEmpty>(4);
			}
			set
			{
				base.SetElement<VTEmpty>(4, value);
			}
		}

		// Token: 0x17006A22 RID: 27170
		// (get) Token: 0x06014C10 RID: 85008 RVA: 0x00316A31 File Offset: 0x00314C31
		// (set) Token: 0x06014C11 RID: 85009 RVA: 0x00316A3A File Offset: 0x00314C3A
		public VTNull VTNull
		{
			get
			{
				return base.GetElement<VTNull>(5);
			}
			set
			{
				base.SetElement<VTNull>(5, value);
			}
		}

		// Token: 0x17006A23 RID: 27171
		// (get) Token: 0x06014C12 RID: 85010 RVA: 0x00316A44 File Offset: 0x00314C44
		// (set) Token: 0x06014C13 RID: 85011 RVA: 0x00316A4D File Offset: 0x00314C4D
		public VTByte VTByte
		{
			get
			{
				return base.GetElement<VTByte>(6);
			}
			set
			{
				base.SetElement<VTByte>(6, value);
			}
		}

		// Token: 0x17006A24 RID: 27172
		// (get) Token: 0x06014C14 RID: 85012 RVA: 0x00316A57 File Offset: 0x00314C57
		// (set) Token: 0x06014C15 RID: 85013 RVA: 0x00316A60 File Offset: 0x00314C60
		public VTShort VTShort
		{
			get
			{
				return base.GetElement<VTShort>(7);
			}
			set
			{
				base.SetElement<VTShort>(7, value);
			}
		}

		// Token: 0x17006A25 RID: 27173
		// (get) Token: 0x06014C16 RID: 85014 RVA: 0x00316A6A File Offset: 0x00314C6A
		// (set) Token: 0x06014C17 RID: 85015 RVA: 0x00316A73 File Offset: 0x00314C73
		public VTInt32 VTInt32
		{
			get
			{
				return base.GetElement<VTInt32>(8);
			}
			set
			{
				base.SetElement<VTInt32>(8, value);
			}
		}

		// Token: 0x17006A26 RID: 27174
		// (get) Token: 0x06014C18 RID: 85016 RVA: 0x00316A7D File Offset: 0x00314C7D
		// (set) Token: 0x06014C19 RID: 85017 RVA: 0x00316A87 File Offset: 0x00314C87
		public VTInt64 VTInt64
		{
			get
			{
				return base.GetElement<VTInt64>(9);
			}
			set
			{
				base.SetElement<VTInt64>(9, value);
			}
		}

		// Token: 0x17006A27 RID: 27175
		// (get) Token: 0x06014C1A RID: 85018 RVA: 0x00316A92 File Offset: 0x00314C92
		// (set) Token: 0x06014C1B RID: 85019 RVA: 0x00316A9C File Offset: 0x00314C9C
		public VTInteger VTInteger
		{
			get
			{
				return base.GetElement<VTInteger>(10);
			}
			set
			{
				base.SetElement<VTInteger>(10, value);
			}
		}

		// Token: 0x17006A28 RID: 27176
		// (get) Token: 0x06014C1C RID: 85020 RVA: 0x00316AA7 File Offset: 0x00314CA7
		// (set) Token: 0x06014C1D RID: 85021 RVA: 0x00316AB1 File Offset: 0x00314CB1
		public VTUnsignedByte VTUnsignedByte
		{
			get
			{
				return base.GetElement<VTUnsignedByte>(11);
			}
			set
			{
				base.SetElement<VTUnsignedByte>(11, value);
			}
		}

		// Token: 0x17006A29 RID: 27177
		// (get) Token: 0x06014C1E RID: 85022 RVA: 0x00316ABC File Offset: 0x00314CBC
		// (set) Token: 0x06014C1F RID: 85023 RVA: 0x00316AC6 File Offset: 0x00314CC6
		public VTUnsignedShort VTUnsignedShort
		{
			get
			{
				return base.GetElement<VTUnsignedShort>(12);
			}
			set
			{
				base.SetElement<VTUnsignedShort>(12, value);
			}
		}

		// Token: 0x17006A2A RID: 27178
		// (get) Token: 0x06014C20 RID: 85024 RVA: 0x00316AD1 File Offset: 0x00314CD1
		// (set) Token: 0x06014C21 RID: 85025 RVA: 0x00316ADB File Offset: 0x00314CDB
		public VTUnsignedInt32 VTUnsignedInt32
		{
			get
			{
				return base.GetElement<VTUnsignedInt32>(13);
			}
			set
			{
				base.SetElement<VTUnsignedInt32>(13, value);
			}
		}

		// Token: 0x17006A2B RID: 27179
		// (get) Token: 0x06014C22 RID: 85026 RVA: 0x00316AE6 File Offset: 0x00314CE6
		// (set) Token: 0x06014C23 RID: 85027 RVA: 0x00316AF0 File Offset: 0x00314CF0
		public VTUnsignedInt64 VTUnsignedInt64
		{
			get
			{
				return base.GetElement<VTUnsignedInt64>(14);
			}
			set
			{
				base.SetElement<VTUnsignedInt64>(14, value);
			}
		}

		// Token: 0x17006A2C RID: 27180
		// (get) Token: 0x06014C24 RID: 85028 RVA: 0x00316AFB File Offset: 0x00314CFB
		// (set) Token: 0x06014C25 RID: 85029 RVA: 0x00316B05 File Offset: 0x00314D05
		public VTUnsignedInteger VTUnsignedInteger
		{
			get
			{
				return base.GetElement<VTUnsignedInteger>(15);
			}
			set
			{
				base.SetElement<VTUnsignedInteger>(15, value);
			}
		}

		// Token: 0x17006A2D RID: 27181
		// (get) Token: 0x06014C26 RID: 85030 RVA: 0x00316B10 File Offset: 0x00314D10
		// (set) Token: 0x06014C27 RID: 85031 RVA: 0x00316B1A File Offset: 0x00314D1A
		public VTFloat VTFloat
		{
			get
			{
				return base.GetElement<VTFloat>(16);
			}
			set
			{
				base.SetElement<VTFloat>(16, value);
			}
		}

		// Token: 0x17006A2E RID: 27182
		// (get) Token: 0x06014C28 RID: 85032 RVA: 0x00316B25 File Offset: 0x00314D25
		// (set) Token: 0x06014C29 RID: 85033 RVA: 0x00316B2F File Offset: 0x00314D2F
		public VTDouble VTDouble
		{
			get
			{
				return base.GetElement<VTDouble>(17);
			}
			set
			{
				base.SetElement<VTDouble>(17, value);
			}
		}

		// Token: 0x17006A2F RID: 27183
		// (get) Token: 0x06014C2A RID: 85034 RVA: 0x00316B3A File Offset: 0x00314D3A
		// (set) Token: 0x06014C2B RID: 85035 RVA: 0x00316B44 File Offset: 0x00314D44
		public VTDecimal VTDecimal
		{
			get
			{
				return base.GetElement<VTDecimal>(18);
			}
			set
			{
				base.SetElement<VTDecimal>(18, value);
			}
		}

		// Token: 0x17006A30 RID: 27184
		// (get) Token: 0x06014C2C RID: 85036 RVA: 0x00316B4F File Offset: 0x00314D4F
		// (set) Token: 0x06014C2D RID: 85037 RVA: 0x00316B59 File Offset: 0x00314D59
		public VTLPSTR VTLPSTR
		{
			get
			{
				return base.GetElement<VTLPSTR>(19);
			}
			set
			{
				base.SetElement<VTLPSTR>(19, value);
			}
		}

		// Token: 0x17006A31 RID: 27185
		// (get) Token: 0x06014C2E RID: 85038 RVA: 0x00316B64 File Offset: 0x00314D64
		// (set) Token: 0x06014C2F RID: 85039 RVA: 0x00316B6E File Offset: 0x00314D6E
		public VTLPWSTR VTLPWSTR
		{
			get
			{
				return base.GetElement<VTLPWSTR>(20);
			}
			set
			{
				base.SetElement<VTLPWSTR>(20, value);
			}
		}

		// Token: 0x17006A32 RID: 27186
		// (get) Token: 0x06014C30 RID: 85040 RVA: 0x00316B79 File Offset: 0x00314D79
		// (set) Token: 0x06014C31 RID: 85041 RVA: 0x00316B83 File Offset: 0x00314D83
		public VTBString VTBString
		{
			get
			{
				return base.GetElement<VTBString>(21);
			}
			set
			{
				base.SetElement<VTBString>(21, value);
			}
		}

		// Token: 0x17006A33 RID: 27187
		// (get) Token: 0x06014C32 RID: 85042 RVA: 0x00316B8E File Offset: 0x00314D8E
		// (set) Token: 0x06014C33 RID: 85043 RVA: 0x00316B98 File Offset: 0x00314D98
		public VTDate VTDate
		{
			get
			{
				return base.GetElement<VTDate>(22);
			}
			set
			{
				base.SetElement<VTDate>(22, value);
			}
		}

		// Token: 0x17006A34 RID: 27188
		// (get) Token: 0x06014C34 RID: 85044 RVA: 0x00316BA3 File Offset: 0x00314DA3
		// (set) Token: 0x06014C35 RID: 85045 RVA: 0x00316BAD File Offset: 0x00314DAD
		public VTFileTime VTFileTime
		{
			get
			{
				return base.GetElement<VTFileTime>(23);
			}
			set
			{
				base.SetElement<VTFileTime>(23, value);
			}
		}

		// Token: 0x17006A35 RID: 27189
		// (get) Token: 0x06014C36 RID: 85046 RVA: 0x00316BB8 File Offset: 0x00314DB8
		// (set) Token: 0x06014C37 RID: 85047 RVA: 0x00316BC2 File Offset: 0x00314DC2
		public VTBool VTBool
		{
			get
			{
				return base.GetElement<VTBool>(24);
			}
			set
			{
				base.SetElement<VTBool>(24, value);
			}
		}

		// Token: 0x17006A36 RID: 27190
		// (get) Token: 0x06014C38 RID: 85048 RVA: 0x00316BCD File Offset: 0x00314DCD
		// (set) Token: 0x06014C39 RID: 85049 RVA: 0x00316BD7 File Offset: 0x00314DD7
		public VTCurrency VTCurrency
		{
			get
			{
				return base.GetElement<VTCurrency>(25);
			}
			set
			{
				base.SetElement<VTCurrency>(25, value);
			}
		}

		// Token: 0x17006A37 RID: 27191
		// (get) Token: 0x06014C3A RID: 85050 RVA: 0x00316BE2 File Offset: 0x00314DE2
		// (set) Token: 0x06014C3B RID: 85051 RVA: 0x00316BEC File Offset: 0x00314DEC
		public VTError VTError
		{
			get
			{
				return base.GetElement<VTError>(26);
			}
			set
			{
				base.SetElement<VTError>(26, value);
			}
		}

		// Token: 0x17006A38 RID: 27192
		// (get) Token: 0x06014C3C RID: 85052 RVA: 0x00316BF7 File Offset: 0x00314DF7
		// (set) Token: 0x06014C3D RID: 85053 RVA: 0x00316C01 File Offset: 0x00314E01
		public VTStreamData VTStreamData
		{
			get
			{
				return base.GetElement<VTStreamData>(27);
			}
			set
			{
				base.SetElement<VTStreamData>(27, value);
			}
		}

		// Token: 0x17006A39 RID: 27193
		// (get) Token: 0x06014C3E RID: 85054 RVA: 0x00316C0C File Offset: 0x00314E0C
		// (set) Token: 0x06014C3F RID: 85055 RVA: 0x00316C16 File Offset: 0x00314E16
		public VTOStreamData VTOStreamData
		{
			get
			{
				return base.GetElement<VTOStreamData>(28);
			}
			set
			{
				base.SetElement<VTOStreamData>(28, value);
			}
		}

		// Token: 0x17006A3A RID: 27194
		// (get) Token: 0x06014C40 RID: 85056 RVA: 0x00316C21 File Offset: 0x00314E21
		// (set) Token: 0x06014C41 RID: 85057 RVA: 0x00316C2B File Offset: 0x00314E2B
		public VTStorage VTStorage
		{
			get
			{
				return base.GetElement<VTStorage>(29);
			}
			set
			{
				base.SetElement<VTStorage>(29, value);
			}
		}

		// Token: 0x17006A3B RID: 27195
		// (get) Token: 0x06014C42 RID: 85058 RVA: 0x00316C36 File Offset: 0x00314E36
		// (set) Token: 0x06014C43 RID: 85059 RVA: 0x00316C40 File Offset: 0x00314E40
		public VTOStorage VTOStorage
		{
			get
			{
				return base.GetElement<VTOStorage>(30);
			}
			set
			{
				base.SetElement<VTOStorage>(30, value);
			}
		}

		// Token: 0x17006A3C RID: 27196
		// (get) Token: 0x06014C44 RID: 85060 RVA: 0x00316C4B File Offset: 0x00314E4B
		// (set) Token: 0x06014C45 RID: 85061 RVA: 0x00316C55 File Offset: 0x00314E55
		public VTVStreamData VTVStreamData
		{
			get
			{
				return base.GetElement<VTVStreamData>(31);
			}
			set
			{
				base.SetElement<VTVStreamData>(31, value);
			}
		}

		// Token: 0x17006A3D RID: 27197
		// (get) Token: 0x06014C46 RID: 85062 RVA: 0x00316C60 File Offset: 0x00314E60
		// (set) Token: 0x06014C47 RID: 85063 RVA: 0x00316C6A File Offset: 0x00314E6A
		public VTClassId VTClassId
		{
			get
			{
				return base.GetElement<VTClassId>(32);
			}
			set
			{
				base.SetElement<VTClassId>(32, value);
			}
		}

		// Token: 0x17006A3E RID: 27198
		// (get) Token: 0x06014C48 RID: 85064 RVA: 0x00316C75 File Offset: 0x00314E75
		// (set) Token: 0x06014C49 RID: 85065 RVA: 0x00316C7F File Offset: 0x00314E7F
		public VTClipboardData VTClipboardData
		{
			get
			{
				return base.GetElement<VTClipboardData>(33);
			}
			set
			{
				base.SetElement<VTClipboardData>(33, value);
			}
		}

		// Token: 0x06014C4A RID: 85066 RVA: 0x00316C8C File Offset: 0x00314E8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fmtid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "pid" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "linkTarget" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014C4B RID: 85067 RVA: 0x00316CF9 File Offset: 0x00314EF9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomDocumentProperty>(deep);
		}

		// Token: 0x06014C4C RID: 85068 RVA: 0x00316D04 File Offset: 0x00314F04
		// Note: this type is marked as 'beforefieldinit'.
		static CustomDocumentProperty()
		{
			byte[] array = new byte[4];
			CustomDocumentProperty.attributeNamespaceIds = array;
			CustomDocumentProperty.eleTagNames = new string[]
			{
				"vector", "array", "blob", "oblob", "empty", "null", "i1", "i2", "i4", "i8",
				"int", "ui1", "ui2", "ui4", "ui8", "uint", "r4", "r8", "decimal", "lpstr",
				"lpwstr", "bstr", "date", "filetime", "bool", "cy", "error", "stream", "ostream", "storage",
				"ostorage", "vstream", "clsid", "cf"
			};
			CustomDocumentProperty.eleNamespaceIds = new byte[]
			{
				5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
				5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
				5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
				5, 5, 5, 5
			};
		}

		// Token: 0x04008FBD RID: 36797
		private const string tagName = "property";

		// Token: 0x04008FBE RID: 36798
		private const byte tagNsId = 4;

		// Token: 0x04008FBF RID: 36799
		internal const int ElementTypeIdConst = 10838;

		// Token: 0x04008FC0 RID: 36800
		private static string[] attributeTagNames = new string[] { "fmtid", "pid", "name", "linkTarget" };

		// Token: 0x04008FC1 RID: 36801
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008FC2 RID: 36802
		private static readonly string[] eleTagNames;

		// Token: 0x04008FC3 RID: 36803
		private static readonly byte[] eleNamespaceIds;
	}
}
