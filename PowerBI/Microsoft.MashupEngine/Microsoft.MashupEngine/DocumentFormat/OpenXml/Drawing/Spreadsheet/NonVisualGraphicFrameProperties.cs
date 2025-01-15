using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002887 RID: 10375
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualGraphicFrameDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	internal class NonVisualGraphicFrameProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006771 RID: 26481
		// (get) Token: 0x060145B8 RID: 83384 RVA: 0x002FC745 File Offset: 0x002FA945
		public override string LocalName
		{
			get
			{
				return "nvGraphicFramePr";
			}
		}

		// Token: 0x17006772 RID: 26482
		// (get) Token: 0x060145B9 RID: 83385 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006773 RID: 26483
		// (get) Token: 0x060145BA RID: 83386 RVA: 0x003127CA File Offset: 0x003109CA
		internal override int ElementTypeId
		{
			get
			{
				return 10737;
			}
		}

		// Token: 0x060145BB RID: 83387 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060145BC RID: 83388 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGraphicFrameProperties()
		{
		}

		// Token: 0x060145BD RID: 83389 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGraphicFrameProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060145BE RID: 83390 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGraphicFrameProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060145BF RID: 83391 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGraphicFrameProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060145C0 RID: 83392 RVA: 0x003127D1 File Offset: 0x003109D1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (18 == namespaceId && "cNvGraphicFramePr" == name)
			{
				return new NonVisualGraphicFrameDrawingProperties();
			}
			return null;
		}

		// Token: 0x17006774 RID: 26484
		// (get) Token: 0x060145C1 RID: 83393 RVA: 0x00312804 File Offset: 0x00310A04
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGraphicFrameProperties.eleTagNames;
			}
		}

		// Token: 0x17006775 RID: 26485
		// (get) Token: 0x060145C2 RID: 83394 RVA: 0x0031280B File Offset: 0x00310A0B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGraphicFrameProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006776 RID: 26486
		// (get) Token: 0x060145C3 RID: 83395 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006777 RID: 26487
		// (get) Token: 0x060145C4 RID: 83396 RVA: 0x003120BF File Offset: 0x003102BF
		// (set) Token: 0x060145C5 RID: 83397 RVA: 0x003120C8 File Offset: 0x003102C8
		public NonVisualDrawingProperties NonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualDrawingProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualDrawingProperties>(0, value);
			}
		}

		// Token: 0x17006778 RID: 26488
		// (get) Token: 0x060145C6 RID: 83398 RVA: 0x00312812 File Offset: 0x00310A12
		// (set) Token: 0x060145C7 RID: 83399 RVA: 0x0031281B File Offset: 0x00310A1B
		public NonVisualGraphicFrameDrawingProperties NonVisualGraphicFrameDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualGraphicFrameDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualGraphicFrameDrawingProperties>(1, value);
			}
		}

		// Token: 0x060145C8 RID: 83400 RVA: 0x00312825 File Offset: 0x00310A25
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGraphicFrameProperties>(deep);
		}

		// Token: 0x04008DBA RID: 36282
		private const string tagName = "nvGraphicFramePr";

		// Token: 0x04008DBB RID: 36283
		private const byte tagNsId = 18;

		// Token: 0x04008DBC RID: 36284
		internal const int ElementTypeIdConst = 10737;

		// Token: 0x04008DBD RID: 36285
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvGraphicFramePr" };

		// Token: 0x04008DBE RID: 36286
		private static readonly byte[] eleNamespaceIds = new byte[] { 18, 18 };
	}
}
