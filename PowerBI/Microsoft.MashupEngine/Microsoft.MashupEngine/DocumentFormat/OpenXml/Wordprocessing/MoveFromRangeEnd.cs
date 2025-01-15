using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D45 RID: 11589
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveFromRangeEnd : MarkupRangeType
	{
		// Token: 0x17008665 RID: 34405
		// (get) Token: 0x06018B86 RID: 101254 RVA: 0x0034449E File Offset: 0x0034269E
		public override string LocalName
		{
			get
			{
				return "moveFromRangeEnd";
			}
		}

		// Token: 0x17008666 RID: 34406
		// (get) Token: 0x06018B87 RID: 101255 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008667 RID: 34407
		// (get) Token: 0x06018B88 RID: 101256 RVA: 0x003444A5 File Offset: 0x003426A5
		internal override int ElementTypeId
		{
			get
			{
				return 11481;
			}
		}

		// Token: 0x06018B89 RID: 101257 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B8B RID: 101259 RVA: 0x003444AC File Offset: 0x003426AC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveFromRangeEnd>(deep);
		}

		// Token: 0x0400A43E RID: 42046
		private const string tagName = "moveFromRangeEnd";

		// Token: 0x0400A43F RID: 42047
		private const byte tagNsId = 23;

		// Token: 0x0400A440 RID: 42048
		internal const int ElementTypeIdConst = 11481;
	}
}
