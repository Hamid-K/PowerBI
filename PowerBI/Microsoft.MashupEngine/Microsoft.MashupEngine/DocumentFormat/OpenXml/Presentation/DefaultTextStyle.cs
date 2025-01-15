using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A76 RID: 10870
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultTextStyle : TextListStyleType
	{
		// Token: 0x17007319 RID: 29465
		// (get) Token: 0x06016021 RID: 90145 RVA: 0x00325A9F File Offset: 0x00323C9F
		public override string LocalName
		{
			get
			{
				return "defaultTextStyle";
			}
		}

		// Token: 0x1700731A RID: 29466
		// (get) Token: 0x06016022 RID: 90146 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700731B RID: 29467
		// (get) Token: 0x06016023 RID: 90147 RVA: 0x00325AA6 File Offset: 0x00323CA6
		internal override int ElementTypeId
		{
			get
			{
				return 12352;
			}
		}

		// Token: 0x06016024 RID: 90148 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016025 RID: 90149 RVA: 0x00325A2E File Offset: 0x00323C2E
		public DefaultTextStyle()
		{
		}

		// Token: 0x06016026 RID: 90150 RVA: 0x00325A36 File Offset: 0x00323C36
		public DefaultTextStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016027 RID: 90151 RVA: 0x00325A3F File Offset: 0x00323C3F
		public DefaultTextStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016028 RID: 90152 RVA: 0x00325A48 File Offset: 0x00323C48
		public DefaultTextStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016029 RID: 90153 RVA: 0x00325AAD File Offset: 0x00323CAD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultTextStyle>(deep);
		}

		// Token: 0x040095C5 RID: 38341
		private const string tagName = "defaultTextStyle";

		// Token: 0x040095C6 RID: 38342
		private const byte tagNsId = 24;

		// Token: 0x040095C7 RID: 38343
		internal const int ElementTypeIdConst = 12352;
	}
}
