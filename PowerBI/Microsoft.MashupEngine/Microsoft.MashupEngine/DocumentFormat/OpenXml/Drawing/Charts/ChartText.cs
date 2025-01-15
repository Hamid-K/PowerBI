using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002536 RID: 9526
	[ChildElementInfo(typeof(StringReference))]
	[ChildElementInfo(typeof(RichText))]
	[ChildElementInfo(typeof(StringLiteral))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartText : OpenXmlCompositeElement
	{
		// Token: 0x170054C0 RID: 21696
		// (get) Token: 0x06011B83 RID: 72579 RVA: 0x002F16DD File Offset: 0x002EF8DD
		public override string LocalName
		{
			get
			{
				return "tx";
			}
		}

		// Token: 0x170054C1 RID: 21697
		// (get) Token: 0x06011B84 RID: 72580 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054C2 RID: 21698
		// (get) Token: 0x06011B85 RID: 72581 RVA: 0x002F16E4 File Offset: 0x002EF8E4
		internal override int ElementTypeId
		{
			get
			{
				return 10354;
			}
		}

		// Token: 0x06011B86 RID: 72582 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B87 RID: 72583 RVA: 0x00293ECF File Offset: 0x002920CF
		public ChartText()
		{
		}

		// Token: 0x06011B88 RID: 72584 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ChartText(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011B89 RID: 72585 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ChartText(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011B8A RID: 72586 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ChartText(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011B8B RID: 72587 RVA: 0x002F16EC File Offset: 0x002EF8EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "strRef" == name)
			{
				return new StringReference();
			}
			if (11 == namespaceId && "rich" == name)
			{
				return new RichText();
			}
			if (11 == namespaceId && "strLit" == name)
			{
				return new StringLiteral();
			}
			return null;
		}

		// Token: 0x170054C3 RID: 21699
		// (get) Token: 0x06011B8C RID: 72588 RVA: 0x002F1742 File Offset: 0x002EF942
		internal override string[] ElementTagNames
		{
			get
			{
				return ChartText.eleTagNames;
			}
		}

		// Token: 0x170054C4 RID: 21700
		// (get) Token: 0x06011B8D RID: 72589 RVA: 0x002F1749 File Offset: 0x002EF949
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ChartText.eleNamespaceIds;
			}
		}

		// Token: 0x170054C5 RID: 21701
		// (get) Token: 0x06011B8E RID: 72590 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170054C6 RID: 21702
		// (get) Token: 0x06011B8F RID: 72591 RVA: 0x002F1750 File Offset: 0x002EF950
		// (set) Token: 0x06011B90 RID: 72592 RVA: 0x002F1759 File Offset: 0x002EF959
		public StringReference StringReference
		{
			get
			{
				return base.GetElement<StringReference>(0);
			}
			set
			{
				base.SetElement<StringReference>(0, value);
			}
		}

		// Token: 0x170054C7 RID: 21703
		// (get) Token: 0x06011B91 RID: 72593 RVA: 0x002F1763 File Offset: 0x002EF963
		// (set) Token: 0x06011B92 RID: 72594 RVA: 0x002F176C File Offset: 0x002EF96C
		public RichText RichText
		{
			get
			{
				return base.GetElement<RichText>(1);
			}
			set
			{
				base.SetElement<RichText>(1, value);
			}
		}

		// Token: 0x170054C8 RID: 21704
		// (get) Token: 0x06011B93 RID: 72595 RVA: 0x002F1776 File Offset: 0x002EF976
		// (set) Token: 0x06011B94 RID: 72596 RVA: 0x002F177F File Offset: 0x002EF97F
		public StringLiteral StringLiteral
		{
			get
			{
				return base.GetElement<StringLiteral>(2);
			}
			set
			{
				base.SetElement<StringLiteral>(2, value);
			}
		}

		// Token: 0x06011B95 RID: 72597 RVA: 0x002F1789 File Offset: 0x002EF989
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartText>(deep);
		}

		// Token: 0x04007C38 RID: 31800
		private const string tagName = "tx";

		// Token: 0x04007C39 RID: 31801
		private const byte tagNsId = 11;

		// Token: 0x04007C3A RID: 31802
		internal const int ElementTypeIdConst = 10354;

		// Token: 0x04007C3B RID: 31803
		private static readonly string[] eleTagNames = new string[] { "strRef", "rich", "strLit" };

		// Token: 0x04007C3C RID: 31804
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11 };
	}
}
