using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200255E RID: 9566
	[GeneratedCode("DomGen", "2.0")]
	internal class Width : DoubleType
	{
		// Token: 0x17005596 RID: 21910
		// (get) Token: 0x06011D61 RID: 73057 RVA: 0x002F2F1C File Offset: 0x002F111C
		public override string LocalName
		{
			get
			{
				return "w";
			}
		}

		// Token: 0x17005597 RID: 21911
		// (get) Token: 0x06011D62 RID: 73058 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005598 RID: 21912
		// (get) Token: 0x06011D63 RID: 73059 RVA: 0x002F2F23 File Offset: 0x002F1123
		internal override int ElementTypeId
		{
			get
			{
				return 10413;
			}
		}

		// Token: 0x06011D64 RID: 73060 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D66 RID: 73062 RVA: 0x002F2F2A File Offset: 0x002F112A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Width>(deep);
		}

		// Token: 0x04007CCA RID: 31946
		private const string tagName = "w";

		// Token: 0x04007CCB RID: 31947
		private const byte tagNsId = 11;

		// Token: 0x04007CCC RID: 31948
		internal const int ElementTypeIdConst = 10413;
	}
}
