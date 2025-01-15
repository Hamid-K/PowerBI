using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E09 RID: 11785
	[GeneratedCode("DomGen", "2.0")]
	internal class UseWord97LineBreakRules : OnOffType
	{
		// Token: 0x170088B7 RID: 34999
		// (get) Token: 0x0601902E RID: 102446 RVA: 0x003457DC File Offset: 0x003439DC
		public override string LocalName
		{
			get
			{
				return "useWord97LineBreakRules";
			}
		}

		// Token: 0x170088B8 RID: 35000
		// (get) Token: 0x0601902F RID: 102447 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088B9 RID: 35001
		// (get) Token: 0x06019030 RID: 102448 RVA: 0x003457E3 File Offset: 0x003439E3
		internal override int ElementTypeId
		{
			get
			{
				return 12095;
			}
		}

		// Token: 0x06019031 RID: 102449 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019033 RID: 102451 RVA: 0x003457EA File Offset: 0x003439EA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UseWord97LineBreakRules>(deep);
		}

		// Token: 0x0400A686 RID: 42630
		private const string tagName = "useWord97LineBreakRules";

		// Token: 0x0400A687 RID: 42631
		private const byte tagNsId = 23;

		// Token: 0x0400A688 RID: 42632
		internal const int ElementTypeIdConst = 12095;
	}
}
