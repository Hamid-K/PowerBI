using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025A7 RID: 9639
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class UpDownBarType : OpenXmlCompositeElement
	{
		// Token: 0x0601208C RID: 73868 RVA: 0x002F17DC File Offset: 0x002EF9DC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			return null;
		}

		// Token: 0x170056F9 RID: 22265
		// (get) Token: 0x0601208D RID: 73869 RVA: 0x002F4EB5 File Offset: 0x002F30B5
		internal override string[] ElementTagNames
		{
			get
			{
				return UpDownBarType.eleTagNames;
			}
		}

		// Token: 0x170056FA RID: 22266
		// (get) Token: 0x0601208E RID: 73870 RVA: 0x002F4EBC File Offset: 0x002F30BC
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return UpDownBarType.eleNamespaceIds;
			}
		}

		// Token: 0x170056FB RID: 22267
		// (get) Token: 0x0601208F RID: 73871 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170056FC RID: 22268
		// (get) Token: 0x06012090 RID: 73872 RVA: 0x002F1805 File Offset: 0x002EFA05
		// (set) Token: 0x06012091 RID: 73873 RVA: 0x002F180E File Offset: 0x002EFA0E
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

		// Token: 0x06012092 RID: 73874 RVA: 0x00293ECF File Offset: 0x002920CF
		protected UpDownBarType()
		{
		}

		// Token: 0x06012093 RID: 73875 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected UpDownBarType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012094 RID: 73876 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected UpDownBarType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012095 RID: 73877 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected UpDownBarType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007DD9 RID: 32217
		private static readonly string[] eleTagNames = new string[] { "spPr" };

		// Token: 0x04007DDA RID: 32218
		private static readonly byte[] eleNamespaceIds = new byte[] { 11 };
	}
}
