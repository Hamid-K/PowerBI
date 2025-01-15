using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002537 RID: 9527
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class ChartLinesType : OpenXmlCompositeElement
	{
		// Token: 0x06011B97 RID: 72599 RVA: 0x002F17DC File Offset: 0x002EF9DC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			return null;
		}

		// Token: 0x170054C9 RID: 21705
		// (get) Token: 0x06011B98 RID: 72600 RVA: 0x002F17F7 File Offset: 0x002EF9F7
		internal override string[] ElementTagNames
		{
			get
			{
				return ChartLinesType.eleTagNames;
			}
		}

		// Token: 0x170054CA RID: 21706
		// (get) Token: 0x06011B99 RID: 72601 RVA: 0x002F17FE File Offset: 0x002EF9FE
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ChartLinesType.eleNamespaceIds;
			}
		}

		// Token: 0x170054CB RID: 21707
		// (get) Token: 0x06011B9A RID: 72602 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170054CC RID: 21708
		// (get) Token: 0x06011B9B RID: 72603 RVA: 0x002F1805 File Offset: 0x002EFA05
		// (set) Token: 0x06011B9C RID: 72604 RVA: 0x002F180E File Offset: 0x002EFA0E
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(0);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(0, value);
			}
		}

		// Token: 0x06011B9D RID: 72605 RVA: 0x00293ECF File Offset: 0x002920CF
		protected ChartLinesType()
		{
		}

		// Token: 0x06011B9E RID: 72606 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected ChartLinesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011B9F RID: 72607 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected ChartLinesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BA0 RID: 72608 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected ChartLinesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007C3D RID: 31805
		private static readonly string[] eleTagNames = new string[] { "spPr" };

		// Token: 0x04007C3E RID: 31806
		private static readonly byte[] eleNamespaceIds = new byte[] { 11 };
	}
}
