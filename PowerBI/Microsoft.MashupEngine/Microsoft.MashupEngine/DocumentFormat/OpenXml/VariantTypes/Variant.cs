using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200290D RID: 10509
	[ChildElementInfo(typeof(VTOStreamData))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(VTUnsignedInteger))]
	[ChildElementInfo(typeof(VTClipboardData))]
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
	[ChildElementInfo(typeof(Variant))]
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
	[ChildElementInfo(typeof(VTStorage))]
	[ChildElementInfo(typeof(VTOStorage))]
	[ChildElementInfo(typeof(VTVStreamData))]
	[ChildElementInfo(typeof(VTClassId))]
	internal class Variant : OpenXmlCompositeElement
	{
		// Token: 0x17006A53 RID: 27219
		// (get) Token: 0x06014C7D RID: 85117 RVA: 0x0031702E File Offset: 0x0031522E
		public override string LocalName
		{
			get
			{
				return "variant";
			}
		}

		// Token: 0x17006A54 RID: 27220
		// (get) Token: 0x06014C7E RID: 85118 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006A55 RID: 27221
		// (get) Token: 0x06014C7F RID: 85119 RVA: 0x00317035 File Offset: 0x00315235
		internal override int ElementTypeId
		{
			get
			{
				return 10963;
			}
		}

		// Token: 0x06014C80 RID: 85120 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014C81 RID: 85121 RVA: 0x00293ECF File Offset: 0x002920CF
		public Variant()
		{
		}

		// Token: 0x06014C82 RID: 85122 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Variant(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014C83 RID: 85123 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Variant(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014C84 RID: 85124 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Variant(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014C85 RID: 85125 RVA: 0x0031703C File Offset: 0x0031523C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (5 == namespaceId && "variant" == name)
			{
				return new Variant();
			}
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

		// Token: 0x17006A56 RID: 27222
		// (get) Token: 0x06014C86 RID: 85126 RVA: 0x0031736F File Offset: 0x0031556F
		internal override string[] ElementTagNames
		{
			get
			{
				return Variant.eleTagNames;
			}
		}

		// Token: 0x17006A57 RID: 27223
		// (get) Token: 0x06014C87 RID: 85127 RVA: 0x00317376 File Offset: 0x00315576
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Variant.eleNamespaceIds;
			}
		}

		// Token: 0x17006A58 RID: 27224
		// (get) Token: 0x06014C88 RID: 85128 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17006A59 RID: 27225
		// (get) Token: 0x06014C89 RID: 85129 RVA: 0x0031737D File Offset: 0x0031557D
		// (set) Token: 0x06014C8A RID: 85130 RVA: 0x00317386 File Offset: 0x00315586
		public Variant InnerVariant
		{
			get
			{
				return base.GetElement<Variant>(0);
			}
			set
			{
				base.SetElement<Variant>(0, value);
			}
		}

		// Token: 0x17006A5A RID: 27226
		// (get) Token: 0x06014C8B RID: 85131 RVA: 0x00317390 File Offset: 0x00315590
		// (set) Token: 0x06014C8C RID: 85132 RVA: 0x00317399 File Offset: 0x00315599
		public VTVector VTVector
		{
			get
			{
				return base.GetElement<VTVector>(1);
			}
			set
			{
				base.SetElement<VTVector>(1, value);
			}
		}

		// Token: 0x17006A5B RID: 27227
		// (get) Token: 0x06014C8D RID: 85133 RVA: 0x003173A3 File Offset: 0x003155A3
		// (set) Token: 0x06014C8E RID: 85134 RVA: 0x003173AC File Offset: 0x003155AC
		public VTArray VTArray
		{
			get
			{
				return base.GetElement<VTArray>(2);
			}
			set
			{
				base.SetElement<VTArray>(2, value);
			}
		}

		// Token: 0x17006A5C RID: 27228
		// (get) Token: 0x06014C8F RID: 85135 RVA: 0x003173B6 File Offset: 0x003155B6
		// (set) Token: 0x06014C90 RID: 85136 RVA: 0x003173BF File Offset: 0x003155BF
		public VTBlob VTBlob
		{
			get
			{
				return base.GetElement<VTBlob>(3);
			}
			set
			{
				base.SetElement<VTBlob>(3, value);
			}
		}

		// Token: 0x17006A5D RID: 27229
		// (get) Token: 0x06014C91 RID: 85137 RVA: 0x003173C9 File Offset: 0x003155C9
		// (set) Token: 0x06014C92 RID: 85138 RVA: 0x003173D2 File Offset: 0x003155D2
		public VTOBlob VTOBlob
		{
			get
			{
				return base.GetElement<VTOBlob>(4);
			}
			set
			{
				base.SetElement<VTOBlob>(4, value);
			}
		}

		// Token: 0x17006A5E RID: 27230
		// (get) Token: 0x06014C93 RID: 85139 RVA: 0x003173DC File Offset: 0x003155DC
		// (set) Token: 0x06014C94 RID: 85140 RVA: 0x003173E5 File Offset: 0x003155E5
		public VTEmpty VTEmpty
		{
			get
			{
				return base.GetElement<VTEmpty>(5);
			}
			set
			{
				base.SetElement<VTEmpty>(5, value);
			}
		}

		// Token: 0x17006A5F RID: 27231
		// (get) Token: 0x06014C95 RID: 85141 RVA: 0x003173EF File Offset: 0x003155EF
		// (set) Token: 0x06014C96 RID: 85142 RVA: 0x003173F8 File Offset: 0x003155F8
		public VTNull VTNull
		{
			get
			{
				return base.GetElement<VTNull>(6);
			}
			set
			{
				base.SetElement<VTNull>(6, value);
			}
		}

		// Token: 0x17006A60 RID: 27232
		// (get) Token: 0x06014C97 RID: 85143 RVA: 0x00317402 File Offset: 0x00315602
		// (set) Token: 0x06014C98 RID: 85144 RVA: 0x0031740B File Offset: 0x0031560B
		public VTByte VTByte
		{
			get
			{
				return base.GetElement<VTByte>(7);
			}
			set
			{
				base.SetElement<VTByte>(7, value);
			}
		}

		// Token: 0x17006A61 RID: 27233
		// (get) Token: 0x06014C99 RID: 85145 RVA: 0x00317415 File Offset: 0x00315615
		// (set) Token: 0x06014C9A RID: 85146 RVA: 0x0031741E File Offset: 0x0031561E
		public VTShort VTShort
		{
			get
			{
				return base.GetElement<VTShort>(8);
			}
			set
			{
				base.SetElement<VTShort>(8, value);
			}
		}

		// Token: 0x17006A62 RID: 27234
		// (get) Token: 0x06014C9B RID: 85147 RVA: 0x00317428 File Offset: 0x00315628
		// (set) Token: 0x06014C9C RID: 85148 RVA: 0x00317432 File Offset: 0x00315632
		public VTInt32 VTInt32
		{
			get
			{
				return base.GetElement<VTInt32>(9);
			}
			set
			{
				base.SetElement<VTInt32>(9, value);
			}
		}

		// Token: 0x17006A63 RID: 27235
		// (get) Token: 0x06014C9D RID: 85149 RVA: 0x0031743D File Offset: 0x0031563D
		// (set) Token: 0x06014C9E RID: 85150 RVA: 0x00317447 File Offset: 0x00315647
		public VTInt64 VTInt64
		{
			get
			{
				return base.GetElement<VTInt64>(10);
			}
			set
			{
				base.SetElement<VTInt64>(10, value);
			}
		}

		// Token: 0x17006A64 RID: 27236
		// (get) Token: 0x06014C9F RID: 85151 RVA: 0x00317452 File Offset: 0x00315652
		// (set) Token: 0x06014CA0 RID: 85152 RVA: 0x0031745C File Offset: 0x0031565C
		public VTInteger VTInteger
		{
			get
			{
				return base.GetElement<VTInteger>(11);
			}
			set
			{
				base.SetElement<VTInteger>(11, value);
			}
		}

		// Token: 0x17006A65 RID: 27237
		// (get) Token: 0x06014CA1 RID: 85153 RVA: 0x00317467 File Offset: 0x00315667
		// (set) Token: 0x06014CA2 RID: 85154 RVA: 0x00317471 File Offset: 0x00315671
		public VTUnsignedByte VTUnsignedByte
		{
			get
			{
				return base.GetElement<VTUnsignedByte>(12);
			}
			set
			{
				base.SetElement<VTUnsignedByte>(12, value);
			}
		}

		// Token: 0x17006A66 RID: 27238
		// (get) Token: 0x06014CA3 RID: 85155 RVA: 0x0031747C File Offset: 0x0031567C
		// (set) Token: 0x06014CA4 RID: 85156 RVA: 0x00317486 File Offset: 0x00315686
		public VTUnsignedShort VTUnsignedShort
		{
			get
			{
				return base.GetElement<VTUnsignedShort>(13);
			}
			set
			{
				base.SetElement<VTUnsignedShort>(13, value);
			}
		}

		// Token: 0x17006A67 RID: 27239
		// (get) Token: 0x06014CA5 RID: 85157 RVA: 0x00317491 File Offset: 0x00315691
		// (set) Token: 0x06014CA6 RID: 85158 RVA: 0x0031749B File Offset: 0x0031569B
		public VTUnsignedInt32 VTUnsignedInt32
		{
			get
			{
				return base.GetElement<VTUnsignedInt32>(14);
			}
			set
			{
				base.SetElement<VTUnsignedInt32>(14, value);
			}
		}

		// Token: 0x17006A68 RID: 27240
		// (get) Token: 0x06014CA7 RID: 85159 RVA: 0x003174A6 File Offset: 0x003156A6
		// (set) Token: 0x06014CA8 RID: 85160 RVA: 0x003174B0 File Offset: 0x003156B0
		public VTUnsignedInt64 VTUnsignedInt64
		{
			get
			{
				return base.GetElement<VTUnsignedInt64>(15);
			}
			set
			{
				base.SetElement<VTUnsignedInt64>(15, value);
			}
		}

		// Token: 0x17006A69 RID: 27241
		// (get) Token: 0x06014CA9 RID: 85161 RVA: 0x003174BB File Offset: 0x003156BB
		// (set) Token: 0x06014CAA RID: 85162 RVA: 0x003174C5 File Offset: 0x003156C5
		public VTUnsignedInteger VTUnsignedInteger
		{
			get
			{
				return base.GetElement<VTUnsignedInteger>(16);
			}
			set
			{
				base.SetElement<VTUnsignedInteger>(16, value);
			}
		}

		// Token: 0x17006A6A RID: 27242
		// (get) Token: 0x06014CAB RID: 85163 RVA: 0x003174D0 File Offset: 0x003156D0
		// (set) Token: 0x06014CAC RID: 85164 RVA: 0x003174DA File Offset: 0x003156DA
		public VTFloat VTFloat
		{
			get
			{
				return base.GetElement<VTFloat>(17);
			}
			set
			{
				base.SetElement<VTFloat>(17, value);
			}
		}

		// Token: 0x17006A6B RID: 27243
		// (get) Token: 0x06014CAD RID: 85165 RVA: 0x003174E5 File Offset: 0x003156E5
		// (set) Token: 0x06014CAE RID: 85166 RVA: 0x003174EF File Offset: 0x003156EF
		public VTDouble VTDouble
		{
			get
			{
				return base.GetElement<VTDouble>(18);
			}
			set
			{
				base.SetElement<VTDouble>(18, value);
			}
		}

		// Token: 0x17006A6C RID: 27244
		// (get) Token: 0x06014CAF RID: 85167 RVA: 0x003174FA File Offset: 0x003156FA
		// (set) Token: 0x06014CB0 RID: 85168 RVA: 0x00317504 File Offset: 0x00315704
		public VTDecimal VTDecimal
		{
			get
			{
				return base.GetElement<VTDecimal>(19);
			}
			set
			{
				base.SetElement<VTDecimal>(19, value);
			}
		}

		// Token: 0x17006A6D RID: 27245
		// (get) Token: 0x06014CB1 RID: 85169 RVA: 0x0031750F File Offset: 0x0031570F
		// (set) Token: 0x06014CB2 RID: 85170 RVA: 0x00317519 File Offset: 0x00315719
		public VTLPSTR VTLPSTR
		{
			get
			{
				return base.GetElement<VTLPSTR>(20);
			}
			set
			{
				base.SetElement<VTLPSTR>(20, value);
			}
		}

		// Token: 0x17006A6E RID: 27246
		// (get) Token: 0x06014CB3 RID: 85171 RVA: 0x00317524 File Offset: 0x00315724
		// (set) Token: 0x06014CB4 RID: 85172 RVA: 0x0031752E File Offset: 0x0031572E
		public VTLPWSTR VTLPWSTR
		{
			get
			{
				return base.GetElement<VTLPWSTR>(21);
			}
			set
			{
				base.SetElement<VTLPWSTR>(21, value);
			}
		}

		// Token: 0x17006A6F RID: 27247
		// (get) Token: 0x06014CB5 RID: 85173 RVA: 0x00317539 File Offset: 0x00315739
		// (set) Token: 0x06014CB6 RID: 85174 RVA: 0x00317543 File Offset: 0x00315743
		public VTBString VTBString
		{
			get
			{
				return base.GetElement<VTBString>(22);
			}
			set
			{
				base.SetElement<VTBString>(22, value);
			}
		}

		// Token: 0x17006A70 RID: 27248
		// (get) Token: 0x06014CB7 RID: 85175 RVA: 0x0031754E File Offset: 0x0031574E
		// (set) Token: 0x06014CB8 RID: 85176 RVA: 0x00317558 File Offset: 0x00315758
		public VTDate VTDate
		{
			get
			{
				return base.GetElement<VTDate>(23);
			}
			set
			{
				base.SetElement<VTDate>(23, value);
			}
		}

		// Token: 0x17006A71 RID: 27249
		// (get) Token: 0x06014CB9 RID: 85177 RVA: 0x00317563 File Offset: 0x00315763
		// (set) Token: 0x06014CBA RID: 85178 RVA: 0x0031756D File Offset: 0x0031576D
		public VTFileTime VTFileTime
		{
			get
			{
				return base.GetElement<VTFileTime>(24);
			}
			set
			{
				base.SetElement<VTFileTime>(24, value);
			}
		}

		// Token: 0x17006A72 RID: 27250
		// (get) Token: 0x06014CBB RID: 85179 RVA: 0x00317578 File Offset: 0x00315778
		// (set) Token: 0x06014CBC RID: 85180 RVA: 0x00317582 File Offset: 0x00315782
		public VTBool VTBool
		{
			get
			{
				return base.GetElement<VTBool>(25);
			}
			set
			{
				base.SetElement<VTBool>(25, value);
			}
		}

		// Token: 0x17006A73 RID: 27251
		// (get) Token: 0x06014CBD RID: 85181 RVA: 0x0031758D File Offset: 0x0031578D
		// (set) Token: 0x06014CBE RID: 85182 RVA: 0x00317597 File Offset: 0x00315797
		public VTCurrency VTCurrency
		{
			get
			{
				return base.GetElement<VTCurrency>(26);
			}
			set
			{
				base.SetElement<VTCurrency>(26, value);
			}
		}

		// Token: 0x17006A74 RID: 27252
		// (get) Token: 0x06014CBF RID: 85183 RVA: 0x003175A2 File Offset: 0x003157A2
		// (set) Token: 0x06014CC0 RID: 85184 RVA: 0x003175AC File Offset: 0x003157AC
		public VTError VTError
		{
			get
			{
				return base.GetElement<VTError>(27);
			}
			set
			{
				base.SetElement<VTError>(27, value);
			}
		}

		// Token: 0x17006A75 RID: 27253
		// (get) Token: 0x06014CC1 RID: 85185 RVA: 0x003175B7 File Offset: 0x003157B7
		// (set) Token: 0x06014CC2 RID: 85186 RVA: 0x003175C1 File Offset: 0x003157C1
		public VTStreamData VTStreamData
		{
			get
			{
				return base.GetElement<VTStreamData>(28);
			}
			set
			{
				base.SetElement<VTStreamData>(28, value);
			}
		}

		// Token: 0x17006A76 RID: 27254
		// (get) Token: 0x06014CC3 RID: 85187 RVA: 0x003175CC File Offset: 0x003157CC
		// (set) Token: 0x06014CC4 RID: 85188 RVA: 0x003175D6 File Offset: 0x003157D6
		public VTOStreamData VTOStreamData
		{
			get
			{
				return base.GetElement<VTOStreamData>(29);
			}
			set
			{
				base.SetElement<VTOStreamData>(29, value);
			}
		}

		// Token: 0x17006A77 RID: 27255
		// (get) Token: 0x06014CC5 RID: 85189 RVA: 0x003175E1 File Offset: 0x003157E1
		// (set) Token: 0x06014CC6 RID: 85190 RVA: 0x003175EB File Offset: 0x003157EB
		public VTStorage VTStorage
		{
			get
			{
				return base.GetElement<VTStorage>(30);
			}
			set
			{
				base.SetElement<VTStorage>(30, value);
			}
		}

		// Token: 0x17006A78 RID: 27256
		// (get) Token: 0x06014CC7 RID: 85191 RVA: 0x003175F6 File Offset: 0x003157F6
		// (set) Token: 0x06014CC8 RID: 85192 RVA: 0x00317600 File Offset: 0x00315800
		public VTOStorage VTOStorage
		{
			get
			{
				return base.GetElement<VTOStorage>(31);
			}
			set
			{
				base.SetElement<VTOStorage>(31, value);
			}
		}

		// Token: 0x17006A79 RID: 27257
		// (get) Token: 0x06014CC9 RID: 85193 RVA: 0x0031760B File Offset: 0x0031580B
		// (set) Token: 0x06014CCA RID: 85194 RVA: 0x00317615 File Offset: 0x00315815
		public VTVStreamData VTVStreamData
		{
			get
			{
				return base.GetElement<VTVStreamData>(32);
			}
			set
			{
				base.SetElement<VTVStreamData>(32, value);
			}
		}

		// Token: 0x17006A7A RID: 27258
		// (get) Token: 0x06014CCB RID: 85195 RVA: 0x00317620 File Offset: 0x00315820
		// (set) Token: 0x06014CCC RID: 85196 RVA: 0x0031762A File Offset: 0x0031582A
		public VTClassId VTClassId
		{
			get
			{
				return base.GetElement<VTClassId>(33);
			}
			set
			{
				base.SetElement<VTClassId>(33, value);
			}
		}

		// Token: 0x17006A7B RID: 27259
		// (get) Token: 0x06014CCD RID: 85197 RVA: 0x00317635 File Offset: 0x00315835
		// (set) Token: 0x06014CCE RID: 85198 RVA: 0x0031763F File Offset: 0x0031583F
		public VTClipboardData VTClipboardData
		{
			get
			{
				return base.GetElement<VTClipboardData>(34);
			}
			set
			{
				base.SetElement<VTClipboardData>(34, value);
			}
		}

		// Token: 0x06014CCF RID: 85199 RVA: 0x0031764A File Offset: 0x0031584A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Variant>(deep);
		}

		// Token: 0x04008FD3 RID: 36819
		private const string tagName = "variant";

		// Token: 0x04008FD4 RID: 36820
		private const byte tagNsId = 5;

		// Token: 0x04008FD5 RID: 36821
		internal const int ElementTypeIdConst = 10963;

		// Token: 0x04008FD6 RID: 36822
		private static readonly string[] eleTagNames = new string[]
		{
			"variant", "vector", "array", "blob", "oblob", "empty", "null", "i1", "i2", "i4",
			"i8", "int", "ui1", "ui2", "ui4", "ui8", "uint", "r4", "r8", "decimal",
			"lpstr", "lpwstr", "bstr", "date", "filetime", "bool", "cy", "error", "stream", "ostream",
			"storage", "ostorage", "vstream", "clsid", "cf"
		};

		// Token: 0x04008FD7 RID: 36823
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5
		};
	}
}
