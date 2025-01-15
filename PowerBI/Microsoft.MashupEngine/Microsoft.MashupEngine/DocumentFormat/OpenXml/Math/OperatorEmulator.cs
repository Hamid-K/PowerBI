using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200296C RID: 10604
	[GeneratedCode("DomGen", "2.0")]
	internal class OperatorEmulator : OnOffType
	{
		// Token: 0x17006C4F RID: 27727
		// (get) Token: 0x0601512C RID: 86316 RVA: 0x0031B419 File Offset: 0x00319619
		public override string LocalName
		{
			get
			{
				return "opEmu";
			}
		}

		// Token: 0x17006C50 RID: 27728
		// (get) Token: 0x0601512D RID: 86317 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C51 RID: 27729
		// (get) Token: 0x0601512E RID: 86318 RVA: 0x0031B420 File Offset: 0x00319620
		internal override int ElementTypeId
		{
			get
			{
				return 10876;
			}
		}

		// Token: 0x0601512F RID: 86319 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015131 RID: 86321 RVA: 0x0031B427 File Offset: 0x00319627
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OperatorEmulator>(deep);
		}

		// Token: 0x04009150 RID: 37200
		private const string tagName = "opEmu";

		// Token: 0x04009151 RID: 37201
		private const byte tagNsId = 21;

		// Token: 0x04009152 RID: 37202
		internal const int ElementTypeIdConst = 10876;
	}
}
