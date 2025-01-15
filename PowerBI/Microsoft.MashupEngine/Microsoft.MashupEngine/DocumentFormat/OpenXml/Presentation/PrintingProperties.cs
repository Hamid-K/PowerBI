using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AA9 RID: 10921
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class PrintingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700748B RID: 29835
		// (get) Token: 0x0601636F RID: 90991 RVA: 0x00327D04 File Offset: 0x00325F04
		public override string LocalName
		{
			get
			{
				return "prnPr";
			}
		}

		// Token: 0x1700748C RID: 29836
		// (get) Token: 0x06016370 RID: 90992 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700748D RID: 29837
		// (get) Token: 0x06016371 RID: 90993 RVA: 0x00327D0B File Offset: 0x00325F0B
		internal override int ElementTypeId
		{
			get
			{
				return 12335;
			}
		}

		// Token: 0x06016372 RID: 90994 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700748E RID: 29838
		// (get) Token: 0x06016373 RID: 90995 RVA: 0x00327D12 File Offset: 0x00325F12
		internal override string[] AttributeTagNames
		{
			get
			{
				return PrintingProperties.attributeTagNames;
			}
		}

		// Token: 0x1700748F RID: 29839
		// (get) Token: 0x06016374 RID: 90996 RVA: 0x00327D19 File Offset: 0x00325F19
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PrintingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007490 RID: 29840
		// (get) Token: 0x06016375 RID: 90997 RVA: 0x00327D20 File Offset: 0x00325F20
		// (set) Token: 0x06016376 RID: 90998 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "prnWhat")]
		public EnumValue<PrintOutputValues> PrintWhat
		{
			get
			{
				return (EnumValue<PrintOutputValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007491 RID: 29841
		// (get) Token: 0x06016377 RID: 90999 RVA: 0x00327D2F File Offset: 0x00325F2F
		// (set) Token: 0x06016378 RID: 91000 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "clrMode")]
		public EnumValue<PrintColorModeValues> ColorMode
		{
			get
			{
				return (EnumValue<PrintColorModeValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007492 RID: 29842
		// (get) Token: 0x06016379 RID: 91001 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601637A RID: 91002 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "hiddenSlides")]
		public BooleanValue HiddenSlides
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

		// Token: 0x17007493 RID: 29843
		// (get) Token: 0x0601637B RID: 91003 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601637C RID: 91004 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "scaleToFitPaper")]
		public BooleanValue ScaleToFitPaper
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

		// Token: 0x17007494 RID: 29844
		// (get) Token: 0x0601637D RID: 91005 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601637E RID: 91006 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "frameSlides")]
		public BooleanValue FrameSlides
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

		// Token: 0x0601637F RID: 91007 RVA: 0x00293ECF File Offset: 0x002920CF
		public PrintingProperties()
		{
		}

		// Token: 0x06016380 RID: 91008 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PrintingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016381 RID: 91009 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PrintingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016382 RID: 91010 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PrintingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016383 RID: 91011 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007495 RID: 29845
		// (get) Token: 0x06016384 RID: 91012 RVA: 0x00327D3E File Offset: 0x00325F3E
		internal override string[] ElementTagNames
		{
			get
			{
				return PrintingProperties.eleTagNames;
			}
		}

		// Token: 0x17007496 RID: 29846
		// (get) Token: 0x06016385 RID: 91013 RVA: 0x00327D45 File Offset: 0x00325F45
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PrintingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007497 RID: 29847
		// (get) Token: 0x06016386 RID: 91014 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007498 RID: 29848
		// (get) Token: 0x06016387 RID: 91015 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x06016388 RID: 91016 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
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

		// Token: 0x06016389 RID: 91017 RVA: 0x00327D4C File Offset: 0x00325F4C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "prnWhat" == name)
			{
				return new EnumValue<PrintOutputValues>();
			}
			if (namespaceId == 0 && "clrMode" == name)
			{
				return new EnumValue<PrintColorModeValues>();
			}
			if (namespaceId == 0 && "hiddenSlides" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "scaleToFitPaper" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "frameSlides" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601638A RID: 91018 RVA: 0x00327DCF File Offset: 0x00325FCF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrintingProperties>(deep);
		}

		// Token: 0x0601638B RID: 91019 RVA: 0x00327DD8 File Offset: 0x00325FD8
		// Note: this type is marked as 'beforefieldinit'.
		static PrintingProperties()
		{
			byte[] array = new byte[5];
			PrintingProperties.attributeNamespaceIds = array;
			PrintingProperties.eleTagNames = new string[] { "extLst" };
			PrintingProperties.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x040096B5 RID: 38581
		private const string tagName = "prnPr";

		// Token: 0x040096B6 RID: 38582
		private const byte tagNsId = 24;

		// Token: 0x040096B7 RID: 38583
		internal const int ElementTypeIdConst = 12335;

		// Token: 0x040096B8 RID: 38584
		private static string[] attributeTagNames = new string[] { "prnWhat", "clrMode", "hiddenSlides", "scaleToFitPaper", "frameSlides" };

		// Token: 0x040096B9 RID: 38585
		private static byte[] attributeNamespaceIds;

		// Token: 0x040096BA RID: 38586
		private static readonly string[] eleTagNames;

		// Token: 0x040096BB RID: 38587
		private static readonly byte[] eleNamespaceIds;
	}
}
