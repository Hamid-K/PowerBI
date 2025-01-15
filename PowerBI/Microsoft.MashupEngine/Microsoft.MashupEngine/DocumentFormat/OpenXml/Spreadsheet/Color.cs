using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BA3 RID: 11171
	[GeneratedCode("DomGen", "2.0")]
	internal class Color : ColorType
	{
		// Token: 0x17007B52 RID: 31570
		// (get) Token: 0x060172A3 RID: 94883 RVA: 0x002E847F File Offset: 0x002E667F
		public override string LocalName
		{
			get
			{
				return "color";
			}
		}

		// Token: 0x17007B53 RID: 31571
		// (get) Token: 0x060172A4 RID: 94884 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B54 RID: 31572
		// (get) Token: 0x060172A5 RID: 94885 RVA: 0x00333667 File Offset: 0x00331867
		internal override int ElementTypeId
		{
			get
			{
				return 11145;
			}
		}

		// Token: 0x060172A6 RID: 94886 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060172A8 RID: 94888 RVA: 0x00333676 File Offset: 0x00331876
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Color>(deep);
		}

		// Token: 0x04009B5D RID: 39773
		private const string tagName = "color";

		// Token: 0x04009B5E RID: 39774
		private const byte tagNsId = 22;

		// Token: 0x04009B5F RID: 39775
		internal const int ElementTypeIdConst = 11145;
	}
}
