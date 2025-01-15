using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DA8 RID: 11688
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotUseLongFileNames : OnOffType
	{
		// Token: 0x17008794 RID: 34708
		// (get) Token: 0x06018DE8 RID: 101864 RVA: 0x00344F25 File Offset: 0x00343125
		public override string LocalName
		{
			get
			{
				return "doNotUseLongFileNames";
			}
		}

		// Token: 0x17008795 RID: 34709
		// (get) Token: 0x06018DE9 RID: 101865 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008796 RID: 34710
		// (get) Token: 0x06018DEA RID: 101866 RVA: 0x00344F2C File Offset: 0x0034312C
		internal override int ElementTypeId
		{
			get
			{
				return 11844;
			}
		}

		// Token: 0x06018DEB RID: 101867 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DED RID: 101869 RVA: 0x00344F33 File Offset: 0x00343133
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotUseLongFileNames>(deep);
		}

		// Token: 0x0400A563 RID: 42339
		private const string tagName = "doNotUseLongFileNames";

		// Token: 0x0400A564 RID: 42340
		private const byte tagNsId = 23;

		// Token: 0x0400A565 RID: 42341
		internal const int ElementTypeIdConst = 11844;
	}
}
