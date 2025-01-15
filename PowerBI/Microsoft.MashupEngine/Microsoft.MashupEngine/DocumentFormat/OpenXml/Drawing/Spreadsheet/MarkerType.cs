using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200288D RID: 10381
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RowOffset))]
	[ChildElementInfo(typeof(ColumnId))]
	[ChildElementInfo(typeof(ColumnOffset))]
	[ChildElementInfo(typeof(RowId))]
	internal abstract class MarkerType : OpenXmlCompositeElement
	{
		// Token: 0x06014605 RID: 83461 RVA: 0x00312A28 File Offset: 0x00310C28
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "col" == name)
			{
				return new ColumnId();
			}
			if (18 == namespaceId && "colOff" == name)
			{
				return new ColumnOffset();
			}
			if (18 == namespaceId && "row" == name)
			{
				return new RowId();
			}
			if (18 == namespaceId && "rowOff" == name)
			{
				return new RowOffset();
			}
			return null;
		}

		// Token: 0x17006792 RID: 26514
		// (get) Token: 0x06014606 RID: 83462 RVA: 0x00312A96 File Offset: 0x00310C96
		internal override string[] ElementTagNames
		{
			get
			{
				return MarkerType.eleTagNames;
			}
		}

		// Token: 0x17006793 RID: 26515
		// (get) Token: 0x06014607 RID: 83463 RVA: 0x00312A9D File Offset: 0x00310C9D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MarkerType.eleNamespaceIds;
			}
		}

		// Token: 0x17006794 RID: 26516
		// (get) Token: 0x06014608 RID: 83464 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006795 RID: 26517
		// (get) Token: 0x06014609 RID: 83465 RVA: 0x00312AA4 File Offset: 0x00310CA4
		// (set) Token: 0x0601460A RID: 83466 RVA: 0x00312AAD File Offset: 0x00310CAD
		public ColumnId ColumnId
		{
			get
			{
				return base.GetElement<ColumnId>(0);
			}
			set
			{
				base.SetElement<ColumnId>(0, value);
			}
		}

		// Token: 0x17006796 RID: 26518
		// (get) Token: 0x0601460B RID: 83467 RVA: 0x00312AB7 File Offset: 0x00310CB7
		// (set) Token: 0x0601460C RID: 83468 RVA: 0x00312AC0 File Offset: 0x00310CC0
		public ColumnOffset ColumnOffset
		{
			get
			{
				return base.GetElement<ColumnOffset>(1);
			}
			set
			{
				base.SetElement<ColumnOffset>(1, value);
			}
		}

		// Token: 0x17006797 RID: 26519
		// (get) Token: 0x0601460D RID: 83469 RVA: 0x00312ACA File Offset: 0x00310CCA
		// (set) Token: 0x0601460E RID: 83470 RVA: 0x00312AD3 File Offset: 0x00310CD3
		public RowId RowId
		{
			get
			{
				return base.GetElement<RowId>(2);
			}
			set
			{
				base.SetElement<RowId>(2, value);
			}
		}

		// Token: 0x17006798 RID: 26520
		// (get) Token: 0x0601460F RID: 83471 RVA: 0x00312ADD File Offset: 0x00310CDD
		// (set) Token: 0x06014610 RID: 83472 RVA: 0x00312AE6 File Offset: 0x00310CE6
		public RowOffset RowOffset
		{
			get
			{
				return base.GetElement<RowOffset>(3);
			}
			set
			{
				base.SetElement<RowOffset>(3, value);
			}
		}

		// Token: 0x06014611 RID: 83473 RVA: 0x00293ECF File Offset: 0x002920CF
		protected MarkerType()
		{
		}

		// Token: 0x06014612 RID: 83474 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected MarkerType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014613 RID: 83475 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected MarkerType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014614 RID: 83476 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected MarkerType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04008DD2 RID: 36306
		private static readonly string[] eleTagNames = new string[] { "col", "colOff", "row", "rowOff" };

		// Token: 0x04008DD3 RID: 36307
		private static readonly byte[] eleNamespaceIds = new byte[] { 18, 18, 18, 18 };
	}
}
