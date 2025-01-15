using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E55 RID: 11861
	[GeneratedCode("DomGen", "2.0")]
	internal class MovieReference : RelationshipType
	{
		// Token: 0x17008A3F RID: 35391
		// (get) Token: 0x06019353 RID: 103251 RVA: 0x00347806 File Offset: 0x00345A06
		public override string LocalName
		{
			get
			{
				return "movie";
			}
		}

		// Token: 0x17008A40 RID: 35392
		// (get) Token: 0x06019354 RID: 103252 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A41 RID: 35393
		// (get) Token: 0x06019355 RID: 103253 RVA: 0x0034780D File Offset: 0x00345A0D
		internal override int ElementTypeId
		{
			get
			{
				return 12159;
			}
		}

		// Token: 0x06019356 RID: 103254 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019358 RID: 103256 RVA: 0x00347814 File Offset: 0x00345A14
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MovieReference>(deep);
		}

		// Token: 0x0400A799 RID: 42905
		private const string tagName = "movie";

		// Token: 0x0400A79A RID: 42906
		private const byte tagNsId = 23;

		// Token: 0x0400A79B RID: 42907
		internal const int ElementTypeIdConst = 12159;
	}
}
