using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E5E RID: 11870
	[GeneratedCode("DomGen", "2.0")]
	internal class SoftHyphen : EmptyType
	{
		// Token: 0x17008A5B RID: 35419
		// (get) Token: 0x06019397 RID: 103319 RVA: 0x00347A2B File Offset: 0x00345C2B
		public override string LocalName
		{
			get
			{
				return "softHyphen";
			}
		}

		// Token: 0x17008A5C RID: 35420
		// (get) Token: 0x06019398 RID: 103320 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A5D RID: 35421
		// (get) Token: 0x06019399 RID: 103321 RVA: 0x00347A32 File Offset: 0x00345C32
		internal override int ElementTypeId
		{
			get
			{
				return 11549;
			}
		}

		// Token: 0x0601939A RID: 103322 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601939C RID: 103324 RVA: 0x00347A39 File Offset: 0x00345C39
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SoftHyphen>(deep);
		}

		// Token: 0x0400A7B2 RID: 42930
		private const string tagName = "softHyphen";

		// Token: 0x0400A7B3 RID: 42931
		private const byte tagNsId = 23;

		// Token: 0x0400A7B4 RID: 42932
		internal const int ElementTypeIdConst = 11549;
	}
}
