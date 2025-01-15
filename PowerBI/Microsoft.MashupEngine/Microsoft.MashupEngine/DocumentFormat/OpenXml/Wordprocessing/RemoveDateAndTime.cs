using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DAE RID: 11694
	[GeneratedCode("DomGen", "2.0")]
	internal class RemoveDateAndTime : OnOffType
	{
		// Token: 0x170087A6 RID: 34726
		// (get) Token: 0x06018E0C RID: 101900 RVA: 0x00344FAF File Offset: 0x003431AF
		public override string LocalName
		{
			get
			{
				return "removeDateAndTime";
			}
		}

		// Token: 0x170087A7 RID: 34727
		// (get) Token: 0x06018E0D RID: 101901 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087A8 RID: 34728
		// (get) Token: 0x06018E0E RID: 101902 RVA: 0x00344FB6 File Offset: 0x003431B6
		internal override int ElementTypeId
		{
			get
			{
				return 11962;
			}
		}

		// Token: 0x06018E0F RID: 101903 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E11 RID: 101905 RVA: 0x00344FBD File Offset: 0x003431BD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RemoveDateAndTime>(deep);
		}

		// Token: 0x0400A575 RID: 42357
		private const string tagName = "removeDateAndTime";

		// Token: 0x0400A576 RID: 42358
		private const byte tagNsId = 23;

		// Token: 0x0400A577 RID: 42359
		internal const int ElementTypeIdConst = 11962;
	}
}
