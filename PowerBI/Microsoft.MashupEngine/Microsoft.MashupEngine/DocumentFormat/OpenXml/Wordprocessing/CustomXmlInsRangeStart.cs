using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D37 RID: 11575
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlInsRangeStart : TrackChangeType
	{
		// Token: 0x1700862C RID: 34348
		// (get) Token: 0x06018B13 RID: 101139 RVA: 0x003440BE File Offset: 0x003422BE
		public override string LocalName
		{
			get
			{
				return "customXmlInsRangeStart";
			}
		}

		// Token: 0x1700862D RID: 34349
		// (get) Token: 0x06018B14 RID: 101140 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700862E RID: 34350
		// (get) Token: 0x06018B15 RID: 101141 RVA: 0x003440C5 File Offset: 0x003422C5
		internal override int ElementTypeId
		{
			get
			{
				return 11484;
			}
		}

		// Token: 0x06018B16 RID: 101142 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B18 RID: 101144 RVA: 0x003440CC File Offset: 0x003422CC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlInsRangeStart>(deep);
		}

		// Token: 0x0400A411 RID: 42001
		private const string tagName = "customXmlInsRangeStart";

		// Token: 0x0400A412 RID: 42002
		private const byte tagNsId = 23;

		// Token: 0x0400A413 RID: 42003
		internal const int ElementTypeIdConst = 11484;
	}
}
