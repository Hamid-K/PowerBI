using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E69 RID: 11881
	[GeneratedCode("DomGen", "2.0")]
	internal class ContinuationSeparatorMark : EmptyType
	{
		// Token: 0x17008A7C RID: 35452
		// (get) Token: 0x060193D9 RID: 103385 RVA: 0x00347B21 File Offset: 0x00345D21
		public override string LocalName
		{
			get
			{
				return "continuationSeparator";
			}
		}

		// Token: 0x17008A7D RID: 35453
		// (get) Token: 0x060193DA RID: 103386 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A7E RID: 35454
		// (get) Token: 0x060193DB RID: 103387 RVA: 0x00347B28 File Offset: 0x00345D28
		internal override int ElementTypeId
		{
			get
			{
				return 11560;
			}
		}

		// Token: 0x060193DC RID: 103388 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193DE RID: 103390 RVA: 0x00347B2F File Offset: 0x00345D2F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContinuationSeparatorMark>(deep);
		}

		// Token: 0x0400A7D3 RID: 42963
		private const string tagName = "continuationSeparator";

		// Token: 0x0400A7D4 RID: 42964
		private const byte tagNsId = 23;

		// Token: 0x0400A7D5 RID: 42965
		internal const int ElementTypeIdConst = 11560;
	}
}
