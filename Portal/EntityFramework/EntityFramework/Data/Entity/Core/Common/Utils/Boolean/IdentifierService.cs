using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000613 RID: 1555
	internal abstract class IdentifierService<T_Identifier>
	{
		// Token: 0x06004B86 RID: 19334 RVA: 0x0010AAA4 File Offset: 0x00108CA4
		private static IdentifierService<T_Identifier> GetIdentifierService()
		{
			Type typeFromHandle = typeof(T_Identifier);
			if (typeFromHandle.IsGenericType() && typeFromHandle.GetGenericTypeDefinition() == typeof(DomainConstraint<, >))
			{
				Type[] genericArguments = typeFromHandle.GetGenericArguments();
				Type type = genericArguments[0];
				Type type2 = genericArguments[1];
				return (IdentifierService<T_Identifier>)Activator.CreateInstance(typeof(IdentifierService<>.DomainConstraintIdentifierService<, >).MakeGenericType(new Type[] { typeFromHandle, type, type2 }));
			}
			return new IdentifierService<T_Identifier>.GenericIdentifierService();
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x0010AB19 File Offset: 0x00108D19
		private IdentifierService()
		{
		}

		// Token: 0x06004B88 RID: 19336
		internal abstract Literal<T_Identifier> NegateLiteral(Literal<T_Identifier> literal);

		// Token: 0x06004B89 RID: 19337
		internal abstract ConversionContext<T_Identifier> CreateConversionContext();

		// Token: 0x06004B8A RID: 19338
		internal abstract BoolExpr<T_Identifier> LocalSimplify(BoolExpr<T_Identifier> expression);

		// Token: 0x04001A6C RID: 6764
		internal static readonly IdentifierService<T_Identifier> Instance = IdentifierService<T_Identifier>.GetIdentifierService();

		// Token: 0x02000C4E RID: 3150
		private class GenericIdentifierService : IdentifierService<T_Identifier>
		{
			// Token: 0x06006A72 RID: 27250 RVA: 0x0016BFC3 File Offset: 0x0016A1C3
			internal override Literal<T_Identifier> NegateLiteral(Literal<T_Identifier> literal)
			{
				return new Literal<T_Identifier>(literal.Term, !literal.IsTermPositive);
			}

			// Token: 0x06006A73 RID: 27251 RVA: 0x0016BFD9 File Offset: 0x0016A1D9
			internal override ConversionContext<T_Identifier> CreateConversionContext()
			{
				return new GenericConversionContext<T_Identifier>();
			}

			// Token: 0x06006A74 RID: 27252 RVA: 0x0016BFE0 File Offset: 0x0016A1E0
			internal override BoolExpr<T_Identifier> LocalSimplify(BoolExpr<T_Identifier> expression)
			{
				return expression.Accept<BoolExpr<T_Identifier>>(Simplifier<T_Identifier>.Instance);
			}
		}

		// Token: 0x02000C4F RID: 3151
		private class DomainConstraintIdentifierService<T_Variable, T_Element> : IdentifierService<DomainConstraint<T_Variable, T_Element>>
		{
			// Token: 0x06006A76 RID: 27254 RVA: 0x0016BFF5 File Offset: 0x0016A1F5
			internal override Literal<DomainConstraint<T_Variable, T_Element>> NegateLiteral(Literal<DomainConstraint<T_Variable, T_Element>> literal)
			{
				return new Literal<DomainConstraint<T_Variable, T_Element>>(new TermExpr<DomainConstraint<T_Variable, T_Element>>(literal.Term.Identifier.InvertDomainConstraint()), literal.IsTermPositive);
			}

			// Token: 0x06006A77 RID: 27255 RVA: 0x0016C017 File Offset: 0x0016A217
			internal override ConversionContext<DomainConstraint<T_Variable, T_Element>> CreateConversionContext()
			{
				return new DomainConstraintConversionContext<T_Variable, T_Element>();
			}

			// Token: 0x06006A78 RID: 27256 RVA: 0x0016C01E File Offset: 0x0016A21E
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> LocalSimplify(BoolExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				expression = NegationPusher.EliminateNot<T_Variable, T_Element>(expression);
				return expression.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(Simplifier<DomainConstraint<T_Variable, T_Element>>.Instance);
			}
		}
	}
}
