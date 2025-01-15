using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F3D RID: 12093
	[GeneratedCode("DomGen", "2.0")]
	internal class ListEntryFormField : String255Type
	{
		// Token: 0x17008FAB RID: 36779
		// (get) Token: 0x06019F32 RID: 106290 RVA: 0x0035A34C File Offset: 0x0035854C
		public override string LocalName
		{
			get
			{
				return "listEntry";
			}
		}

		// Token: 0x17008FAC RID: 36780
		// (get) Token: 0x06019F33 RID: 106291 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FAD RID: 36781
		// (get) Token: 0x06019F34 RID: 106292 RVA: 0x0035A353 File Offset: 0x00358553
		internal override int ElementTypeId
		{
			get
			{
				return 11742;
			}
		}

		// Token: 0x06019F35 RID: 106293 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019F37 RID: 106295 RVA: 0x0035A362 File Offset: 0x00358562
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ListEntryFormField>(deep);
		}

		// Token: 0x0400AB0B RID: 43787
		private const string tagName = "listEntry";

		// Token: 0x0400AB0C RID: 43788
		private const byte tagNsId = 23;

		// Token: 0x0400AB0D RID: 43789
		internal const int ElementTypeIdConst = 11742;
	}
}
