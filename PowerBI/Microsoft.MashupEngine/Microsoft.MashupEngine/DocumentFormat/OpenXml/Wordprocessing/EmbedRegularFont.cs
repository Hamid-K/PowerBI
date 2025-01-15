using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FB7 RID: 12215
	[GeneratedCode("DomGen", "2.0")]
	internal class EmbedRegularFont : FontRelationshipType
	{
		// Token: 0x170093AB RID: 37803
		// (get) Token: 0x0601A7BB RID: 108475 RVA: 0x00362EEC File Offset: 0x003610EC
		public override string LocalName
		{
			get
			{
				return "embedRegular";
			}
		}

		// Token: 0x170093AC RID: 37804
		// (get) Token: 0x0601A7BC RID: 108476 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093AD RID: 37805
		// (get) Token: 0x0601A7BD RID: 108477 RVA: 0x00362EF3 File Offset: 0x003610F3
		internal override int ElementTypeId
		{
			get
			{
				return 11922;
			}
		}

		// Token: 0x0601A7BE RID: 108478 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A7C0 RID: 108480 RVA: 0x00362F02 File Offset: 0x00361102
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EmbedRegularFont>(deep);
		}

		// Token: 0x0400AD25 RID: 44325
		private const string tagName = "embedRegular";

		// Token: 0x0400AD26 RID: 44326
		private const byte tagNsId = 23;

		// Token: 0x0400AD27 RID: 44327
		internal const int ElementTypeIdConst = 11922;
	}
}
