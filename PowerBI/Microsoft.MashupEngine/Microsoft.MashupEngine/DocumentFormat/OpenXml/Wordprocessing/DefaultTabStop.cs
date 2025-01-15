using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FE5 RID: 12261
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultTabStop : NonNegativeShortType
	{
		// Token: 0x17009509 RID: 38153
		// (get) Token: 0x0601AAB0 RID: 109232 RVA: 0x00365C2C File Offset: 0x00363E2C
		public override string LocalName
		{
			get
			{
				return "defaultTabStop";
			}
		}

		// Token: 0x1700950A RID: 38154
		// (get) Token: 0x0601AAB1 RID: 109233 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700950B RID: 38155
		// (get) Token: 0x0601AAB2 RID: 109234 RVA: 0x00365C33 File Offset: 0x00363E33
		internal override int ElementTypeId
		{
			get
			{
				return 11996;
			}
		}

		// Token: 0x0601AAB3 RID: 109235 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AAB5 RID: 109237 RVA: 0x00365C42 File Offset: 0x00363E42
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultTabStop>(deep);
		}

		// Token: 0x0400ADE5 RID: 44517
		private const string tagName = "defaultTabStop";

		// Token: 0x0400ADE6 RID: 44518
		private const byte tagNsId = 23;

		// Token: 0x0400ADE7 RID: 44519
		internal const int ElementTypeIdConst = 11996;
	}
}
