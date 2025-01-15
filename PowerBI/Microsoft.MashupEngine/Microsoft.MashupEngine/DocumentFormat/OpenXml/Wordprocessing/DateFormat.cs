using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D52 RID: 11602
	[GeneratedCode("DomGen", "2.0")]
	internal class DateFormat : StringType
	{
		// Token: 0x17008692 RID: 34450
		// (get) Token: 0x06018BE3 RID: 101347 RVA: 0x0034478B File Offset: 0x0034298B
		public override string LocalName
		{
			get
			{
				return "dateFormat";
			}
		}

		// Token: 0x17008693 RID: 34451
		// (get) Token: 0x06018BE4 RID: 101348 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008694 RID: 34452
		// (get) Token: 0x06018BE5 RID: 101349 RVA: 0x00344792 File Offset: 0x00342992
		internal override int ElementTypeId
		{
			get
			{
				return 11761;
			}
		}

		// Token: 0x06018BE6 RID: 101350 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BE8 RID: 101352 RVA: 0x00344799 File Offset: 0x00342999
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DateFormat>(deep);
		}

		// Token: 0x0400A462 RID: 42082
		private const string tagName = "dateFormat";

		// Token: 0x0400A463 RID: 42083
		private const byte tagNsId = 23;

		// Token: 0x0400A464 RID: 42084
		internal const int ElementTypeIdConst = 11761;
	}
}
