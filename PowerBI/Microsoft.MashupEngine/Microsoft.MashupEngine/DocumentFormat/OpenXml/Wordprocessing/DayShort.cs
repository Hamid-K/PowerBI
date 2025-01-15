using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E5F RID: 11871
	[GeneratedCode("DomGen", "2.0")]
	internal class DayShort : EmptyType
	{
		// Token: 0x17008A5E RID: 35422
		// (get) Token: 0x0601939D RID: 103325 RVA: 0x00347A42 File Offset: 0x00345C42
		public override string LocalName
		{
			get
			{
				return "dayShort";
			}
		}

		// Token: 0x17008A5F RID: 35423
		// (get) Token: 0x0601939E RID: 103326 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A60 RID: 35424
		// (get) Token: 0x0601939F RID: 103327 RVA: 0x00347A49 File Offset: 0x00345C49
		internal override int ElementTypeId
		{
			get
			{
				return 11550;
			}
		}

		// Token: 0x060193A0 RID: 103328 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193A2 RID: 103330 RVA: 0x00347A50 File Offset: 0x00345C50
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DayShort>(deep);
		}

		// Token: 0x0400A7B5 RID: 42933
		private const string tagName = "dayShort";

		// Token: 0x0400A7B6 RID: 42934
		private const byte tagNsId = 23;

		// Token: 0x0400A7B7 RID: 42935
		internal const int ElementTypeIdConst = 11550;
	}
}
