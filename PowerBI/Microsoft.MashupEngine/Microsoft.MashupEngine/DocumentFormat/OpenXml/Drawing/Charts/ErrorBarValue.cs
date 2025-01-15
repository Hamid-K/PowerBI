using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002563 RID: 9571
	[GeneratedCode("DomGen", "2.0")]
	internal class ErrorBarValue : DoubleType
	{
		// Token: 0x170055A5 RID: 21925
		// (get) Token: 0x06011D7F RID: 73087 RVA: 0x002F2F88 File Offset: 0x002F1188
		public override string LocalName
		{
			get
			{
				return "val";
			}
		}

		// Token: 0x170055A6 RID: 21926
		// (get) Token: 0x06011D80 RID: 73088 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055A7 RID: 21927
		// (get) Token: 0x06011D81 RID: 73089 RVA: 0x002F2F8F File Offset: 0x002F118F
		internal override int ElementTypeId
		{
			get
			{
				return 10452;
			}
		}

		// Token: 0x06011D82 RID: 73090 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D84 RID: 73092 RVA: 0x002F2F96 File Offset: 0x002F1196
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ErrorBarValue>(deep);
		}

		// Token: 0x04007CD9 RID: 31961
		private const string tagName = "val";

		// Token: 0x04007CDA RID: 31962
		private const byte tagNsId = 11;

		// Token: 0x04007CDB RID: 31963
		internal const int ElementTypeIdConst = 10452;
	}
}
