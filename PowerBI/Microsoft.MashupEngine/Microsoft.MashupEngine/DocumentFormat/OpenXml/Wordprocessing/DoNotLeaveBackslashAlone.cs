using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DE9 RID: 11753
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotLeaveBackslashAlone : OnOffType
	{
		// Token: 0x17008857 RID: 34903
		// (get) Token: 0x06018F6E RID: 102254 RVA: 0x003454FC File Offset: 0x003436FC
		public override string LocalName
		{
			get
			{
				return "doNotLeaveBackslashAlone";
			}
		}

		// Token: 0x17008858 RID: 34904
		// (get) Token: 0x06018F6F RID: 102255 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008859 RID: 34905
		// (get) Token: 0x06018F70 RID: 102256 RVA: 0x00345503 File Offset: 0x00343703
		internal override int ElementTypeId
		{
			get
			{
				return 12063;
			}
		}

		// Token: 0x06018F71 RID: 102257 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F73 RID: 102259 RVA: 0x0034550A File Offset: 0x0034370A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotLeaveBackslashAlone>(deep);
		}

		// Token: 0x0400A626 RID: 42534
		private const string tagName = "doNotLeaveBackslashAlone";

		// Token: 0x0400A627 RID: 42535
		private const byte tagNsId = 23;

		// Token: 0x0400A628 RID: 42536
		internal const int ElementTypeIdConst = 12063;
	}
}
