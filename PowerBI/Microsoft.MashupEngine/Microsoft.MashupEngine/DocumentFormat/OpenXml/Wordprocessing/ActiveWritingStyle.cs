using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FDC RID: 12252
	[GeneratedCode("DomGen", "2.0")]
	internal class ActiveWritingStyle : OpenXmlLeafElement
	{
		// Token: 0x1700949B RID: 38043
		// (get) Token: 0x0601A9D1 RID: 109009 RVA: 0x00364E89 File Offset: 0x00363089
		public override string LocalName
		{
			get
			{
				return "activeWritingStyle";
			}
		}

		// Token: 0x1700949C RID: 38044
		// (get) Token: 0x0601A9D2 RID: 109010 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700949D RID: 38045
		// (get) Token: 0x0601A9D3 RID: 109011 RVA: 0x00364E90 File Offset: 0x00363090
		internal override int ElementTypeId
		{
			get
			{
				return 11979;
			}
		}

		// Token: 0x0601A9D4 RID: 109012 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700949E RID: 38046
		// (get) Token: 0x0601A9D5 RID: 109013 RVA: 0x00364E97 File Offset: 0x00363097
		internal override string[] AttributeTagNames
		{
			get
			{
				return ActiveWritingStyle.attributeTagNames;
			}
		}

		// Token: 0x1700949F RID: 38047
		// (get) Token: 0x0601A9D6 RID: 109014 RVA: 0x00364E9E File Offset: 0x0036309E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ActiveWritingStyle.attributeNamespaceIds;
			}
		}

		// Token: 0x170094A0 RID: 38048
		// (get) Token: 0x0601A9D7 RID: 109015 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A9D8 RID: 109016 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "lang")]
		public StringValue Language
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

		// Token: 0x170094A1 RID: 38049
		// (get) Token: 0x0601A9D9 RID: 109017 RVA: 0x002F0823 File Offset: 0x002EEA23
		// (set) Token: 0x0601A9DA RID: 109018 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "vendorID")]
		public UInt16Value VendorID
		{
			get
			{
				return (UInt16Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170094A2 RID: 38050
		// (get) Token: 0x0601A9DB RID: 109019 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x0601A9DC RID: 109020 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "dllVersion")]
		public Int32Value DllVersion
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170094A3 RID: 38051
		// (get) Token: 0x0601A9DD RID: 109021 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x0601A9DE RID: 109022 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "nlCheck")]
		public OnOffValue NaturalLanguageGrammarCheck
		{
			get
			{
				return (OnOffValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170094A4 RID: 38052
		// (get) Token: 0x0601A9DF RID: 109023 RVA: 0x002EB443 File Offset: 0x002E9643
		// (set) Token: 0x0601A9E0 RID: 109024 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "checkStyle")]
		public OnOffValue CheckStyle
		{
			get
			{
				return (OnOffValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170094A5 RID: 38053
		// (get) Token: 0x0601A9E1 RID: 109025 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0601A9E2 RID: 109026 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "appName")]
		public StringValue ApplicationName
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

		// Token: 0x0601A9E4 RID: 109028 RVA: 0x00364EA8 File Offset: 0x003630A8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "lang" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "vendorID" == name)
			{
				return new UInt16Value();
			}
			if (23 == namespaceId && "dllVersion" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "nlCheck" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "checkStyle" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "appName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A9E5 RID: 109029 RVA: 0x00364F4D File Offset: 0x0036314D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ActiveWritingStyle>(deep);
		}

		// Token: 0x0400ADBB RID: 44475
		private const string tagName = "activeWritingStyle";

		// Token: 0x0400ADBC RID: 44476
		private const byte tagNsId = 23;

		// Token: 0x0400ADBD RID: 44477
		internal const int ElementTypeIdConst = 11979;

		// Token: 0x0400ADBE RID: 44478
		private static string[] attributeTagNames = new string[] { "lang", "vendorID", "dllVersion", "nlCheck", "checkStyle", "appName" };

		// Token: 0x0400ADBF RID: 44479
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
