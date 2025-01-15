using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003081 RID: 12417
	[ChildElementInfo(typeof(Grammar))]
	[ChildElementInfo(typeof(EndPointInfo))]
	[ChildElementInfo(typeof(Interpretation))]
	[ChildElementInfo(typeof(OneOf))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Derivation))]
	[ChildElementInfo(typeof(Model))]
	[ChildElementInfo(typeof(Info))]
	[ChildElementInfo(typeof(Group))]
	[ChildElementInfo(typeof(Sequence))]
	internal class Emma : OpenXmlCompositeElement
	{
		// Token: 0x1700974B RID: 38731
		// (get) Token: 0x0601AFA8 RID: 110504 RVA: 0x0036A375 File Offset: 0x00368575
		public override string LocalName
		{
			get
			{
				return "emma";
			}
		}

		// Token: 0x1700974C RID: 38732
		// (get) Token: 0x0601AFA9 RID: 110505 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x1700974D RID: 38733
		// (get) Token: 0x0601AFAA RID: 110506 RVA: 0x0036A37C File Offset: 0x0036857C
		internal override int ElementTypeId
		{
			get
			{
				return 12686;
			}
		}

		// Token: 0x0601AFAB RID: 110507 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700974E RID: 38734
		// (get) Token: 0x0601AFAC RID: 110508 RVA: 0x0036A383 File Offset: 0x00368583
		internal override string[] AttributeTagNames
		{
			get
			{
				return Emma.attributeTagNames;
			}
		}

		// Token: 0x1700974F RID: 38735
		// (get) Token: 0x0601AFAD RID: 110509 RVA: 0x0036A38A File Offset: 0x0036858A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Emma.attributeNamespaceIds;
			}
		}

		// Token: 0x17009750 RID: 38736
		// (get) Token: 0x0601AFAE RID: 110510 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AFAF RID: 110511 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "version")]
		public StringValue Version
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

		// Token: 0x0601AFB0 RID: 110512 RVA: 0x00293ECF File Offset: 0x002920CF
		public Emma()
		{
		}

		// Token: 0x0601AFB1 RID: 110513 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Emma(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AFB2 RID: 110514 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Emma(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AFB3 RID: 110515 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Emma(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AFB4 RID: 110516 RVA: 0x0036A394 File Offset: 0x00368594
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (44 == namespaceId && "derivation" == name)
			{
				return new Derivation();
			}
			if (44 == namespaceId && "grammar" == name)
			{
				return new Grammar();
			}
			if (44 == namespaceId && "model" == name)
			{
				return new Model();
			}
			if (44 == namespaceId && "endpoint-info" == name)
			{
				return new EndPointInfo();
			}
			if (44 == namespaceId && "info" == name)
			{
				return new Info();
			}
			if (44 == namespaceId && "interpretation" == name)
			{
				return new Interpretation();
			}
			if (44 == namespaceId && "one-of" == name)
			{
				return new OneOf();
			}
			if (44 == namespaceId && "group" == name)
			{
				return new Group();
			}
			if (44 == namespaceId && "sequence" == name)
			{
				return new Sequence();
			}
			return null;
		}

		// Token: 0x0601AFB5 RID: 110517 RVA: 0x0031828B File Offset: 0x0031648B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "version" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AFB6 RID: 110518 RVA: 0x0036A47A File Offset: 0x0036867A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Emma>(deep);
		}

		// Token: 0x0601AFB7 RID: 110519 RVA: 0x0036A484 File Offset: 0x00368684
		// Note: this type is marked as 'beforefieldinit'.
		static Emma()
		{
			byte[] array = new byte[1];
			Emma.attributeNamespaceIds = array;
		}

		// Token: 0x0400B257 RID: 45655
		private const string tagName = "emma";

		// Token: 0x0400B258 RID: 45656
		private const byte tagNsId = 44;

		// Token: 0x0400B259 RID: 45657
		internal const int ElementTypeIdConst = 12686;

		// Token: 0x0400B25A RID: 45658
		private static string[] attributeTagNames = new string[] { "version" };

		// Token: 0x0400B25B RID: 45659
		private static byte[] attributeNamespaceIds;
	}
}
