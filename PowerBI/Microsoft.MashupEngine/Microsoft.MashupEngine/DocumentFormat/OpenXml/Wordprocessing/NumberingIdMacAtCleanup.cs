using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E3A RID: 11834
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingIdMacAtCleanup : DecimalNumberType
	{
		// Token: 0x1700899A RID: 35226
		// (get) Token: 0x060191FD RID: 102909 RVA: 0x003468D6 File Offset: 0x00344AD6
		public override string LocalName
		{
			get
			{
				return "numIdMacAtCleanup";
			}
		}

		// Token: 0x1700899B RID: 35227
		// (get) Token: 0x060191FE RID: 102910 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700899C RID: 35228
		// (get) Token: 0x060191FF RID: 102911 RVA: 0x003468DD File Offset: 0x00344ADD
		internal override int ElementTypeId
		{
			get
			{
				return 11887;
			}
		}

		// Token: 0x06019200 RID: 102912 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019202 RID: 102914 RVA: 0x003468E4 File Offset: 0x00344AE4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingIdMacAtCleanup>(deep);
		}

		// Token: 0x0400A72C RID: 42796
		private const string tagName = "numIdMacAtCleanup";

		// Token: 0x0400A72D RID: 42797
		private const byte tagNsId = 23;

		// Token: 0x0400A72E RID: 42798
		internal const int ElementTypeIdConst = 11887;
	}
}
