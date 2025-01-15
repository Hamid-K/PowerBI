using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.CustomXmlDataProperties
{
	// Token: 0x0200290C RID: 10508
	[ChildElementInfo(typeof(SchemaReference))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SchemaReferences : OpenXmlCompositeElement
	{
		// Token: 0x17006A50 RID: 27216
		// (get) Token: 0x06014C73 RID: 85107 RVA: 0x00316FFC File Offset: 0x003151FC
		public override string LocalName
		{
			get
			{
				return "schemaRefs";
			}
		}

		// Token: 0x17006A51 RID: 27217
		// (get) Token: 0x06014C74 RID: 85108 RVA: 0x002435AE File Offset: 0x002417AE
		internal override byte NamespaceId
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x17006A52 RID: 27218
		// (get) Token: 0x06014C75 RID: 85109 RVA: 0x00317003 File Offset: 0x00315203
		internal override int ElementTypeId
		{
			get
			{
				return 10836;
			}
		}

		// Token: 0x06014C76 RID: 85110 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014C77 RID: 85111 RVA: 0x00293ECF File Offset: 0x002920CF
		public SchemaReferences()
		{
		}

		// Token: 0x06014C78 RID: 85112 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SchemaReferences(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014C79 RID: 85113 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SchemaReferences(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014C7A RID: 85114 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SchemaReferences(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014C7B RID: 85115 RVA: 0x0031700A File Offset: 0x0031520A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (20 == namespaceId && "schemaRef" == name)
			{
				return new SchemaReference();
			}
			return null;
		}

		// Token: 0x06014C7C RID: 85116 RVA: 0x00317025 File Offset: 0x00315225
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SchemaReferences>(deep);
		}

		// Token: 0x04008FD0 RID: 36816
		private const string tagName = "schemaRefs";

		// Token: 0x04008FD1 RID: 36817
		private const byte tagNsId = 20;

		// Token: 0x04008FD2 RID: 36818
		internal const int ElementTypeIdConst = 10836;
	}
}
