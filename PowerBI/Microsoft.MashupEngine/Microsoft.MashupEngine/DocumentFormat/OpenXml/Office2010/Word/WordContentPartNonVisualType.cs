using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024BE RID: 9406
	[ChildElementInfo(typeof(NonVisualInkContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualInkProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class WordContentPartNonVisualType : OpenXmlCompositeElement
	{
		// Token: 0x060116D8 RID: 71384 RVA: 0x002EE5F4 File Offset: 0x002EC7F4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (52 == namespaceId && "cNvInkPr" == name)
			{
				return new NonVisualInkProperties();
			}
			if (52 == namespaceId && "cNvContentPartPr" == name)
			{
				return new NonVisualInkContentPartProperties();
			}
			return null;
		}

		// Token: 0x170052A1 RID: 21153
		// (get) Token: 0x060116D9 RID: 71385 RVA: 0x002EE64A File Offset: 0x002EC84A
		internal override string[] ElementTagNames
		{
			get
			{
				return WordContentPartNonVisualType.eleTagNames;
			}
		}

		// Token: 0x170052A2 RID: 21154
		// (get) Token: 0x060116DA RID: 71386 RVA: 0x002EE651 File Offset: 0x002EC851
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WordContentPartNonVisualType.eleNamespaceIds;
			}
		}

		// Token: 0x170052A3 RID: 21155
		// (get) Token: 0x060116DB RID: 71387 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170052A4 RID: 21156
		// (get) Token: 0x060116DC RID: 71388 RVA: 0x002EE658 File Offset: 0x002EC858
		// (set) Token: 0x060116DD RID: 71389 RVA: 0x002EE661 File Offset: 0x002EC861
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

		// Token: 0x170052A5 RID: 21157
		// (get) Token: 0x060116DE RID: 71390 RVA: 0x002EE66B File Offset: 0x002EC86B
		// (set) Token: 0x060116DF RID: 71391 RVA: 0x002EE674 File Offset: 0x002EC874
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

		// Token: 0x170052A6 RID: 21158
		// (get) Token: 0x060116E0 RID: 71392 RVA: 0x002EE67E File Offset: 0x002EC87E
		// (set) Token: 0x060116E1 RID: 71393 RVA: 0x002EE687 File Offset: 0x002EC887
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

		// Token: 0x060116E2 RID: 71394 RVA: 0x00293ECF File Offset: 0x002920CF
		protected WordContentPartNonVisualType()
		{
		}

		// Token: 0x060116E3 RID: 71395 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected WordContentPartNonVisualType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116E4 RID: 71396 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected WordContentPartNonVisualType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116E5 RID: 71397 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected WordContentPartNonVisualType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x040079C9 RID: 31177
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvInkPr", "cNvContentPartPr" };

		// Token: 0x040079CA RID: 31178
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52, 52 };
	}
}
