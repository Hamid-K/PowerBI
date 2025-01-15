using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BD7 RID: 11223
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Break))]
	internal abstract class PageBreakType : OpenXmlCompositeElement
	{
		// Token: 0x17007D5D RID: 32093
		// (get) Token: 0x060176F4 RID: 95988 RVA: 0x00336BF7 File Offset: 0x00334DF7
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageBreakType.attributeTagNames;
			}
		}

		// Token: 0x17007D5E RID: 32094
		// (get) Token: 0x060176F5 RID: 95989 RVA: 0x00336BFE File Offset: 0x00334DFE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageBreakType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D5F RID: 32095
		// (get) Token: 0x060176F6 RID: 95990 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060176F7 RID: 95991 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007D60 RID: 32096
		// (get) Token: 0x060176F8 RID: 95992 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060176F9 RID: 95993 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "manualBreakCount")]
		public UInt32Value ManualBreakCount
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

		// Token: 0x060176FA RID: 95994 RVA: 0x00336C05 File Offset: 0x00334E05
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "brk" == name)
			{
				return new Break();
			}
			return null;
		}

		// Token: 0x060176FB RID: 95995 RVA: 0x00336C20 File Offset: 0x00334E20
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "manualBreakCount" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060176FC RID: 95996 RVA: 0x00293ECF File Offset: 0x002920CF
		protected PageBreakType()
		{
		}

		// Token: 0x060176FD RID: 95997 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected PageBreakType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060176FE RID: 95998 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected PageBreakType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060176FF RID: 95999 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected PageBreakType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017700 RID: 96000 RVA: 0x00336C58 File Offset: 0x00334E58
		// Note: this type is marked as 'beforefieldinit'.
		static PageBreakType()
		{
			byte[] array = new byte[2];
			PageBreakType.attributeNamespaceIds = array;
		}

		// Token: 0x04009C56 RID: 40022
		private static string[] attributeTagNames = new string[] { "count", "manualBreakCount" };

		// Token: 0x04009C57 RID: 40023
		private static byte[] attributeNamespaceIds;
	}
}
