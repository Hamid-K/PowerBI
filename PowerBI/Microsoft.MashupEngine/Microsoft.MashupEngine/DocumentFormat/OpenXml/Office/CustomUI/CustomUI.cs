using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002287 RID: 8839
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RepurposedCommands))]
	[ChildElementInfo(typeof(Ribbon))]
	internal class CustomUI : OpenXmlPartRootElement
	{
		// Token: 0x17003FF4 RID: 16372
		// (get) Token: 0x0600EE0E RID: 60942 RVA: 0x002ADC57 File Offset: 0x002ABE57
		public override string LocalName
		{
			get
			{
				return "customUI";
			}
		}

		// Token: 0x17003FF5 RID: 16373
		// (get) Token: 0x0600EE0F RID: 60943 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003FF6 RID: 16374
		// (get) Token: 0x0600EE10 RID: 60944 RVA: 0x002CEAFB File Offset: 0x002CCCFB
		internal override int ElementTypeId
		{
			get
			{
				return 12598;
			}
		}

		// Token: 0x0600EE11 RID: 60945 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003FF7 RID: 16375
		// (get) Token: 0x0600EE12 RID: 60946 RVA: 0x002CEB02 File Offset: 0x002CCD02
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomUI.attributeTagNames;
			}
		}

		// Token: 0x17003FF8 RID: 16376
		// (get) Token: 0x0600EE13 RID: 60947 RVA: 0x002CEB09 File Offset: 0x002CCD09
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomUI.attributeNamespaceIds;
			}
		}

		// Token: 0x17003FF9 RID: 16377
		// (get) Token: 0x0600EE14 RID: 60948 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EE15 RID: 60949 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "onLoad")]
		public StringValue OnLoad
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

		// Token: 0x17003FFA RID: 16378
		// (get) Token: 0x0600EE16 RID: 60950 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EE17 RID: 60951 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "loadImage")]
		public StringValue LoadImage
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

		// Token: 0x0600EE18 RID: 60952 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public CustomUI()
		{
		}

		// Token: 0x0600EE19 RID: 60953 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public CustomUI(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EE1A RID: 60954 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public CustomUI(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EE1B RID: 60955 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public CustomUI(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EE1C RID: 60956 RVA: 0x002CEB33 File Offset: 0x002CCD33
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "commands" == name)
			{
				return new RepurposedCommands();
			}
			if (34 == namespaceId && "ribbon" == name)
			{
				return new Ribbon();
			}
			return null;
		}

		// Token: 0x17003FFB RID: 16379
		// (get) Token: 0x0600EE1D RID: 60957 RVA: 0x002CEB66 File Offset: 0x002CCD66
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomUI.eleTagNames;
			}
		}

		// Token: 0x17003FFC RID: 16380
		// (get) Token: 0x0600EE1E RID: 60958 RVA: 0x002CEB6D File Offset: 0x002CCD6D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomUI.eleNamespaceIds;
			}
		}

		// Token: 0x17003FFD RID: 16381
		// (get) Token: 0x0600EE1F RID: 60959 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17003FFE RID: 16382
		// (get) Token: 0x0600EE20 RID: 60960 RVA: 0x002CEB74 File Offset: 0x002CCD74
		// (set) Token: 0x0600EE21 RID: 60961 RVA: 0x002CEB7D File Offset: 0x002CCD7D
		public RepurposedCommands RepurposedCommands
		{
			get
			{
				return base.GetElement<RepurposedCommands>(0);
			}
			set
			{
				base.SetElement<RepurposedCommands>(0, value);
			}
		}

		// Token: 0x17003FFF RID: 16383
		// (get) Token: 0x0600EE22 RID: 60962 RVA: 0x002CEB87 File Offset: 0x002CCD87
		// (set) Token: 0x0600EE23 RID: 60963 RVA: 0x002CEB90 File Offset: 0x002CCD90
		public Ribbon Ribbon
		{
			get
			{
				return base.GetElement<Ribbon>(1);
			}
			set
			{
				base.SetElement<Ribbon>(1, value);
			}
		}

		// Token: 0x0600EE24 RID: 60964 RVA: 0x002CEB9A File Offset: 0x002CCD9A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "onLoad" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "loadImage" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600EE25 RID: 60965 RVA: 0x002CEBD0 File Offset: 0x002CCDD0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomUI>(deep);
		}

		// Token: 0x0600EE26 RID: 60966 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal CustomUI(CustomUIPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0600EE27 RID: 60967 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CustomUIPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x0600EE28 RID: 60968 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CustomUIPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x17004000 RID: 16384
		// (get) Token: 0x0600EE29 RID: 60969 RVA: 0x002CEBF4 File Offset: 0x002CCDF4
		// (set) Token: 0x0600EE2A RID: 60970 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public CustomUIPart CustomUIPart
		{
			get
			{
				return base.OpenXmlPart as CustomUIPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x0600EE2B RID: 60971 RVA: 0x002CEC0C File Offset: 0x002CCE0C
		// Note: this type is marked as 'beforefieldinit'.
		static CustomUI()
		{
			byte[] array = new byte[2];
			CustomUI.attributeNamespaceIds = array;
			CustomUI.eleTagNames = new string[] { "commands", "ribbon" };
			CustomUI.eleNamespaceIds = new byte[] { 34, 34 };
		}

		// Token: 0x04007002 RID: 28674
		private const string tagName = "customUI";

		// Token: 0x04007003 RID: 28675
		private const byte tagNsId = 34;

		// Token: 0x04007004 RID: 28676
		internal const int ElementTypeIdConst = 12598;

		// Token: 0x04007005 RID: 28677
		private static string[] attributeTagNames = new string[] { "onLoad", "loadImage" };

		// Token: 0x04007006 RID: 28678
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007007 RID: 28679
		private static readonly string[] eleTagNames;

		// Token: 0x04007008 RID: 28680
		private static readonly byte[] eleNamespaceIds;
	}
}
