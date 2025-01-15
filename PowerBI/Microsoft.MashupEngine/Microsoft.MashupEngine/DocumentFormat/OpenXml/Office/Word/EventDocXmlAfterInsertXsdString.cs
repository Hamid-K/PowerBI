using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200246E RID: 9326
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocXmlAfterInsertXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x170050FD RID: 20733
		// (get) Token: 0x0601132A RID: 70442 RVA: 0x002EB954 File Offset: 0x002E9B54
		public override string LocalName
		{
			get
			{
				return "eventDocXmlAfterInsert";
			}
		}

		// Token: 0x170050FE RID: 20734
		// (get) Token: 0x0601132B RID: 70443 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050FF RID: 20735
		// (get) Token: 0x0601132C RID: 70444 RVA: 0x002EB95B File Offset: 0x002E9B5B
		internal override int ElementTypeId
		{
			get
			{
				return 12553;
			}
		}

		// Token: 0x0601132D RID: 70445 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601132E RID: 70446 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocXmlAfterInsertXsdString()
		{
		}

		// Token: 0x0601132F RID: 70447 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocXmlAfterInsertXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011330 RID: 70448 RVA: 0x002EB964 File Offset: 0x002E9B64
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011331 RID: 70449 RVA: 0x002EB97F File Offset: 0x002E9B7F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocXmlAfterInsertXsdString>(deep);
		}

		// Token: 0x0400789E RID: 30878
		private const string tagName = "eventDocXmlAfterInsert";

		// Token: 0x0400789F RID: 30879
		private const byte tagNsId = 33;

		// Token: 0x040078A0 RID: 30880
		internal const int ElementTypeIdConst = 12553;
	}
}
