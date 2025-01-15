using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029B9 RID: 10681
	[GeneratedCode("DomGen", "2.0")]
	internal class PostSpacing : TwipsMeasureType
	{
		// Token: 0x17006DA3 RID: 28067
		// (get) Token: 0x06015416 RID: 87062 RVA: 0x0031D4E8 File Offset: 0x0031B6E8
		public override string LocalName
		{
			get
			{
				return "postSp";
			}
		}

		// Token: 0x17006DA4 RID: 28068
		// (get) Token: 0x06015417 RID: 87063 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DA5 RID: 28069
		// (get) Token: 0x06015418 RID: 87064 RVA: 0x0031D4EF File Offset: 0x0031B6EF
		internal override int ElementTypeId
		{
			get
			{
				return 10955;
			}
		}

		// Token: 0x06015419 RID: 87065 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601541B RID: 87067 RVA: 0x0031D4F6 File Offset: 0x0031B6F6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PostSpacing>(deep);
		}

		// Token: 0x0400925B RID: 37467
		private const string tagName = "postSp";

		// Token: 0x0400925C RID: 37468
		private const byte tagNsId = 21;

		// Token: 0x0400925D RID: 37469
		internal const int ElementTypeIdConst = 10955;
	}
}
