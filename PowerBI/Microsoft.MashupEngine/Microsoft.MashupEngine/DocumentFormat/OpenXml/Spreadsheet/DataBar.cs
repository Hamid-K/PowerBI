using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C8A RID: 11402
	[ChildElementInfo(typeof(ConditionalFormatValueObject))]
	[ChildElementInfo(typeof(Color))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataBar : OpenXmlCompositeElement
	{
		// Token: 0x17008392 RID: 33682
		// (get) Token: 0x060184D3 RID: 99539 RVA: 0x002E8C59 File Offset: 0x002E6E59
		public override string LocalName
		{
			get
			{
				return "dataBar";
			}
		}

		// Token: 0x17008393 RID: 33683
		// (get) Token: 0x060184D4 RID: 99540 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008394 RID: 33684
		// (get) Token: 0x060184D5 RID: 99541 RVA: 0x00340492 File Offset: 0x0033E692
		internal override int ElementTypeId
		{
			get
			{
				return 11381;
			}
		}

		// Token: 0x060184D6 RID: 99542 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008395 RID: 33685
		// (get) Token: 0x060184D7 RID: 99543 RVA: 0x00340499 File Offset: 0x0033E699
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataBar.attributeTagNames;
			}
		}

		// Token: 0x17008396 RID: 33686
		// (get) Token: 0x060184D8 RID: 99544 RVA: 0x003404A0 File Offset: 0x0033E6A0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataBar.attributeNamespaceIds;
			}
		}

		// Token: 0x17008397 RID: 33687
		// (get) Token: 0x060184D9 RID: 99545 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060184DA RID: 99546 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "minLength")]
		public UInt32Value MinLength
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

		// Token: 0x17008398 RID: 33688
		// (get) Token: 0x060184DB RID: 99547 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060184DC RID: 99548 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "maxLength")]
		public UInt32Value MaxLength
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008399 RID: 33689
		// (get) Token: 0x060184DD RID: 99549 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060184DE RID: 99550 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showValue")]
		public BooleanValue ShowValue
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060184DF RID: 99551 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataBar()
		{
		}

		// Token: 0x060184E0 RID: 99552 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataBar(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060184E1 RID: 99553 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataBar(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060184E2 RID: 99554 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataBar(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060184E3 RID: 99555 RVA: 0x00340456 File Offset: 0x0033E656
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cfvo" == name)
			{
				return new ConditionalFormatValueObject();
			}
			if (22 == namespaceId && "color" == name)
			{
				return new Color();
			}
			return null;
		}

		// Token: 0x060184E4 RID: 99556 RVA: 0x003404A8 File Offset: 0x0033E6A8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "minLength" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "maxLength" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "showValue" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060184E5 RID: 99557 RVA: 0x003404FF File Offset: 0x0033E6FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataBar>(deep);
		}

		// Token: 0x060184E6 RID: 99558 RVA: 0x00340508 File Offset: 0x0033E708
		// Note: this type is marked as 'beforefieldinit'.
		static DataBar()
		{
			byte[] array = new byte[3];
			DataBar.attributeNamespaceIds = array;
		}

		// Token: 0x04009FB5 RID: 40885
		private const string tagName = "dataBar";

		// Token: 0x04009FB6 RID: 40886
		private const byte tagNsId = 22;

		// Token: 0x04009FB7 RID: 40887
		internal const int ElementTypeIdConst = 11381;

		// Token: 0x04009FB8 RID: 40888
		private static string[] attributeTagNames = new string[] { "minLength", "maxLength", "showValue" };

		// Token: 0x04009FB9 RID: 40889
		private static byte[] attributeNamespaceIds;
	}
}
