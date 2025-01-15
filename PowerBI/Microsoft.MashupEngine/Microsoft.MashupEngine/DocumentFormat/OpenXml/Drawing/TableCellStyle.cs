using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002802 RID: 10242
	[ChildElementInfo(typeof(Cell3DProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableCellBorders))]
	[ChildElementInfo(typeof(FillProperties))]
	[ChildElementInfo(typeof(FillReference))]
	internal class TableCellStyle : OpenXmlCompositeElement
	{
		// Token: 0x1700652F RID: 25903
		// (get) Token: 0x0601405D RID: 82013 RVA: 0x0030E82B File Offset: 0x0030CA2B
		public override string LocalName
		{
			get
			{
				return "tcStyle";
			}
		}

		// Token: 0x17006530 RID: 25904
		// (get) Token: 0x0601405E RID: 82014 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006531 RID: 25905
		// (get) Token: 0x0601405F RID: 82015 RVA: 0x0030E832 File Offset: 0x0030CA32
		internal override int ElementTypeId
		{
			get
			{
				return 10278;
			}
		}

		// Token: 0x06014060 RID: 82016 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014061 RID: 82017 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCellStyle()
		{
		}

		// Token: 0x06014062 RID: 82018 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCellStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014063 RID: 82019 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCellStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014064 RID: 82020 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCellStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014065 RID: 82021 RVA: 0x0030E83C File Offset: 0x0030CA3C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "tcBdr" == name)
			{
				return new TableCellBorders();
			}
			if (10 == namespaceId && "fill" == name)
			{
				return new FillProperties();
			}
			if (10 == namespaceId && "fillRef" == name)
			{
				return new FillReference();
			}
			if (10 == namespaceId && "cell3D" == name)
			{
				return new Cell3DProperties();
			}
			return null;
		}

		// Token: 0x17006532 RID: 25906
		// (get) Token: 0x06014066 RID: 82022 RVA: 0x0030E8AA File Offset: 0x0030CAAA
		internal override string[] ElementTagNames
		{
			get
			{
				return TableCellStyle.eleTagNames;
			}
		}

		// Token: 0x17006533 RID: 25907
		// (get) Token: 0x06014067 RID: 82023 RVA: 0x0030E8B1 File Offset: 0x0030CAB1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableCellStyle.eleNamespaceIds;
			}
		}

		// Token: 0x17006534 RID: 25908
		// (get) Token: 0x06014068 RID: 82024 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006535 RID: 25909
		// (get) Token: 0x06014069 RID: 82025 RVA: 0x0030E8B8 File Offset: 0x0030CAB8
		// (set) Token: 0x0601406A RID: 82026 RVA: 0x0030E8C1 File Offset: 0x0030CAC1
		public TableCellBorders TableCellBorders
		{
			get
			{
				return base.GetElement<TableCellBorders>(0);
			}
			set
			{
				base.SetElement<TableCellBorders>(0, value);
			}
		}

		// Token: 0x0601406B RID: 82027 RVA: 0x0030E8CB File Offset: 0x0030CACB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellStyle>(deep);
		}

		// Token: 0x040088AE RID: 34990
		private const string tagName = "tcStyle";

		// Token: 0x040088AF RID: 34991
		private const byte tagNsId = 10;

		// Token: 0x040088B0 RID: 34992
		internal const int ElementTypeIdConst = 10278;

		// Token: 0x040088B1 RID: 34993
		private static readonly string[] eleTagNames = new string[] { "tcBdr", "fill", "fillRef", "cell3D" };

		// Token: 0x040088B2 RID: 34994
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
