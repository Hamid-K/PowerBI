using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200022A RID: 554
	internal interface IComplexRdlCollection
	{
		// Token: 0x060012B6 RID: 4790
		IInternalExpression CreateReference();

		// Token: 0x060012B7 RID: 4791
		IInternalExpression CreateReference(IInternalExpression itemNameExpr);

		// Token: 0x060012B8 RID: 4792
		IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr);

		// Token: 0x060012B9 RID: 4793
		bool IsPredefinedItemProperty(string name);

		// Token: 0x060012BA RID: 4794
		bool IsPredefinedItemMethod(string name);

		// Token: 0x060012BB RID: 4795
		bool IsPredefinedCollectionProperty(string name);

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060012BC RID: 4796
		string Name { get; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060012BD RID: 4797
		string ItemName { get; }
	}
}
