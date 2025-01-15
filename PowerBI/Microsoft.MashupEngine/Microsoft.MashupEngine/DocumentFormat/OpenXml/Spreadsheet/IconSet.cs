using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C8B RID: 11403
	[ChildElementInfo(typeof(ConditionalFormatValueObject))]
	[GeneratedCode("DomGen", "2.0")]
	internal class IconSet : OpenXmlCompositeElement
	{
		// Token: 0x1700839A RID: 33690
		// (get) Token: 0x060184E7 RID: 99559 RVA: 0x002E8E89 File Offset: 0x002E7089
		public override string LocalName
		{
			get
			{
				return "iconSet";
			}
		}

		// Token: 0x1700839B RID: 33691
		// (get) Token: 0x060184E8 RID: 99560 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700839C RID: 33692
		// (get) Token: 0x060184E9 RID: 99561 RVA: 0x00340547 File Offset: 0x0033E747
		internal override int ElementTypeId
		{
			get
			{
				return 11382;
			}
		}

		// Token: 0x060184EA RID: 99562 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700839D RID: 33693
		// (get) Token: 0x060184EB RID: 99563 RVA: 0x0034054E File Offset: 0x0033E74E
		internal override string[] AttributeTagNames
		{
			get
			{
				return IconSet.attributeTagNames;
			}
		}

		// Token: 0x1700839E RID: 33694
		// (get) Token: 0x060184EC RID: 99564 RVA: 0x00340555 File Offset: 0x0033E755
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return IconSet.attributeNamespaceIds;
			}
		}

		// Token: 0x1700839F RID: 33695
		// (get) Token: 0x060184ED RID: 99565 RVA: 0x0034055C File Offset: 0x0033E75C
		// (set) Token: 0x060184EE RID: 99566 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "iconSet")]
		public EnumValue<IconSetValues> IconSetValue
		{
			get
			{
				return (EnumValue<IconSetValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170083A0 RID: 33696
		// (get) Token: 0x060184EF RID: 99567 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060184F0 RID: 99568 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showValue")]
		public BooleanValue ShowValue
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170083A1 RID: 33697
		// (get) Token: 0x060184F1 RID: 99569 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060184F2 RID: 99570 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "percent")]
		public BooleanValue Percent
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

		// Token: 0x170083A2 RID: 33698
		// (get) Token: 0x060184F3 RID: 99571 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060184F4 RID: 99572 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "reverse")]
		public BooleanValue Reverse
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060184F5 RID: 99573 RVA: 0x00293ECF File Offset: 0x002920CF
		public IconSet()
		{
		}

		// Token: 0x060184F6 RID: 99574 RVA: 0x00293ED7 File Offset: 0x002920D7
		public IconSet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060184F7 RID: 99575 RVA: 0x00293EE0 File Offset: 0x002920E0
		public IconSet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060184F8 RID: 99576 RVA: 0x00293EE9 File Offset: 0x002920E9
		public IconSet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060184F9 RID: 99577 RVA: 0x0034056B File Offset: 0x0033E76B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cfvo" == name)
			{
				return new ConditionalFormatValueObject();
			}
			return null;
		}

		// Token: 0x060184FA RID: 99578 RVA: 0x00340588 File Offset: 0x0033E788
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "iconSet" == name)
			{
				return new EnumValue<IconSetValues>();
			}
			if (namespaceId == 0 && "showValue" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "percent" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "reverse" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060184FB RID: 99579 RVA: 0x003405F5 File Offset: 0x0033E7F5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IconSet>(deep);
		}

		// Token: 0x060184FC RID: 99580 RVA: 0x00340600 File Offset: 0x0033E800
		// Note: this type is marked as 'beforefieldinit'.
		static IconSet()
		{
			byte[] array = new byte[4];
			IconSet.attributeNamespaceIds = array;
		}

		// Token: 0x04009FBA RID: 40890
		private const string tagName = "iconSet";

		// Token: 0x04009FBB RID: 40891
		private const byte tagNsId = 22;

		// Token: 0x04009FBC RID: 40892
		internal const int ElementTypeIdConst = 11382;

		// Token: 0x04009FBD RID: 40893
		private static string[] attributeTagNames = new string[] { "iconSet", "showValue", "percent", "reverse" };

		// Token: 0x04009FBE RID: 40894
		private static byte[] attributeNamespaceIds;
	}
}
