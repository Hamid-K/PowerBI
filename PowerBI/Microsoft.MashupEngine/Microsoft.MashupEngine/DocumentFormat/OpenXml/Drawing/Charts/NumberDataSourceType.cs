using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200259E RID: 9630
	[ChildElementInfo(typeof(NumberReference))]
	[ChildElementInfo(typeof(NumberLiteral))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class NumberDataSourceType : OpenXmlCompositeElement
	{
		// Token: 0x0601203F RID: 73791 RVA: 0x002F4D07 File Offset: 0x002F2F07
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "numRef" == name)
			{
				return new NumberReference();
			}
			if (11 == namespaceId && "numLit" == name)
			{
				return new NumberLiteral();
			}
			return null;
		}

		// Token: 0x170056DC RID: 22236
		// (get) Token: 0x06012040 RID: 73792 RVA: 0x002F4D3A File Offset: 0x002F2F3A
		internal override string[] ElementTagNames
		{
			get
			{
				return NumberDataSourceType.eleTagNames;
			}
		}

		// Token: 0x170056DD RID: 22237
		// (get) Token: 0x06012041 RID: 73793 RVA: 0x002F4D41 File Offset: 0x002F2F41
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NumberDataSourceType.eleNamespaceIds;
			}
		}

		// Token: 0x170056DE RID: 22238
		// (get) Token: 0x06012042 RID: 73794 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170056DF RID: 22239
		// (get) Token: 0x06012043 RID: 73795 RVA: 0x002F4D48 File Offset: 0x002F2F48
		// (set) Token: 0x06012044 RID: 73796 RVA: 0x002F4D51 File Offset: 0x002F2F51
		public NumberReference NumberReference
		{
			get
			{
				return base.GetElement<NumberReference>(0);
			}
			set
			{
				base.SetElement<NumberReference>(0, value);
			}
		}

		// Token: 0x170056E0 RID: 22240
		// (get) Token: 0x06012045 RID: 73797 RVA: 0x002F4D5B File Offset: 0x002F2F5B
		// (set) Token: 0x06012046 RID: 73798 RVA: 0x002F4D64 File Offset: 0x002F2F64
		public NumberLiteral NumberLiteral
		{
			get
			{
				return base.GetElement<NumberLiteral>(1);
			}
			set
			{
				base.SetElement<NumberLiteral>(1, value);
			}
		}

		// Token: 0x06012047 RID: 73799 RVA: 0x00293ECF File Offset: 0x002920CF
		protected NumberDataSourceType()
		{
		}

		// Token: 0x06012048 RID: 73800 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected NumberDataSourceType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012049 RID: 73801 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected NumberDataSourceType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601204A RID: 73802 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected NumberDataSourceType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007DC0 RID: 32192
		private static readonly string[] eleTagNames = new string[] { "numRef", "numLit" };

		// Token: 0x04007DC1 RID: 32193
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11 };
	}
}
