using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E5D RID: 11869
	[GeneratedCode("DomGen", "2.0")]
	internal class NoBreakHyphen : EmptyType
	{
		// Token: 0x17008A58 RID: 35416
		// (get) Token: 0x06019391 RID: 103313 RVA: 0x00347A0C File Offset: 0x00345C0C
		public override string LocalName
		{
			get
			{
				return "noBreakHyphen";
			}
		}

		// Token: 0x17008A59 RID: 35417
		// (get) Token: 0x06019392 RID: 103314 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A5A RID: 35418
		// (get) Token: 0x06019393 RID: 103315 RVA: 0x00347A13 File Offset: 0x00345C13
		internal override int ElementTypeId
		{
			get
			{
				return 11548;
			}
		}

		// Token: 0x06019394 RID: 103316 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019396 RID: 103318 RVA: 0x00347A22 File Offset: 0x00345C22
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoBreakHyphen>(deep);
		}

		// Token: 0x0400A7AF RID: 42927
		private const string tagName = "noBreakHyphen";

		// Token: 0x0400A7B0 RID: 42928
		private const byte tagNsId = 23;

		// Token: 0x0400A7B1 RID: 42929
		internal const int ElementTypeIdConst = 11548;
	}
}
