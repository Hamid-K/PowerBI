using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DBB RID: 11707
	[GeneratedCode("DomGen", "2.0")]
	internal class BordersDoNotSurroundFooter : OnOffType
	{
		// Token: 0x170087CD RID: 34765
		// (get) Token: 0x06018E5A RID: 101978 RVA: 0x003450DA File Offset: 0x003432DA
		public override string LocalName
		{
			get
			{
				return "bordersDoNotSurroundFooter";
			}
		}

		// Token: 0x170087CE RID: 34766
		// (get) Token: 0x06018E5B RID: 101979 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087CF RID: 34767
		// (get) Token: 0x06018E5C RID: 101980 RVA: 0x003450E1 File Offset: 0x003432E1
		internal override int ElementTypeId
		{
			get
			{
				return 11975;
			}
		}

		// Token: 0x06018E5D RID: 101981 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E5F RID: 101983 RVA: 0x003450E8 File Offset: 0x003432E8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BordersDoNotSurroundFooter>(deep);
		}

		// Token: 0x0400A59C RID: 42396
		private const string tagName = "bordersDoNotSurroundFooter";

		// Token: 0x0400A59D RID: 42397
		private const byte tagNsId = 23;

		// Token: 0x0400A59E RID: 42398
		internal const int ElementTypeIdConst = 11975;
	}
}
