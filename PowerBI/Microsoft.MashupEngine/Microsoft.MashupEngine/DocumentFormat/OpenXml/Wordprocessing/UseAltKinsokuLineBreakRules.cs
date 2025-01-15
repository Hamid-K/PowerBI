using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E15 RID: 11797
	[GeneratedCode("DomGen", "2.0")]
	internal class UseAltKinsokuLineBreakRules : OnOffType
	{
		// Token: 0x170088DB RID: 35035
		// (get) Token: 0x06019076 RID: 102518 RVA: 0x003458F0 File Offset: 0x00343AF0
		public override string LocalName
		{
			get
			{
				return "useAltKinsokuLineBreakRules";
			}
		}

		// Token: 0x170088DC RID: 35036
		// (get) Token: 0x06019077 RID: 102519 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088DD RID: 35037
		// (get) Token: 0x06019078 RID: 102520 RVA: 0x003458F7 File Offset: 0x00343AF7
		internal override int ElementTypeId
		{
			get
			{
				return 12107;
			}
		}

		// Token: 0x06019079 RID: 102521 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601907B RID: 102523 RVA: 0x003458FE File Offset: 0x00343AFE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UseAltKinsokuLineBreakRules>(deep);
		}

		// Token: 0x0400A6AA RID: 42666
		private const string tagName = "useAltKinsokuLineBreakRules";

		// Token: 0x0400A6AB RID: 42667
		private const byte tagNsId = 23;

		// Token: 0x0400A6AC RID: 42668
		internal const int ElementTypeIdConst = 12107;
	}
}
