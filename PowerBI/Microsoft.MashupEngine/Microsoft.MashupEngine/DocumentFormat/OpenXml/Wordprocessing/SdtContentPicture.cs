using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E71 RID: 11889
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtContentPicture : EmptyType
	{
		// Token: 0x17008A94 RID: 35476
		// (get) Token: 0x06019409 RID: 103433 RVA: 0x002D0AB9 File Offset: 0x002CECB9
		public override string LocalName
		{
			get
			{
				return "picture";
			}
		}

		// Token: 0x17008A95 RID: 35477
		// (get) Token: 0x0601940A RID: 103434 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A96 RID: 35478
		// (get) Token: 0x0601940B RID: 103435 RVA: 0x00347BD2 File Offset: 0x00345DD2
		internal override int ElementTypeId
		{
			get
			{
				return 12153;
			}
		}

		// Token: 0x0601940C RID: 103436 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601940E RID: 103438 RVA: 0x00347BD9 File Offset: 0x00345DD9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentPicture>(deep);
		}

		// Token: 0x0400A7EB RID: 42987
		private const string tagName = "picture";

		// Token: 0x0400A7EC RID: 42988
		private const byte tagNsId = 23;

		// Token: 0x0400A7ED RID: 42989
		internal const int ElementTypeIdConst = 12153;
	}
}
