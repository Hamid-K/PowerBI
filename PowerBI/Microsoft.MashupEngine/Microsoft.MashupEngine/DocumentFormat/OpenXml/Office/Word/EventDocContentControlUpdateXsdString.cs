using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002475 RID: 9333
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocContentControlUpdateXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x17005112 RID: 20754
		// (get) Token: 0x06011362 RID: 70498 RVA: 0x002EBAC0 File Offset: 0x002E9CC0
		public override string LocalName
		{
			get
			{
				return "eventDocContentControlContentUpdate";
			}
		}

		// Token: 0x17005113 RID: 20755
		// (get) Token: 0x06011363 RID: 70499 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005114 RID: 20756
		// (get) Token: 0x06011364 RID: 70500 RVA: 0x002EBAC7 File Offset: 0x002E9CC7
		internal override int ElementTypeId
		{
			get
			{
				return 12560;
			}
		}

		// Token: 0x06011365 RID: 70501 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011366 RID: 70502 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocContentControlUpdateXsdString()
		{
		}

		// Token: 0x06011367 RID: 70503 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocContentControlUpdateXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011368 RID: 70504 RVA: 0x002EBAD0 File Offset: 0x002E9CD0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011369 RID: 70505 RVA: 0x002EBAEB File Offset: 0x002E9CEB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocContentControlUpdateXsdString>(deep);
		}

		// Token: 0x040078B3 RID: 30899
		private const string tagName = "eventDocContentControlContentUpdate";

		// Token: 0x040078B4 RID: 30900
		private const byte tagNsId = 33;

		// Token: 0x040078B5 RID: 30901
		internal const int ElementTypeIdConst = 12560;
	}
}
