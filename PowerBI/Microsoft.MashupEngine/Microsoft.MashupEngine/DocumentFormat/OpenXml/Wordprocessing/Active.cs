using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D9C RID: 11676
	[GeneratedCode("DomGen", "2.0")]
	internal class Active : OnOffType
	{
		// Token: 0x17008770 RID: 34672
		// (get) Token: 0x06018DA0 RID: 101792 RVA: 0x002EBFA8 File Offset: 0x002EA1A8
		public override string LocalName
		{
			get
			{
				return "active";
			}
		}

		// Token: 0x17008771 RID: 34673
		// (get) Token: 0x06018DA1 RID: 101793 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008772 RID: 34674
		// (get) Token: 0x06018DA2 RID: 101794 RVA: 0x00344E18 File Offset: 0x00343018
		internal override int ElementTypeId
		{
			get
			{
				return 11796;
			}
		}

		// Token: 0x06018DA3 RID: 101795 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DA5 RID: 101797 RVA: 0x00344E1F File Offset: 0x0034301F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Active>(deep);
		}

		// Token: 0x0400A53F RID: 42303
		private const string tagName = "active";

		// Token: 0x0400A540 RID: 42304
		private const byte tagNsId = 23;

		// Token: 0x0400A541 RID: 42305
		internal const int ElementTypeIdConst = 11796;
	}
}
