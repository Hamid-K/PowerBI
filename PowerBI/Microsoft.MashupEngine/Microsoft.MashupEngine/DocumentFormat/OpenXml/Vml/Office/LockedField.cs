using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200221C RID: 8732
	[GeneratedCode("DomGen", "2.0")]
	internal class LockedField : OpenXmlLeafTextElement
	{
		// Token: 0x17003935 RID: 14645
		// (get) Token: 0x0600E015 RID: 57365 RVA: 0x002BF9D4 File Offset: 0x002BDBD4
		public override string LocalName
		{
			get
			{
				return "LockedField";
			}
		}

		// Token: 0x17003936 RID: 14646
		// (get) Token: 0x0600E016 RID: 57366 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003937 RID: 14647
		// (get) Token: 0x0600E017 RID: 57367 RVA: 0x002BF9DB File Offset: 0x002BDBDB
		internal override int ElementTypeId
		{
			get
			{
				return 12425;
			}
		}

		// Token: 0x0600E018 RID: 57368 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E019 RID: 57369 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public LockedField()
		{
		}

		// Token: 0x0600E01A RID: 57370 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public LockedField(string text)
			: base(text)
		{
		}

		// Token: 0x0600E01B RID: 57371 RVA: 0x002BF9E4 File Offset: 0x002BDBE4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new TrueFalseBlankValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600E01C RID: 57372 RVA: 0x002BF9FF File Offset: 0x002BDBFF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LockedField>(deep);
		}

		// Token: 0x04006DCE RID: 28110
		private const string tagName = "LockedField";

		// Token: 0x04006DCF RID: 28111
		private const byte tagNsId = 27;

		// Token: 0x04006DD0 RID: 28112
		internal const int ElementTypeIdConst = 12425;
	}
}
