using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200070A RID: 1802
	internal sealed class ExpressionInfoExtended : ExpressionInfo
	{
		// Token: 0x170023A6 RID: 9126
		// (get) Token: 0x060064A8 RID: 25768 RVA: 0x0018E191 File Offset: 0x0018C391
		// (set) Token: 0x060064A9 RID: 25769 RVA: 0x0018E199 File Offset: 0x0018C399
		internal bool IsExtendedSimpleFieldReference
		{
			get
			{
				return this.m_isExtendedSimpleFieldReference;
			}
			set
			{
				this.m_isExtendedSimpleFieldReference = value;
			}
		}

		// Token: 0x04003277 RID: 12919
		[NonSerialized]
		private bool m_isExtendedSimpleFieldReference;
	}
}
