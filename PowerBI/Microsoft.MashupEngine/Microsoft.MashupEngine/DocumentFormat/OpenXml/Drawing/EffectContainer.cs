using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002706 RID: 9990
	[GeneratedCode("DomGen", "2.0")]
	internal class EffectContainer : EffectContainerType
	{
		// Token: 0x17005EA0 RID: 24224
		// (get) Token: 0x06013196 RID: 78230 RVA: 0x00303B77 File Offset: 0x00301D77
		public override string LocalName
		{
			get
			{
				return "cont";
			}
		}

		// Token: 0x17005EA1 RID: 24225
		// (get) Token: 0x06013197 RID: 78231 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EA2 RID: 24226
		// (get) Token: 0x06013198 RID: 78232 RVA: 0x00303B7E File Offset: 0x00301D7E
		internal override int ElementTypeId
		{
			get
			{
				return 10053;
			}
		}

		// Token: 0x06013199 RID: 78233 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601319A RID: 78234 RVA: 0x00303B85 File Offset: 0x00301D85
		public EffectContainer()
		{
		}

		// Token: 0x0601319B RID: 78235 RVA: 0x00303B8D File Offset: 0x00301D8D
		public EffectContainer(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601319C RID: 78236 RVA: 0x00303B96 File Offset: 0x00301D96
		public EffectContainer(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601319D RID: 78237 RVA: 0x00303B9F File Offset: 0x00301D9F
		public EffectContainer(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601319E RID: 78238 RVA: 0x00303BA8 File Offset: 0x00301DA8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EffectContainer>(deep);
		}

		// Token: 0x040084A7 RID: 33959
		private const string tagName = "cont";

		// Token: 0x040084A8 RID: 33960
		private const byte tagNsId = 10;

		// Token: 0x040084A9 RID: 33961
		internal const int ElementTypeIdConst = 10053;
	}
}
