using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D8C RID: 11660
	[GeneratedCode("DomGen", "2.0")]
	internal class NoProof : OnOffType
	{
		// Token: 0x17008740 RID: 34624
		// (get) Token: 0x06018D40 RID: 101696 RVA: 0x00344CC4 File Offset: 0x00342EC4
		public override string LocalName
		{
			get
			{
				return "noProof";
			}
		}

		// Token: 0x17008741 RID: 34625
		// (get) Token: 0x06018D41 RID: 101697 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008742 RID: 34626
		// (get) Token: 0x06018D42 RID: 101698 RVA: 0x00344CCB File Offset: 0x00342ECB
		internal override int ElementTypeId
		{
			get
			{
				return 11589;
			}
		}

		// Token: 0x06018D43 RID: 101699 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D45 RID: 101701 RVA: 0x00344CD2 File Offset: 0x00342ED2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoProof>(deep);
		}

		// Token: 0x0400A50F RID: 42255
		private const string tagName = "noProof";

		// Token: 0x0400A510 RID: 42256
		private const byte tagNsId = 23;

		// Token: 0x0400A511 RID: 42257
		internal const int ElementTypeIdConst = 11589;
	}
}
