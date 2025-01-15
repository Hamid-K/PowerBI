using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028D2 RID: 10450
	[GeneratedCode("DomGen", "2.0")]
	internal class LcId : OpenXmlLeafTextElement
	{
		// Token: 0x1700691E RID: 26910
		// (get) Token: 0x06014984 RID: 84356 RVA: 0x00314D7C File Offset: 0x00312F7C
		public override string LocalName
		{
			get
			{
				return "LCID";
			}
		}

		// Token: 0x1700691F RID: 26911
		// (get) Token: 0x06014985 RID: 84357 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006920 RID: 26912
		// (get) Token: 0x06014986 RID: 84358 RVA: 0x00314D83 File Offset: 0x00312F83
		internal override int ElementTypeId
		{
			get
			{
				return 10804;
			}
		}

		// Token: 0x06014987 RID: 84359 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014988 RID: 84360 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public LcId()
		{
		}

		// Token: 0x06014989 RID: 84361 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public LcId(string text)
			: base(text)
		{
		}

		// Token: 0x0601498A RID: 84362 RVA: 0x00314D8C File Offset: 0x00312F8C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601498B RID: 84363 RVA: 0x00314DA7 File Offset: 0x00312FA7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LcId>(deep);
		}

		// Token: 0x04008F02 RID: 36610
		private const string tagName = "LCID";

		// Token: 0x04008F03 RID: 36611
		private const byte tagNsId = 9;

		// Token: 0x04008F04 RID: 36612
		internal const int ElementTypeIdConst = 10804;
	}
}
