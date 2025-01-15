using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002979 RID: 10617
	[GeneratedCode("DomGen", "2.0")]
	internal class ObjectDistribution : OnOffType
	{
		// Token: 0x17006C76 RID: 27766
		// (get) Token: 0x0601517A RID: 86394 RVA: 0x0031B544 File Offset: 0x00319744
		public override string LocalName
		{
			get
			{
				return "objDist";
			}
		}

		// Token: 0x17006C77 RID: 27767
		// (get) Token: 0x0601517B RID: 86395 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C78 RID: 27768
		// (get) Token: 0x0601517C RID: 86396 RVA: 0x0031B54B File Offset: 0x0031974B
		internal override int ElementTypeId
		{
			get
			{
				return 10897;
			}
		}

		// Token: 0x0601517D RID: 86397 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601517F RID: 86399 RVA: 0x0031B552 File Offset: 0x00319752
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ObjectDistribution>(deep);
		}

		// Token: 0x04009177 RID: 37239
		private const string tagName = "objDist";

		// Token: 0x04009178 RID: 37240
		private const byte tagNsId = 21;

		// Token: 0x04009179 RID: 37241
		internal const int ElementTypeIdConst = 10897;
	}
}
