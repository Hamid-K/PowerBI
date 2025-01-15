using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E1E RID: 11806
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotBreakConstrainedForcedTable : OnOffType
	{
		// Token: 0x170088F6 RID: 35062
		// (get) Token: 0x060190AC RID: 102572 RVA: 0x003459BF File Offset: 0x00343BBF
		public override string LocalName
		{
			get
			{
				return "doNotBreakConstrainedForcedTable";
			}
		}

		// Token: 0x170088F7 RID: 35063
		// (get) Token: 0x060190AD RID: 102573 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088F8 RID: 35064
		// (get) Token: 0x060190AE RID: 102574 RVA: 0x003459C6 File Offset: 0x00343BC6
		internal override int ElementTypeId
		{
			get
			{
				return 12116;
			}
		}

		// Token: 0x060190AF RID: 102575 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060190B1 RID: 102577 RVA: 0x003459CD File Offset: 0x00343BCD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotBreakConstrainedForcedTable>(deep);
		}

		// Token: 0x0400A6C5 RID: 42693
		private const string tagName = "doNotBreakConstrainedForcedTable";

		// Token: 0x0400A6C6 RID: 42694
		private const byte tagNsId = 23;

		// Token: 0x0400A6C7 RID: 42695
		internal const int ElementTypeIdConst = 12116;
	}
}
