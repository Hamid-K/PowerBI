using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002804 RID: 10244
	[ChildElementInfo(typeof(TableCellTextStyle))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableCellStyle))]
	internal abstract class TablePartStyleType : OpenXmlCompositeElement
	{
		// Token: 0x06014077 RID: 82039 RVA: 0x0030E9AB File Offset: 0x0030CBAB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "tcTxStyle" == name)
			{
				return new TableCellTextStyle();
			}
			if (10 == namespaceId && "tcStyle" == name)
			{
				return new TableCellStyle();
			}
			return null;
		}

		// Token: 0x17006539 RID: 25913
		// (get) Token: 0x06014078 RID: 82040 RVA: 0x0030E9DE File Offset: 0x0030CBDE
		internal override string[] ElementTagNames
		{
			get
			{
				return TablePartStyleType.eleTagNames;
			}
		}

		// Token: 0x1700653A RID: 25914
		// (get) Token: 0x06014079 RID: 82041 RVA: 0x0030E9E5 File Offset: 0x0030CBE5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TablePartStyleType.eleNamespaceIds;
			}
		}

		// Token: 0x1700653B RID: 25915
		// (get) Token: 0x0601407A RID: 82042 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700653C RID: 25916
		// (get) Token: 0x0601407B RID: 82043 RVA: 0x0030E9EC File Offset: 0x0030CBEC
		// (set) Token: 0x0601407C RID: 82044 RVA: 0x0030E9F5 File Offset: 0x0030CBF5
		public TableCellTextStyle TableCellTextStyle
		{
			get
			{
				return base.GetElement<TableCellTextStyle>(0);
			}
			set
			{
				base.SetElement<TableCellTextStyle>(0, value);
			}
		}

		// Token: 0x1700653D RID: 25917
		// (get) Token: 0x0601407D RID: 82045 RVA: 0x0030E9FF File Offset: 0x0030CBFF
		// (set) Token: 0x0601407E RID: 82046 RVA: 0x0030EA08 File Offset: 0x0030CC08
		public TableCellStyle TableCellStyle
		{
			get
			{
				return base.GetElement<TableCellStyle>(1);
			}
			set
			{
				base.SetElement<TableCellStyle>(1, value);
			}
		}

		// Token: 0x0601407F RID: 82047 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TablePartStyleType()
		{
		}

		// Token: 0x06014080 RID: 82048 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TablePartStyleType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014081 RID: 82049 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TablePartStyleType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014082 RID: 82050 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TablePartStyleType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x040088B6 RID: 34998
		private static readonly string[] eleTagNames = new string[] { "tcTxStyle", "tcStyle" };

		// Token: 0x040088B7 RID: 34999
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
