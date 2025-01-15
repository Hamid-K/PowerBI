using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002470 RID: 9328
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocContentControlAfterInsertXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x17005103 RID: 20739
		// (get) Token: 0x0601133A RID: 70458 RVA: 0x002EB9BC File Offset: 0x002E9BBC
		public override string LocalName
		{
			get
			{
				return "eventDocContentControlAfterInsert";
			}
		}

		// Token: 0x17005104 RID: 20740
		// (get) Token: 0x0601133B RID: 70459 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005105 RID: 20741
		// (get) Token: 0x0601133C RID: 70460 RVA: 0x002EB9C3 File Offset: 0x002E9BC3
		internal override int ElementTypeId
		{
			get
			{
				return 12555;
			}
		}

		// Token: 0x0601133D RID: 70461 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601133E RID: 70462 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocContentControlAfterInsertXsdString()
		{
		}

		// Token: 0x0601133F RID: 70463 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocContentControlAfterInsertXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011340 RID: 70464 RVA: 0x002EB9CC File Offset: 0x002E9BCC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011341 RID: 70465 RVA: 0x002EB9E7 File Offset: 0x002E9BE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocContentControlAfterInsertXsdString>(deep);
		}

		// Token: 0x040078A4 RID: 30884
		private const string tagName = "eventDocContentControlAfterInsert";

		// Token: 0x040078A5 RID: 30885
		private const byte tagNsId = 33;

		// Token: 0x040078A6 RID: 30886
		internal const int ElementTypeIdConst = 12555;
	}
}
