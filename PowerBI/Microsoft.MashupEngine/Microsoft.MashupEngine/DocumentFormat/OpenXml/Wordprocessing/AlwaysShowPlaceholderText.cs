using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DD6 RID: 11734
	[GeneratedCode("DomGen", "2.0")]
	internal class AlwaysShowPlaceholderText : OnOffType
	{
		// Token: 0x1700881E RID: 34846
		// (get) Token: 0x06018EFC RID: 102140 RVA: 0x00345347 File Offset: 0x00343547
		public override string LocalName
		{
			get
			{
				return "alwaysShowPlaceholderText";
			}
		}

		// Token: 0x1700881F RID: 34847
		// (get) Token: 0x06018EFD RID: 102141 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008820 RID: 34848
		// (get) Token: 0x06018EFE RID: 102142 RVA: 0x0034534E File Offset: 0x0034354E
		internal override int ElementTypeId
		{
			get
			{
				return 12027;
			}
		}

		// Token: 0x06018EFF RID: 102143 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F01 RID: 102145 RVA: 0x00345355 File Offset: 0x00343555
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlwaysShowPlaceholderText>(deep);
		}

		// Token: 0x0400A5ED RID: 42477
		private const string tagName = "alwaysShowPlaceholderText";

		// Token: 0x0400A5EE RID: 42478
		private const byte tagNsId = 23;

		// Token: 0x0400A5EF RID: 42479
		internal const int ElementTypeIdConst = 12027;
	}
}
