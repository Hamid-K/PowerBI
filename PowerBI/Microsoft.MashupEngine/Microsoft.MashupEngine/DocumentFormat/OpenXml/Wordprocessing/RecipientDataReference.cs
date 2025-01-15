using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E50 RID: 11856
	[GeneratedCode("DomGen", "2.0")]
	internal class RecipientDataReference : RelationshipType
	{
		// Token: 0x17008A30 RID: 35376
		// (get) Token: 0x06019335 RID: 103221 RVA: 0x002EC0C0 File Offset: 0x002EA2C0
		public override string LocalName
		{
			get
			{
				return "recipientData";
			}
		}

		// Token: 0x17008A31 RID: 35377
		// (get) Token: 0x06019336 RID: 103222 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A32 RID: 35378
		// (get) Token: 0x06019337 RID: 103223 RVA: 0x0034779A File Offset: 0x0034599A
		internal override int ElementTypeId
		{
			get
			{
				return 11811;
			}
		}

		// Token: 0x06019338 RID: 103224 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601933A RID: 103226 RVA: 0x003477A1 File Offset: 0x003459A1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RecipientDataReference>(deep);
		}

		// Token: 0x0400A78A RID: 42890
		private const string tagName = "recipientData";

		// Token: 0x0400A78B RID: 42891
		private const byte tagNsId = 23;

		// Token: 0x0400A78C RID: 42892
		internal const int ElementTypeIdConst = 11811;
	}
}
