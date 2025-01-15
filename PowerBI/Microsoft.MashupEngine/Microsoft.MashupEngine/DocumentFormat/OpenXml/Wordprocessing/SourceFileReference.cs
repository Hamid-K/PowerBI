using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E53 RID: 11859
	[GeneratedCode("DomGen", "2.0")]
	internal class SourceFileReference : RelationshipType
	{
		// Token: 0x17008A39 RID: 35385
		// (get) Token: 0x06019347 RID: 103239 RVA: 0x003477D8 File Offset: 0x003459D8
		public override string LocalName
		{
			get
			{
				return "sourceFileName";
			}
		}

		// Token: 0x17008A3A RID: 35386
		// (get) Token: 0x06019348 RID: 103240 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A3B RID: 35387
		// (get) Token: 0x06019349 RID: 103241 RVA: 0x003477DF File Offset: 0x003459DF
		internal override int ElementTypeId
		{
			get
			{
				return 11850;
			}
		}

		// Token: 0x0601934A RID: 103242 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601934C RID: 103244 RVA: 0x003477E6 File Offset: 0x003459E6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SourceFileReference>(deep);
		}

		// Token: 0x0400A793 RID: 42899
		private const string tagName = "sourceFileName";

		// Token: 0x0400A794 RID: 42900
		private const byte tagNsId = 23;

		// Token: 0x0400A795 RID: 42901
		internal const int ElementTypeIdConst = 11850;
	}
}
