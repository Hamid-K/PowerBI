using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EB6 RID: 11958
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveFromMathControl : MathControlMoveType
	{
		// Token: 0x17008C1B RID: 35867
		// (get) Token: 0x06019739 RID: 104249 RVA: 0x00344148 File Offset: 0x00342348
		public override string LocalName
		{
			get
			{
				return "moveFrom";
			}
		}

		// Token: 0x17008C1C RID: 35868
		// (get) Token: 0x0601973A RID: 104250 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C1D RID: 35869
		// (get) Token: 0x0601973B RID: 104251 RVA: 0x0034A3CC File Offset: 0x003485CC
		internal override int ElementTypeId
		{
			get
			{
				return 11616;
			}
		}

		// Token: 0x0601973C RID: 104252 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601973D RID: 104253 RVA: 0x0034A3D3 File Offset: 0x003485D3
		public MoveFromMathControl()
		{
		}

		// Token: 0x0601973E RID: 104254 RVA: 0x0034A3DB File Offset: 0x003485DB
		public MoveFromMathControl(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601973F RID: 104255 RVA: 0x0034A3E4 File Offset: 0x003485E4
		public MoveFromMathControl(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019740 RID: 104256 RVA: 0x0034A3ED File Offset: 0x003485ED
		public MoveFromMathControl(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019741 RID: 104257 RVA: 0x0034A3F6 File Offset: 0x003485F6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveFromMathControl>(deep);
		}

		// Token: 0x0400A8E8 RID: 43240
		private const string tagName = "moveFrom";

		// Token: 0x0400A8E9 RID: 43241
		private const byte tagNsId = 23;

		// Token: 0x0400A8EA RID: 43242
		internal const int ElementTypeIdConst = 11616;
	}
}
