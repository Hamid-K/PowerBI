using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002876 RID: 10358
	[ChildElementInfo(typeof(FromMarker))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ToMarker))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(GroupShape))]
	[ChildElementInfo(typeof(GraphicFrame))]
	[ChildElementInfo(typeof(ConnectionShape))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ClientData))]
	internal class TwoCellAnchor : OpenXmlCompositeElement
	{
		// Token: 0x170066C4 RID: 26308
		// (get) Token: 0x0601443F RID: 83007 RVA: 0x0031128E File Offset: 0x0030F48E
		public override string LocalName
		{
			get
			{
				return "twoCellAnchor";
			}
		}

		// Token: 0x170066C5 RID: 26309
		// (get) Token: 0x06014440 RID: 83008 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170066C6 RID: 26310
		// (get) Token: 0x06014441 RID: 83009 RVA: 0x00311295 File Offset: 0x0030F495
		internal override int ElementTypeId
		{
			get
			{
				return 10720;
			}
		}

		// Token: 0x06014442 RID: 83010 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170066C7 RID: 26311
		// (get) Token: 0x06014443 RID: 83011 RVA: 0x0031129C File Offset: 0x0030F49C
		internal override string[] AttributeTagNames
		{
			get
			{
				return TwoCellAnchor.attributeTagNames;
			}
		}

		// Token: 0x170066C8 RID: 26312
		// (get) Token: 0x06014444 RID: 83012 RVA: 0x003112A3 File Offset: 0x0030F4A3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TwoCellAnchor.attributeNamespaceIds;
			}
		}

		// Token: 0x170066C9 RID: 26313
		// (get) Token: 0x06014445 RID: 83013 RVA: 0x003112AA File Offset: 0x0030F4AA
		// (set) Token: 0x06014446 RID: 83014 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "editAs")]
		public EnumValue<EditAsValues> EditAs
		{
			get
			{
				return (EnumValue<EditAsValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06014447 RID: 83015 RVA: 0x00293ECF File Offset: 0x002920CF
		public TwoCellAnchor()
		{
		}

		// Token: 0x06014448 RID: 83016 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TwoCellAnchor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014449 RID: 83017 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TwoCellAnchor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601444A RID: 83018 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TwoCellAnchor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601444B RID: 83019 RVA: 0x003112BC File Offset: 0x0030F4BC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "from" == name)
			{
				return new FromMarker();
			}
			if (18 == namespaceId && "to" == name)
			{
				return new ToMarker();
			}
			if (18 == namespaceId && "sp" == name)
			{
				return new Shape();
			}
			if (18 == namespaceId && "grpSp" == name)
			{
				return new GroupShape();
			}
			if (18 == namespaceId && "graphicFrame" == name)
			{
				return new GraphicFrame();
			}
			if (18 == namespaceId && "cxnSp" == name)
			{
				return new ConnectionShape();
			}
			if (18 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			if (18 == namespaceId && "contentPart" == name)
			{
				return new ContentPart();
			}
			if (18 == namespaceId && "clientData" == name)
			{
				return new ClientData();
			}
			return null;
		}

		// Token: 0x170066CA RID: 26314
		// (get) Token: 0x0601444C RID: 83020 RVA: 0x003113A2 File Offset: 0x0030F5A2
		internal override string[] ElementTagNames
		{
			get
			{
				return TwoCellAnchor.eleTagNames;
			}
		}

		// Token: 0x170066CB RID: 26315
		// (get) Token: 0x0601444D RID: 83021 RVA: 0x003113A9 File Offset: 0x0030F5A9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TwoCellAnchor.eleNamespaceIds;
			}
		}

		// Token: 0x170066CC RID: 26316
		// (get) Token: 0x0601444E RID: 83022 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170066CD RID: 26317
		// (get) Token: 0x0601444F RID: 83023 RVA: 0x003113B0 File Offset: 0x0030F5B0
		// (set) Token: 0x06014450 RID: 83024 RVA: 0x003113B9 File Offset: 0x0030F5B9
		public FromMarker FromMarker
		{
			get
			{
				return base.GetElement<FromMarker>(0);
			}
			set
			{
				base.SetElement<FromMarker>(0, value);
			}
		}

		// Token: 0x170066CE RID: 26318
		// (get) Token: 0x06014451 RID: 83025 RVA: 0x003113C3 File Offset: 0x0030F5C3
		// (set) Token: 0x06014452 RID: 83026 RVA: 0x003113CC File Offset: 0x0030F5CC
		public ToMarker ToMarker
		{
			get
			{
				return base.GetElement<ToMarker>(1);
			}
			set
			{
				base.SetElement<ToMarker>(1, value);
			}
		}

		// Token: 0x06014453 RID: 83027 RVA: 0x003113D6 File Offset: 0x0030F5D6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "editAs" == name)
			{
				return new EnumValue<EditAsValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014454 RID: 83028 RVA: 0x003113F6 File Offset: 0x0030F5F6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TwoCellAnchor>(deep);
		}

		// Token: 0x06014455 RID: 83029 RVA: 0x00311400 File Offset: 0x0030F600
		// Note: this type is marked as 'beforefieldinit'.
		static TwoCellAnchor()
		{
			byte[] array = new byte[1];
			TwoCellAnchor.attributeNamespaceIds = array;
			TwoCellAnchor.eleTagNames = new string[] { "from", "to", "sp", "grpSp", "graphicFrame", "cxnSp", "pic", "contentPart", "clientData" };
			TwoCellAnchor.eleNamespaceIds = new byte[] { 18, 18, 18, 18, 18, 18, 18, 18, 18 };
		}

		// Token: 0x04008D57 RID: 36183
		private const string tagName = "twoCellAnchor";

		// Token: 0x04008D58 RID: 36184
		private const byte tagNsId = 18;

		// Token: 0x04008D59 RID: 36185
		internal const int ElementTypeIdConst = 10720;

		// Token: 0x04008D5A RID: 36186
		private static string[] attributeTagNames = new string[] { "editAs" };

		// Token: 0x04008D5B RID: 36187
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D5C RID: 36188
		private static readonly string[] eleTagNames;

		// Token: 0x04008D5D RID: 36189
		private static readonly byte[] eleNamespaceIds;
	}
}
