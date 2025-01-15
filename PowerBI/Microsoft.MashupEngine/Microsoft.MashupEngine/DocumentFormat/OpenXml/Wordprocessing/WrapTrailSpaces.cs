using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DFE RID: 11774
	[GeneratedCode("DomGen", "2.0")]
	internal class WrapTrailSpaces : OnOffType
	{
		// Token: 0x17008896 RID: 34966
		// (get) Token: 0x06018FEC RID: 102380 RVA: 0x003456DF File Offset: 0x003438DF
		public override string LocalName
		{
			get
			{
				return "wrapTrailSpaces";
			}
		}

		// Token: 0x17008897 RID: 34967
		// (get) Token: 0x06018FED RID: 102381 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008898 RID: 34968
		// (get) Token: 0x06018FEE RID: 102382 RVA: 0x003456E6 File Offset: 0x003438E6
		internal override int ElementTypeId
		{
			get
			{
				return 12084;
			}
		}

		// Token: 0x06018FEF RID: 102383 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FF1 RID: 102385 RVA: 0x003456ED File Offset: 0x003438ED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WrapTrailSpaces>(deep);
		}

		// Token: 0x0400A665 RID: 42597
		private const string tagName = "wrapTrailSpaces";

		// Token: 0x0400A666 RID: 42598
		private const byte tagNsId = 23;

		// Token: 0x0400A667 RID: 42599
		internal const int ElementTypeIdConst = 12084;
	}
}
