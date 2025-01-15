using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C4B RID: 11339
	[ChildElementInfo(typeof(CacheHierarchy), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class CacheHierarchyExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081D9 RID: 33241
		// (get) Token: 0x060180C2 RID: 98498 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081DA RID: 33242
		// (get) Token: 0x060180C3 RID: 98499 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081DB RID: 33243
		// (get) Token: 0x060180C4 RID: 98500 RVA: 0x0033DEFB File Offset: 0x0033C0FB
		internal override int ElementTypeId
		{
			get
			{
				return 11320;
			}
		}

		// Token: 0x060180C5 RID: 98501 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081DC RID: 33244
		// (get) Token: 0x060180C6 RID: 98502 RVA: 0x0033DF02 File Offset: 0x0033C102
		internal override string[] AttributeTagNames
		{
			get
			{
				return CacheHierarchyExtension.attributeTagNames;
			}
		}

		// Token: 0x170081DD RID: 33245
		// (get) Token: 0x060180C7 RID: 98503 RVA: 0x0033DF09 File Offset: 0x0033C109
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CacheHierarchyExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081DE RID: 33246
		// (get) Token: 0x060180C8 RID: 98504 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060180C9 RID: 98505 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060180CA RID: 98506 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheHierarchyExtension()
		{
		}

		// Token: 0x060180CB RID: 98507 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheHierarchyExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180CC RID: 98508 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheHierarchyExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180CD RID: 98509 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheHierarchyExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060180CE RID: 98510 RVA: 0x0033DF10 File Offset: 0x0033C110
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "cacheHierarchy" == name)
			{
				return new CacheHierarchy();
			}
			return null;
		}

		// Token: 0x060180CF RID: 98511 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060180D0 RID: 98512 RVA: 0x0033DF2B File Offset: 0x0033C12B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheHierarchyExtension>(deep);
		}

		// Token: 0x060180D1 RID: 98513 RVA: 0x0033DF34 File Offset: 0x0033C134
		// Note: this type is marked as 'beforefieldinit'.
		static CacheHierarchyExtension()
		{
			byte[] array = new byte[1];
			CacheHierarchyExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009EA8 RID: 40616
		private const string tagName = "ext";

		// Token: 0x04009EA9 RID: 40617
		private const byte tagNsId = 22;

		// Token: 0x04009EAA RID: 40618
		internal const int ElementTypeIdConst = 11320;

		// Token: 0x04009EAB RID: 40619
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009EAC RID: 40620
		private static byte[] attributeNamespaceIds;
	}
}
