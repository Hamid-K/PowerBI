using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F3E RID: 12094
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultTextBoxFormFieldString : String255Type
	{
		// Token: 0x17008FAE RID: 36782
		// (get) Token: 0x06019F38 RID: 106296 RVA: 0x00344DAC File Offset: 0x00342FAC
		public override string LocalName
		{
			get
			{
				return "default";
			}
		}

		// Token: 0x17008FAF RID: 36783
		// (get) Token: 0x06019F39 RID: 106297 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FB0 RID: 36784
		// (get) Token: 0x06019F3A RID: 106298 RVA: 0x0035A36B File Offset: 0x0035856B
		internal override int ElementTypeId
		{
			get
			{
				return 11744;
			}
		}

		// Token: 0x06019F3B RID: 106299 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019F3D RID: 106301 RVA: 0x0035A372 File Offset: 0x00358572
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultTextBoxFormFieldString>(deep);
		}

		// Token: 0x0400AB0E RID: 43790
		private const string tagName = "default";

		// Token: 0x0400AB0F RID: 43791
		private const byte tagNsId = 23;

		// Token: 0x0400AB10 RID: 43792
		internal const int ElementTypeIdConst = 11744;
	}
}
