using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029B5 RID: 10677
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnSpacing : TwipsMeasureType
	{
		// Token: 0x17006D97 RID: 28055
		// (get) Token: 0x060153FE RID: 87038 RVA: 0x0031D484 File Offset: 0x0031B684
		public override string LocalName
		{
			get
			{
				return "cSp";
			}
		}

		// Token: 0x17006D98 RID: 28056
		// (get) Token: 0x060153FF RID: 87039 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D99 RID: 28057
		// (get) Token: 0x06015400 RID: 87040 RVA: 0x0031D48B File Offset: 0x0031B68B
		internal override int ElementTypeId
		{
			get
			{
				return 10918;
			}
		}

		// Token: 0x06015401 RID: 87041 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015403 RID: 87043 RVA: 0x0031D49A File Offset: 0x0031B69A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnSpacing>(deep);
		}

		// Token: 0x0400924F RID: 37455
		private const string tagName = "cSp";

		// Token: 0x04009250 RID: 37456
		private const byte tagNsId = 21;

		// Token: 0x04009251 RID: 37457
		internal const int ElementTypeIdConst = 10918;
	}
}
