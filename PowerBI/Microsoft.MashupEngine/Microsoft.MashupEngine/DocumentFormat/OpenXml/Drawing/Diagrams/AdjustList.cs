using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002667 RID: 9831
	[ChildElementInfo(typeof(Adjust))]
	[GeneratedCode("DomGen", "2.0")]
	internal class AdjustList : OpenXmlCompositeElement
	{
		// Token: 0x17005BE9 RID: 23529
		// (get) Token: 0x06012B9B RID: 76699 RVA: 0x002FE84B File Offset: 0x002FCA4B
		public override string LocalName
		{
			get
			{
				return "adjLst";
			}
		}

		// Token: 0x17005BEA RID: 23530
		// (get) Token: 0x06012B9C RID: 76700 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BEB RID: 23531
		// (get) Token: 0x06012B9D RID: 76701 RVA: 0x002FE852 File Offset: 0x002FCA52
		internal override int ElementTypeId
		{
			get
			{
				return 10648;
			}
		}

		// Token: 0x06012B9E RID: 76702 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012B9F RID: 76703 RVA: 0x00293ECF File Offset: 0x002920CF
		public AdjustList()
		{
		}

		// Token: 0x06012BA0 RID: 76704 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AdjustList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012BA1 RID: 76705 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AdjustList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012BA2 RID: 76706 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AdjustList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012BA3 RID: 76707 RVA: 0x002FE859 File Offset: 0x002FCA59
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "adj" == name)
			{
				return new Adjust();
			}
			return null;
		}

		// Token: 0x06012BA4 RID: 76708 RVA: 0x002FE874 File Offset: 0x002FCA74
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AdjustList>(deep);
		}

		// Token: 0x0400815B RID: 33115
		private const string tagName = "adjLst";

		// Token: 0x0400815C RID: 33116
		private const byte tagNsId = 14;

		// Token: 0x0400815D RID: 33117
		internal const int ElementTypeIdConst = 10648;
	}
}
