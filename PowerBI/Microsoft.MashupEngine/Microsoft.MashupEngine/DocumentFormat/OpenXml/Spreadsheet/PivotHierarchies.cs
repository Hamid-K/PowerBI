using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CB8 RID: 11448
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotHierarchy))]
	internal class PivotHierarchies : OpenXmlCompositeElement
	{
		// Token: 0x170084B8 RID: 33976
		// (get) Token: 0x060187BB RID: 100283 RVA: 0x00341D43 File Offset: 0x0033FF43
		public override string LocalName
		{
			get
			{
				return "pivotHierarchies";
			}
		}

		// Token: 0x170084B9 RID: 33977
		// (get) Token: 0x060187BC RID: 100284 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084BA RID: 33978
		// (get) Token: 0x060187BD RID: 100285 RVA: 0x00341D4A File Offset: 0x0033FF4A
		internal override int ElementTypeId
		{
			get
			{
				return 11428;
			}
		}

		// Token: 0x060187BE RID: 100286 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084BB RID: 33979
		// (get) Token: 0x060187BF RID: 100287 RVA: 0x00341D51 File Offset: 0x0033FF51
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotHierarchies.attributeTagNames;
			}
		}

		// Token: 0x170084BC RID: 33980
		// (get) Token: 0x060187C0 RID: 100288 RVA: 0x00341D58 File Offset: 0x0033FF58
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotHierarchies.attributeNamespaceIds;
			}
		}

		// Token: 0x170084BD RID: 33981
		// (get) Token: 0x060187C1 RID: 100289 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060187C2 RID: 100290 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060187C3 RID: 100291 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotHierarchies()
		{
		}

		// Token: 0x060187C4 RID: 100292 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotHierarchies(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060187C5 RID: 100293 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotHierarchies(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060187C6 RID: 100294 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotHierarchies(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060187C7 RID: 100295 RVA: 0x00341D5F File Offset: 0x0033FF5F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotHierarchy" == name)
			{
				return new PivotHierarchy();
			}
			return null;
		}

		// Token: 0x060187C8 RID: 100296 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060187C9 RID: 100297 RVA: 0x00341D7A File Offset: 0x0033FF7A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotHierarchies>(deep);
		}

		// Token: 0x060187CA RID: 100298 RVA: 0x00341D84 File Offset: 0x0033FF84
		// Note: this type is marked as 'beforefieldinit'.
		static PivotHierarchies()
		{
			byte[] array = new byte[1];
			PivotHierarchies.attributeNamespaceIds = array;
		}

		// Token: 0x0400A07D RID: 41085
		private const string tagName = "pivotHierarchies";

		// Token: 0x0400A07E RID: 41086
		private const byte tagNsId = 22;

		// Token: 0x0400A07F RID: 41087
		internal const int ElementTypeIdConst = 11428;

		// Token: 0x0400A080 RID: 41088
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A081 RID: 41089
		private static byte[] attributeNamespaceIds;
	}
}
