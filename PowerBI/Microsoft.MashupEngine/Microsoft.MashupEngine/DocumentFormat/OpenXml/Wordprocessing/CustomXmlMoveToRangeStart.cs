using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D3A RID: 11578
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlMoveToRangeStart : TrackChangeType
	{
		// Token: 0x17008635 RID: 34357
		// (get) Token: 0x06018B25 RID: 101157 RVA: 0x00344103 File Offset: 0x00342303
		public override string LocalName
		{
			get
			{
				return "customXmlMoveToRangeStart";
			}
		}

		// Token: 0x17008636 RID: 34358
		// (get) Token: 0x06018B26 RID: 101158 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008637 RID: 34359
		// (get) Token: 0x06018B27 RID: 101159 RVA: 0x0034410A File Offset: 0x0034230A
		internal override int ElementTypeId
		{
			get
			{
				return 11490;
			}
		}

		// Token: 0x06018B28 RID: 101160 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B2A RID: 101162 RVA: 0x00344111 File Offset: 0x00342311
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlMoveToRangeStart>(deep);
		}

		// Token: 0x0400A41A RID: 42010
		private const string tagName = "customXmlMoveToRangeStart";

		// Token: 0x0400A41B RID: 42011
		private const byte tagNsId = 23;

		// Token: 0x0400A41C RID: 42012
		internal const int ElementTypeIdConst = 11490;
	}
}
