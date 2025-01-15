using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A5F RID: 10847
	[ChildElementInfo(typeof(QuickTimeFromFile))]
	[ChildElementInfo(typeof(VideoFromFile))]
	[ChildElementInfo(typeof(CustomerDataList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AudioFromCD))]
	[ChildElementInfo(typeof(WaveAudioFile))]
	[ChildElementInfo(typeof(AudioFromFile))]
	[ChildElementInfo(typeof(PlaceholderShape))]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingPropertiesExtensionList))]
	internal class ApplicationNonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007249 RID: 29257
		// (get) Token: 0x06015E53 RID: 89683 RVA: 0x002DFF99 File Offset: 0x002DE199
		public override string LocalName
		{
			get
			{
				return "nvPr";
			}
		}

		// Token: 0x1700724A RID: 29258
		// (get) Token: 0x06015E54 RID: 89684 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700724B RID: 29259
		// (get) Token: 0x06015E55 RID: 89685 RVA: 0x00324273 File Offset: 0x00322473
		internal override int ElementTypeId
		{
			get
			{
				return 12265;
			}
		}

		// Token: 0x06015E56 RID: 89686 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700724C RID: 29260
		// (get) Token: 0x06015E57 RID: 89687 RVA: 0x0032427A File Offset: 0x0032247A
		internal override string[] AttributeTagNames
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x1700724D RID: 29261
		// (get) Token: 0x06015E58 RID: 89688 RVA: 0x00324281 File Offset: 0x00322481
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700724E RID: 29262
		// (get) Token: 0x06015E59 RID: 89689 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015E5A RID: 89690 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700724F RID: 29263
		// (get) Token: 0x06015E5B RID: 89691 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06015E5C RID: 89692 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06015E5D RID: 89693 RVA: 0x00293ECF File Offset: 0x002920CF
		public ApplicationNonVisualDrawingProperties()
		{
		}

		// Token: 0x06015E5E RID: 89694 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ApplicationNonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E5F RID: 89695 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ApplicationNonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E60 RID: 89696 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ApplicationNonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015E61 RID: 89697 RVA: 0x00324288 File Offset: 0x00322488
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

		// Token: 0x17007250 RID: 29264
		// (get) Token: 0x06015E62 RID: 89698 RVA: 0x00324356 File Offset: 0x00322556
		internal override string[] ElementTagNames
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17007251 RID: 29265
		// (get) Token: 0x06015E63 RID: 89699 RVA: 0x0032435D File Offset: 0x0032255D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007252 RID: 29266
		// (get) Token: 0x06015E64 RID: 89700 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007253 RID: 29267
		// (get) Token: 0x06015E65 RID: 89701 RVA: 0x002E5114 File Offset: 0x002E3314
		// (set) Token: 0x06015E66 RID: 89702 RVA: 0x002E511D File Offset: 0x002E331D
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

		// Token: 0x06015E67 RID: 89703 RVA: 0x002E5127 File Offset: 0x002E3327
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

		// Token: 0x06015E68 RID: 89704 RVA: 0x00324364 File Offset: 0x00322564
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplicationNonVisualDrawingProperties>(deep);
		}

		// Token: 0x06015E69 RID: 89705 RVA: 0x00324370 File Offset: 0x00322570
		// Note: this type is marked as 'beforefieldinit'.
		static ApplicationNonVisualDrawingProperties()
		{
			byte[] array = new byte[2];
			ApplicationNonVisualDrawingProperties.attributeNamespaceIds = array;
			ApplicationNonVisualDrawingProperties.eleTagNames = new string[] { "ph", "audioCd", "wavAudioFile", "audioFile", "videoFile", "quickTimeFile", "custDataLst", "extLst" };
			ApplicationNonVisualDrawingProperties.eleNamespaceIds = new byte[] { 24, 10, 10, 10, 10, 10, 24, 24 };
		}

		// Token: 0x0400954F RID: 38223
		private const string tagName = "nvPr";

		// Token: 0x04009550 RID: 38224
		private const byte tagNsId = 24;

		// Token: 0x04009551 RID: 38225
		internal const int ElementTypeIdConst = 12265;

		// Token: 0x04009552 RID: 38226
		private static string[] attributeTagNames = new string[] { "isPhoto", "userDrawn" };

		// Token: 0x04009553 RID: 38227
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009554 RID: 38228
		private static readonly string[] eleTagNames;

		// Token: 0x04009555 RID: 38229
		private static readonly byte[] eleNamespaceIds;
	}
}
