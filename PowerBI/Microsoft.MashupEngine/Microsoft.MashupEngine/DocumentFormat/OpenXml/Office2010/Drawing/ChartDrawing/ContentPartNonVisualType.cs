using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x0200233A RID: 9018
	[ChildElementInfo(typeof(NonVisualInkProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualInkContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class ContentPartNonVisualType : OpenXmlCompositeElement
	{
		// Token: 0x060101D0 RID: 66000 RVA: 0x002DFE60 File Offset: 0x002DE060
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (47 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (47 == namespaceId && "cNvInkPr" == name)
			{
				return new NonVisualInkProperties();
			}
			if (47 == namespaceId && "cNvContentPartPr" == name)
			{
				return new NonVisualInkContentPartProperties();
			}
			return null;
		}

		// Token: 0x1700492A RID: 18730
		// (get) Token: 0x060101D1 RID: 66001 RVA: 0x002DFEB6 File Offset: 0x002DE0B6
		internal override string[] ElementTagNames
		{
			get
			{
				return ContentPartNonVisualType.eleTagNames;
			}
		}

		// Token: 0x1700492B RID: 18731
		// (get) Token: 0x060101D2 RID: 66002 RVA: 0x002DFEBD File Offset: 0x002DE0BD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ContentPartNonVisualType.eleNamespaceIds;
			}
		}

		// Token: 0x1700492C RID: 18732
		// (get) Token: 0x060101D3 RID: 66003 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700492D RID: 18733
		// (get) Token: 0x060101D4 RID: 66004 RVA: 0x002DFEC4 File Offset: 0x002DE0C4
		// (set) Token: 0x060101D5 RID: 66005 RVA: 0x002DFECD File Offset: 0x002DE0CD
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

		// Token: 0x1700492E RID: 18734
		// (get) Token: 0x060101D6 RID: 66006 RVA: 0x002DFED7 File Offset: 0x002DE0D7
		// (set) Token: 0x060101D7 RID: 66007 RVA: 0x002DFEE0 File Offset: 0x002DE0E0
		public NonVisualInkProperties NonVisualInkProperties
		{
			get
			{
				return base.GetElement<NonVisualInkProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualInkProperties>(1, value);
			}
		}

		// Token: 0x1700492F RID: 18735
		// (get) Token: 0x060101D8 RID: 66008 RVA: 0x002DFEEA File Offset: 0x002DE0EA
		// (set) Token: 0x060101D9 RID: 66009 RVA: 0x002DFEF3 File Offset: 0x002DE0F3
		public NonVisualInkContentPartProperties NonVisualInkContentPartProperties
		{
			get
			{
				return base.GetElement<NonVisualInkContentPartProperties>(2);
			}
			set
			{
				base.SetElement<NonVisualInkContentPartProperties>(2, value);
			}
		}

		// Token: 0x060101DA RID: 66010 RVA: 0x00293ECF File Offset: 0x002920CF
		protected ContentPartNonVisualType()
		{
		}

		// Token: 0x060101DB RID: 66011 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected ContentPartNonVisualType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101DC RID: 66012 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected ContentPartNonVisualType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101DD RID: 66013 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected ContentPartNonVisualType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007320 RID: 29472
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvInkPr", "cNvContentPartPr" };

		// Token: 0x04007321 RID: 29473
		private static readonly byte[] eleNamespaceIds = new byte[] { 47, 47, 47 };
	}
}
