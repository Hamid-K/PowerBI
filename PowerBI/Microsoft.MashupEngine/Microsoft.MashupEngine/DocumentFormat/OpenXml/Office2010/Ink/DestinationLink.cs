using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Ink
{
	// Token: 0x0200226A RID: 8810
	[GeneratedCode("DomGen", "2.0")]
	internal class DestinationLink : ContextLinkType
	{
		// Token: 0x17003CD9 RID: 15577
		// (get) Token: 0x0600E7A4 RID: 59300 RVA: 0x002C8732 File Offset: 0x002C6932
		public override string LocalName
		{
			get
			{
				return "destinationLink";
			}
		}

		// Token: 0x17003CDA RID: 15578
		// (get) Token: 0x0600E7A5 RID: 59301 RVA: 0x002C826A File Offset: 0x002C646A
		internal override byte NamespaceId
		{
			get
			{
				return 45;
			}
		}

		// Token: 0x17003CDB RID: 15579
		// (get) Token: 0x0600E7A6 RID: 59302 RVA: 0x002C8739 File Offset: 0x002C6939
		internal override int ElementTypeId
		{
			get
			{
				return 12690;
			}
		}

		// Token: 0x0600E7A7 RID: 59303 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E7A9 RID: 59305 RVA: 0x002C8740 File Offset: 0x002C6940
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DestinationLink>(deep);
		}

		// Token: 0x04006F5A RID: 28506
		private const string tagName = "destinationLink";

		// Token: 0x04006F5B RID: 28507
		private const byte tagNsId = 45;

		// Token: 0x04006F5C RID: 28508
		internal const int ElementTypeIdConst = 12690;
	}
}
