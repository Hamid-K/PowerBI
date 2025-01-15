using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x0200238B RID: 9099
	[ChildElementInfo(typeof(NonVisualInkContentPartProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualInkProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualDrawingProperties), FileFormatVersions.Office2010)]
	internal abstract class ContentPartNonVisualType : OpenXmlCompositeElement
	{
		// Token: 0x06010710 RID: 67344 RVA: 0x002E39AC File Offset: 0x002E1BAC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (54 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (54 == namespaceId && "cNvInkPr" == name)
			{
				return new NonVisualInkProperties();
			}
			if (54 == namespaceId && "cNvContentPartPr" == name)
			{
				return new NonVisualInkContentPartProperties();
			}
			return null;
		}

		// Token: 0x17004B95 RID: 19349
		// (get) Token: 0x06010711 RID: 67345 RVA: 0x002E3A02 File Offset: 0x002E1C02
		internal override string[] ElementTagNames
		{
			get
			{
				return ContentPartNonVisualType.eleTagNames;
			}
		}

		// Token: 0x17004B96 RID: 19350
		// (get) Token: 0x06010712 RID: 67346 RVA: 0x002E3A09 File Offset: 0x002E1C09
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ContentPartNonVisualType.eleNamespaceIds;
			}
		}

		// Token: 0x17004B97 RID: 19351
		// (get) Token: 0x06010713 RID: 67347 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004B98 RID: 19352
		// (get) Token: 0x06010714 RID: 67348 RVA: 0x002E3A10 File Offset: 0x002E1C10
		// (set) Token: 0x06010715 RID: 67349 RVA: 0x002E3A19 File Offset: 0x002E1C19
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

		// Token: 0x17004B99 RID: 19353
		// (get) Token: 0x06010716 RID: 67350 RVA: 0x002E3A23 File Offset: 0x002E1C23
		// (set) Token: 0x06010717 RID: 67351 RVA: 0x002E3A2C File Offset: 0x002E1C2C
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

		// Token: 0x17004B9A RID: 19354
		// (get) Token: 0x06010718 RID: 67352 RVA: 0x002E3A36 File Offset: 0x002E1C36
		// (set) Token: 0x06010719 RID: 67353 RVA: 0x002E3A3F File Offset: 0x002E1C3F
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

		// Token: 0x0601071A RID: 67354 RVA: 0x00293ECF File Offset: 0x002920CF
		protected ContentPartNonVisualType()
		{
		}

		// Token: 0x0601071B RID: 67355 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected ContentPartNonVisualType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601071C RID: 67356 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected ContentPartNonVisualType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601071D RID: 67357 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected ContentPartNonVisualType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0400749E RID: 29854
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvInkPr", "cNvContentPartPr" };

		// Token: 0x0400749F RID: 29855
		private static readonly byte[] eleNamespaceIds = new byte[] { 54, 54, 54 };
	}
}
