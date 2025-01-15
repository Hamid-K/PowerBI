using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E87 RID: 11911
	[GeneratedCode("DomGen", "2.0")]
	internal class Aliases : String253Type
	{
		// Token: 0x17008AFE RID: 35582
		// (get) Token: 0x060194EC RID: 103660 RVA: 0x003486AD File Offset: 0x003468AD
		public override string LocalName
		{
			get
			{
				return "aliases";
			}
		}

		// Token: 0x17008AFF RID: 35583
		// (get) Token: 0x060194ED RID: 103661 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B00 RID: 35584
		// (get) Token: 0x060194EE RID: 103662 RVA: 0x003486B4 File Offset: 0x003468B4
		internal override int ElementTypeId
		{
			get
			{
				return 11893;
			}
		}

		// Token: 0x060194EF RID: 103663 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060194F1 RID: 103665 RVA: 0x003486BB File Offset: 0x003468BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Aliases>(deep);
		}

		// Token: 0x0400A83B RID: 43067
		private const string tagName = "aliases";

		// Token: 0x0400A83C RID: 43068
		private const byte tagNsId = 23;

		// Token: 0x0400A83D RID: 43069
		internal const int ElementTypeIdConst = 11893;
	}
}
