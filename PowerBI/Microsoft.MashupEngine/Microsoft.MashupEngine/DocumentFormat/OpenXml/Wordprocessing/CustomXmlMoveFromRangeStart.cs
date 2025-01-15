using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D39 RID: 11577
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlMoveFromRangeStart : TrackChangeType
	{
		// Token: 0x17008632 RID: 34354
		// (get) Token: 0x06018B1F RID: 101151 RVA: 0x003440EC File Offset: 0x003422EC
		public override string LocalName
		{
			get
			{
				return "customXmlMoveFromRangeStart";
			}
		}

		// Token: 0x17008633 RID: 34355
		// (get) Token: 0x06018B20 RID: 101152 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008634 RID: 34356
		// (get) Token: 0x06018B21 RID: 101153 RVA: 0x003440F3 File Offset: 0x003422F3
		internal override int ElementTypeId
		{
			get
			{
				return 11488;
			}
		}

		// Token: 0x06018B22 RID: 101154 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B24 RID: 101156 RVA: 0x003440FA File Offset: 0x003422FA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlMoveFromRangeStart>(deep);
		}

		// Token: 0x0400A417 RID: 42007
		private const string tagName = "customXmlMoveFromRangeStart";

		// Token: 0x0400A418 RID: 42008
		private const byte tagNsId = 23;

		// Token: 0x0400A419 RID: 42009
		internal const int ElementTypeIdConst = 11488;
	}
}
