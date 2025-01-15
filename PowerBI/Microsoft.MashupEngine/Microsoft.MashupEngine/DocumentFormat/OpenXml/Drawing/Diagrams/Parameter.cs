using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002668 RID: 9832
	[GeneratedCode("DomGen", "2.0")]
	internal class Parameter : OpenXmlLeafElement
	{
		// Token: 0x17005BEC RID: 23532
		// (get) Token: 0x06012BA5 RID: 76709 RVA: 0x002FE87D File Offset: 0x002FCA7D
		public override string LocalName
		{
			get
			{
				return "param";
			}
		}

		// Token: 0x17005BED RID: 23533
		// (get) Token: 0x06012BA6 RID: 76710 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BEE RID: 23534
		// (get) Token: 0x06012BA7 RID: 76711 RVA: 0x002FE884 File Offset: 0x002FCA84
		internal override int ElementTypeId
		{
			get
			{
				return 10649;
			}
		}

		// Token: 0x06012BA8 RID: 76712 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005BEF RID: 23535
		// (get) Token: 0x06012BA9 RID: 76713 RVA: 0x002FE88B File Offset: 0x002FCA8B
		internal override string[] AttributeTagNames
		{
			get
			{
				return Parameter.attributeTagNames;
			}
		}

		// Token: 0x17005BF0 RID: 23536
		// (get) Token: 0x06012BAA RID: 76714 RVA: 0x002FE892 File Offset: 0x002FCA92
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Parameter.attributeNamespaceIds;
			}
		}

		// Token: 0x17005BF1 RID: 23537
		// (get) Token: 0x06012BAB RID: 76715 RVA: 0x002FE899 File Offset: 0x002FCA99
		// (set) Token: 0x06012BAC RID: 76716 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<ParameterIdValues> Type
		{
			get
			{
				return (EnumValue<ParameterIdValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005BF2 RID: 23538
		// (get) Token: 0x06012BAD RID: 76717 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012BAE RID: 76718 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06012BB0 RID: 76720 RVA: 0x002FE8A8 File Offset: 0x002FCAA8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ParameterIdValues>();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012BB1 RID: 76721 RVA: 0x002FE8DE File Offset: 0x002FCADE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Parameter>(deep);
		}

		// Token: 0x06012BB2 RID: 76722 RVA: 0x002FE8E8 File Offset: 0x002FCAE8
		// Note: this type is marked as 'beforefieldinit'.
		static Parameter()
		{
			byte[] array = new byte[2];
			Parameter.attributeNamespaceIds = array;
		}

		// Token: 0x0400815E RID: 33118
		private const string tagName = "param";

		// Token: 0x0400815F RID: 33119
		private const byte tagNsId = 14;

		// Token: 0x04008160 RID: 33120
		internal const int ElementTypeIdConst = 10649;

		// Token: 0x04008161 RID: 33121
		private static string[] attributeTagNames = new string[] { "type", "val" };

		// Token: 0x04008162 RID: 33122
		private static byte[] attributeNamespaceIds;
	}
}
