using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CBA RID: 11450
	[ChildElementInfo(typeof(PivotFilter))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotFilters : OpenXmlCompositeElement
	{
		// Token: 0x170084C9 RID: 33993
		// (get) Token: 0x060187E1 RID: 100321 RVA: 0x00341ECB File Offset: 0x003400CB
		public override string LocalName
		{
			get
			{
				return "filters";
			}
		}

		// Token: 0x170084CA RID: 33994
		// (get) Token: 0x060187E2 RID: 100322 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084CB RID: 33995
		// (get) Token: 0x060187E3 RID: 100323 RVA: 0x00341ED2 File Offset: 0x003400D2
		internal override int ElementTypeId
		{
			get
			{
				return 11430;
			}
		}

		// Token: 0x060187E4 RID: 100324 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084CC RID: 33996
		// (get) Token: 0x060187E5 RID: 100325 RVA: 0x00341ED9 File Offset: 0x003400D9
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotFilters.attributeTagNames;
			}
		}

		// Token: 0x170084CD RID: 33997
		// (get) Token: 0x060187E6 RID: 100326 RVA: 0x00341EE0 File Offset: 0x003400E0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotFilters.attributeNamespaceIds;
			}
		}

		// Token: 0x170084CE RID: 33998
		// (get) Token: 0x060187E7 RID: 100327 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060187E8 RID: 100328 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060187E9 RID: 100329 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotFilters()
		{
		}

		// Token: 0x060187EA RID: 100330 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotFilters(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060187EB RID: 100331 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotFilters(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060187EC RID: 100332 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotFilters(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060187ED RID: 100333 RVA: 0x00341EE7 File Offset: 0x003400E7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "filter" == name)
			{
				return new PivotFilter();
			}
			return null;
		}

		// Token: 0x060187EE RID: 100334 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060187EF RID: 100335 RVA: 0x00341F02 File Offset: 0x00340102
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotFilters>(deep);
		}

		// Token: 0x060187F0 RID: 100336 RVA: 0x00341F0C File Offset: 0x0034010C
		// Note: this type is marked as 'beforefieldinit'.
		static PivotFilters()
		{
			byte[] array = new byte[1];
			PivotFilters.attributeNamespaceIds = array;
		}

		// Token: 0x0400A087 RID: 41095
		private const string tagName = "filters";

		// Token: 0x0400A088 RID: 41096
		private const byte tagNsId = 22;

		// Token: 0x0400A089 RID: 41097
		internal const int ElementTypeIdConst = 11430;

		// Token: 0x0400A08A RID: 41098
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A08B RID: 41099
		private static byte[] attributeNamespaceIds;
	}
}
