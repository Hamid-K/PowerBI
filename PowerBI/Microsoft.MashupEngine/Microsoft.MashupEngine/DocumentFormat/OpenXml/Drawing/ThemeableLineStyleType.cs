using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027F7 RID: 10231
	[ChildElementInfo(typeof(LineReference))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Outline))]
	internal abstract class ThemeableLineStyleType : OpenXmlCompositeElement
	{
		// Token: 0x06013FD6 RID: 81878 RVA: 0x0030E30B File Offset: 0x0030C50B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ln" == name)
			{
				return new Outline();
			}
			if (10 == namespaceId && "lnRef" == name)
			{
				return new LineReference();
			}
			return null;
		}

		// Token: 0x170064FC RID: 25852
		// (get) Token: 0x06013FD7 RID: 81879 RVA: 0x0030E33E File Offset: 0x0030C53E
		internal override string[] ElementTagNames
		{
			get
			{
				return ThemeableLineStyleType.eleTagNames;
			}
		}

		// Token: 0x170064FD RID: 25853
		// (get) Token: 0x06013FD8 RID: 81880 RVA: 0x0030E345 File Offset: 0x0030C545
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ThemeableLineStyleType.eleNamespaceIds;
			}
		}

		// Token: 0x170064FE RID: 25854
		// (get) Token: 0x06013FD9 RID: 81881 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170064FF RID: 25855
		// (get) Token: 0x06013FDA RID: 81882 RVA: 0x002EF250 File Offset: 0x002ED450
		// (set) Token: 0x06013FDB RID: 81883 RVA: 0x002EF259 File Offset: 0x002ED459
		public Outline Outline
		{
			get
			{
				return base.GetElement<Outline>(0);
			}
			set
			{
				base.SetElement<Outline>(0, value);
			}
		}

		// Token: 0x17006500 RID: 25856
		// (get) Token: 0x06013FDC RID: 81884 RVA: 0x0030E34C File Offset: 0x0030C54C
		// (set) Token: 0x06013FDD RID: 81885 RVA: 0x0030E355 File Offset: 0x0030C555
		public LineReference LineReference
		{
			get
			{
				return base.GetElement<LineReference>(1);
			}
			set
			{
				base.SetElement<LineReference>(1, value);
			}
		}

		// Token: 0x06013FDE RID: 81886 RVA: 0x00293ECF File Offset: 0x002920CF
		protected ThemeableLineStyleType()
		{
		}

		// Token: 0x06013FDF RID: 81887 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected ThemeableLineStyleType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FE0 RID: 81888 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected ThemeableLineStyleType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FE1 RID: 81889 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected ThemeableLineStyleType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0400888A RID: 34954
		private static readonly string[] eleTagNames = new string[] { "ln", "lnRef" };

		// Token: 0x0400888B RID: 34955
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
