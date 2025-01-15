using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002472 RID: 9330
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocContentControlOnExistXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x17005109 RID: 20745
		// (get) Token: 0x0601134A RID: 70474 RVA: 0x002EBA24 File Offset: 0x002E9C24
		public override string LocalName
		{
			get
			{
				return "eventDocContentControlOnExit";
			}
		}

		// Token: 0x1700510A RID: 20746
		// (get) Token: 0x0601134B RID: 70475 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x1700510B RID: 20747
		// (get) Token: 0x0601134C RID: 70476 RVA: 0x002EBA2B File Offset: 0x002E9C2B
		internal override int ElementTypeId
		{
			get
			{
				return 12557;
			}
		}

		// Token: 0x0601134D RID: 70477 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601134E RID: 70478 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocContentControlOnExistXsdString()
		{
		}

		// Token: 0x0601134F RID: 70479 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocContentControlOnExistXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011350 RID: 70480 RVA: 0x002EBA34 File Offset: 0x002E9C34
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011351 RID: 70481 RVA: 0x002EBA4F File Offset: 0x002E9C4F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocContentControlOnExistXsdString>(deep);
		}

		// Token: 0x040078AA RID: 30890
		private const string tagName = "eventDocContentControlOnExit";

		// Token: 0x040078AB RID: 30891
		private const byte tagNsId = 33;

		// Token: 0x040078AC RID: 30892
		internal const int ElementTypeIdConst = 12557;
	}
}
