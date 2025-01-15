using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200246A RID: 9322
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocNewXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x170050F1 RID: 20721
		// (get) Token: 0x0601130A RID: 70410 RVA: 0x002EB884 File Offset: 0x002E9A84
		public override string LocalName
		{
			get
			{
				return "eventDocNew";
			}
		}

		// Token: 0x170050F2 RID: 20722
		// (get) Token: 0x0601130B RID: 70411 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050F3 RID: 20723
		// (get) Token: 0x0601130C RID: 70412 RVA: 0x002EB88B File Offset: 0x002E9A8B
		internal override int ElementTypeId
		{
			get
			{
				return 12549;
			}
		}

		// Token: 0x0601130D RID: 70413 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601130E RID: 70414 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocNewXsdString()
		{
		}

		// Token: 0x0601130F RID: 70415 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocNewXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011310 RID: 70416 RVA: 0x002EB894 File Offset: 0x002E9A94
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011311 RID: 70417 RVA: 0x002EB8AF File Offset: 0x002E9AAF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocNewXsdString>(deep);
		}

		// Token: 0x04007892 RID: 30866
		private const string tagName = "eventDocNew";

		// Token: 0x04007893 RID: 30867
		private const byte tagNsId = 33;

		// Token: 0x04007894 RID: 30868
		internal const int ElementTypeIdConst = 12549;
	}
}
