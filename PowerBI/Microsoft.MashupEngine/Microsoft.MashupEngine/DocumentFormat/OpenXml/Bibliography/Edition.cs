using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028CC RID: 10444
	[GeneratedCode("DomGen", "2.0")]
	internal class Edition : OpenXmlLeafTextElement
	{
		// Token: 0x1700690C RID: 26892
		// (get) Token: 0x06014954 RID: 84308 RVA: 0x00314C44 File Offset: 0x00312E44
		public override string LocalName
		{
			get
			{
				return "Edition";
			}
		}

		// Token: 0x1700690D RID: 26893
		// (get) Token: 0x06014955 RID: 84309 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700690E RID: 26894
		// (get) Token: 0x06014956 RID: 84310 RVA: 0x00314C4B File Offset: 0x00312E4B
		internal override int ElementTypeId
		{
			get
			{
				return 10798;
			}
		}

		// Token: 0x06014957 RID: 84311 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014958 RID: 84312 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Edition()
		{
		}

		// Token: 0x06014959 RID: 84313 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Edition(string text)
			: base(text)
		{
		}

		// Token: 0x0601495A RID: 84314 RVA: 0x00314C54 File Offset: 0x00312E54
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601495B RID: 84315 RVA: 0x00314C6F File Offset: 0x00312E6F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Edition>(deep);
		}

		// Token: 0x04008EF0 RID: 36592
		private const string tagName = "Edition";

		// Token: 0x04008EF1 RID: 36593
		private const byte tagNsId = 9;

		// Token: 0x04008EF2 RID: 36594
		internal const int ElementTypeIdConst = 10798;
	}
}
