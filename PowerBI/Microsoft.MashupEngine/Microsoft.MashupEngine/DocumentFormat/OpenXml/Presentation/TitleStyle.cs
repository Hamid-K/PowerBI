using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A72 RID: 10866
	[GeneratedCode("DomGen", "2.0")]
	internal class TitleStyle : TextListStyleType
	{
		// Token: 0x1700730D RID: 29453
		// (get) Token: 0x06015FFD RID: 90109 RVA: 0x00325A20 File Offset: 0x00323C20
		public override string LocalName
		{
			get
			{
				return "titleStyle";
			}
		}

		// Token: 0x1700730E RID: 29454
		// (get) Token: 0x06015FFE RID: 90110 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700730F RID: 29455
		// (get) Token: 0x06015FFF RID: 90111 RVA: 0x00325A27 File Offset: 0x00323C27
		internal override int ElementTypeId
		{
			get
			{
				return 12283;
			}
		}

		// Token: 0x06016000 RID: 90112 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016001 RID: 90113 RVA: 0x00325A2E File Offset: 0x00323C2E
		public TitleStyle()
		{
		}

		// Token: 0x06016002 RID: 90114 RVA: 0x00325A36 File Offset: 0x00323C36
		public TitleStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016003 RID: 90115 RVA: 0x00325A3F File Offset: 0x00323C3F
		public TitleStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016004 RID: 90116 RVA: 0x00325A48 File Offset: 0x00323C48
		public TitleStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016005 RID: 90117 RVA: 0x00325A51 File Offset: 0x00323C51
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TitleStyle>(deep);
		}

		// Token: 0x040095B9 RID: 38329
		private const string tagName = "titleStyle";

		// Token: 0x040095BA RID: 38330
		private const byte tagNsId = 24;

		// Token: 0x040095BB RID: 38331
		internal const int ElementTypeIdConst = 12283;
	}
}
