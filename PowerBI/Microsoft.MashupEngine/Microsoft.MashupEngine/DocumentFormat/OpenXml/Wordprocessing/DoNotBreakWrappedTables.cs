using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E0A RID: 11786
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotBreakWrappedTables : OnOffType
	{
		// Token: 0x170088BA RID: 35002
		// (get) Token: 0x06019034 RID: 102452 RVA: 0x003457F3 File Offset: 0x003439F3
		public override string LocalName
		{
			get
			{
				return "doNotBreakWrappedTables";
			}
		}

		// Token: 0x170088BB RID: 35003
		// (get) Token: 0x06019035 RID: 102453 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088BC RID: 35004
		// (get) Token: 0x06019036 RID: 102454 RVA: 0x003457FA File Offset: 0x003439FA
		internal override int ElementTypeId
		{
			get
			{
				return 12096;
			}
		}

		// Token: 0x06019037 RID: 102455 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019039 RID: 102457 RVA: 0x00345801 File Offset: 0x00343A01
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotBreakWrappedTables>(deep);
		}

		// Token: 0x0400A689 RID: 42633
		private const string tagName = "doNotBreakWrappedTables";

		// Token: 0x0400A68A RID: 42634
		private const byte tagNsId = 23;

		// Token: 0x0400A68B RID: 42635
		internal const int ElementTypeIdConst = 12096;
	}
}
