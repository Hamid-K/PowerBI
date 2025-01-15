using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002878 RID: 10360
	[ChildElementInfo(typeof(Extent))]
	[ChildElementInfo(typeof(Position))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GroupShape))]
	[ChildElementInfo(typeof(GraphicFrame))]
	[ChildElementInfo(typeof(ConnectionShape))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(ClientData))]
	internal class AbsoluteAnchor : OpenXmlCompositeElement
	{
		// Token: 0x170066D7 RID: 26327
		// (get) Token: 0x06014468 RID: 83048 RVA: 0x00311636 File Offset: 0x0030F836
		public override string LocalName
		{
			get
			{
				return "absoluteAnchor";
			}
		}

		// Token: 0x170066D8 RID: 26328
		// (get) Token: 0x06014469 RID: 83049 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170066D9 RID: 26329
		// (get) Token: 0x0601446A RID: 83050 RVA: 0x0031163D File Offset: 0x0030F83D
		internal override int ElementTypeId
		{
			get
			{
				return 10722;
			}
		}

		// Token: 0x0601446B RID: 83051 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601446C RID: 83052 RVA: 0x00293ECF File Offset: 0x002920CF
		public AbsoluteAnchor()
		{
		}

		// Token: 0x0601446D RID: 83053 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AbsoluteAnchor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601446E RID: 83054 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AbsoluteAnchor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601446F RID: 83055 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AbsoluteAnchor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014470 RID: 83056 RVA: 0x00311644 File Offset: 0x0030F844
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "pos" == name)
			{
				return new Position();
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

		// Token: 0x170066DA RID: 26330
		// (get) Token: 0x06014471 RID: 83057 RVA: 0x0031172A File Offset: 0x0030F92A
		internal override string[] ElementTagNames
		{
			get
			{
				return AbsoluteAnchor.eleTagNames;
			}
		}

		// Token: 0x170066DB RID: 26331
		// (get) Token: 0x06014472 RID: 83058 RVA: 0x00311731 File Offset: 0x0030F931
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AbsoluteAnchor.eleNamespaceIds;
			}
		}

		// Token: 0x170066DC RID: 26332
		// (get) Token: 0x06014473 RID: 83059 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170066DD RID: 26333
		// (get) Token: 0x06014474 RID: 83060 RVA: 0x00311738 File Offset: 0x0030F938
		// (set) Token: 0x06014475 RID: 83061 RVA: 0x00311741 File Offset: 0x0030F941
		public Position Position
		{
			get
			{
				return base.GetElement<Position>(0);
			}
			set
			{
				base.SetElement<Position>(0, value);
			}
		}

		// Token: 0x170066DE RID: 26334
		// (get) Token: 0x06014476 RID: 83062 RVA: 0x003115A0 File Offset: 0x0030F7A0
		// (set) Token: 0x06014477 RID: 83063 RVA: 0x003115A9 File Offset: 0x0030F7A9
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

		// Token: 0x06014478 RID: 83064 RVA: 0x0031174B File Offset: 0x0030F94B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AbsoluteAnchor>(deep);
		}

		// Token: 0x04008D63 RID: 36195
		private const string tagName = "absoluteAnchor";

		// Token: 0x04008D64 RID: 36196
		private const byte tagNsId = 18;

		// Token: 0x04008D65 RID: 36197
		internal const int ElementTypeIdConst = 10722;

		// Token: 0x04008D66 RID: 36198
		private static readonly string[] eleTagNames = new string[] { "pos", "ext", "sp", "grpSp", "graphicFrame", "cxnSp", "pic", "contentPart", "clientData" };

		// Token: 0x04008D67 RID: 36199
		private static readonly byte[] eleNamespaceIds = new byte[] { 18, 18, 18, 18, 18, 18, 18, 18, 18 };
	}
}
