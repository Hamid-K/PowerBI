using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C8C RID: 11404
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConditionalFormattingRuleExtension))]
	internal class ConditionalFormattingRuleExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170083A3 RID: 33699
		// (get) Token: 0x060184FD RID: 99581 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170083A4 RID: 33700
		// (get) Token: 0x060184FE RID: 99582 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170083A5 RID: 33701
		// (get) Token: 0x060184FF RID: 99583 RVA: 0x00340647 File Offset: 0x0033E847
		internal override int ElementTypeId
		{
			get
			{
				return 11383;
			}
		}

		// Token: 0x06018500 RID: 99584 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018501 RID: 99585 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormattingRuleExtensionList()
		{
		}

		// Token: 0x06018502 RID: 99586 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormattingRuleExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018503 RID: 99587 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormattingRuleExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018504 RID: 99588 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormattingRuleExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018505 RID: 99589 RVA: 0x0034064E File Offset: 0x0033E84E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new ConditionalFormattingRuleExtension();
			}
			return null;
		}

		// Token: 0x06018506 RID: 99590 RVA: 0x00340669 File Offset: 0x0033E869
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormattingRuleExtensionList>(deep);
		}

		// Token: 0x04009FBF RID: 40895
		private const string tagName = "extLst";

		// Token: 0x04009FC0 RID: 40896
		private const byte tagNsId = 22;

		// Token: 0x04009FC1 RID: 40897
		internal const int ElementTypeIdConst = 11383;
	}
}
