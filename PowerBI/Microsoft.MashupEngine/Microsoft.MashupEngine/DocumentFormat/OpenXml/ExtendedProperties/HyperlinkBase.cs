using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002937 RID: 10551
	[GeneratedCode("DomGen", "2.0")]
	internal class HyperlinkBase : OpenXmlLeafTextElement
	{
		// Token: 0x17006B20 RID: 27424
		// (get) Token: 0x06014E6A RID: 85610 RVA: 0x00318A80 File Offset: 0x00316C80
		public override string LocalName
		{
			get
			{
				return "HyperlinkBase";
			}
		}

		// Token: 0x17006B21 RID: 27425
		// (get) Token: 0x06014E6B RID: 85611 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B22 RID: 27426
		// (get) Token: 0x06014E6C RID: 85612 RVA: 0x00318A87 File Offset: 0x00316C87
		internal override int ElementTypeId
		{
			get
			{
				return 11019;
			}
		}

		// Token: 0x06014E6D RID: 85613 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E6E RID: 85614 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public HyperlinkBase()
		{
		}

		// Token: 0x06014E6F RID: 85615 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public HyperlinkBase(string text)
			: base(text)
		{
		}

		// Token: 0x06014E70 RID: 85616 RVA: 0x00318A90 File Offset: 0x00316C90
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014E71 RID: 85617 RVA: 0x00318AAB File Offset: 0x00316CAB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HyperlinkBase>(deep);
		}

		// Token: 0x0400907F RID: 36991
		private const string tagName = "HyperlinkBase";

		// Token: 0x04009080 RID: 36992
		private const byte tagNsId = 3;

		// Token: 0x04009081 RID: 36993
		internal const int ElementTypeIdConst = 11019;
	}
}
