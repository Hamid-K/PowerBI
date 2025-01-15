using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002884 RID: 10372
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualConnectorShapeDrawingProperties))]
	internal class NonVisualConnectionShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006756 RID: 26454
		// (get) Token: 0x0601457D RID: 83325 RVA: 0x002FC2F4 File Offset: 0x002FA4F4
		public override string LocalName
		{
			get
			{
				return "nvCxnSpPr";
			}
		}

		// Token: 0x17006757 RID: 26455
		// (get) Token: 0x0601457E RID: 83326 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006758 RID: 26456
		// (get) Token: 0x0601457F RID: 83327 RVA: 0x0031254C File Offset: 0x0031074C
		internal override int ElementTypeId
		{
			get
			{
				return 10734;
			}
		}

		// Token: 0x06014580 RID: 83328 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014581 RID: 83329 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualConnectionShapeProperties()
		{
		}

		// Token: 0x06014582 RID: 83330 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualConnectionShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014583 RID: 83331 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualConnectionShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014584 RID: 83332 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualConnectionShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014585 RID: 83333 RVA: 0x00312553 File Offset: 0x00310753
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (18 == namespaceId && "cNvCxnSpPr" == name)
			{
				return new NonVisualConnectorShapeDrawingProperties();
			}
			return null;
		}

		// Token: 0x17006759 RID: 26457
		// (get) Token: 0x06014586 RID: 83334 RVA: 0x00312586 File Offset: 0x00310786
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualConnectionShapeProperties.eleTagNames;
			}
		}

		// Token: 0x1700675A RID: 26458
		// (get) Token: 0x06014587 RID: 83335 RVA: 0x0031258D File Offset: 0x0031078D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualConnectionShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700675B RID: 26459
		// (get) Token: 0x06014588 RID: 83336 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700675C RID: 26460
		// (get) Token: 0x06014589 RID: 83337 RVA: 0x003120BF File Offset: 0x003102BF
		// (set) Token: 0x0601458A RID: 83338 RVA: 0x003120C8 File Offset: 0x003102C8
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

		// Token: 0x1700675D RID: 26461
		// (get) Token: 0x0601458B RID: 83339 RVA: 0x00312594 File Offset: 0x00310794
		// (set) Token: 0x0601458C RID: 83340 RVA: 0x0031259D File Offset: 0x0031079D
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

		// Token: 0x0601458D RID: 83341 RVA: 0x003125A7 File Offset: 0x003107A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualConnectionShapeProperties>(deep);
		}

		// Token: 0x04008DA9 RID: 36265
		private const string tagName = "nvCxnSpPr";

		// Token: 0x04008DAA RID: 36266
		private const byte tagNsId = 18;

		// Token: 0x04008DAB RID: 36267
		internal const int ElementTypeIdConst = 10734;

		// Token: 0x04008DAC RID: 36268
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvCxnSpPr" };

		// Token: 0x04008DAD RID: 36269
		private static readonly byte[] eleNamespaceIds = new byte[] { 18, 18 };
	}
}
