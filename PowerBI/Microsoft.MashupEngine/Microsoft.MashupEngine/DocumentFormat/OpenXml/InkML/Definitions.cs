using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x020030A0 RID: 12448
	[ChildElementInfo(typeof(TraceFormat))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TraceView))]
	[ChildElementInfo(typeof(Brush))]
	[ChildElementInfo(typeof(Canvas))]
	[ChildElementInfo(typeof(CanvasTransform))]
	[ChildElementInfo(typeof(Context))]
	[ChildElementInfo(typeof(InkSource))]
	[ChildElementInfo(typeof(Mapping))]
	[ChildElementInfo(typeof(Timestamp))]
	[ChildElementInfo(typeof(Trace))]
	[ChildElementInfo(typeof(TraceGroup))]
	internal class Definitions : OpenXmlCompositeElement
	{
		// Token: 0x1700983C RID: 38972
		// (get) Token: 0x0601B1C9 RID: 111049 RVA: 0x0036BF5A File Offset: 0x0036A15A
		public override string LocalName
		{
			get
			{
				return "definitions";
			}
		}

		// Token: 0x1700983D RID: 38973
		// (get) Token: 0x0601B1CA RID: 111050 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x1700983E RID: 38974
		// (get) Token: 0x0601B1CB RID: 111051 RVA: 0x0036BF61 File Offset: 0x0036A161
		internal override int ElementTypeId
		{
			get
			{
				return 12669;
			}
		}

		// Token: 0x0601B1CC RID: 111052 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601B1CD RID: 111053 RVA: 0x00293ECF File Offset: 0x002920CF
		public Definitions()
		{
		}

		// Token: 0x0601B1CE RID: 111054 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Definitions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B1CF RID: 111055 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Definitions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B1D0 RID: 111056 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Definitions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B1D1 RID: 111057 RVA: 0x0036BF68 File Offset: 0x0036A168
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "brush" == name)
			{
				return new Brush();
			}
			if (43 == namespaceId && "canvas" == name)
			{
				return new Canvas();
			}
			if (43 == namespaceId && "canvasTransform" == name)
			{
				return new CanvasTransform();
			}
			if (43 == namespaceId && "context" == name)
			{
				return new Context();
			}
			if (43 == namespaceId && "inkSource" == name)
			{
				return new InkSource();
			}
			if (43 == namespaceId && "mapping" == name)
			{
				return new Mapping();
			}
			if (43 == namespaceId && "timestamp" == name)
			{
				return new Timestamp();
			}
			if (43 == namespaceId && "trace" == name)
			{
				return new Trace();
			}
			if (43 == namespaceId && "traceFormat" == name)
			{
				return new TraceFormat();
			}
			if (43 == namespaceId && "traceGroup" == name)
			{
				return new TraceGroup();
			}
			if (43 == namespaceId && "traceView" == name)
			{
				return new TraceView();
			}
			return null;
		}

		// Token: 0x0601B1D2 RID: 111058 RVA: 0x0036C07E File Offset: 0x0036A27E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Definitions>(deep);
		}

		// Token: 0x0400B2F3 RID: 45811
		private const string tagName = "definitions";

		// Token: 0x0400B2F4 RID: 45812
		private const byte tagNsId = 43;

		// Token: 0x0400B2F5 RID: 45813
		internal const int ElementTypeIdConst = 12669;
	}
}
