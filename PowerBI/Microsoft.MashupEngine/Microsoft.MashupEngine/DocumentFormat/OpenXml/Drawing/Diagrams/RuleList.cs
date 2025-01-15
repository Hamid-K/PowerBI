using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200266D RID: 9837
	[ChildElementInfo(typeof(Rule))]
	[GeneratedCode("DomGen", "2.0")]
	internal class RuleList : OpenXmlCompositeElement
	{
		// Token: 0x17005C1D RID: 23581
		// (get) Token: 0x06012C11 RID: 76817 RVA: 0x002FED9C File Offset: 0x002FCF9C
		public override string LocalName
		{
			get
			{
				return "ruleLst";
			}
		}

		// Token: 0x17005C1E RID: 23582
		// (get) Token: 0x06012C12 RID: 76818 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C1F RID: 23583
		// (get) Token: 0x06012C13 RID: 76819 RVA: 0x002FEDA3 File Offset: 0x002FCFA3
		internal override int ElementTypeId
		{
			get
			{
				return 10654;
			}
		}

		// Token: 0x06012C14 RID: 76820 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012C15 RID: 76821 RVA: 0x00293ECF File Offset: 0x002920CF
		public RuleList()
		{
		}

		// Token: 0x06012C16 RID: 76822 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RuleList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C17 RID: 76823 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RuleList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C18 RID: 76824 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RuleList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012C19 RID: 76825 RVA: 0x002FEDAA File Offset: 0x002FCFAA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "rule" == name)
			{
				return new Rule();
			}
			return null;
		}

		// Token: 0x06012C1A RID: 76826 RVA: 0x002FEDC5 File Offset: 0x002FCFC5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RuleList>(deep);
		}

		// Token: 0x04008179 RID: 33145
		private const string tagName = "ruleLst";

		// Token: 0x0400817A RID: 33146
		private const byte tagNsId = 14;

		// Token: 0x0400817B RID: 33147
		internal const int ElementTypeIdConst = 10654;
	}
}
