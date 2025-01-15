using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E70 RID: 11888
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtContentEquation : EmptyType
	{
		// Token: 0x17008A91 RID: 35473
		// (get) Token: 0x06019403 RID: 103427 RVA: 0x00347BBB File Offset: 0x00345DBB
		public override string LocalName
		{
			get
			{
				return "equation";
			}
		}

		// Token: 0x17008A92 RID: 35474
		// (get) Token: 0x06019404 RID: 103428 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A93 RID: 35475
		// (get) Token: 0x06019405 RID: 103429 RVA: 0x00347BC2 File Offset: 0x00345DC2
		internal override int ElementTypeId
		{
			get
			{
				return 12147;
			}
		}

		// Token: 0x06019406 RID: 103430 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019408 RID: 103432 RVA: 0x00347BC9 File Offset: 0x00345DC9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentEquation>(deep);
		}

		// Token: 0x0400A7E8 RID: 42984
		private const string tagName = "equation";

		// Token: 0x0400A7E9 RID: 42985
		private const byte tagNsId = 23;

		// Token: 0x0400A7EA RID: 42986
		internal const int ElementTypeIdConst = 12147;
	}
}
