using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200220D RID: 8717
	[GeneratedCode("DomGen", "2.0")]
	internal class LeftStroke : StrokeChildType
	{
		// Token: 0x170038E4 RID: 14564
		// (get) Token: 0x0600DF61 RID: 57185 RVA: 0x002BF360 File Offset: 0x002BD560
		public override string LocalName
		{
			get
			{
				return "left";
			}
		}

		// Token: 0x170038E5 RID: 14565
		// (get) Token: 0x0600DF62 RID: 57186 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170038E6 RID: 14566
		// (get) Token: 0x0600DF63 RID: 57187 RVA: 0x002BF367 File Offset: 0x002BD567
		internal override int ElementTypeId
		{
			get
			{
				return 12410;
			}
		}

		// Token: 0x0600DF64 RID: 57188 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DF66 RID: 57190 RVA: 0x002BF376 File Offset: 0x002BD576
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeftStroke>(deep);
		}

		// Token: 0x04006D8F RID: 28047
		private const string tagName = "left";

		// Token: 0x04006D90 RID: 28048
		private const byte tagNsId = 27;

		// Token: 0x04006D91 RID: 28049
		internal const int ElementTypeIdConst = 12410;
	}
}
