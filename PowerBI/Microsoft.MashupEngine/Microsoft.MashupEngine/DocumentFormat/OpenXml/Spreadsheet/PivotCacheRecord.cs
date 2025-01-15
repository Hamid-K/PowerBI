using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B60 RID: 11104
	[ChildElementInfo(typeof(StringItem))]
	[ChildElementInfo(typeof(NumberItem))]
	[ChildElementInfo(typeof(BooleanItem))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MissingItem))]
	[ChildElementInfo(typeof(ErrorItem))]
	[ChildElementInfo(typeof(DateTimeItem))]
	[ChildElementInfo(typeof(FieldItem))]
	internal class PivotCacheRecord : OpenXmlCompositeElement
	{
		// Token: 0x170078D0 RID: 30928
		// (get) Token: 0x06016D3B RID: 93499 RVA: 0x002BF737 File Offset: 0x002BD937
		public override string LocalName
		{
			get
			{
				return "r";
			}
		}

		// Token: 0x170078D1 RID: 30929
		// (get) Token: 0x06016D3C RID: 93500 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078D2 RID: 30930
		// (get) Token: 0x06016D3D RID: 93501 RVA: 0x0032F812 File Offset: 0x0032DA12
		internal override int ElementTypeId
		{
			get
			{
				return 11083;
			}
		}

		// Token: 0x06016D3E RID: 93502 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016D3F RID: 93503 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotCacheRecord()
		{
		}

		// Token: 0x06016D40 RID: 93504 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotCacheRecord(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D41 RID: 93505 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotCacheRecord(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D42 RID: 93506 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotCacheRecord(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016D43 RID: 93507 RVA: 0x0032F81C File Offset: 0x0032DA1C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "m" == name)
			{
				return new MissingItem();
			}
			if (22 == namespaceId && "n" == name)
			{
				return new NumberItem();
			}
			if (22 == namespaceId && "b" == name)
			{
				return new BooleanItem();
			}
			if (22 == namespaceId && "e" == name)
			{
				return new ErrorItem();
			}
			if (22 == namespaceId && "s" == name)
			{
				return new StringItem();
			}
			if (22 == namespaceId && "d" == name)
			{
				return new DateTimeItem();
			}
			if (22 == namespaceId && "x" == name)
			{
				return new FieldItem();
			}
			return null;
		}

		// Token: 0x06016D44 RID: 93508 RVA: 0x0032F8D2 File Offset: 0x0032DAD2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotCacheRecord>(deep);
		}

		// Token: 0x04009A12 RID: 39442
		private const string tagName = "r";

		// Token: 0x04009A13 RID: 39443
		private const byte tagNsId = 22;

		// Token: 0x04009A14 RID: 39444
		internal const int ElementTypeIdConst = 11083;
	}
}
