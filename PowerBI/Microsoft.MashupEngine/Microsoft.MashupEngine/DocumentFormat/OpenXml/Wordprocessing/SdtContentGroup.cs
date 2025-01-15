using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E74 RID: 11892
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtContentGroup : EmptyType
	{
		// Token: 0x17008A9D RID: 35485
		// (get) Token: 0x0601941B RID: 103451 RVA: 0x002C29FF File Offset: 0x002C0BFF
		public override string LocalName
		{
			get
			{
				return "group";
			}
		}

		// Token: 0x17008A9E RID: 35486
		// (get) Token: 0x0601941C RID: 103452 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A9F RID: 35487
		// (get) Token: 0x0601941D RID: 103453 RVA: 0x00347C10 File Offset: 0x00345E10
		internal override int ElementTypeId
		{
			get
			{
				return 12157;
			}
		}

		// Token: 0x0601941E RID: 103454 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019420 RID: 103456 RVA: 0x00347C17 File Offset: 0x00345E17
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentGroup>(deep);
		}

		// Token: 0x0400A7F4 RID: 42996
		private const string tagName = "group";

		// Token: 0x0400A7F5 RID: 42997
		private const byte tagNsId = 23;

		// Token: 0x0400A7F6 RID: 42998
		internal const int ElementTypeIdConst = 12157;
	}
}
