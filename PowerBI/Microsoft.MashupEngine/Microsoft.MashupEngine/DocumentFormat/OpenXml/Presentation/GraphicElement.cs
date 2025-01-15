using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ACB RID: 10955
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Diagram))]
	[ChildElementInfo(typeof(Chart))]
	internal class GraphicElement : OpenXmlCompositeElement
	{
		// Token: 0x17007552 RID: 30034
		// (get) Token: 0x0601653C RID: 91452 RVA: 0x0032908D File Offset: 0x0032728D
		public override string LocalName
		{
			get
			{
				return "graphicEl";
			}
		}

		// Token: 0x17007553 RID: 30035
		// (get) Token: 0x0601653D RID: 91453 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007554 RID: 30036
		// (get) Token: 0x0601653E RID: 91454 RVA: 0x00329094 File Offset: 0x00327294
		internal override int ElementTypeId
		{
			get
			{
				return 12374;
			}
		}

		// Token: 0x0601653F RID: 91455 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016540 RID: 91456 RVA: 0x00293ECF File Offset: 0x002920CF
		public GraphicElement()
		{
		}

		// Token: 0x06016541 RID: 91457 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GraphicElement(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016542 RID: 91458 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GraphicElement(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016543 RID: 91459 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GraphicElement(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016544 RID: 91460 RVA: 0x0032909B File Offset: 0x0032729B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "dgm" == name)
			{
				return new Diagram();
			}
			if (10 == namespaceId && "chart" == name)
			{
				return new Chart();
			}
			return null;
		}

		// Token: 0x17007555 RID: 30037
		// (get) Token: 0x06016545 RID: 91461 RVA: 0x003290CE File Offset: 0x003272CE
		internal override string[] ElementTagNames
		{
			get
			{
				return GraphicElement.eleTagNames;
			}
		}

		// Token: 0x17007556 RID: 30038
		// (get) Token: 0x06016546 RID: 91462 RVA: 0x003290D5 File Offset: 0x003272D5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GraphicElement.eleNamespaceIds;
			}
		}

		// Token: 0x17007557 RID: 30039
		// (get) Token: 0x06016547 RID: 91463 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007558 RID: 30040
		// (get) Token: 0x06016548 RID: 91464 RVA: 0x003290DC File Offset: 0x003272DC
		// (set) Token: 0x06016549 RID: 91465 RVA: 0x003290E5 File Offset: 0x003272E5
		public Diagram Diagram
		{
			get
			{
				return base.GetElement<Diagram>(0);
			}
			set
			{
				base.SetElement<Diagram>(0, value);
			}
		}

		// Token: 0x17007559 RID: 30041
		// (get) Token: 0x0601654A RID: 91466 RVA: 0x003290EF File Offset: 0x003272EF
		// (set) Token: 0x0601654B RID: 91467 RVA: 0x003290F8 File Offset: 0x003272F8
		public Chart Chart
		{
			get
			{
				return base.GetElement<Chart>(1);
			}
			set
			{
				base.SetElement<Chart>(1, value);
			}
		}

		// Token: 0x0601654C RID: 91468 RVA: 0x00329102 File Offset: 0x00327302
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GraphicElement>(deep);
		}

		// Token: 0x0400973A RID: 38714
		private const string tagName = "graphicEl";

		// Token: 0x0400973B RID: 38715
		private const byte tagNsId = 24;

		// Token: 0x0400973C RID: 38716
		internal const int ElementTypeIdConst = 12374;

		// Token: 0x0400973D RID: 38717
		private static readonly string[] eleTagNames = new string[] { "dgm", "chart" };

		// Token: 0x0400973E RID: 38718
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
