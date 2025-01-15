using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023BB RID: 9147
	[ChildElementInfo(typeof(CustomerDataList))]
	[ChildElementInfo(typeof(AudioFromFile))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(AudioFromCD))]
	[ChildElementInfo(typeof(WaveAudioFile))]
	[ChildElementInfo(typeof(VideoFromFile))]
	[ChildElementInfo(typeof(QuickTimeFromFile))]
	[ChildElementInfo(typeof(PlaceholderShape))]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingPropertiesExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ApplicationNonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17004CA7 RID: 19623
		// (get) Token: 0x06010961 RID: 67937 RVA: 0x002DFF99 File Offset: 0x002DE199
		public override string LocalName
		{
			get
			{
				return "nvPr";
			}
		}

		// Token: 0x17004CA8 RID: 19624
		// (get) Token: 0x06010962 RID: 67938 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CA9 RID: 19625
		// (get) Token: 0x06010963 RID: 67939 RVA: 0x002E5023 File Offset: 0x002E3223
		internal override int ElementTypeId
		{
			get
			{
				return 12801;
			}
		}

		// Token: 0x06010964 RID: 67940 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004CAA RID: 19626
		// (get) Token: 0x06010965 RID: 67941 RVA: 0x002E502A File Offset: 0x002E322A
		internal override string[] AttributeTagNames
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17004CAB RID: 19627
		// (get) Token: 0x06010966 RID: 67942 RVA: 0x002E5031 File Offset: 0x002E3231
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17004CAC RID: 19628
		// (get) Token: 0x06010967 RID: 67943 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010968 RID: 67944 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "isPhoto")]
		public BooleanValue IsPhoto
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

		// Token: 0x17004CAD RID: 19629
		// (get) Token: 0x06010969 RID: 67945 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601096A RID: 67946 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "userDrawn")]
		public BooleanValue UserDrawn
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

		// Token: 0x0601096B RID: 67947 RVA: 0x00293ECF File Offset: 0x002920CF
		public ApplicationNonVisualDrawingProperties()
		{
		}

		// Token: 0x0601096C RID: 67948 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ApplicationNonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601096D RID: 67949 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ApplicationNonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601096E RID: 67950 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ApplicationNonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601096F RID: 67951 RVA: 0x002E5038 File Offset: 0x002E3238
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ph" == name)
			{
				return new PlaceholderShape();
			}
			if (10 == namespaceId && "audioCd" == name)
			{
				return new AudioFromCD();
			}
			if (10 == namespaceId && "wavAudioFile" == name)
			{
				return new WaveAudioFile();
			}
			if (10 == namespaceId && "audioFile" == name)
			{
				return new AudioFromFile();
			}
			if (10 == namespaceId && "videoFile" == name)
			{
				return new VideoFromFile();
			}
			if (10 == namespaceId && "quickTimeFile" == name)
			{
				return new QuickTimeFromFile();
			}
			if (24 == namespaceId && "custDataLst" == name)
			{
				return new CustomerDataList();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ApplicationNonVisualDrawingPropertiesExtensionList();
			}
			return null;
		}

		// Token: 0x17004CAE RID: 19630
		// (get) Token: 0x06010970 RID: 67952 RVA: 0x002E5106 File Offset: 0x002E3306
		internal override string[] ElementTagNames
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17004CAF RID: 19631
		// (get) Token: 0x06010971 RID: 67953 RVA: 0x002E510D File Offset: 0x002E330D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004CB0 RID: 19632
		// (get) Token: 0x06010972 RID: 67954 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004CB1 RID: 19633
		// (get) Token: 0x06010973 RID: 67955 RVA: 0x002E5114 File Offset: 0x002E3314
		// (set) Token: 0x06010974 RID: 67956 RVA: 0x002E511D File Offset: 0x002E331D
		public PlaceholderShape PlaceholderShape
		{
			get
			{
				return base.GetElement<PlaceholderShape>(0);
			}
			set
			{
				base.SetElement<PlaceholderShape>(0, value);
			}
		}

		// Token: 0x06010975 RID: 67957 RVA: 0x002E5127 File Offset: 0x002E3327
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "isPhoto" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "userDrawn" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010976 RID: 67958 RVA: 0x002E515D File Offset: 0x002E335D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplicationNonVisualDrawingProperties>(deep);
		}

		// Token: 0x06010977 RID: 67959 RVA: 0x002E5168 File Offset: 0x002E3368
		// Note: this type is marked as 'beforefieldinit'.
		static ApplicationNonVisualDrawingProperties()
		{
			byte[] array = new byte[2];
			ApplicationNonVisualDrawingProperties.attributeNamespaceIds = array;
			ApplicationNonVisualDrawingProperties.eleTagNames = new string[] { "ph", "audioCd", "wavAudioFile", "audioFile", "videoFile", "quickTimeFile", "custDataLst", "extLst" };
			ApplicationNonVisualDrawingProperties.eleNamespaceIds = new byte[] { 24, 10, 10, 10, 10, 10, 24, 24 };
		}

		// Token: 0x0400755E RID: 30046
		private const string tagName = "nvPr";

		// Token: 0x0400755F RID: 30047
		private const byte tagNsId = 49;

		// Token: 0x04007560 RID: 30048
		internal const int ElementTypeIdConst = 12801;

		// Token: 0x04007561 RID: 30049
		private static string[] attributeTagNames = new string[] { "isPhoto", "userDrawn" };

		// Token: 0x04007562 RID: 30050
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007563 RID: 30051
		private static readonly string[] eleTagNames;

		// Token: 0x04007564 RID: 30052
		private static readonly byte[] eleNamespaceIds;
	}
}
