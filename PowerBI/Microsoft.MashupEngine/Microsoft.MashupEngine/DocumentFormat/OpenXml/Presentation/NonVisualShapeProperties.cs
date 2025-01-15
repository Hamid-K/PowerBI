using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A60 RID: 10848
	[ChildElementInfo(typeof(NonVisualShapeDrawingProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingProperties))]
	internal class NonVisualShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007254 RID: 29268
		// (get) Token: 0x06015E6A RID: 89706 RVA: 0x002DEC0B File Offset: 0x002DCE0B
		public override string LocalName
		{
			get
			{
				return "nvSpPr";
			}
		}

		// Token: 0x17007255 RID: 29269
		// (get) Token: 0x06015E6B RID: 89707 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007256 RID: 29270
		// (get) Token: 0x06015E6C RID: 89708 RVA: 0x0032440A File Offset: 0x0032260A
		internal override int ElementTypeId
		{
			get
			{
				return 12266;
			}
		}

		// Token: 0x06015E6D RID: 89709 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015E6E RID: 89710 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualShapeProperties()
		{
		}

		// Token: 0x06015E6F RID: 89711 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E70 RID: 89712 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E71 RID: 89713 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015E72 RID: 89714 RVA: 0x00324414 File Offset: 0x00322614
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (24 == namespaceId && "cNvSpPr" == name)
			{
				return new NonVisualShapeDrawingProperties();
			}
			if (24 == namespaceId && "nvPr" == name)
			{
				return new ApplicationNonVisualDrawingProperties();
			}
			return null;
		}

		// Token: 0x17007257 RID: 29271
		// (get) Token: 0x06015E73 RID: 89715 RVA: 0x0032446A File Offset: 0x0032266A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17007258 RID: 29272
		// (get) Token: 0x06015E74 RID: 89716 RVA: 0x00324471 File Offset: 0x00322671
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007259 RID: 29273
		// (get) Token: 0x06015E75 RID: 89717 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700725A RID: 29274
		// (get) Token: 0x06015E76 RID: 89718 RVA: 0x00324478 File Offset: 0x00322678
		// (set) Token: 0x06015E77 RID: 89719 RVA: 0x00324481 File Offset: 0x00322681
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

		// Token: 0x1700725B RID: 29275
		// (get) Token: 0x06015E78 RID: 89720 RVA: 0x0032448B File Offset: 0x0032268B
		// (set) Token: 0x06015E79 RID: 89721 RVA: 0x00324494 File Offset: 0x00322694
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

		// Token: 0x1700725C RID: 29276
		// (get) Token: 0x06015E7A RID: 89722 RVA: 0x0032449E File Offset: 0x0032269E
		// (set) Token: 0x06015E7B RID: 89723 RVA: 0x003244A7 File Offset: 0x003226A7
		public ApplicationNonVisualDrawingProperties ApplicationNonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<ApplicationNonVisualDrawingProperties>(2);
			}
			set
			{
				base.SetElement<ApplicationNonVisualDrawingProperties>(2, value);
			}
		}

		// Token: 0x06015E7C RID: 89724 RVA: 0x003244B1 File Offset: 0x003226B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualShapeProperties>(deep);
		}

		// Token: 0x04009556 RID: 38230
		private const string tagName = "nvSpPr";

		// Token: 0x04009557 RID: 38231
		private const byte tagNsId = 24;

		// Token: 0x04009558 RID: 38232
		internal const int ElementTypeIdConst = 12266;

		// Token: 0x04009559 RID: 38233
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvSpPr", "nvPr" };

		// Token: 0x0400955A RID: 38234
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24 };
	}
}
