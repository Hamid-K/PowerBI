using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CCC RID: 11468
	[ChildElementInfo(typeof(Sets))]
	[ChildElementInfo(typeof(ServerFormats))]
	[ChildElementInfo(typeof(QueryCache))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(Entries))]
	internal class TupleCache : OpenXmlCompositeElement
	{
		// Token: 0x17008543 RID: 34115
		// (get) Token: 0x06018913 RID: 100627 RVA: 0x00342A4F File Offset: 0x00340C4F
		public override string LocalName
		{
			get
			{
				return "tupleCache";
			}
		}

		// Token: 0x17008544 RID: 34116
		// (get) Token: 0x06018914 RID: 100628 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008545 RID: 34117
		// (get) Token: 0x06018915 RID: 100629 RVA: 0x00342A56 File Offset: 0x00340C56
		internal override int ElementTypeId
		{
			get
			{
				return 11449;
			}
		}

		// Token: 0x06018916 RID: 100630 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018917 RID: 100631 RVA: 0x00293ECF File Offset: 0x002920CF
		public TupleCache()
		{
		}

		// Token: 0x06018918 RID: 100632 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TupleCache(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018919 RID: 100633 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TupleCache(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601891A RID: 100634 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TupleCache(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601891B RID: 100635 RVA: 0x00342A60 File Offset: 0x00340C60
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "entries" == name)
			{
				return new Entries();
			}
			if (22 == namespaceId && "sets" == name)
			{
				return new Sets();
			}
			if (22 == namespaceId && "queryCache" == name)
			{
				return new QueryCache();
			}
			if (22 == namespaceId && "serverFormats" == name)
			{
				return new ServerFormats();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17008546 RID: 34118
		// (get) Token: 0x0601891C RID: 100636 RVA: 0x00342AE6 File Offset: 0x00340CE6
		internal override string[] ElementTagNames
		{
			get
			{
				return TupleCache.eleTagNames;
			}
		}

		// Token: 0x17008547 RID: 34119
		// (get) Token: 0x0601891D RID: 100637 RVA: 0x00342AED File Offset: 0x00340CED
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TupleCache.eleNamespaceIds;
			}
		}

		// Token: 0x17008548 RID: 34120
		// (get) Token: 0x0601891E RID: 100638 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008549 RID: 34121
		// (get) Token: 0x0601891F RID: 100639 RVA: 0x00342AF4 File Offset: 0x00340CF4
		// (set) Token: 0x06018920 RID: 100640 RVA: 0x00342AFD File Offset: 0x00340CFD
		public Entries Entries
		{
			get
			{
				return base.GetElement<Entries>(0);
			}
			set
			{
				base.SetElement<Entries>(0, value);
			}
		}

		// Token: 0x1700854A RID: 34122
		// (get) Token: 0x06018921 RID: 100641 RVA: 0x00342B07 File Offset: 0x00340D07
		// (set) Token: 0x06018922 RID: 100642 RVA: 0x00342B10 File Offset: 0x00340D10
		public Sets Sets
		{
			get
			{
				return base.GetElement<Sets>(1);
			}
			set
			{
				base.SetElement<Sets>(1, value);
			}
		}

		// Token: 0x1700854B RID: 34123
		// (get) Token: 0x06018923 RID: 100643 RVA: 0x00342B1A File Offset: 0x00340D1A
		// (set) Token: 0x06018924 RID: 100644 RVA: 0x00342B23 File Offset: 0x00340D23
		public QueryCache QueryCache
		{
			get
			{
				return base.GetElement<QueryCache>(2);
			}
			set
			{
				base.SetElement<QueryCache>(2, value);
			}
		}

		// Token: 0x1700854C RID: 34124
		// (get) Token: 0x06018925 RID: 100645 RVA: 0x00342B2D File Offset: 0x00340D2D
		// (set) Token: 0x06018926 RID: 100646 RVA: 0x00342B36 File Offset: 0x00340D36
		public ServerFormats ServerFormats
		{
			get
			{
				return base.GetElement<ServerFormats>(3);
			}
			set
			{
				base.SetElement<ServerFormats>(3, value);
			}
		}

		// Token: 0x1700854D RID: 34125
		// (get) Token: 0x06018927 RID: 100647 RVA: 0x00334564 File Offset: 0x00332764
		// (set) Token: 0x06018928 RID: 100648 RVA: 0x0033456D File Offset: 0x0033276D
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x06018929 RID: 100649 RVA: 0x00342B40 File Offset: 0x00340D40
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TupleCache>(deep);
		}

		// Token: 0x0400A0DB RID: 41179
		private const string tagName = "tupleCache";

		// Token: 0x0400A0DC RID: 41180
		private const byte tagNsId = 22;

		// Token: 0x0400A0DD RID: 41181
		internal const int ElementTypeIdConst = 11449;

		// Token: 0x0400A0DE RID: 41182
		private static readonly string[] eleTagNames = new string[] { "entries", "sets", "queryCache", "serverFormats", "extLst" };

		// Token: 0x0400A0DF RID: 41183
		private static readonly byte[] eleNamespaceIds = new byte[] { 22, 22, 22, 22, 22 };
	}
}
