using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028CD RID: 10445
	[GeneratedCode("DomGen", "2.0")]
	internal class GuidString : OpenXmlLeafTextElement
	{
		// Token: 0x1700690F RID: 26895
		// (get) Token: 0x0601495C RID: 84316 RVA: 0x00314C78 File Offset: 0x00312E78
		public override string LocalName
		{
			get
			{
				return "Guid";
			}
		}

		// Token: 0x17006910 RID: 26896
		// (get) Token: 0x0601495D RID: 84317 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006911 RID: 26897
		// (get) Token: 0x0601495E RID: 84318 RVA: 0x00314C7F File Offset: 0x00312E7F
		internal override int ElementTypeId
		{
			get
			{
				return 10799;
			}
		}

		// Token: 0x0601495F RID: 84319 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014960 RID: 84320 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public GuidString()
		{
		}

		// Token: 0x06014961 RID: 84321 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public GuidString(string text)
			: base(text)
		{
		}

		// Token: 0x06014962 RID: 84322 RVA: 0x00314C88 File Offset: 0x00312E88
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014963 RID: 84323 RVA: 0x00314CA3 File Offset: 0x00312EA3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GuidString>(deep);
		}

		// Token: 0x04008EF3 RID: 36595
		private const string tagName = "Guid";

		// Token: 0x04008EF4 RID: 36596
		private const byte tagNsId = 9;

		// Token: 0x04008EF5 RID: 36597
		internal const int ElementTypeIdConst = 10799;
	}
}
