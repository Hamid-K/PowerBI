using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x02002389 RID: 9097
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualInkProperties : NonVisualInkContentPartPropertiesType
	{
		// Token: 0x17004B8F RID: 19343
		// (get) Token: 0x060106FE RID: 67326 RVA: 0x002DFE0F File Offset: 0x002DE00F
		public override string LocalName
		{
			get
			{
				return "cNvInkPr";
			}
		}

		// Token: 0x17004B90 RID: 19344
		// (get) Token: 0x060106FF RID: 67327 RVA: 0x002E35B9 File Offset: 0x002E17B9
		internal override byte NamespaceId
		{
			get
			{
				return 54;
			}
		}

		// Token: 0x17004B91 RID: 19345
		// (get) Token: 0x06010700 RID: 67328 RVA: 0x002E3967 File Offset: 0x002E1B67
		internal override int ElementTypeId
		{
			get
			{
				return 13012;
			}
		}

		// Token: 0x06010701 RID: 67329 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010702 RID: 67330 RVA: 0x002E396E File Offset: 0x002E1B6E
		public NonVisualInkProperties()
		{
		}

		// Token: 0x06010703 RID: 67331 RVA: 0x002E3976 File Offset: 0x002E1B76
		public NonVisualInkProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010704 RID: 67332 RVA: 0x002E397F File Offset: 0x002E1B7F
		public NonVisualInkProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010705 RID: 67333 RVA: 0x002E3988 File Offset: 0x002E1B88
		public NonVisualInkProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010706 RID: 67334 RVA: 0x002E3991 File Offset: 0x002E1B91
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualInkProperties>(deep);
		}

		// Token: 0x04007498 RID: 29848
		private const string tagName = "cNvInkPr";

		// Token: 0x04007499 RID: 29849
		private const byte tagNsId = 54;

		// Token: 0x0400749A RID: 29850
		internal const int ElementTypeIdConst = 13012;
	}
}
