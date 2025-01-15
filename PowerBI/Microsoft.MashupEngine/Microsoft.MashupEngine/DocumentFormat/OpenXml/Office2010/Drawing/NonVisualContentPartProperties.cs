using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002358 RID: 9048
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualContentPartProperties : GvmlContentPartNonVisualType
	{
		// Token: 0x17004A15 RID: 18965
		// (get) Token: 0x060103D7 RID: 66519 RVA: 0x002DFF82 File Offset: 0x002DE182
		public override string LocalName
		{
			get
			{
				return "nvContentPartPr";
			}
		}

		// Token: 0x17004A16 RID: 18966
		// (get) Token: 0x060103D8 RID: 66520 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A17 RID: 18967
		// (get) Token: 0x060103D9 RID: 66521 RVA: 0x002E155F File Offset: 0x002DF75F
		internal override int ElementTypeId
		{
			get
			{
				return 12731;
			}
		}

		// Token: 0x060103DA RID: 66522 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060103DB RID: 66523 RVA: 0x002E1533 File Offset: 0x002DF733
		public NonVisualContentPartProperties()
		{
		}

		// Token: 0x060103DC RID: 66524 RVA: 0x002E153B File Offset: 0x002DF73B
		public NonVisualContentPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103DD RID: 66525 RVA: 0x002E1544 File Offset: 0x002DF744
		public NonVisualContentPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103DE RID: 66526 RVA: 0x002E154D File Offset: 0x002DF74D
		public NonVisualContentPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060103DF RID: 66527 RVA: 0x002E1566 File Offset: 0x002DF766
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualContentPartProperties>(deep);
		}

		// Token: 0x040073AB RID: 29611
		private const string tagName = "nvContentPartPr";

		// Token: 0x040073AC RID: 29612
		private const byte tagNsId = 48;

		// Token: 0x040073AD RID: 29613
		internal const int ElementTypeIdConst = 12731;
	}
}
