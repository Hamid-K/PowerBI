using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029EC RID: 10732
	[GeneratedCode("DomGen", "2.0")]
	internal class RandomTransition : EmptyType
	{
		// Token: 0x17006E5D RID: 28253
		// (get) Token: 0x060155A9 RID: 87465 RVA: 0x0031E243 File Offset: 0x0031C443
		public override string LocalName
		{
			get
			{
				return "random";
			}
		}

		// Token: 0x17006E5E RID: 28254
		// (get) Token: 0x060155AA RID: 87466 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E5F RID: 28255
		// (get) Token: 0x060155AB RID: 87467 RVA: 0x0031E24A File Offset: 0x0031C44A
		internal override int ElementTypeId
		{
			get
			{
				return 12388;
			}
		}

		// Token: 0x060155AC RID: 87468 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060155AE RID: 87470 RVA: 0x0031E251 File Offset: 0x0031C451
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RandomTransition>(deep);
		}

		// Token: 0x0400931E RID: 37662
		private const string tagName = "random";

		// Token: 0x0400931F RID: 37663
		private const byte tagNsId = 24;

		// Token: 0x04009320 RID: 37664
		internal const int ElementTypeIdConst = 12388;
	}
}
