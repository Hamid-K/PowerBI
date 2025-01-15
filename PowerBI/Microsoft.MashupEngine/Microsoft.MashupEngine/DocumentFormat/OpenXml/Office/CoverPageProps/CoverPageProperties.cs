using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CoverPageProps
{
	// Token: 0x020022A3 RID: 8867
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CompanyPhoneNumber))]
	[ChildElementInfo(typeof(CompanyEmailAddress))]
	[ChildElementInfo(typeof(PublishDate))]
	[ChildElementInfo(typeof(CompanyFaxNumber))]
	[ChildElementInfo(typeof(DocumentAbstract))]
	[ChildElementInfo(typeof(CompanyAddress))]
	internal class CoverPageProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700410D RID: 16653
		// (get) Token: 0x0600F079 RID: 61561 RVA: 0x002D0B34 File Offset: 0x002CED34
		public override string LocalName
		{
			get
			{
				return "CoverPageProperties";
			}
		}

		// Token: 0x1700410E RID: 16654
		// (get) Token: 0x0600F07A RID: 61562 RVA: 0x002D0B3B File Offset: 0x002CED3B
		internal override byte NamespaceId
		{
			get
			{
				return 36;
			}
		}

		// Token: 0x1700410F RID: 16655
		// (get) Token: 0x0600F07B RID: 61563 RVA: 0x002D0B3F File Offset: 0x002CED3F
		internal override int ElementTypeId
		{
			get
			{
				return 12621;
			}
		}

		// Token: 0x0600F07C RID: 61564 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F07D RID: 61565 RVA: 0x00293ECF File Offset: 0x002920CF
		public CoverPageProperties()
		{
		}

		// Token: 0x0600F07E RID: 61566 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CoverPageProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F07F RID: 61567 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CoverPageProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F080 RID: 61568 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CoverPageProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F081 RID: 61569 RVA: 0x002D0B48 File Offset: 0x002CED48
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (36 == namespaceId && "PublishDate" == name)
			{
				return new PublishDate();
			}
			if (36 == namespaceId && "Abstract" == name)
			{
				return new DocumentAbstract();
			}
			if (36 == namespaceId && "CompanyAddress" == name)
			{
				return new CompanyAddress();
			}
			if (36 == namespaceId && "CompanyPhone" == name)
			{
				return new CompanyPhoneNumber();
			}
			if (36 == namespaceId && "CompanyFax" == name)
			{
				return new CompanyFaxNumber();
			}
			if (36 == namespaceId && "CompanyEmail" == name)
			{
				return new CompanyEmailAddress();
			}
			return null;
		}

		// Token: 0x17004110 RID: 16656
		// (get) Token: 0x0600F082 RID: 61570 RVA: 0x002D0BE6 File Offset: 0x002CEDE6
		internal override string[] ElementTagNames
		{
			get
			{
				return CoverPageProperties.eleTagNames;
			}
		}

		// Token: 0x17004111 RID: 16657
		// (get) Token: 0x0600F083 RID: 61571 RVA: 0x002D0BED File Offset: 0x002CEDED
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CoverPageProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004112 RID: 16658
		// (get) Token: 0x0600F084 RID: 61572 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004113 RID: 16659
		// (get) Token: 0x0600F085 RID: 61573 RVA: 0x002D0BF4 File Offset: 0x002CEDF4
		// (set) Token: 0x0600F086 RID: 61574 RVA: 0x002D0BFD File Offset: 0x002CEDFD
		public PublishDate PublishDate
		{
			get
			{
				return base.GetElement<PublishDate>(0);
			}
			set
			{
				base.SetElement<PublishDate>(0, value);
			}
		}

		// Token: 0x17004114 RID: 16660
		// (get) Token: 0x0600F087 RID: 61575 RVA: 0x002D0C07 File Offset: 0x002CEE07
		// (set) Token: 0x0600F088 RID: 61576 RVA: 0x002D0C10 File Offset: 0x002CEE10
		public DocumentAbstract DocumentAbstract
		{
			get
			{
				return base.GetElement<DocumentAbstract>(1);
			}
			set
			{
				base.SetElement<DocumentAbstract>(1, value);
			}
		}

		// Token: 0x17004115 RID: 16661
		// (get) Token: 0x0600F089 RID: 61577 RVA: 0x002D0C1A File Offset: 0x002CEE1A
		// (set) Token: 0x0600F08A RID: 61578 RVA: 0x002D0C23 File Offset: 0x002CEE23
		public CompanyAddress CompanyAddress
		{
			get
			{
				return base.GetElement<CompanyAddress>(2);
			}
			set
			{
				base.SetElement<CompanyAddress>(2, value);
			}
		}

		// Token: 0x17004116 RID: 16662
		// (get) Token: 0x0600F08B RID: 61579 RVA: 0x002D0C2D File Offset: 0x002CEE2D
		// (set) Token: 0x0600F08C RID: 61580 RVA: 0x002D0C36 File Offset: 0x002CEE36
		public CompanyPhoneNumber CompanyPhoneNumber
		{
			get
			{
				return base.GetElement<CompanyPhoneNumber>(3);
			}
			set
			{
				base.SetElement<CompanyPhoneNumber>(3, value);
			}
		}

		// Token: 0x17004117 RID: 16663
		// (get) Token: 0x0600F08D RID: 61581 RVA: 0x002D0C40 File Offset: 0x002CEE40
		// (set) Token: 0x0600F08E RID: 61582 RVA: 0x002D0C49 File Offset: 0x002CEE49
		public CompanyFaxNumber CompanyFaxNumber
		{
			get
			{
				return base.GetElement<CompanyFaxNumber>(4);
			}
			set
			{
				base.SetElement<CompanyFaxNumber>(4, value);
			}
		}

		// Token: 0x17004118 RID: 16664
		// (get) Token: 0x0600F08F RID: 61583 RVA: 0x002D0C53 File Offset: 0x002CEE53
		// (set) Token: 0x0600F090 RID: 61584 RVA: 0x002D0C5C File Offset: 0x002CEE5C
		public CompanyEmailAddress CompanyEmailAddress
		{
			get
			{
				return base.GetElement<CompanyEmailAddress>(5);
			}
			set
			{
				base.SetElement<CompanyEmailAddress>(5, value);
			}
		}

		// Token: 0x0600F091 RID: 61585 RVA: 0x002D0C66 File Offset: 0x002CEE66
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CoverPageProperties>(deep);
		}

		// Token: 0x0400707D RID: 28797
		private const string tagName = "CoverPageProperties";

		// Token: 0x0400707E RID: 28798
		private const byte tagNsId = 36;

		// Token: 0x0400707F RID: 28799
		internal const int ElementTypeIdConst = 12621;

		// Token: 0x04007080 RID: 28800
		private static readonly string[] eleTagNames = new string[] { "PublishDate", "Abstract", "CompanyAddress", "CompanyPhone", "CompanyFax", "CompanyEmail" };

		// Token: 0x04007081 RID: 28801
		private static readonly byte[] eleNamespaceIds = new byte[] { 36, 36, 36, 36, 36, 36 };
	}
}
