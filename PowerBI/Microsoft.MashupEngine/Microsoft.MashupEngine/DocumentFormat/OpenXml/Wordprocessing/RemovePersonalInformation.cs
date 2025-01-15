using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DAD RID: 11693
	[GeneratedCode("DomGen", "2.0")]
	internal class RemovePersonalInformation : OnOffType
	{
		// Token: 0x170087A3 RID: 34723
		// (get) Token: 0x06018E06 RID: 101894 RVA: 0x00344F98 File Offset: 0x00343198
		public override string LocalName
		{
			get
			{
				return "removePersonalInformation";
			}
		}

		// Token: 0x170087A4 RID: 34724
		// (get) Token: 0x06018E07 RID: 101895 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087A5 RID: 34725
		// (get) Token: 0x06018E08 RID: 101896 RVA: 0x00344F9F File Offset: 0x0034319F
		internal override int ElementTypeId
		{
			get
			{
				return 11961;
			}
		}

		// Token: 0x06018E09 RID: 101897 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E0B RID: 101899 RVA: 0x00344FA6 File Offset: 0x003431A6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RemovePersonalInformation>(deep);
		}

		// Token: 0x0400A572 RID: 42354
		private const string tagName = "removePersonalInformation";

		// Token: 0x0400A573 RID: 42355
		private const byte tagNsId = 23;

		// Token: 0x0400A574 RID: 42356
		internal const int ElementTypeIdConst = 11961;
	}
}
