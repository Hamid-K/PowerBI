using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028D1 RID: 10449
	[GeneratedCode("DomGen", "2.0")]
	internal class JournalName : OpenXmlLeafTextElement
	{
		// Token: 0x1700691B RID: 26907
		// (get) Token: 0x0601497C RID: 84348 RVA: 0x00314D48 File Offset: 0x00312F48
		public override string LocalName
		{
			get
			{
				return "JournalName";
			}
		}

		// Token: 0x1700691C RID: 26908
		// (get) Token: 0x0601497D RID: 84349 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700691D RID: 26909
		// (get) Token: 0x0601497E RID: 84350 RVA: 0x00314D4F File Offset: 0x00312F4F
		internal override int ElementTypeId
		{
			get
			{
				return 10803;
			}
		}

		// Token: 0x0601497F RID: 84351 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014980 RID: 84352 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public JournalName()
		{
		}

		// Token: 0x06014981 RID: 84353 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public JournalName(string text)
			: base(text)
		{
		}

		// Token: 0x06014982 RID: 84354 RVA: 0x00314D58 File Offset: 0x00312F58
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014983 RID: 84355 RVA: 0x00314D73 File Offset: 0x00312F73
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<JournalName>(deep);
		}

		// Token: 0x04008EFF RID: 36607
		private const string tagName = "JournalName";

		// Token: 0x04008F00 RID: 36608
		private const byte tagNsId = 9;

		// Token: 0x04008F01 RID: 36609
		internal const int ElementTypeIdConst = 10803;
	}
}
