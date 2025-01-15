using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EA2 RID: 11938
	[GeneratedCode("DomGen", "2.0")]
	internal class BetweenBorder : BorderType
	{
		// Token: 0x17008B81 RID: 35713
		// (get) Token: 0x060195F4 RID: 103924 RVA: 0x00349071 File Offset: 0x00347271
		public override string LocalName
		{
			get
			{
				return "between";
			}
		}

		// Token: 0x17008B82 RID: 35714
		// (get) Token: 0x060195F5 RID: 103925 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B83 RID: 35715
		// (get) Token: 0x060195F6 RID: 103926 RVA: 0x00349078 File Offset: 0x00347278
		internal override int ElementTypeId
		{
			get
			{
				return 11719;
			}
		}

		// Token: 0x060195F7 RID: 103927 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060195F9 RID: 103929 RVA: 0x0034907F File Offset: 0x0034727F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BetweenBorder>(deep);
		}

		// Token: 0x0400A89C RID: 43164
		private const string tagName = "between";

		// Token: 0x0400A89D RID: 43165
		private const byte tagNsId = 23;

		// Token: 0x0400A89E RID: 43166
		internal const int ElementTypeIdConst = 11719;
	}
}
