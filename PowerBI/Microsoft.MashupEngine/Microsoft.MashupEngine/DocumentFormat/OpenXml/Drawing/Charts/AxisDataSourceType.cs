using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025D3 RID: 9683
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StringReference))]
	[ChildElementInfo(typeof(StringLiteral))]
	[ChildElementInfo(typeof(MultiLevelStringReference))]
	[ChildElementInfo(typeof(NumberReference))]
	[ChildElementInfo(typeof(NumberLiteral))]
	internal abstract class AxisDataSourceType : OpenXmlCompositeElement
	{
		// Token: 0x0601231C RID: 74524 RVA: 0x002F7028 File Offset: 0x002F5228
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "multiLvlStrRef" == name)
			{
				return new MultiLevelStringReference();
			}
			if (11 == namespaceId && "numRef" == name)
			{
				return new NumberReference();
			}
			if (11 == namespaceId && "numLit" == name)
			{
				return new NumberLiteral();
			}
			if (11 == namespaceId && "strRef" == name)
			{
				return new StringReference();
			}
			if (11 == namespaceId && "strLit" == name)
			{
				return new StringLiteral();
			}
			return null;
		}

		// Token: 0x17005824 RID: 22564
		// (get) Token: 0x0601231D RID: 74525 RVA: 0x002F70AE File Offset: 0x002F52AE
		internal override string[] ElementTagNames
		{
			get
			{
				return AxisDataSourceType.eleTagNames;
			}
		}

		// Token: 0x17005825 RID: 22565
		// (get) Token: 0x0601231E RID: 74526 RVA: 0x002F70B5 File Offset: 0x002F52B5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AxisDataSourceType.eleNamespaceIds;
			}
		}

		// Token: 0x17005826 RID: 22566
		// (get) Token: 0x0601231F RID: 74527 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005827 RID: 22567
		// (get) Token: 0x06012320 RID: 74528 RVA: 0x002F70BC File Offset: 0x002F52BC
		// (set) Token: 0x06012321 RID: 74529 RVA: 0x002F70C5 File Offset: 0x002F52C5
		public MultiLevelStringReference MultiLevelStringReference
		{
			get
			{
				return base.GetElement<MultiLevelStringReference>(0);
			}
			set
			{
				base.SetElement<MultiLevelStringReference>(0, value);
			}
		}

		// Token: 0x17005828 RID: 22568
		// (get) Token: 0x06012322 RID: 74530 RVA: 0x002F70CF File Offset: 0x002F52CF
		// (set) Token: 0x06012323 RID: 74531 RVA: 0x002F70D8 File Offset: 0x002F52D8
		public NumberReference NumberReference
		{
			get
			{
				return base.GetElement<NumberReference>(1);
			}
			set
			{
				base.SetElement<NumberReference>(1, value);
			}
		}

		// Token: 0x17005829 RID: 22569
		// (get) Token: 0x06012324 RID: 74532 RVA: 0x002F70E2 File Offset: 0x002F52E2
		// (set) Token: 0x06012325 RID: 74533 RVA: 0x002F70EB File Offset: 0x002F52EB
		public NumberLiteral NumberLiteral
		{
			get
			{
				return base.GetElement<NumberLiteral>(2);
			}
			set
			{
				base.SetElement<NumberLiteral>(2, value);
			}
		}

		// Token: 0x1700582A RID: 22570
		// (get) Token: 0x06012326 RID: 74534 RVA: 0x002F70F5 File Offset: 0x002F52F5
		// (set) Token: 0x06012327 RID: 74535 RVA: 0x002F70FE File Offset: 0x002F52FE
		public StringReference StringReference
		{
			get
			{
				return base.GetElement<StringReference>(3);
			}
			set
			{
				base.SetElement<StringReference>(3, value);
			}
		}

		// Token: 0x1700582B RID: 22571
		// (get) Token: 0x06012328 RID: 74536 RVA: 0x002F7108 File Offset: 0x002F5308
		// (set) Token: 0x06012329 RID: 74537 RVA: 0x002F7111 File Offset: 0x002F5311
		public StringLiteral StringLiteral
		{
			get
			{
				return base.GetElement<StringLiteral>(4);
			}
			set
			{
				base.SetElement<StringLiteral>(4, value);
			}
		}

		// Token: 0x0601232A RID: 74538 RVA: 0x00293ECF File Offset: 0x002920CF
		protected AxisDataSourceType()
		{
		}

		// Token: 0x0601232B RID: 74539 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected AxisDataSourceType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601232C RID: 74540 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected AxisDataSourceType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601232D RID: 74541 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected AxisDataSourceType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007E93 RID: 32403
		private static readonly string[] eleTagNames = new string[] { "multiLvlStrRef", "numRef", "numLit", "strRef", "strLit" };

		// Token: 0x04007E94 RID: 32404
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11 };
	}
}
