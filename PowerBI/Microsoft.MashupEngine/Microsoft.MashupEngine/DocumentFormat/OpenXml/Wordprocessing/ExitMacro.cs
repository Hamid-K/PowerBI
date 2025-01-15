using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F35 RID: 12085
	[GeneratedCode("DomGen", "2.0")]
	internal class ExitMacro : MacroNameType
	{
		// Token: 0x17008F7C RID: 36732
		// (get) Token: 0x06019ECB RID: 106187 RVA: 0x00359EE7 File Offset: 0x003580E7
		public override string LocalName
		{
			get
			{
				return "exitMacro";
			}
		}

		// Token: 0x17008F7D RID: 36733
		// (get) Token: 0x06019ECC RID: 106188 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F7E RID: 36734
		// (get) Token: 0x06019ECD RID: 106189 RVA: 0x00359EEE File Offset: 0x003580EE
		internal override int ElementTypeId
		{
			get
			{
				return 11730;
			}
		}

		// Token: 0x06019ECE RID: 106190 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019ED0 RID: 106192 RVA: 0x00359EF5 File Offset: 0x003580F5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExitMacro>(deep);
		}

		// Token: 0x0400AAEA RID: 43754
		private const string tagName = "exitMacro";

		// Token: 0x0400AAEB RID: 43755
		private const byte tagNsId = 23;

		// Token: 0x0400AAEC RID: 43756
		internal const int ElementTypeIdConst = 11730;
	}
}
