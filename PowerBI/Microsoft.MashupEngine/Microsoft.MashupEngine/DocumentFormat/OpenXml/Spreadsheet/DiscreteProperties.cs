using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B89 RID: 11145
	[ChildElementInfo(typeof(FieldItem))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DiscreteProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007AA9 RID: 31401
		// (get) Token: 0x06017128 RID: 94504 RVA: 0x0033275F File Offset: 0x0033095F
		public override string LocalName
		{
			get
			{
				return "discretePr";
			}
		}

		// Token: 0x17007AAA RID: 31402
		// (get) Token: 0x06017129 RID: 94505 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007AAB RID: 31403
		// (get) Token: 0x0601712A RID: 94506 RVA: 0x00332766 File Offset: 0x00330966
		internal override int ElementTypeId
		{
			get
			{
				return 11123;
			}
		}

		// Token: 0x0601712B RID: 94507 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007AAC RID: 31404
		// (get) Token: 0x0601712C RID: 94508 RVA: 0x0033276D File Offset: 0x0033096D
		internal override string[] AttributeTagNames
		{
			get
			{
				return DiscreteProperties.attributeTagNames;
			}
		}

		// Token: 0x17007AAD RID: 31405
		// (get) Token: 0x0601712D RID: 94509 RVA: 0x00332774 File Offset: 0x00330974
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DiscreteProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007AAE RID: 31406
		// (get) Token: 0x0601712E RID: 94510 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601712F RID: 94511 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06017130 RID: 94512 RVA: 0x00293ECF File Offset: 0x002920CF
		public DiscreteProperties()
		{
		}

		// Token: 0x06017131 RID: 94513 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DiscreteProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017132 RID: 94514 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DiscreteProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017133 RID: 94515 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DiscreteProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017134 RID: 94516 RVA: 0x0033277B File Offset: 0x0033097B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "x" == name)
			{
				return new FieldItem();
			}
			return null;
		}

		// Token: 0x06017135 RID: 94517 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017136 RID: 94518 RVA: 0x00332796 File Offset: 0x00330996
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DiscreteProperties>(deep);
		}

		// Token: 0x06017137 RID: 94519 RVA: 0x003327A0 File Offset: 0x003309A0
		// Note: this type is marked as 'beforefieldinit'.
		static DiscreteProperties()
		{
			byte[] array = new byte[1];
			DiscreteProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009AF2 RID: 39666
		private const string tagName = "discretePr";

		// Token: 0x04009AF3 RID: 39667
		private const byte tagNsId = 22;

		// Token: 0x04009AF4 RID: 39668
		internal const int ElementTypeIdConst = 11123;

		// Token: 0x04009AF5 RID: 39669
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009AF6 RID: 39670
		private static byte[] attributeNamespaceIds;
	}
}
