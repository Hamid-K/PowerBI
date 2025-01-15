using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002356 RID: 9046
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualInkContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualInkProperties), FileFormatVersions.Office2010)]
	internal abstract class GvmlContentPartNonVisualType : OpenXmlCompositeElement
	{
		// Token: 0x060103BF RID: 66495 RVA: 0x002E1444 File Offset: 0x002DF644
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (48 == namespaceId && "cNvInkPr" == name)
			{
				return new NonVisualInkProperties();
			}
			if (48 == namespaceId && "cNvContentPartPr" == name)
			{
				return new NonVisualInkContentPartProperties();
			}
			return null;
		}

		// Token: 0x17004A0C RID: 18956
		// (get) Token: 0x060103C0 RID: 66496 RVA: 0x002E149A File Offset: 0x002DF69A
		internal override string[] ElementTagNames
		{
			get
			{
				return GvmlContentPartNonVisualType.eleTagNames;
			}
		}

		// Token: 0x17004A0D RID: 18957
		// (get) Token: 0x060103C1 RID: 66497 RVA: 0x002E14A1 File Offset: 0x002DF6A1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GvmlContentPartNonVisualType.eleNamespaceIds;
			}
		}

		// Token: 0x17004A0E RID: 18958
		// (get) Token: 0x060103C2 RID: 66498 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004A0F RID: 18959
		// (get) Token: 0x060103C3 RID: 66499 RVA: 0x002E14A8 File Offset: 0x002DF6A8
		// (set) Token: 0x060103C4 RID: 66500 RVA: 0x002E14B1 File Offset: 0x002DF6B1
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

		// Token: 0x17004A10 RID: 18960
		// (get) Token: 0x060103C5 RID: 66501 RVA: 0x002E14BB File Offset: 0x002DF6BB
		// (set) Token: 0x060103C6 RID: 66502 RVA: 0x002E14C4 File Offset: 0x002DF6C4
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

		// Token: 0x17004A11 RID: 18961
		// (get) Token: 0x060103C7 RID: 66503 RVA: 0x002E14CE File Offset: 0x002DF6CE
		// (set) Token: 0x060103C8 RID: 66504 RVA: 0x002E14D7 File Offset: 0x002DF6D7
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

		// Token: 0x060103C9 RID: 66505 RVA: 0x00293ECF File Offset: 0x002920CF
		protected GvmlContentPartNonVisualType()
		{
		}

		// Token: 0x060103CA RID: 66506 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected GvmlContentPartNonVisualType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103CB RID: 66507 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected GvmlContentPartNonVisualType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103CC RID: 66508 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected GvmlContentPartNonVisualType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x040073A6 RID: 29606
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvInkPr", "cNvContentPartPr" };

		// Token: 0x040073A7 RID: 29607
		private static readonly byte[] eleNamespaceIds = new byte[] { 48, 48, 48 };
	}
}
