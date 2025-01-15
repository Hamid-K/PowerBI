using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AC8 RID: 10952
	[GeneratedCode("DomGen", "2.0")]
	internal class SubShape : TimeListSubShapeIdType
	{
		// Token: 0x17007540 RID: 30016
		// (get) Token: 0x06016516 RID: 91414 RVA: 0x00328F12 File Offset: 0x00327112
		public override string LocalName
		{
			get
			{
				return "subSp";
			}
		}

		// Token: 0x17007541 RID: 30017
		// (get) Token: 0x06016517 RID: 91415 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007542 RID: 30018
		// (get) Token: 0x06016518 RID: 91416 RVA: 0x00328F19 File Offset: 0x00327119
		internal override int ElementTypeId
		{
			get
			{
				return 12371;
			}
		}

		// Token: 0x06016519 RID: 91417 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601651B RID: 91419 RVA: 0x00328F20 File Offset: 0x00327120
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SubShape>(deep);
		}

		// Token: 0x0400972D RID: 38701
		private const string tagName = "subSp";

		// Token: 0x0400972E RID: 38702
		private const byte tagNsId = 24;

		// Token: 0x0400972F RID: 38703
		internal const int ElementTypeIdConst = 12371;
	}
}
