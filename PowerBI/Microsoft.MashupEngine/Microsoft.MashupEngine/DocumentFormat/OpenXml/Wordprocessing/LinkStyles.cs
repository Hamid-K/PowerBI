using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DC0 RID: 11712
	[GeneratedCode("DomGen", "2.0")]
	internal class LinkStyles : OnOffType
	{
		// Token: 0x170087DC RID: 34780
		// (get) Token: 0x06018E78 RID: 102008 RVA: 0x0034514D File Offset: 0x0034334D
		public override string LocalName
		{
			get
			{
				return "linkStyles";
			}
		}

		// Token: 0x170087DD RID: 34781
		// (get) Token: 0x06018E79 RID: 102009 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087DE RID: 34782
		// (get) Token: 0x06018E7A RID: 102010 RVA: 0x00345154 File Offset: 0x00343354
		internal override int ElementTypeId
		{
			get
			{
				return 11983;
			}
		}

		// Token: 0x06018E7B RID: 102011 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E7D RID: 102013 RVA: 0x0034515B File Offset: 0x0034335B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LinkStyles>(deep);
		}

		// Token: 0x0400A5AB RID: 42411
		private const string tagName = "linkStyles";

		// Token: 0x0400A5AC RID: 42412
		private const byte tagNsId = 23;

		// Token: 0x0400A5AD RID: 42413
		internal const int ElementTypeIdConst = 11983;
	}
}
