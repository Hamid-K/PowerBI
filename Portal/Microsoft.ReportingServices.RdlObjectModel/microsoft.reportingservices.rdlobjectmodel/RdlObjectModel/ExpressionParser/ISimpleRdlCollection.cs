using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200022B RID: 555
	internal interface ISimpleRdlCollection
	{
		// Token: 0x060012BE RID: 4798
		IInternalExpression CreateReference();

		// Token: 0x060012BF RID: 4799
		IInternalExpression CreateCollectionReference(IInternalExpression itemNameExpr);

		// Token: 0x060012C0 RID: 4800
		IInternalExpression CreatePropertyReference(string propertyName);

		// Token: 0x060012C1 RID: 4801
		bool IsPredefinedCollectionProperty(string name);

		// Token: 0x060012C2 RID: 4802
		bool IsPredefinedChildCollection(string name, out ISimpleRdlCollection childCollection);

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060012C3 RID: 4803
		string Name { get; }
	}
}
