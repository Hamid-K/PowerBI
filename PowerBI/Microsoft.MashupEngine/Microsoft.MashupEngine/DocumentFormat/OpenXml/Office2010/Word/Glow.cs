using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024A3 RID: 9379
	[ChildElementInfo(typeof(RgbColorModelHex), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SchemeColor), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Glow : OpenXmlCompositeElement
	{
		// Token: 0x170051DB RID: 20955
		// (get) Token: 0x0601152F RID: 70959 RVA: 0x002ED29C File Offset: 0x002EB49C
		public override string LocalName
		{
			get
			{
				return "glow";
			}
		}

		// Token: 0x170051DC RID: 20956
		// (get) Token: 0x06011530 RID: 70960 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051DD RID: 20957
		// (get) Token: 0x06011531 RID: 70961 RVA: 0x002ED2A3 File Offset: 0x002EB4A3
		internal override int ElementTypeId
		{
			get
			{
				return 12853;
			}
		}

		// Token: 0x06011532 RID: 70962 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170051DE RID: 20958
		// (get) Token: 0x06011533 RID: 70963 RVA: 0x002ED2AA File Offset: 0x002EB4AA
		internal override string[] AttributeTagNames
		{
			get
			{
				return Glow.attributeTagNames;
			}
		}

		// Token: 0x170051DF RID: 20959
		// (get) Token: 0x06011534 RID: 70964 RVA: 0x002ED2B1 File Offset: 0x002EB4B1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Glow.attributeNamespaceIds;
			}
		}

		// Token: 0x170051E0 RID: 20960
		// (get) Token: 0x06011535 RID: 70965 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06011536 RID: 70966 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "rad")]
		public Int64Value GlowRadius
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011537 RID: 70967 RVA: 0x00293ECF File Offset: 0x002920CF
		public Glow()
		{
		}

		// Token: 0x06011538 RID: 70968 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Glow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011539 RID: 70969 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Glow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601153A RID: 70970 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Glow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601153B RID: 70971 RVA: 0x002ED009 File Offset: 0x002EB209
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "srgbClr" == name)
			{
				return new RgbColorModelHex();
			}
			if (52 == namespaceId && "schemeClr" == name)
			{
				return new SchemeColor();
			}
			return null;
		}

		// Token: 0x170051E1 RID: 20961
		// (get) Token: 0x0601153C RID: 70972 RVA: 0x002ED2B8 File Offset: 0x002EB4B8
		internal override string[] ElementTagNames
		{
			get
			{
				return Glow.eleTagNames;
			}
		}

		// Token: 0x170051E2 RID: 20962
		// (get) Token: 0x0601153D RID: 70973 RVA: 0x002ED2BF File Offset: 0x002EB4BF
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Glow.eleNamespaceIds;
			}
		}

		// Token: 0x170051E3 RID: 20963
		// (get) Token: 0x0601153E RID: 70974 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170051E4 RID: 20964
		// (get) Token: 0x0601153F RID: 70975 RVA: 0x002ED04A File Offset: 0x002EB24A
		// (set) Token: 0x06011540 RID: 70976 RVA: 0x002ED053 File Offset: 0x002EB253
		public RgbColorModelHex RgbColorModelHex
		{
			get
			{
				return base.GetElement<RgbColorModelHex>(0);
			}
			set
			{
				base.SetElement<RgbColorModelHex>(0, value);
			}
		}

		// Token: 0x170051E5 RID: 20965
		// (get) Token: 0x06011541 RID: 70977 RVA: 0x002ED05D File Offset: 0x002EB25D
		// (set) Token: 0x06011542 RID: 70978 RVA: 0x002ED066 File Offset: 0x002EB266
		public SchemeColor SchemeColor
		{
			get
			{
				return base.GetElement<SchemeColor>(1);
			}
			set
			{
				base.SetElement<SchemeColor>(1, value);
			}
		}

		// Token: 0x06011543 RID: 70979 RVA: 0x002ED2C6 File Offset: 0x002EB4C6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "rad" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011544 RID: 70980 RVA: 0x002ED2E8 File Offset: 0x002EB4E8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Glow>(deep);
		}

		// Token: 0x04007951 RID: 31057
		private const string tagName = "glow";

		// Token: 0x04007952 RID: 31058
		private const byte tagNsId = 52;

		// Token: 0x04007953 RID: 31059
		internal const int ElementTypeIdConst = 12853;

		// Token: 0x04007954 RID: 31060
		private static string[] attributeTagNames = new string[] { "rad" };

		// Token: 0x04007955 RID: 31061
		private static byte[] attributeNamespaceIds = new byte[] { 52 };

		// Token: 0x04007956 RID: 31062
		private static readonly string[] eleTagNames = new string[] { "srgbClr", "schemeClr" };

		// Token: 0x04007957 RID: 31063
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52 };
	}
}
