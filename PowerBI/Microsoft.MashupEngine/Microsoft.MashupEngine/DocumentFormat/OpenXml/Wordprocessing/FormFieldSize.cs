using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E96 RID: 11926
	[GeneratedCode("DomGen", "2.0")]
	internal class FormFieldSize : HpsMeasureType
	{
		// Token: 0x17008B48 RID: 35656
		// (get) Token: 0x06019581 RID: 103809 RVA: 0x002F460F File Offset: 0x002F280F
		public override string LocalName
		{
			get
			{
				return "size";
			}
		}

		// Token: 0x17008B49 RID: 35657
		// (get) Token: 0x06019582 RID: 103810 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B4A RID: 35658
		// (get) Token: 0x06019583 RID: 103811 RVA: 0x00348C13 File Offset: 0x00346E13
		internal override int ElementTypeId
		{
			get
			{
				return 11736;
			}
		}

		// Token: 0x06019584 RID: 103812 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019586 RID: 103814 RVA: 0x00348C1A File Offset: 0x00346E1A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormFieldSize>(deep);
		}

		// Token: 0x0400A873 RID: 43123
		private const string tagName = "size";

		// Token: 0x0400A874 RID: 43124
		private const byte tagNsId = 23;

		// Token: 0x0400A875 RID: 43125
		internal const int ElementTypeIdConst = 11736;
	}
}
