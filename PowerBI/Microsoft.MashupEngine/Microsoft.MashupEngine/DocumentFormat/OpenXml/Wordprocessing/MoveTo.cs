using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D3E RID: 11582
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveTo : TrackChangeType
	{
		// Token: 0x17008641 RID: 34369
		// (get) Token: 0x06018B3D RID: 101181 RVA: 0x0030BDF9 File Offset: 0x00309FF9
		public override string LocalName
		{
			get
			{
				return "moveTo";
			}
		}

		// Token: 0x17008642 RID: 34370
		// (get) Token: 0x06018B3E RID: 101182 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008643 RID: 34371
		// (get) Token: 0x06018B3F RID: 101183 RVA: 0x0034415F File Offset: 0x0034235F
		internal override int ElementTypeId
		{
			get
			{
				return 11689;
			}
		}

		// Token: 0x06018B40 RID: 101184 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B42 RID: 101186 RVA: 0x00344166 File Offset: 0x00342366
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveTo>(deep);
		}

		// Token: 0x0400A426 RID: 42022
		private const string tagName = "moveTo";

		// Token: 0x0400A427 RID: 42023
		private const byte tagNsId = 23;

		// Token: 0x0400A428 RID: 42024
		internal const int ElementTypeIdConst = 11689;
	}
}
