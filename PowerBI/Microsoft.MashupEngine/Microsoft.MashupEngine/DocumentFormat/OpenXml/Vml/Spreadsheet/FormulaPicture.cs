using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021E6 RID: 8678
	[GeneratedCode("DomGen", "2.0")]
	internal class FormulaPicture : OpenXmlLeafTextElement
	{
		// Token: 0x170037BF RID: 14271
		// (get) Token: 0x0600DCE2 RID: 56546 RVA: 0x002BCF60 File Offset: 0x002BB160
		public override string LocalName
		{
			get
			{
				return "FmlaPict";
			}
		}

		// Token: 0x170037C0 RID: 14272
		// (get) Token: 0x0600DCE3 RID: 56547 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037C1 RID: 14273
		// (get) Token: 0x0600DCE4 RID: 56548 RVA: 0x002BCF67 File Offset: 0x002BB167
		internal override int ElementTypeId
		{
			get
			{
				return 12481;
			}
		}

		// Token: 0x0600DCE5 RID: 56549 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCE6 RID: 56550 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FormulaPicture()
		{
		}

		// Token: 0x0600DCE7 RID: 56551 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FormulaPicture(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCE8 RID: 56552 RVA: 0x002BCF70 File Offset: 0x002BB170
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCE9 RID: 56553 RVA: 0x002BCF8B File Offset: 0x002BB18B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormulaPicture>(deep);
		}

		// Token: 0x04006CE6 RID: 27878
		private const string tagName = "FmlaPict";

		// Token: 0x04006CE7 RID: 27879
		private const byte tagNsId = 29;

		// Token: 0x04006CE8 RID: 27880
		internal const int ElementTypeIdConst = 12481;
	}
}
