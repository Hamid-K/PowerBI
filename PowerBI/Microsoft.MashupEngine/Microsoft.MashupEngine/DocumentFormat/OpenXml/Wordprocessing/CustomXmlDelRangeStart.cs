using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D38 RID: 11576
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlDelRangeStart : TrackChangeType
	{
		// Token: 0x1700862F RID: 34351
		// (get) Token: 0x06018B19 RID: 101145 RVA: 0x003440D5 File Offset: 0x003422D5
		public override string LocalName
		{
			get
			{
				return "customXmlDelRangeStart";
			}
		}

		// Token: 0x17008630 RID: 34352
		// (get) Token: 0x06018B1A RID: 101146 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008631 RID: 34353
		// (get) Token: 0x06018B1B RID: 101147 RVA: 0x003440DC File Offset: 0x003422DC
		internal override int ElementTypeId
		{
			get
			{
				return 11486;
			}
		}

		// Token: 0x06018B1C RID: 101148 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B1E RID: 101150 RVA: 0x003440E3 File Offset: 0x003422E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlDelRangeStart>(deep);
		}

		// Token: 0x0400A414 RID: 42004
		private const string tagName = "customXmlDelRangeStart";

		// Token: 0x0400A415 RID: 42005
		private const byte tagNsId = 23;

		// Token: 0x0400A416 RID: 42006
		internal const int ElementTypeIdConst = 11486;
	}
}
