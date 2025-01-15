using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200251A RID: 9498
	[GeneratedCode("DomGen", "2.0")]
	internal class InvertIfNegative : BooleanType
	{
		// Token: 0x17005467 RID: 21607
		// (get) Token: 0x06011AC9 RID: 72393 RVA: 0x002F137F File Offset: 0x002EF57F
		public override string LocalName
		{
			get
			{
				return "invertIfNegative";
			}
		}

		// Token: 0x17005468 RID: 21608
		// (get) Token: 0x06011ACA RID: 72394 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005469 RID: 21609
		// (get) Token: 0x06011ACB RID: 72395 RVA: 0x002F1386 File Offset: 0x002EF586
		internal override int ElementTypeId
		{
			get
			{
				return 10431;
			}
		}

		// Token: 0x06011ACC RID: 72396 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011ACE RID: 72398 RVA: 0x002F138D File Offset: 0x002EF58D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InvertIfNegative>(deep);
		}

		// Token: 0x04007BE2 RID: 31714
		private const string tagName = "invertIfNegative";

		// Token: 0x04007BE3 RID: 31715
		private const byte tagNsId = 11;

		// Token: 0x04007BE4 RID: 31716
		internal const int ElementTypeIdConst = 10431;
	}
}
