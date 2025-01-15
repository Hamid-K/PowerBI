using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027E0 RID: 10208
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeStyle))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(BodyProperties))]
	[ChildElementInfo(typeof(ListStyle))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal abstract class DefaultShapeDefinitionType : OpenXmlCompositeElement
	{
		// Token: 0x06013E24 RID: 81444 RVA: 0x0030CD28 File Offset: 0x0030AF28
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (10 == namespaceId && "bodyPr" == name)
			{
				return new BodyProperties();
			}
			if (10 == namespaceId && "lstStyle" == name)
			{
				return new ListStyle();
			}
			if (10 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006441 RID: 25665
		// (get) Token: 0x06013E25 RID: 81445 RVA: 0x0030CDAE File Offset: 0x0030AFAE
		internal override string[] ElementTagNames
		{
			get
			{
				return DefaultShapeDefinitionType.eleTagNames;
			}
		}

		// Token: 0x17006442 RID: 25666
		// (get) Token: 0x06013E26 RID: 81446 RVA: 0x0030CDB5 File Offset: 0x0030AFB5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DefaultShapeDefinitionType.eleNamespaceIds;
			}
		}

		// Token: 0x17006443 RID: 25667
		// (get) Token: 0x06013E27 RID: 81447 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006444 RID: 25668
		// (get) Token: 0x06013E28 RID: 81448 RVA: 0x0030CDBC File Offset: 0x0030AFBC
		// (set) Token: 0x06013E29 RID: 81449 RVA: 0x0030CDC5 File Offset: 0x0030AFC5
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(0);
			}
			set
			{
				base.SetElement<ShapeProperties>(0, value);
			}
		}

		// Token: 0x17006445 RID: 25669
		// (get) Token: 0x06013E2A RID: 81450 RVA: 0x0030CDCF File Offset: 0x0030AFCF
		// (set) Token: 0x06013E2B RID: 81451 RVA: 0x0030CDD8 File Offset: 0x0030AFD8
		public BodyProperties BodyProperties
		{
			get
			{
				return base.GetElement<BodyProperties>(1);
			}
			set
			{
				base.SetElement<BodyProperties>(1, value);
			}
		}

		// Token: 0x17006446 RID: 25670
		// (get) Token: 0x06013E2C RID: 81452 RVA: 0x0030CDE2 File Offset: 0x0030AFE2
		// (set) Token: 0x06013E2D RID: 81453 RVA: 0x0030CDEB File Offset: 0x0030AFEB
		public ListStyle ListStyle
		{
			get
			{
				return base.GetElement<ListStyle>(2);
			}
			set
			{
				base.SetElement<ListStyle>(2, value);
			}
		}

		// Token: 0x17006447 RID: 25671
		// (get) Token: 0x06013E2E RID: 81454 RVA: 0x0030CDF5 File Offset: 0x0030AFF5
		// (set) Token: 0x06013E2F RID: 81455 RVA: 0x0030CDFE File Offset: 0x0030AFFE
		public ShapeStyle ShapeStyle
		{
			get
			{
				return base.GetElement<ShapeStyle>(3);
			}
			set
			{
				base.SetElement<ShapeStyle>(3, value);
			}
		}

		// Token: 0x17006448 RID: 25672
		// (get) Token: 0x06013E30 RID: 81456 RVA: 0x002E0DD0 File Offset: 0x002DEFD0
		// (set) Token: 0x06013E31 RID: 81457 RVA: 0x002E0DD9 File Offset: 0x002DEFD9
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x06013E32 RID: 81458 RVA: 0x00293ECF File Offset: 0x002920CF
		protected DefaultShapeDefinitionType()
		{
		}

		// Token: 0x06013E33 RID: 81459 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected DefaultShapeDefinitionType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E34 RID: 81460 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected DefaultShapeDefinitionType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E35 RID: 81461 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected DefaultShapeDefinitionType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0400882A RID: 34858
		private static readonly string[] eleTagNames = new string[] { "spPr", "bodyPr", "lstStyle", "style", "extLst" };

		// Token: 0x0400882B RID: 34859
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10 };
	}
}
