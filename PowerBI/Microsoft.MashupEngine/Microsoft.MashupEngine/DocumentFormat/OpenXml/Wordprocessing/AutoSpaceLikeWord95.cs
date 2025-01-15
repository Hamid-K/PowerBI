using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E04 RID: 11780
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoSpaceLikeWord95 : OnOffType
	{
		// Token: 0x170088A8 RID: 34984
		// (get) Token: 0x06019010 RID: 102416 RVA: 0x00345769 File Offset: 0x00343969
		public override string LocalName
		{
			get
			{
				return "autoSpaceLikeWord95";
			}
		}

		// Token: 0x170088A9 RID: 34985
		// (get) Token: 0x06019011 RID: 102417 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088AA RID: 34986
		// (get) Token: 0x06019012 RID: 102418 RVA: 0x00345770 File Offset: 0x00343970
		internal override int ElementTypeId
		{
			get
			{
				return 12090;
			}
		}

		// Token: 0x06019013 RID: 102419 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019015 RID: 102421 RVA: 0x00345777 File Offset: 0x00343977
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoSpaceLikeWord95>(deep);
		}

		// Token: 0x0400A677 RID: 42615
		private const string tagName = "autoSpaceLikeWord95";

		// Token: 0x0400A678 RID: 42616
		private const byte tagNsId = 23;

		// Token: 0x0400A679 RID: 42617
		internal const int ElementTypeIdConst = 12090;
	}
}
