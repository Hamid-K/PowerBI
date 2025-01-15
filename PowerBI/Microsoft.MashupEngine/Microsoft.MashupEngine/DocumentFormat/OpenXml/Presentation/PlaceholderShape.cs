using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A98 RID: 10904
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PlaceholderShape : OpenXmlCompositeElement
	{
		// Token: 0x17007409 RID: 29705
		// (get) Token: 0x0601623F RID: 90687 RVA: 0x00326E7A File Offset: 0x0032507A
		public override string LocalName
		{
			get
			{
				return "ph";
			}
		}

		// Token: 0x1700740A RID: 29706
		// (get) Token: 0x06016240 RID: 90688 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700740B RID: 29707
		// (get) Token: 0x06016241 RID: 90689 RVA: 0x00326E81 File Offset: 0x00325081
		internal override int ElementTypeId
		{
			get
			{
				return 12319;
			}
		}

		// Token: 0x06016242 RID: 90690 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700740C RID: 29708
		// (get) Token: 0x06016243 RID: 90691 RVA: 0x00326E88 File Offset: 0x00325088
		internal override string[] AttributeTagNames
		{
			get
			{
				return PlaceholderShape.attributeTagNames;
			}
		}

		// Token: 0x1700740D RID: 29709
		// (get) Token: 0x06016244 RID: 90692 RVA: 0x00326E8F File Offset: 0x0032508F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PlaceholderShape.attributeNamespaceIds;
			}
		}

		// Token: 0x1700740E RID: 29710
		// (get) Token: 0x06016245 RID: 90693 RVA: 0x00326E96 File Offset: 0x00325096
		// (set) Token: 0x06016246 RID: 90694 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<PlaceholderValues> Type
		{
			get
			{
				return (EnumValue<PlaceholderValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700740F RID: 29711
		// (get) Token: 0x06016247 RID: 90695 RVA: 0x00326EA5 File Offset: 0x003250A5
		// (set) Token: 0x06016248 RID: 90696 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "orient")]
		public EnumValue<DirectionValues> Orientation
		{
			get
			{
				return (EnumValue<DirectionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007410 RID: 29712
		// (get) Token: 0x06016249 RID: 90697 RVA: 0x00326EB4 File Offset: 0x003250B4
		// (set) Token: 0x0601624A RID: 90698 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "sz")]
		public EnumValue<PlaceholderSizeValues> Size
		{
			get
			{
				return (EnumValue<PlaceholderSizeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007411 RID: 29713
		// (get) Token: 0x0601624B RID: 90699 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601624C RID: 90700 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idx")]
		public UInt32Value Index
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007412 RID: 29714
		// (get) Token: 0x0601624D RID: 90701 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601624E RID: 90702 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "hasCustomPrompt")]
		public BooleanValue HasCustomPrompt
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

		// Token: 0x0601624F RID: 90703 RVA: 0x00293ECF File Offset: 0x002920CF
		public PlaceholderShape()
		{
		}

		// Token: 0x06016250 RID: 90704 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PlaceholderShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016251 RID: 90705 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PlaceholderShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016252 RID: 90706 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PlaceholderShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016253 RID: 90707 RVA: 0x0032574C File Offset: 0x0032394C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x17007413 RID: 29715
		// (get) Token: 0x06016254 RID: 90708 RVA: 0x00326EC3 File Offset: 0x003250C3
		internal override string[] ElementTagNames
		{
			get
			{
				return PlaceholderShape.eleTagNames;
			}
		}

		// Token: 0x17007414 RID: 29716
		// (get) Token: 0x06016255 RID: 90709 RVA: 0x00326ECA File Offset: 0x003250CA
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PlaceholderShape.eleNamespaceIds;
			}
		}

		// Token: 0x17007415 RID: 29717
		// (get) Token: 0x06016256 RID: 90710 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007416 RID: 29718
		// (get) Token: 0x06016257 RID: 90711 RVA: 0x00325775 File Offset: 0x00323975
		// (set) Token: 0x06016258 RID: 90712 RVA: 0x0032577E File Offset: 0x0032397E
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(0);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(0, value);
			}
		}

		// Token: 0x06016259 RID: 90713 RVA: 0x00326ED4 File Offset: 0x003250D4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<PlaceholderValues>();
			}
			if (namespaceId == 0 && "orient" == name)
			{
				return new EnumValue<DirectionValues>();
			}
			if (namespaceId == 0 && "sz" == name)
			{
				return new EnumValue<PlaceholderSizeValues>();
			}
			if (namespaceId == 0 && "idx" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "hasCustomPrompt" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601625A RID: 90714 RVA: 0x00326F57 File Offset: 0x00325157
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PlaceholderShape>(deep);
		}

		// Token: 0x0601625B RID: 90715 RVA: 0x00326F60 File Offset: 0x00325160
		// Note: this type is marked as 'beforefieldinit'.
		static PlaceholderShape()
		{
			byte[] array = new byte[5];
			PlaceholderShape.attributeNamespaceIds = array;
			PlaceholderShape.eleTagNames = new string[] { "extLst" };
			PlaceholderShape.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x04009667 RID: 38503
		private const string tagName = "ph";

		// Token: 0x04009668 RID: 38504
		private const byte tagNsId = 24;

		// Token: 0x04009669 RID: 38505
		internal const int ElementTypeIdConst = 12319;

		// Token: 0x0400966A RID: 38506
		private static string[] attributeTagNames = new string[] { "type", "orient", "sz", "idx", "hasCustomPrompt" };

		// Token: 0x0400966B RID: 38507
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400966C RID: 38508
		private static readonly string[] eleTagNames;

		// Token: 0x0400966D RID: 38509
		private static readonly byte[] eleNamespaceIds;
	}
}
