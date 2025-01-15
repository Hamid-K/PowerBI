using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002885 RID: 10373
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualPictureDrawingProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualPictureProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700675E RID: 26462
		// (get) Token: 0x0601458F RID: 83343 RVA: 0x002FC4B3 File Offset: 0x002FA6B3
		public override string LocalName
		{
			get
			{
				return "nvPicPr";
			}
		}

		// Token: 0x1700675F RID: 26463
		// (get) Token: 0x06014590 RID: 83344 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006760 RID: 26464
		// (get) Token: 0x06014591 RID: 83345 RVA: 0x003125F1 File Offset: 0x003107F1
		internal override int ElementTypeId
		{
			get
			{
				return 10735;
			}
		}

		// Token: 0x06014592 RID: 83346 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014593 RID: 83347 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPictureProperties()
		{
		}

		// Token: 0x06014594 RID: 83348 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPictureProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014595 RID: 83349 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPictureProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014596 RID: 83350 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPictureProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014597 RID: 83351 RVA: 0x003125F8 File Offset: 0x003107F8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (18 == namespaceId && "cNvPicPr" == name)
			{
				return new NonVisualPictureDrawingProperties();
			}
			return null;
		}

		// Token: 0x17006761 RID: 26465
		// (get) Token: 0x06014598 RID: 83352 RVA: 0x0031262B File Offset: 0x0031082B
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualPictureProperties.eleTagNames;
			}
		}

		// Token: 0x17006762 RID: 26466
		// (get) Token: 0x06014599 RID: 83353 RVA: 0x00312632 File Offset: 0x00310832
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualPictureProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006763 RID: 26467
		// (get) Token: 0x0601459A RID: 83354 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006764 RID: 26468
		// (get) Token: 0x0601459B RID: 83355 RVA: 0x003120BF File Offset: 0x003102BF
		// (set) Token: 0x0601459C RID: 83356 RVA: 0x003120C8 File Offset: 0x003102C8
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

		// Token: 0x17006765 RID: 26469
		// (get) Token: 0x0601459D RID: 83357 RVA: 0x00312639 File Offset: 0x00310839
		// (set) Token: 0x0601459E RID: 83358 RVA: 0x00312642 File Offset: 0x00310842
		public NonVisualPictureDrawingProperties NonVisualPictureDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualPictureDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualPictureDrawingProperties>(1, value);
			}
		}

		// Token: 0x0601459F RID: 83359 RVA: 0x0031264C File Offset: 0x0031084C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPictureProperties>(deep);
		}

		// Token: 0x04008DAE RID: 36270
		private const string tagName = "nvPicPr";

		// Token: 0x04008DAF RID: 36271
		private const byte tagNsId = 18;

		// Token: 0x04008DB0 RID: 36272
		internal const int ElementTypeIdConst = 10735;

		// Token: 0x04008DB1 RID: 36273
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvPicPr" };

		// Token: 0x04008DB2 RID: 36274
		private static readonly byte[] eleNamespaceIds = new byte[] { 18, 18 };
	}
}
