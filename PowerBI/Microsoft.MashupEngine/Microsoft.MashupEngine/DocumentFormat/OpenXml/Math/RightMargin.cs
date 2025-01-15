using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029B7 RID: 10679
	[GeneratedCode("DomGen", "2.0")]
	internal class RightMargin : TwipsMeasureType
	{
		// Token: 0x17006D9D RID: 28061
		// (get) Token: 0x0601540A RID: 87050 RVA: 0x0031D4BA File Offset: 0x0031B6BA
		public override string LocalName
		{
			get
			{
				return "rMargin";
			}
		}

		// Token: 0x17006D9E RID: 28062
		// (get) Token: 0x0601540B RID: 87051 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D9F RID: 28063
		// (get) Token: 0x0601540C RID: 87052 RVA: 0x0031D4C1 File Offset: 0x0031B6C1
		internal override int ElementTypeId
		{
			get
			{
				return 10952;
			}
		}

		// Token: 0x0601540D RID: 87053 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601540F RID: 87055 RVA: 0x0031D4C8 File Offset: 0x0031B6C8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightMargin>(deep);
		}

		// Token: 0x04009255 RID: 37461
		private const string tagName = "rMargin";

		// Token: 0x04009256 RID: 37462
		private const byte tagNsId = 21;

		// Token: 0x04009257 RID: 37463
		internal const int ElementTypeIdConst = 10952;
	}
}
