using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A65 RID: 10853
	[ChildElementInfo(typeof(NonVisualConnectorShapeDrawingProperties))]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	internal class NonVisualConnectionShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007283 RID: 29315
		// (get) Token: 0x06015ED1 RID: 89809 RVA: 0x002FC2F4 File Offset: 0x002FA4F4
		public override string LocalName
		{
			get
			{
				return "nvCxnSpPr";
			}
		}

		// Token: 0x17007284 RID: 29316
		// (get) Token: 0x06015ED2 RID: 89810 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007285 RID: 29317
		// (get) Token: 0x06015ED3 RID: 89811 RVA: 0x00324A00 File Offset: 0x00322C00
		internal override int ElementTypeId
		{
			get
			{
				return 12271;
			}
		}

		// Token: 0x06015ED4 RID: 89812 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015ED5 RID: 89813 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualConnectionShapeProperties()
		{
		}

		// Token: 0x06015ED6 RID: 89814 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualConnectionShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015ED7 RID: 89815 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualConnectionShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015ED8 RID: 89816 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualConnectionShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015ED9 RID: 89817 RVA: 0x00324A08 File Offset: 0x00322C08
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (24 == namespaceId && "cNvCxnSpPr" == name)
			{
				return new NonVisualConnectorShapeDrawingProperties();
			}
			if (24 == namespaceId && "nvPr" == name)
			{
				return new ApplicationNonVisualDrawingProperties();
			}
			return null;
		}

		// Token: 0x17007286 RID: 29318
		// (get) Token: 0x06015EDA RID: 89818 RVA: 0x00324A5E File Offset: 0x00322C5E
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualConnectionShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17007287 RID: 29319
		// (get) Token: 0x06015EDB RID: 89819 RVA: 0x00324A65 File Offset: 0x00322C65
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualConnectionShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007288 RID: 29320
		// (get) Token: 0x06015EDC RID: 89820 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007289 RID: 29321
		// (get) Token: 0x06015EDD RID: 89821 RVA: 0x00324478 File Offset: 0x00322678
		// (set) Token: 0x06015EDE RID: 89822 RVA: 0x00324481 File Offset: 0x00322681
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

		// Token: 0x1700728A RID: 29322
		// (get) Token: 0x06015EDF RID: 89823 RVA: 0x00324A6C File Offset: 0x00322C6C
		// (set) Token: 0x06015EE0 RID: 89824 RVA: 0x00324A75 File Offset: 0x00322C75
		public NonVisualConnectorShapeDrawingProperties NonVisualConnectorShapeDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualConnectorShapeDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualConnectorShapeDrawingProperties>(1, value);
			}
		}

		// Token: 0x1700728B RID: 29323
		// (get) Token: 0x06015EE1 RID: 89825 RVA: 0x0032449E File Offset: 0x0032269E
		// (set) Token: 0x06015EE2 RID: 89826 RVA: 0x003244A7 File Offset: 0x003226A7
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

		// Token: 0x06015EE3 RID: 89827 RVA: 0x00324A7F File Offset: 0x00322C7F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualConnectionShapeProperties>(deep);
		}

		// Token: 0x04009571 RID: 38257
		private const string tagName = "nvCxnSpPr";

		// Token: 0x04009572 RID: 38258
		private const byte tagNsId = 24;

		// Token: 0x04009573 RID: 38259
		internal const int ElementTypeIdConst = 12271;

		// Token: 0x04009574 RID: 38260
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvCxnSpPr", "nvPr" };

		// Token: 0x04009575 RID: 38261
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24 };
	}
}
