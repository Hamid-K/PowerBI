using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200297B RID: 10619
	[GeneratedCode("DomGen", "2.0")]
	internal class HideSubArgument : OnOffType
	{
		// Token: 0x17006C7C RID: 27772
		// (get) Token: 0x06015186 RID: 86406 RVA: 0x0031B572 File Offset: 0x00319772
		public override string LocalName
		{
			get
			{
				return "subHide";
			}
		}

		// Token: 0x17006C7D RID: 27773
		// (get) Token: 0x06015187 RID: 86407 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C7E RID: 27774
		// (get) Token: 0x06015188 RID: 86408 RVA: 0x0031B579 File Offset: 0x00319779
		internal override int ElementTypeId
		{
			get
			{
				return 10924;
			}
		}

		// Token: 0x06015189 RID: 86409 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601518B RID: 86411 RVA: 0x0031B580 File Offset: 0x00319780
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HideSubArgument>(deep);
		}

		// Token: 0x0400917D RID: 37245
		private const string tagName = "subHide";

		// Token: 0x0400917E RID: 37246
		private const byte tagNsId = 21;

		// Token: 0x0400917F RID: 37247
		internal const int ElementTypeIdConst = 10924;
	}
}
