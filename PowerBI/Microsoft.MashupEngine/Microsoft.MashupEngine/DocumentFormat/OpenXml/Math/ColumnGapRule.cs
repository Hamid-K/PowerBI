using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029A5 RID: 10661
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnGapRule : SpacingRuleType
	{
		// Token: 0x17006D34 RID: 27956
		// (get) Token: 0x06015326 RID: 86822 RVA: 0x0031CC2B File Offset: 0x0031AE2B
		public override string LocalName
		{
			get
			{
				return "cGpRule";
			}
		}

		// Token: 0x17006D35 RID: 27957
		// (get) Token: 0x06015327 RID: 86823 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D36 RID: 27958
		// (get) Token: 0x06015328 RID: 86824 RVA: 0x0031CC32 File Offset: 0x0031AE32
		internal override int ElementTypeId
		{
			get
			{
				return 10917;
			}
		}

		// Token: 0x06015329 RID: 86825 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601532B RID: 86827 RVA: 0x0031CC39 File Offset: 0x0031AE39
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnGapRule>(deep);
		}

		// Token: 0x0400920B RID: 37387
		private const string tagName = "cGpRule";

		// Token: 0x0400920C RID: 37388
		private const byte tagNsId = 21;

		// Token: 0x0400920D RID: 37389
		internal const int ElementTypeIdConst = 10917;
	}
}
