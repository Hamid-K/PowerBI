using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002877 RID: 10359
	[ChildElementInfo(typeof(FromMarker))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extent))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(GroupShape))]
	[ChildElementInfo(typeof(GraphicFrame))]
	[ChildElementInfo(typeof(ConnectionShape))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ClientData))]
	internal class OneCellAnchor : OpenXmlCompositeElement
	{
		// Token: 0x170066CF RID: 26319
		// (get) Token: 0x06014456 RID: 83030 RVA: 0x0031149C File Offset: 0x0030F69C
		public override string LocalName
		{
			get
			{
				return "oneCellAnchor";
			}
		}

		// Token: 0x170066D0 RID: 26320
		// (get) Token: 0x06014457 RID: 83031 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170066D1 RID: 26321
		// (get) Token: 0x06014458 RID: 83032 RVA: 0x003114A3 File Offset: 0x0030F6A3
		internal override int ElementTypeId
		{
			get
			{
				return 10721;
			}
		}

		// Token: 0x06014459 RID: 83033 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601445A RID: 83034 RVA: 0x00293ECF File Offset: 0x002920CF
		public OneCellAnchor()
		{
		}

		// Token: 0x0601445B RID: 83035 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OneCellAnchor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601445C RID: 83036 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OneCellAnchor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601445D RID: 83037 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OneCellAnchor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601445E RID: 83038 RVA: 0x003114AC File Offset: 0x0030F6AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "from" == name)
			{
				return new FromMarker();
			}
			if (18 == namespaceId && "ext" == name)
			{
				return new Extent();
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

		// Token: 0x170066D2 RID: 26322
		// (get) Token: 0x0601445F RID: 83039 RVA: 0x00311592 File Offset: 0x0030F792
		internal override string[] ElementTagNames
		{
			get
			{
				return OneCellAnchor.eleTagNames;
			}
		}

		// Token: 0x170066D3 RID: 26323
		// (get) Token: 0x06014460 RID: 83040 RVA: 0x00311599 File Offset: 0x0030F799
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OneCellAnchor.eleNamespaceIds;
			}
		}

		// Token: 0x170066D4 RID: 26324
		// (get) Token: 0x06014461 RID: 83041 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170066D5 RID: 26325
		// (get) Token: 0x06014462 RID: 83042 RVA: 0x003113B0 File Offset: 0x0030F5B0
		// (set) Token: 0x06014463 RID: 83043 RVA: 0x003113B9 File Offset: 0x0030F5B9
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

		// Token: 0x170066D6 RID: 26326
		// (get) Token: 0x06014464 RID: 83044 RVA: 0x003115A0 File Offset: 0x0030F7A0
		// (set) Token: 0x06014465 RID: 83045 RVA: 0x003115A9 File Offset: 0x0030F7A9
		public Extent Extent
		{
			get
			{
				return base.GetElement<Extent>(1);
			}
			set
			{
				base.SetElement<Extent>(1, value);
			}
		}

		// Token: 0x06014466 RID: 83046 RVA: 0x003115B3 File Offset: 0x0030F7B3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OneCellAnchor>(deep);
		}

		// Token: 0x04008D5E RID: 36190
		private const string tagName = "oneCellAnchor";

		// Token: 0x04008D5F RID: 36191
		private const byte tagNsId = 18;

		// Token: 0x04008D60 RID: 36192
		internal const int ElementTypeIdConst = 10721;

		// Token: 0x04008D61 RID: 36193
		private static readonly string[] eleTagNames = new string[] { "from", "ext", "sp", "grpSp", "graphicFrame", "cxnSp", "pic", "contentPart", "clientData" };

		// Token: 0x04008D62 RID: 36194
		private static readonly byte[] eleNamespaceIds = new byte[] { 18, 18, 18, 18, 18, 18, 18, 18, 18 };
	}
}
