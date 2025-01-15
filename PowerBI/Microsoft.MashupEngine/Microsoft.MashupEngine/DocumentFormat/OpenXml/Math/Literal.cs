using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002969 RID: 10601
	[GeneratedCode("DomGen", "2.0")]
	internal class Literal : OnOffType
	{
		// Token: 0x17006C46 RID: 27718
		// (get) Token: 0x0601511A RID: 86298 RVA: 0x0031B3CC File Offset: 0x003195CC
		public override string LocalName
		{
			get
			{
				return "lit";
			}
		}

		// Token: 0x17006C47 RID: 27719
		// (get) Token: 0x0601511B RID: 86299 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C48 RID: 27720
		// (get) Token: 0x0601511C RID: 86300 RVA: 0x0031B3D3 File Offset: 0x003195D3
		internal override int ElementTypeId
		{
			get
			{
				return 10864;
			}
		}

		// Token: 0x0601511D RID: 86301 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601511F RID: 86303 RVA: 0x0031B3E2 File Offset: 0x003195E2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Literal>(deep);
		}

		// Token: 0x04009147 RID: 37191
		private const string tagName = "lit";

		// Token: 0x04009148 RID: 37192
		private const byte tagNsId = 21;

		// Token: 0x04009149 RID: 37193
		internal const int ElementTypeIdConst = 10864;
	}
}
