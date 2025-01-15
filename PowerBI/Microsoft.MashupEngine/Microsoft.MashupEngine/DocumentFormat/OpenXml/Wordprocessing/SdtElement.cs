using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EBD RID: 11965
	internal abstract class SdtElement : OpenXmlCompositeElement
	{
		// Token: 0x060197B2 RID: 104370 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected SdtElement(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060197B3 RID: 104371 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected SdtElement(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060197B4 RID: 104372 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected SdtElement(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x17008C4F RID: 35919
		// (get) Token: 0x060197B5 RID: 104373 RVA: 0x0034C135 File Offset: 0x0034A335
		// (set) Token: 0x060197B6 RID: 104374 RVA: 0x0034C13E File Offset: 0x0034A33E
		public SdtProperties SdtProperties
		{
			get
			{
				return base.GetElement<SdtProperties>(0);
			}
			set
			{
				base.SetElement<SdtProperties>(0, value);
			}
		}

		// Token: 0x17008C50 RID: 35920
		// (get) Token: 0x060197B7 RID: 104375 RVA: 0x0034C148 File Offset: 0x0034A348
		// (set) Token: 0x060197B8 RID: 104376 RVA: 0x0034C151 File Offset: 0x0034A351
		public SdtEndCharProperties SdtEndCharProperties
		{
			get
			{
				return base.GetElement<SdtEndCharProperties>(1);
			}
			set
			{
				base.SetElement<SdtEndCharProperties>(1, value);
			}
		}
	}
}
