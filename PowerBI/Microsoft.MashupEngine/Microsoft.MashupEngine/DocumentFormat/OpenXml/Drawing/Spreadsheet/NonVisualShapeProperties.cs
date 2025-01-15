using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002880 RID: 10368
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualShapeDrawingProperties))]
	internal class NonVisualShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006732 RID: 26418
		// (get) Token: 0x0601452E RID: 83246 RVA: 0x002DEC0B File Offset: 0x002DCE0B
		public override string LocalName
		{
			get
			{
				return "nvSpPr";
			}
		}

		// Token: 0x17006733 RID: 26419
		// (get) Token: 0x0601452F RID: 83247 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006734 RID: 26420
		// (get) Token: 0x06014530 RID: 83248 RVA: 0x00312077 File Offset: 0x00310277
		internal override int ElementTypeId
		{
			get
			{
				return 10730;
			}
		}

		// Token: 0x06014531 RID: 83249 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014532 RID: 83250 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualShapeProperties()
		{
		}

		// Token: 0x06014533 RID: 83251 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014534 RID: 83252 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014535 RID: 83253 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014536 RID: 83254 RVA: 0x0031207E File Offset: 0x0031027E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (18 == namespaceId && "cNvSpPr" == name)
			{
				return new NonVisualShapeDrawingProperties();
			}
			return null;
		}

		// Token: 0x17006735 RID: 26421
		// (get) Token: 0x06014537 RID: 83255 RVA: 0x003120B1 File Offset: 0x003102B1
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17006736 RID: 26422
		// (get) Token: 0x06014538 RID: 83256 RVA: 0x003120B8 File Offset: 0x003102B8
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006737 RID: 26423
		// (get) Token: 0x06014539 RID: 83257 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006738 RID: 26424
		// (get) Token: 0x0601453A RID: 83258 RVA: 0x003120BF File Offset: 0x003102BF
		// (set) Token: 0x0601453B RID: 83259 RVA: 0x003120C8 File Offset: 0x003102C8
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

		// Token: 0x17006739 RID: 26425
		// (get) Token: 0x0601453C RID: 83260 RVA: 0x003120D2 File Offset: 0x003102D2
		// (set) Token: 0x0601453D RID: 83261 RVA: 0x003120DB File Offset: 0x003102DB
		public NonVisualShapeDrawingProperties NonVisualShapeDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualShapeDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualShapeDrawingProperties>(1, value);
			}
		}

		// Token: 0x0601453E RID: 83262 RVA: 0x003120E5 File Offset: 0x003102E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualShapeProperties>(deep);
		}

		// Token: 0x04008D93 RID: 36243
		private const string tagName = "nvSpPr";

		// Token: 0x04008D94 RID: 36244
		private const byte tagNsId = 18;

		// Token: 0x04008D95 RID: 36245
		internal const int ElementTypeIdConst = 10730;

		// Token: 0x04008D96 RID: 36246
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvSpPr" };

		// Token: 0x04008D97 RID: 36247
		private static readonly byte[] eleNamespaceIds = new byte[] { 18, 18 };
	}
}
