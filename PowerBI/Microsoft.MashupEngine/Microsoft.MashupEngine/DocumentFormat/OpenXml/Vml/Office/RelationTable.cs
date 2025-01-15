using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002219 RID: 8729
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Relation))]
	internal class RelationTable : OpenXmlCompositeElement
	{
		// Token: 0x17003923 RID: 14627
		// (get) Token: 0x0600DFEB RID: 57323 RVA: 0x002BF84B File Offset: 0x002BDA4B
		public override string LocalName
		{
			get
			{
				return "relationtable";
			}
		}

		// Token: 0x17003924 RID: 14628
		// (get) Token: 0x0600DFEC RID: 57324 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003925 RID: 14629
		// (get) Token: 0x0600DFED RID: 57325 RVA: 0x002BF852 File Offset: 0x002BDA52
		internal override int ElementTypeId
		{
			get
			{
				return 12422;
			}
		}

		// Token: 0x0600DFEE RID: 57326 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003926 RID: 14630
		// (get) Token: 0x0600DFEF RID: 57327 RVA: 0x002BF859 File Offset: 0x002BDA59
		internal override string[] AttributeTagNames
		{
			get
			{
				return RelationTable.attributeTagNames;
			}
		}

		// Token: 0x17003927 RID: 14631
		// (get) Token: 0x0600DFF0 RID: 57328 RVA: 0x002BF860 File Offset: 0x002BDA60
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RelationTable.attributeNamespaceIds;
			}
		}

		// Token: 0x17003928 RID: 14632
		// (get) Token: 0x0600DFF1 RID: 57329 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DFF2 RID: 57330 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0600DFF3 RID: 57331 RVA: 0x00293ECF File Offset: 0x002920CF
		public RelationTable()
		{
		}

		// Token: 0x0600DFF4 RID: 57332 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RelationTable(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DFF5 RID: 57333 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RelationTable(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DFF6 RID: 57334 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RelationTable(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600DFF7 RID: 57335 RVA: 0x002BF867 File Offset: 0x002BDA67
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "rel" == name)
			{
				return new Relation();
			}
			return null;
		}

		// Token: 0x0600DFF8 RID: 57336 RVA: 0x002BDA15 File Offset: 0x002BBC15
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DFF9 RID: 57337 RVA: 0x002BF882 File Offset: 0x002BDA82
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RelationTable>(deep);
		}

		// Token: 0x04006DC1 RID: 28097
		private const string tagName = "relationtable";

		// Token: 0x04006DC2 RID: 28098
		private const byte tagNsId = 27;

		// Token: 0x04006DC3 RID: 28099
		internal const int ElementTypeIdConst = 12422;

		// Token: 0x04006DC4 RID: 28100
		private static string[] attributeTagNames = new string[] { "ext" };

		// Token: 0x04006DC5 RID: 28101
		private static byte[] attributeNamespaceIds = new byte[] { 26 };
	}
}
