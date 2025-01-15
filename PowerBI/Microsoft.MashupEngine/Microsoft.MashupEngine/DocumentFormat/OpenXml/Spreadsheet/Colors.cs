using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C7A RID: 11386
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(IndexedColors))]
	[ChildElementInfo(typeof(MruColors))]
	internal class Colors : OpenXmlCompositeElement
	{
		// Token: 0x17008307 RID: 33543
		// (get) Token: 0x060183A3 RID: 99235 RVA: 0x002ACC7A File Offset: 0x002AAE7A
		public override string LocalName
		{
			get
			{
				return "colors";
			}
		}

		// Token: 0x17008308 RID: 33544
		// (get) Token: 0x060183A4 RID: 99236 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008309 RID: 33545
		// (get) Token: 0x060183A5 RID: 99237 RVA: 0x0033F7EF File Offset: 0x0033D9EF
		internal override int ElementTypeId
		{
			get
			{
				return 11366;
			}
		}

		// Token: 0x060183A6 RID: 99238 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060183A7 RID: 99239 RVA: 0x00293ECF File Offset: 0x002920CF
		public Colors()
		{
		}

		// Token: 0x060183A8 RID: 99240 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Colors(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060183A9 RID: 99241 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Colors(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060183AA RID: 99242 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Colors(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060183AB RID: 99243 RVA: 0x0033F7F6 File Offset: 0x0033D9F6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "indexedColors" == name)
			{
				return new IndexedColors();
			}
			if (22 == namespaceId && "mruColors" == name)
			{
				return new MruColors();
			}
			return null;
		}

		// Token: 0x1700830A RID: 33546
		// (get) Token: 0x060183AC RID: 99244 RVA: 0x0033F829 File Offset: 0x0033DA29
		internal override string[] ElementTagNames
		{
			get
			{
				return Colors.eleTagNames;
			}
		}

		// Token: 0x1700830B RID: 33547
		// (get) Token: 0x060183AD RID: 99245 RVA: 0x0033F830 File Offset: 0x0033DA30
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Colors.eleNamespaceIds;
			}
		}

		// Token: 0x1700830C RID: 33548
		// (get) Token: 0x060183AE RID: 99246 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700830D RID: 33549
		// (get) Token: 0x060183AF RID: 99247 RVA: 0x0033F837 File Offset: 0x0033DA37
		// (set) Token: 0x060183B0 RID: 99248 RVA: 0x0033F840 File Offset: 0x0033DA40
		public IndexedColors IndexedColors
		{
			get
			{
				return base.GetElement<IndexedColors>(0);
			}
			set
			{
				base.SetElement<IndexedColors>(0, value);
			}
		}

		// Token: 0x1700830E RID: 33550
		// (get) Token: 0x060183B1 RID: 99249 RVA: 0x0033F84A File Offset: 0x0033DA4A
		// (set) Token: 0x060183B2 RID: 99250 RVA: 0x0033F853 File Offset: 0x0033DA53
		public MruColors MruColors
		{
			get
			{
				return base.GetElement<MruColors>(1);
			}
			set
			{
				base.SetElement<MruColors>(1, value);
			}
		}

		// Token: 0x060183B3 RID: 99251 RVA: 0x0033F85D File Offset: 0x0033DA5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Colors>(deep);
		}

		// Token: 0x04009F6E RID: 40814
		private const string tagName = "colors";

		// Token: 0x04009F6F RID: 40815
		private const byte tagNsId = 22;

		// Token: 0x04009F70 RID: 40816
		internal const int ElementTypeIdConst = 11366;

		// Token: 0x04009F71 RID: 40817
		private static readonly string[] eleTagNames = new string[] { "indexedColors", "mruColors" };

		// Token: 0x04009F72 RID: 40818
		private static readonly byte[] eleNamespaceIds = new byte[] { 22, 22 };
	}
}
