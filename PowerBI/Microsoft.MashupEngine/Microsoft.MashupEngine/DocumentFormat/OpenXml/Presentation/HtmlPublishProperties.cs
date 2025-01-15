using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AA7 RID: 10919
	[ChildElementInfo(typeof(SlideAll))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(CustomShowReference))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2007)]
	[ChildElementInfo(typeof(SlideRange))]
	internal class HtmlPublishProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007471 RID: 29809
		// (get) Token: 0x06016336 RID: 90934 RVA: 0x003279F0 File Offset: 0x00325BF0
		public override string LocalName
		{
			get
			{
				return "htmlPubPr";
			}
		}

		// Token: 0x17007472 RID: 29810
		// (get) Token: 0x06016337 RID: 90935 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007473 RID: 29811
		// (get) Token: 0x06016338 RID: 90936 RVA: 0x003279F7 File Offset: 0x00325BF7
		internal override int ElementTypeId
		{
			get
			{
				return 12333;
			}
		}

		// Token: 0x06016339 RID: 90937 RVA: 0x003279FE File Offset: 0x00325BFE
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2007 & version) > FileFormatVersions.None;
		}

		// Token: 0x17007474 RID: 29812
		// (get) Token: 0x0601633A RID: 90938 RVA: 0x00327A09 File Offset: 0x00325C09
		internal override string[] AttributeTagNames
		{
			get
			{
				return HtmlPublishProperties.attributeTagNames;
			}
		}

		// Token: 0x17007475 RID: 29813
		// (get) Token: 0x0601633B RID: 90939 RVA: 0x00327A10 File Offset: 0x00325C10
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HtmlPublishProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007476 RID: 29814
		// (get) Token: 0x0601633C RID: 90940 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601633D RID: 90941 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "showSpeakerNotes")]
		public BooleanValue ShowSpeakerNotes
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

		// Token: 0x17007477 RID: 29815
		// (get) Token: 0x0601633E RID: 90942 RVA: 0x00327A17 File Offset: 0x00325C17
		// (set) Token: 0x0601633F RID: 90943 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pubBrowser")]
		public EnumValue<HtmlPublishWebBrowserSupportValues> TargetBrowser
		{
			get
			{
				return (EnumValue<HtmlPublishWebBrowserSupportValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007478 RID: 29816
		// (get) Token: 0x06016340 RID: 90944 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016341 RID: 90945 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x06016342 RID: 90946 RVA: 0x00293ECF File Offset: 0x002920CF
		public HtmlPublishProperties()
		{
		}

		// Token: 0x06016343 RID: 90947 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HtmlPublishProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016344 RID: 90948 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HtmlPublishProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016345 RID: 90949 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HtmlPublishProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016346 RID: 90950 RVA: 0x00327A28 File Offset: 0x00325C28
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "sldAll" == name)
			{
				return new SlideAll();
			}
			if (24 == namespaceId && "sldRg" == name)
			{
				return new SlideRange();
			}
			if (24 == namespaceId && "custShow" == name)
			{
				return new CustomShowReference();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06016347 RID: 90951 RVA: 0x00327A98 File Offset: 0x00325C98
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "showSpeakerNotes" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pubBrowser" == name)
			{
				return new EnumValue<HtmlPublishWebBrowserSupportValues>();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016348 RID: 90952 RVA: 0x00327AF1 File Offset: 0x00325CF1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HtmlPublishProperties>(deep);
		}

		// Token: 0x040096A9 RID: 38569
		private const string tagName = "htmlPubPr";

		// Token: 0x040096AA RID: 38570
		private const byte tagNsId = 24;

		// Token: 0x040096AB RID: 38571
		internal const int ElementTypeIdConst = 12333;

		// Token: 0x040096AC RID: 38572
		private static string[] attributeTagNames = new string[] { "showSpeakerNotes", "pubBrowser", "id" };

		// Token: 0x040096AD RID: 38573
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 19 };
	}
}
