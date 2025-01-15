using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E52 RID: 11858
	[GeneratedCode("DomGen", "2.0")]
	internal class HeaderSource : RelationshipType
	{
		// Token: 0x17008A36 RID: 35382
		// (get) Token: 0x06019341 RID: 103233 RVA: 0x003477C1 File Offset: 0x003459C1
		public override string LocalName
		{
			get
			{
				return "headerSource";
			}
		}

		// Token: 0x17008A37 RID: 35383
		// (get) Token: 0x06019342 RID: 103234 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A38 RID: 35384
		// (get) Token: 0x06019343 RID: 103235 RVA: 0x003477C8 File Offset: 0x003459C8
		internal override int ElementTypeId
		{
			get
			{
				return 11818;
			}
		}

		// Token: 0x06019344 RID: 103236 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019346 RID: 103238 RVA: 0x003477CF File Offset: 0x003459CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HeaderSource>(deep);
		}

		// Token: 0x0400A790 RID: 42896
		private const string tagName = "headerSource";

		// Token: 0x0400A791 RID: 42897
		private const byte tagNsId = 23;

		// Token: 0x0400A792 RID: 42898
		internal const int ElementTypeIdConst = 11818;
	}
}
