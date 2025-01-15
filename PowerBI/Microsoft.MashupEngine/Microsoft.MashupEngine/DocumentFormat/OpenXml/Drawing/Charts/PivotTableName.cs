using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002577 RID: 9591
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotTableName : OpenXmlLeafTextElement
	{
		// Token: 0x170055EC RID: 21996
		// (get) Token: 0x06011E2E RID: 73262 RVA: 0x002F15F0 File Offset: 0x002EF7F0
		public override string LocalName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x170055ED RID: 21997
		// (get) Token: 0x06011E2F RID: 73263 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055EE RID: 21998
		// (get) Token: 0x06011E30 RID: 73264 RVA: 0x002F3570 File Offset: 0x002F1770
		internal override int ElementTypeId
		{
			get
			{
				return 10527;
			}
		}

		// Token: 0x06011E31 RID: 73265 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E32 RID: 73266 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PivotTableName()
		{
		}

		// Token: 0x06011E33 RID: 73267 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PivotTableName(string text)
			: base(text)
		{
		}

		// Token: 0x06011E34 RID: 73268 RVA: 0x002F3578 File Offset: 0x002F1778
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011E35 RID: 73269 RVA: 0x002F3593 File Offset: 0x002F1793
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotTableName>(deep);
		}

		// Token: 0x04007D18 RID: 32024
		private const string tagName = "name";

		// Token: 0x04007D19 RID: 32025
		private const byte tagNsId = 11;

		// Token: 0x04007D1A RID: 32026
		internal const int ElementTypeIdConst = 10527;
	}
}
