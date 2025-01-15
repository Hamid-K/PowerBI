using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B5A RID: 11098
	[ChildElementInfo(typeof(Tuple))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TuplesType : OpenXmlCompositeElement
	{
		// Token: 0x170078BE RID: 30910
		// (get) Token: 0x06016D0B RID: 93451 RVA: 0x0032F6BB File Offset: 0x0032D8BB
		internal override string[] AttributeTagNames
		{
			get
			{
				return TuplesType.attributeTagNames;
			}
		}

		// Token: 0x170078BF RID: 30911
		// (get) Token: 0x06016D0C RID: 93452 RVA: 0x0032F6C2 File Offset: 0x0032D8C2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TuplesType.attributeNamespaceIds;
			}
		}

		// Token: 0x170078C0 RID: 30912
		// (get) Token: 0x06016D0D RID: 93453 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016D0E RID: 93454 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "c")]
		public UInt32Value MemberNameCount
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

		// Token: 0x06016D0F RID: 93455 RVA: 0x0032F6C9 File Offset: 0x0032D8C9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tpl" == name)
			{
				return new Tuple();
			}
			return null;
		}

		// Token: 0x06016D10 RID: 93456 RVA: 0x0032F6E4 File Offset: 0x0032D8E4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "c" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016D11 RID: 93457 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TuplesType()
		{
		}

		// Token: 0x06016D12 RID: 93458 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TuplesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D13 RID: 93459 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TuplesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D14 RID: 93460 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TuplesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016D15 RID: 93461 RVA: 0x0032F704 File Offset: 0x0032D904
		// Note: this type is marked as 'beforefieldinit'.
		static TuplesType()
		{
			byte[] array = new byte[1];
			TuplesType.attributeNamespaceIds = array;
		}

		// Token: 0x04009A02 RID: 39426
		private static string[] attributeTagNames = new string[] { "c" };

		// Token: 0x04009A03 RID: 39427
		private static byte[] attributeNamespaceIds;
	}
}
