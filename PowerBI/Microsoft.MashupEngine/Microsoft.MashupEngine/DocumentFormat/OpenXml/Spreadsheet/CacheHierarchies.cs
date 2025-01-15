using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CCA RID: 11466
	[ChildElementInfo(typeof(CacheHierarchy))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CacheHierarchies : OpenXmlCompositeElement
	{
		// Token: 0x17008537 RID: 34103
		// (get) Token: 0x060188F3 RID: 100595 RVA: 0x0034296F File Offset: 0x00340B6F
		public override string LocalName
		{
			get
			{
				return "cacheHierarchies";
			}
		}

		// Token: 0x17008538 RID: 34104
		// (get) Token: 0x060188F4 RID: 100596 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008539 RID: 34105
		// (get) Token: 0x060188F5 RID: 100597 RVA: 0x00342976 File Offset: 0x00340B76
		internal override int ElementTypeId
		{
			get
			{
				return 11447;
			}
		}

		// Token: 0x060188F6 RID: 100598 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700853A RID: 34106
		// (get) Token: 0x060188F7 RID: 100599 RVA: 0x0034297D File Offset: 0x00340B7D
		internal override string[] AttributeTagNames
		{
			get
			{
				return CacheHierarchies.attributeTagNames;
			}
		}

		// Token: 0x1700853B RID: 34107
		// (get) Token: 0x060188F8 RID: 100600 RVA: 0x00342984 File Offset: 0x00340B84
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CacheHierarchies.attributeNamespaceIds;
			}
		}

		// Token: 0x1700853C RID: 34108
		// (get) Token: 0x060188F9 RID: 100601 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060188FA RID: 100602 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060188FB RID: 100603 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheHierarchies()
		{
		}

		// Token: 0x060188FC RID: 100604 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheHierarchies(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188FD RID: 100605 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheHierarchies(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188FE RID: 100606 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheHierarchies(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060188FF RID: 100607 RVA: 0x0034298B File Offset: 0x00340B8B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cacheHierarchy" == name)
			{
				return new CacheHierarchy();
			}
			return null;
		}

		// Token: 0x06018900 RID: 100608 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018901 RID: 100609 RVA: 0x003429A6 File Offset: 0x00340BA6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheHierarchies>(deep);
		}

		// Token: 0x06018902 RID: 100610 RVA: 0x003429B0 File Offset: 0x00340BB0
		// Note: this type is marked as 'beforefieldinit'.
		static CacheHierarchies()
		{
			byte[] array = new byte[1];
			CacheHierarchies.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0D1 RID: 41169
		private const string tagName = "cacheHierarchies";

		// Token: 0x0400A0D2 RID: 41170
		private const byte tagNsId = 22;

		// Token: 0x0400A0D3 RID: 41171
		internal const int ElementTypeIdConst = 11447;

		// Token: 0x0400A0D4 RID: 41172
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A0D5 RID: 41173
		private static byte[] attributeNamespaceIds;
	}
}
