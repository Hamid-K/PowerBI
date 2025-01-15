using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DDD RID: 11741
	[GeneratedCode("DomGen", "2.0")]
	internal class UICompatibleWith97To2003 : OnOffType
	{
		// Token: 0x17008833 RID: 34867
		// (get) Token: 0x06018F26 RID: 102182 RVA: 0x003453E8 File Offset: 0x003435E8
		public override string LocalName
		{
			get
			{
				return "uiCompat97To2003";
			}
		}

		// Token: 0x17008834 RID: 34868
		// (get) Token: 0x06018F27 RID: 102183 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008835 RID: 34869
		// (get) Token: 0x06018F28 RID: 102184 RVA: 0x003453EF File Offset: 0x003435EF
		internal override int ElementTypeId
		{
			get
			{
				return 12041;
			}
		}

		// Token: 0x06018F29 RID: 102185 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F2B RID: 102187 RVA: 0x003453F6 File Offset: 0x003435F6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UICompatibleWith97To2003>(deep);
		}

		// Token: 0x0400A602 RID: 42498
		private const string tagName = "uiCompat97To2003";

		// Token: 0x0400A603 RID: 42499
		private const byte tagNsId = 23;

		// Token: 0x0400A604 RID: 42500
		internal const int ElementTypeIdConst = 12041;
	}
}
