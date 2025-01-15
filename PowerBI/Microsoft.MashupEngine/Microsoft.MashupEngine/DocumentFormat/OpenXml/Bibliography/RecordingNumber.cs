using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028DD RID: 10461
	[GeneratedCode("DomGen", "2.0")]
	internal class RecordingNumber : OpenXmlLeafTextElement
	{
		// Token: 0x1700693F RID: 26943
		// (get) Token: 0x060149DC RID: 84444 RVA: 0x00314FB8 File Offset: 0x003131B8
		public override string LocalName
		{
			get
			{
				return "RecordingNumber";
			}
		}

		// Token: 0x17006940 RID: 26944
		// (get) Token: 0x060149DD RID: 84445 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006941 RID: 26945
		// (get) Token: 0x060149DE RID: 84446 RVA: 0x00314FBF File Offset: 0x003131BF
		internal override int ElementTypeId
		{
			get
			{
				return 10815;
			}
		}

		// Token: 0x060149DF RID: 84447 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149E0 RID: 84448 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public RecordingNumber()
		{
		}

		// Token: 0x060149E1 RID: 84449 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public RecordingNumber(string text)
			: base(text)
		{
		}

		// Token: 0x060149E2 RID: 84450 RVA: 0x00314FC8 File Offset: 0x003131C8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149E3 RID: 84451 RVA: 0x00314FE3 File Offset: 0x003131E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RecordingNumber>(deep);
		}

		// Token: 0x04008F23 RID: 36643
		private const string tagName = "RecordingNumber";

		// Token: 0x04008F24 RID: 36644
		private const byte tagNsId = 9;

		// Token: 0x04008F25 RID: 36645
		internal const int ElementTypeIdConst = 10815;
	}
}
