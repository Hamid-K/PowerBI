using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FCD RID: 12237
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Background))]
	internal class DocumentBackground : OpenXmlCompositeElement
	{
		// Token: 0x1700942F RID: 37935
		// (get) Token: 0x0601A8DB RID: 108763 RVA: 0x002C30A6 File Offset: 0x002C12A6
		public override string LocalName
		{
			get
			{
				return "background";
			}
		}

		// Token: 0x17009430 RID: 37936
		// (get) Token: 0x0601A8DC RID: 108764 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009431 RID: 37937
		// (get) Token: 0x0601A8DD RID: 108765 RVA: 0x0036401A File Offset: 0x0036221A
		internal override int ElementTypeId
		{
			get
			{
				return 11945;
			}
		}

		// Token: 0x0601A8DE RID: 108766 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009432 RID: 37938
		// (get) Token: 0x0601A8DF RID: 108767 RVA: 0x00364021 File Offset: 0x00362221
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocumentBackground.attributeTagNames;
			}
		}

		// Token: 0x17009433 RID: 37939
		// (get) Token: 0x0601A8E0 RID: 108768 RVA: 0x00364028 File Offset: 0x00362228
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocumentBackground.attributeNamespaceIds;
			}
		}

		// Token: 0x17009434 RID: 37940
		// (get) Token: 0x0601A8E1 RID: 108769 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A8E2 RID: 108770 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "color")]
		public StringValue Color
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

		// Token: 0x17009435 RID: 37941
		// (get) Token: 0x0601A8E3 RID: 108771 RVA: 0x0034891F File Offset: 0x00346B1F
		// (set) Token: 0x0601A8E4 RID: 108772 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "themeColor")]
		public EnumValue<ThemeColorValues> ThemeColor
		{
			get
			{
				return (EnumValue<ThemeColorValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009436 RID: 37942
		// (get) Token: 0x0601A8E5 RID: 108773 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601A8E6 RID: 108774 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "themeTint")]
		public StringValue ThemeTint
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

		// Token: 0x17009437 RID: 37943
		// (get) Token: 0x0601A8E7 RID: 108775 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601A8E8 RID: 108776 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "themeShade")]
		public StringValue ThemeShade
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

		// Token: 0x0601A8E9 RID: 108777 RVA: 0x00293ECF File Offset: 0x002920CF
		public DocumentBackground()
		{
		}

		// Token: 0x0601A8EA RID: 108778 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DocumentBackground(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A8EB RID: 108779 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DocumentBackground(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A8EC RID: 108780 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DocumentBackground(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A8ED RID: 108781 RVA: 0x0036402F File Offset: 0x0036222F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "background" == name)
			{
				return new Background();
			}
			return null;
		}

		// Token: 0x17009438 RID: 37944
		// (get) Token: 0x0601A8EE RID: 108782 RVA: 0x0036404A File Offset: 0x0036224A
		internal override string[] ElementTagNames
		{
			get
			{
				return DocumentBackground.eleTagNames;
			}
		}

		// Token: 0x17009439 RID: 37945
		// (get) Token: 0x0601A8EF RID: 108783 RVA: 0x00364051 File Offset: 0x00362251
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DocumentBackground.eleNamespaceIds;
			}
		}

		// Token: 0x1700943A RID: 37946
		// (get) Token: 0x0601A8F0 RID: 108784 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700943B RID: 37947
		// (get) Token: 0x0601A8F1 RID: 108785 RVA: 0x00364058 File Offset: 0x00362258
		// (set) Token: 0x0601A8F2 RID: 108786 RVA: 0x00364061 File Offset: 0x00362261
		public Background Background
		{
			get
			{
				return base.GetElement<Background>(0);
			}
			set
			{
				base.SetElement<Background>(0, value);
			}
		}

		// Token: 0x0601A8F3 RID: 108787 RVA: 0x0036406C File Offset: 0x0036226C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "color" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "themeColor" == name)
			{
				return new EnumValue<ThemeColorValues>();
			}
			if (23 == namespaceId && "themeTint" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "themeShade" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A8F4 RID: 108788 RVA: 0x003640E1 File Offset: 0x003622E1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocumentBackground>(deep);
		}

		// Token: 0x0400AD7B RID: 44411
		private const string tagName = "background";

		// Token: 0x0400AD7C RID: 44412
		private const byte tagNsId = 23;

		// Token: 0x0400AD7D RID: 44413
		internal const int ElementTypeIdConst = 11945;

		// Token: 0x0400AD7E RID: 44414
		private static string[] attributeTagNames = new string[] { "color", "themeColor", "themeTint", "themeShade" };

		// Token: 0x0400AD7F RID: 44415
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };

		// Token: 0x0400AD80 RID: 44416
		private static readonly string[] eleTagNames = new string[] { "background" };

		// Token: 0x0400AD81 RID: 44417
		private static readonly byte[] eleNamespaceIds = new byte[] { 26 };
	}
}
