using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002476 RID: 9334
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocBuildingBlockAfterInsertXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x17005115 RID: 20757
		// (get) Token: 0x0601136A RID: 70506 RVA: 0x002EBAF4 File Offset: 0x002E9CF4
		public override string LocalName
		{
			get
			{
				return "eventDocBuildingBlockAfterInsert";
			}
		}

		// Token: 0x17005116 RID: 20758
		// (get) Token: 0x0601136B RID: 70507 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005117 RID: 20759
		// (get) Token: 0x0601136C RID: 70508 RVA: 0x002EBAFB File Offset: 0x002E9CFB
		internal override int ElementTypeId
		{
			get
			{
				return 12561;
			}
		}

		// Token: 0x0601136D RID: 70509 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601136E RID: 70510 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocBuildingBlockAfterInsertXsdString()
		{
		}

		// Token: 0x0601136F RID: 70511 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocBuildingBlockAfterInsertXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011370 RID: 70512 RVA: 0x002EBB04 File Offset: 0x002E9D04
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011371 RID: 70513 RVA: 0x002EBB1F File Offset: 0x002E9D1F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocBuildingBlockAfterInsertXsdString>(deep);
		}

		// Token: 0x040078B6 RID: 30902
		private const string tagName = "eventDocBuildingBlockAfterInsert";

		// Token: 0x040078B7 RID: 30903
		private const byte tagNsId = 33;

		// Token: 0x040078B8 RID: 30904
		internal const int ElementTypeIdConst = 12561;
	}
}
