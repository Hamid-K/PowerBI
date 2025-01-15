using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E17 RID: 11799
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotSuppressIndentation : OnOffType
	{
		// Token: 0x170088E1 RID: 35041
		// (get) Token: 0x06019082 RID: 102530 RVA: 0x0034591E File Offset: 0x00343B1E
		public override string LocalName
		{
			get
			{
				return "doNotSuppressIndentation";
			}
		}

		// Token: 0x170088E2 RID: 35042
		// (get) Token: 0x06019083 RID: 102531 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088E3 RID: 35043
		// (get) Token: 0x06019084 RID: 102532 RVA: 0x00345925 File Offset: 0x00343B25
		internal override int ElementTypeId
		{
			get
			{
				return 12109;
			}
		}

		// Token: 0x06019085 RID: 102533 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019087 RID: 102535 RVA: 0x0034592C File Offset: 0x00343B2C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotSuppressIndentation>(deep);
		}

		// Token: 0x0400A6B0 RID: 42672
		private const string tagName = "doNotSuppressIndentation";

		// Token: 0x0400A6B1 RID: 42673
		private const byte tagNsId = 23;

		// Token: 0x0400A6B2 RID: 42674
		internal const int ElementTypeIdConst = 12109;
	}
}
