using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200267B RID: 9851
	[ChildElementInfo(typeof(DataModel))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class SampleDataType : OpenXmlCompositeElement
	{
		// Token: 0x17005C87 RID: 23687
		// (get) Token: 0x06012D0C RID: 77068 RVA: 0x002FFBC6 File Offset: 0x002FDDC6
		internal override string[] AttributeTagNames
		{
			get
			{
				return SampleDataType.attributeTagNames;
			}
		}

		// Token: 0x17005C88 RID: 23688
		// (get) Token: 0x06012D0D RID: 77069 RVA: 0x002FFBCD File Offset: 0x002FDDCD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SampleDataType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005C89 RID: 23689
		// (get) Token: 0x06012D0E RID: 77070 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06012D0F RID: 77071 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "useDef")]
		public BooleanValue UseDefault
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012D10 RID: 77072 RVA: 0x002FFBD4 File Offset: 0x002FDDD4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "dataModel" == name)
			{
				return new DataModel();
			}
			return null;
		}

		// Token: 0x17005C8A RID: 23690
		// (get) Token: 0x06012D11 RID: 77073 RVA: 0x002FFBEF File Offset: 0x002FDDEF
		internal override string[] ElementTagNames
		{
			get
			{
				return SampleDataType.eleTagNames;
			}
		}

		// Token: 0x17005C8B RID: 23691
		// (get) Token: 0x06012D12 RID: 77074 RVA: 0x002FFBF6 File Offset: 0x002FDDF6
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SampleDataType.eleNamespaceIds;
			}
		}

		// Token: 0x17005C8C RID: 23692
		// (get) Token: 0x06012D13 RID: 77075 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005C8D RID: 23693
		// (get) Token: 0x06012D14 RID: 77076 RVA: 0x002FFBFD File Offset: 0x002FDDFD
		// (set) Token: 0x06012D15 RID: 77077 RVA: 0x002FFC06 File Offset: 0x002FDE06
		public DataModel DataModel
		{
			get
			{
				return base.GetElement<DataModel>(0);
			}
			set
			{
				base.SetElement<DataModel>(0, value);
			}
		}

		// Token: 0x06012D16 RID: 77078 RVA: 0x002FFC10 File Offset: 0x002FDE10
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "useDef" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012D17 RID: 77079 RVA: 0x00293ECF File Offset: 0x002920CF
		protected SampleDataType()
		{
		}

		// Token: 0x06012D18 RID: 77080 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected SampleDataType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D19 RID: 77081 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected SampleDataType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D1A RID: 77082 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected SampleDataType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012D1B RID: 77083 RVA: 0x002FFC30 File Offset: 0x002FDE30
		// Note: this type is marked as 'beforefieldinit'.
		static SampleDataType()
		{
			byte[] array = new byte[1];
			SampleDataType.attributeNamespaceIds = array;
			SampleDataType.eleTagNames = new string[] { "dataModel" };
			SampleDataType.eleNamespaceIds = new byte[] { 14 };
		}

		// Token: 0x040081B4 RID: 33204
		private static string[] attributeTagNames = new string[] { "useDef" };

		// Token: 0x040081B5 RID: 33205
		private static byte[] attributeNamespaceIds;

		// Token: 0x040081B6 RID: 33206
		private static readonly string[] eleTagNames;

		// Token: 0x040081B7 RID: 33207
		private static readonly byte[] eleNamespaceIds;
	}
}
