using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C41 RID: 11329
	[ChildElementInfo(typeof(ColumnOffset))]
	[ChildElementInfo(typeof(RowId))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ColumnId))]
	[ChildElementInfo(typeof(RowOffset))]
	internal abstract class MarkerType : OpenXmlCompositeElement
	{
		// Token: 0x0601802F RID: 98351 RVA: 0x0033DA90 File Offset: 0x0033BC90
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

		// Token: 0x170081A2 RID: 33186
		// (get) Token: 0x06018030 RID: 98352 RVA: 0x0033DAFE File Offset: 0x0033BCFE
		internal override string[] ElementTagNames
		{
			get
			{
				return MarkerType.eleTagNames;
			}
		}

		// Token: 0x170081A3 RID: 33187
		// (get) Token: 0x06018031 RID: 98353 RVA: 0x0033DB05 File Offset: 0x0033BD05
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MarkerType.eleNamespaceIds;
			}
		}

		// Token: 0x170081A4 RID: 33188
		// (get) Token: 0x06018032 RID: 98354 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170081A5 RID: 33189
		// (get) Token: 0x06018033 RID: 98355 RVA: 0x00312AA4 File Offset: 0x00310CA4
		// (set) Token: 0x06018034 RID: 98356 RVA: 0x00312AAD File Offset: 0x00310CAD
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

		// Token: 0x170081A6 RID: 33190
		// (get) Token: 0x06018035 RID: 98357 RVA: 0x00312AB7 File Offset: 0x00310CB7
		// (set) Token: 0x06018036 RID: 98358 RVA: 0x00312AC0 File Offset: 0x00310CC0
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

		// Token: 0x170081A7 RID: 33191
		// (get) Token: 0x06018037 RID: 98359 RVA: 0x00312ACA File Offset: 0x00310CCA
		// (set) Token: 0x06018038 RID: 98360 RVA: 0x00312AD3 File Offset: 0x00310CD3
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

		// Token: 0x170081A8 RID: 33192
		// (get) Token: 0x06018039 RID: 98361 RVA: 0x00312ADD File Offset: 0x00310CDD
		// (set) Token: 0x0601803A RID: 98362 RVA: 0x00312AE6 File Offset: 0x00310CE6
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

		// Token: 0x0601803B RID: 98363 RVA: 0x00293ECF File Offset: 0x002920CF
		protected MarkerType()
		{
		}

		// Token: 0x0601803C RID: 98364 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected MarkerType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601803D RID: 98365 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected MarkerType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601803E RID: 98366 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected MarkerType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04009E7D RID: 40573
		private static readonly string[] eleTagNames = new string[] { "col", "colOff", "row", "rowOff" };

		// Token: 0x04009E7E RID: 40574
		private static readonly byte[] eleNamespaceIds = new byte[] { 18, 18, 18, 18 };
	}
}
