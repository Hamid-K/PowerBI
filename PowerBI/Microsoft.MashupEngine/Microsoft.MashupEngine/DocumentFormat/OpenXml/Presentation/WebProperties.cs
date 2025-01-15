using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AA8 RID: 10920
	[OfficeAvailability(FileFormatVersions.Office2007)]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class WebProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007479 RID: 29817
		// (get) Token: 0x0601634A RID: 90954 RVA: 0x00327B40 File Offset: 0x00325D40
		public override string LocalName
		{
			get
			{
				return "webPr";
			}
		}

		// Token: 0x1700747A RID: 29818
		// (get) Token: 0x0601634B RID: 90955 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700747B RID: 29819
		// (get) Token: 0x0601634C RID: 90956 RVA: 0x00327B47 File Offset: 0x00325D47
		internal override int ElementTypeId
		{
			get
			{
				return 12334;
			}
		}

		// Token: 0x0601634D RID: 90957 RVA: 0x003279FE File Offset: 0x00325BFE
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2007 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700747C RID: 29820
		// (get) Token: 0x0601634E RID: 90958 RVA: 0x00327B4E File Offset: 0x00325D4E
		internal override string[] AttributeTagNames
		{
			get
			{
				return WebProperties.attributeTagNames;
			}
		}

		// Token: 0x1700747D RID: 29821
		// (get) Token: 0x0601634F RID: 90959 RVA: 0x00327B55 File Offset: 0x00325D55
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WebProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700747E RID: 29822
		// (get) Token: 0x06016350 RID: 90960 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06016351 RID: 90961 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "showAnimation")]
		public BooleanValue ShowAnimation
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700747F RID: 29823
		// (get) Token: 0x06016352 RID: 90962 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016353 RID: 90963 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "resizeGraphics")]
		public BooleanValue ResizeGraphics
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007480 RID: 29824
		// (get) Token: 0x06016354 RID: 90964 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016355 RID: 90965 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "allowPng")]
		public BooleanValue AllowPng
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

		// Token: 0x17007481 RID: 29825
		// (get) Token: 0x06016356 RID: 90966 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06016357 RID: 90967 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "relyOnVml")]
		public BooleanValue RelyOnVml
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

		// Token: 0x17007482 RID: 29826
		// (get) Token: 0x06016358 RID: 90968 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06016359 RID: 90969 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "organizeInFolders")]
		public BooleanValue OrganizeInFolders
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

		// Token: 0x17007483 RID: 29827
		// (get) Token: 0x0601635A RID: 90970 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0601635B RID: 90971 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "useLongFilenames")]
		public BooleanValue UseLongFilenames
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007484 RID: 29828
		// (get) Token: 0x0601635C RID: 90972 RVA: 0x00327B5C File Offset: 0x00325D5C
		// (set) Token: 0x0601635D RID: 90973 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "imgSz")]
		public EnumValue<WebScreenSizeValues> ImageSize
		{
			get
			{
				return (EnumValue<WebScreenSizeValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007485 RID: 29829
		// (get) Token: 0x0601635E RID: 90974 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0601635F RID: 90975 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "encoding")]
		public StringValue Encoding
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

		// Token: 0x17007486 RID: 29830
		// (get) Token: 0x06016360 RID: 90976 RVA: 0x00327B6B File Offset: 0x00325D6B
		// (set) Token: 0x06016361 RID: 90977 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "clr")]
		public EnumValue<WebColorValues> Color
		{
			get
			{
				return (EnumValue<WebColorValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x06016362 RID: 90978 RVA: 0x00293ECF File Offset: 0x002920CF
		public WebProperties()
		{
		}

		// Token: 0x06016363 RID: 90979 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WebProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016364 RID: 90980 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WebProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016365 RID: 90981 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WebProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016366 RID: 90982 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007487 RID: 29831
		// (get) Token: 0x06016367 RID: 90983 RVA: 0x00327B7A File Offset: 0x00325D7A
		internal override string[] ElementTagNames
		{
			get
			{
				return WebProperties.eleTagNames;
			}
		}

		// Token: 0x17007488 RID: 29832
		// (get) Token: 0x06016368 RID: 90984 RVA: 0x00327B81 File Offset: 0x00325D81
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WebProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007489 RID: 29833
		// (get) Token: 0x06016369 RID: 90985 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700748A RID: 29834
		// (get) Token: 0x0601636A RID: 90986 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x0601636B RID: 90987 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x0601636C RID: 90988 RVA: 0x00327B88 File Offset: 0x00325D88
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "showAnimation" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "resizeGraphics" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "allowPng" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "relyOnVml" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "organizeInFolders" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "useLongFilenames" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "imgSz" == name)
			{
				return new EnumValue<WebScreenSizeValues>();
			}
			if (namespaceId == 0 && "encoding" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "clr" == name)
			{
				return new EnumValue<WebColorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601636D RID: 90989 RVA: 0x00327C63 File Offset: 0x00325E63
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WebProperties>(deep);
		}

		// Token: 0x0601636E RID: 90990 RVA: 0x00327C6C File Offset: 0x00325E6C
		// Note: this type is marked as 'beforefieldinit'.
		static WebProperties()
		{
			byte[] array = new byte[9];
			WebProperties.attributeNamespaceIds = array;
			WebProperties.eleTagNames = new string[] { "extLst" };
			WebProperties.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x040096AE RID: 38574
		private const string tagName = "webPr";

		// Token: 0x040096AF RID: 38575
		private const byte tagNsId = 24;

		// Token: 0x040096B0 RID: 38576
		internal const int ElementTypeIdConst = 12334;

		// Token: 0x040096B1 RID: 38577
		private static string[] attributeTagNames = new string[] { "showAnimation", "resizeGraphics", "allowPng", "relyOnVml", "organizeInFolders", "useLongFilenames", "imgSz", "encoding", "clr" };

		// Token: 0x040096B2 RID: 38578
		private static byte[] attributeNamespaceIds;

		// Token: 0x040096B3 RID: 38579
		private static readonly string[] eleTagNames;

		// Token: 0x040096B4 RID: 38580
		private static readonly byte[] eleNamespaceIds;
	}
}
