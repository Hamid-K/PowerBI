using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200246C RID: 9324
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocCloseXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x170050F7 RID: 20727
		// (get) Token: 0x0601131A RID: 70426 RVA: 0x002EB8EC File Offset: 0x002E9AEC
		public override string LocalName
		{
			get
			{
				return "eventDocClose";
			}
		}

		// Token: 0x170050F8 RID: 20728
		// (get) Token: 0x0601131B RID: 70427 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050F9 RID: 20729
		// (get) Token: 0x0601131C RID: 70428 RVA: 0x002EB8F3 File Offset: 0x002E9AF3
		internal override int ElementTypeId
		{
			get
			{
				return 12551;
			}
		}

		// Token: 0x0601131D RID: 70429 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601131E RID: 70430 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocCloseXsdString()
		{
		}

		// Token: 0x0601131F RID: 70431 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocCloseXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011320 RID: 70432 RVA: 0x002EB8FC File Offset: 0x002E9AFC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011321 RID: 70433 RVA: 0x002EB917 File Offset: 0x002E9B17
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocCloseXsdString>(deep);
		}

		// Token: 0x04007898 RID: 30872
		private const string tagName = "eventDocClose";

		// Token: 0x04007899 RID: 30873
		private const byte tagNsId = 33;

		// Token: 0x0400789A RID: 30874
		internal const int ElementTypeIdConst = 12551;
	}
}
