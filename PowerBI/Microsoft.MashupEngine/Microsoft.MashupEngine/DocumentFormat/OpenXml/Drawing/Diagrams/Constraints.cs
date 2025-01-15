using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200266C RID: 9836
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Constraint))]
	internal class Constraints : OpenXmlCompositeElement
	{
		// Token: 0x17005C1A RID: 23578
		// (get) Token: 0x06012C07 RID: 76807 RVA: 0x002FED6A File Offset: 0x002FCF6A
		public override string LocalName
		{
			get
			{
				return "constrLst";
			}
		}

		// Token: 0x17005C1B RID: 23579
		// (get) Token: 0x06012C08 RID: 76808 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C1C RID: 23580
		// (get) Token: 0x06012C09 RID: 76809 RVA: 0x002FED71 File Offset: 0x002FCF71
		internal override int ElementTypeId
		{
			get
			{
				return 10653;
			}
		}

		// Token: 0x06012C0A RID: 76810 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012C0B RID: 76811 RVA: 0x00293ECF File Offset: 0x002920CF
		public Constraints()
		{
		}

		// Token: 0x06012C0C RID: 76812 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Constraints(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C0D RID: 76813 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Constraints(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C0E RID: 76814 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Constraints(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012C0F RID: 76815 RVA: 0x002FED78 File Offset: 0x002FCF78
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "constr" == name)
			{
				return new Constraint();
			}
			return null;
		}

		// Token: 0x06012C10 RID: 76816 RVA: 0x002FED93 File Offset: 0x002FCF93
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Constraints>(deep);
		}

		// Token: 0x04008176 RID: 33142
		private const string tagName = "constrLst";

		// Token: 0x04008177 RID: 33143
		private const byte tagNsId = 14;

		// Token: 0x04008178 RID: 33144
		internal const int ElementTypeIdConst = 10653;
	}
}
