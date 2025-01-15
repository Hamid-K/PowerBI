using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C57 RID: 11351
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExternalReference))]
	internal class ExternalReferences : OpenXmlCompositeElement
	{
		// Token: 0x17008243 RID: 33347
		// (get) Token: 0x060181B6 RID: 98742 RVA: 0x002A804F File Offset: 0x002A624F
		public override string LocalName
		{
			get
			{
				return "externalReferences";
			}
		}

		// Token: 0x17008244 RID: 33348
		// (get) Token: 0x060181B7 RID: 98743 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008245 RID: 33349
		// (get) Token: 0x060181B8 RID: 98744 RVA: 0x0033E923 File Offset: 0x0033CB23
		internal override int ElementTypeId
		{
			get
			{
				return 11332;
			}
		}

		// Token: 0x060181B9 RID: 98745 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060181BA RID: 98746 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExternalReferences()
		{
		}

		// Token: 0x060181BB RID: 98747 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExternalReferences(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060181BC RID: 98748 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExternalReferences(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060181BD RID: 98749 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExternalReferences(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060181BE RID: 98750 RVA: 0x0033E92A File Offset: 0x0033CB2A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "externalReference" == name)
			{
				return new ExternalReference();
			}
			return null;
		}

		// Token: 0x060181BF RID: 98751 RVA: 0x0033E945 File Offset: 0x0033CB45
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExternalReferences>(deep);
		}

		// Token: 0x04009EE0 RID: 40672
		private const string tagName = "externalReferences";

		// Token: 0x04009EE1 RID: 40673
		private const byte tagNsId = 22;

		// Token: 0x04009EE2 RID: 40674
		internal const int ElementTypeIdConst = 11332;
	}
}
