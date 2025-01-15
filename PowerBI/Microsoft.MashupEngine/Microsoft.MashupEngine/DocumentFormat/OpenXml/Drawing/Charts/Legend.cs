using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025CB RID: 9675
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(Layout))]
	[ChildElementInfo(typeof(Overlay))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(LegendPosition))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(LegendEntry))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Legend : OpenXmlCompositeElement
	{
		// Token: 0x170057BE RID: 22462
		// (get) Token: 0x06012247 RID: 74311 RVA: 0x002F629D File Offset: 0x002F449D
		public override string LocalName
		{
			get
			{
				return "legend";
			}
		}

		// Token: 0x170057BF RID: 22463
		// (get) Token: 0x06012248 RID: 74312 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170057C0 RID: 22464
		// (get) Token: 0x06012249 RID: 74313 RVA: 0x002F62A4 File Offset: 0x002F44A4
		internal override int ElementTypeId
		{
			get
			{
				return 10501;
			}
		}

		// Token: 0x0601224A RID: 74314 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601224B RID: 74315 RVA: 0x00293ECF File Offset: 0x002920CF
		public Legend()
		{
		}

		// Token: 0x0601224C RID: 74316 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Legend(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601224D RID: 74317 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Legend(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601224E RID: 74318 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Legend(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601224F RID: 74319 RVA: 0x002F62AC File Offset: 0x002F44AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "legendPos" == name)
			{
				return new LegendPosition();
			}
			if (11 == namespaceId && "legendEntry" == name)
			{
				return new LegendEntry();
			}
			if (11 == namespaceId && "layout" == name)
			{
				return new Layout();
			}
			if (11 == namespaceId && "overlay" == name)
			{
				return new Overlay();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "txPr" == name)
			{
				return new TextProperties();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170057C1 RID: 22465
		// (get) Token: 0x06012250 RID: 74320 RVA: 0x002F6362 File Offset: 0x002F4562
		internal override string[] ElementTagNames
		{
			get
			{
				return Legend.eleTagNames;
			}
		}

		// Token: 0x170057C2 RID: 22466
		// (get) Token: 0x06012251 RID: 74321 RVA: 0x002F6369 File Offset: 0x002F4569
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Legend.eleNamespaceIds;
			}
		}

		// Token: 0x170057C3 RID: 22467
		// (get) Token: 0x06012252 RID: 74322 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170057C4 RID: 22468
		// (get) Token: 0x06012253 RID: 74323 RVA: 0x002F6370 File Offset: 0x002F4570
		// (set) Token: 0x06012254 RID: 74324 RVA: 0x002F6379 File Offset: 0x002F4579
		public LegendPosition LegendPosition
		{
			get
			{
				return base.GetElement<LegendPosition>(0);
			}
			set
			{
				base.SetElement<LegendPosition>(0, value);
			}
		}

		// Token: 0x06012255 RID: 74325 RVA: 0x002F6383 File Offset: 0x002F4583
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Legend>(deep);
		}

		// Token: 0x04007E69 RID: 32361
		private const string tagName = "legend";

		// Token: 0x04007E6A RID: 32362
		private const byte tagNsId = 11;

		// Token: 0x04007E6B RID: 32363
		internal const int ElementTypeIdConst = 10501;

		// Token: 0x04007E6C RID: 32364
		private static readonly string[] eleTagNames = new string[] { "legendPos", "legendEntry", "layout", "overlay", "spPr", "txPr", "extLst" };

		// Token: 0x04007E6D RID: 32365
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11 };
	}
}
