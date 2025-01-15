using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D9D RID: 11677
	[GeneratedCode("DomGen", "2.0")]
	internal class DynamicAddress : OnOffType
	{
		// Token: 0x17008773 RID: 34675
		// (get) Token: 0x06018DA6 RID: 101798 RVA: 0x00344E28 File Offset: 0x00343028
		public override string LocalName
		{
			get
			{
				return "dynamicAddress";
			}
		}

		// Token: 0x17008774 RID: 34676
		// (get) Token: 0x06018DA7 RID: 101799 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008775 RID: 34677
		// (get) Token: 0x06018DA8 RID: 101800 RVA: 0x00344E2F File Offset: 0x0034302F
		internal override int ElementTypeId
		{
			get
			{
				return 11803;
			}
		}

		// Token: 0x06018DA9 RID: 101801 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DAB RID: 101803 RVA: 0x00344E36 File Offset: 0x00343036
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DynamicAddress>(deep);
		}

		// Token: 0x0400A542 RID: 42306
		private const string tagName = "dynamicAddress";

		// Token: 0x0400A543 RID: 42307
		private const byte tagNsId = 23;

		// Token: 0x0400A544 RID: 42308
		internal const int ElementTypeIdConst = 11803;
	}
}
