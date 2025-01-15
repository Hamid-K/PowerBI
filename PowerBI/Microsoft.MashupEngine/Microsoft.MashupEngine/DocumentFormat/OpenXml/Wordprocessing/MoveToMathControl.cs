using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EB7 RID: 11959
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveToMathControl : MathControlMoveType
	{
		// Token: 0x17008C1E RID: 35870
		// (get) Token: 0x06019742 RID: 104258 RVA: 0x0030BDF9 File Offset: 0x00309FF9
		public override string LocalName
		{
			get
			{
				return "moveTo";
			}
		}

		// Token: 0x17008C1F RID: 35871
		// (get) Token: 0x06019743 RID: 104259 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C20 RID: 35872
		// (get) Token: 0x06019744 RID: 104260 RVA: 0x0034A3FF File Offset: 0x003485FF
		internal override int ElementTypeId
		{
			get
			{
				return 11617;
			}
		}

		// Token: 0x06019745 RID: 104261 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019746 RID: 104262 RVA: 0x0034A3D3 File Offset: 0x003485D3
		public MoveToMathControl()
		{
		}

		// Token: 0x06019747 RID: 104263 RVA: 0x0034A3DB File Offset: 0x003485DB
		public MoveToMathControl(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019748 RID: 104264 RVA: 0x0034A3E4 File Offset: 0x003485E4
		public MoveToMathControl(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019749 RID: 104265 RVA: 0x0034A3ED File Offset: 0x003485ED
		public MoveToMathControl(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601974A RID: 104266 RVA: 0x0034A406 File Offset: 0x00348606
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveToMathControl>(deep);
		}

		// Token: 0x0400A8EB RID: 43243
		private const string tagName = "moveTo";

		// Token: 0x0400A8EC RID: 43244
		private const byte tagNsId = 23;

		// Token: 0x0400A8ED RID: 43245
		internal const int ElementTypeIdConst = 11617;
	}
}
