using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CBB RID: 11451
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RowHierarchyUsage))]
	internal class RowHierarchiesUsage : OpenXmlCompositeElement
	{
		// Token: 0x170084CF RID: 33999
		// (get) Token: 0x060187F1 RID: 100337 RVA: 0x00341F3B File Offset: 0x0034013B
		public override string LocalName
		{
			get
			{
				return "rowHierarchiesUsage";
			}
		}

		// Token: 0x170084D0 RID: 34000
		// (get) Token: 0x060187F2 RID: 100338 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084D1 RID: 34001
		// (get) Token: 0x060187F3 RID: 100339 RVA: 0x00341F42 File Offset: 0x00340142
		internal override int ElementTypeId
		{
			get
			{
				return 11431;
			}
		}

		// Token: 0x060187F4 RID: 100340 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084D2 RID: 34002
		// (get) Token: 0x060187F5 RID: 100341 RVA: 0x00341F49 File Offset: 0x00340149
		internal override string[] AttributeTagNames
		{
			get
			{
				return RowHierarchiesUsage.attributeTagNames;
			}
		}

		// Token: 0x170084D3 RID: 34003
		// (get) Token: 0x060187F6 RID: 100342 RVA: 0x00341F50 File Offset: 0x00340150
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RowHierarchiesUsage.attributeNamespaceIds;
			}
		}

		// Token: 0x170084D4 RID: 34004
		// (get) Token: 0x060187F7 RID: 100343 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060187F8 RID: 100344 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060187F9 RID: 100345 RVA: 0x00293ECF File Offset: 0x002920CF
		public RowHierarchiesUsage()
		{
		}

		// Token: 0x060187FA RID: 100346 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RowHierarchiesUsage(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060187FB RID: 100347 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RowHierarchiesUsage(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060187FC RID: 100348 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RowHierarchiesUsage(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060187FD RID: 100349 RVA: 0x00341F57 File Offset: 0x00340157
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "rowHierarchyUsage" == name)
			{
				return new RowHierarchyUsage();
			}
			return null;
		}

		// Token: 0x060187FE RID: 100350 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060187FF RID: 100351 RVA: 0x00341F72 File Offset: 0x00340172
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowHierarchiesUsage>(deep);
		}

		// Token: 0x06018800 RID: 100352 RVA: 0x00341F7C File Offset: 0x0034017C
		// Note: this type is marked as 'beforefieldinit'.
		static RowHierarchiesUsage()
		{
			byte[] array = new byte[1];
			RowHierarchiesUsage.attributeNamespaceIds = array;
		}

		// Token: 0x0400A08C RID: 41100
		private const string tagName = "rowHierarchiesUsage";

		// Token: 0x0400A08D RID: 41101
		private const byte tagNsId = 22;

		// Token: 0x0400A08E RID: 41102
		internal const int ElementTypeIdConst = 11431;

		// Token: 0x0400A08F RID: 41103
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A090 RID: 41104
		private static byte[] attributeNamespaceIds;
	}
}
