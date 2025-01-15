using System;
using System.Data.Entity.Core.Objects.Internal;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000644 RID: 1604
	internal class TranslatorResult
	{
		// Token: 0x06004D1D RID: 19741 RVA: 0x001107DF File Offset: 0x0010E9DF
		internal TranslatorResult(Expression returnedExpression, Type requestedType)
		{
			this.RequestedType = requestedType;
			this.ReturnedExpression = returnedExpression;
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x001107F5 File Offset: 0x0010E9F5
		internal Expression Expression
		{
			get
			{
				return CodeGenEmitter.Emit_EnsureType(this.ReturnedExpression, this.RequestedType);
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06004D1F RID: 19743 RVA: 0x00110808 File Offset: 0x0010EA08
		internal Expression UnconvertedExpression
		{
			get
			{
				return this.ReturnedExpression;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06004D20 RID: 19744 RVA: 0x00110810 File Offset: 0x0010EA10
		internal Expression UnwrappedExpression
		{
			get
			{
				if (!typeof(IEntityWrapper).IsAssignableFrom(this.ReturnedExpression.Type))
				{
					return this.ReturnedExpression;
				}
				return CodeGenEmitter.Emit_UnwrapAndEnsureType(this.ReturnedExpression, this.RequestedType);
			}
		}

		// Token: 0x04001B67 RID: 7015
		private readonly Expression ReturnedExpression;

		// Token: 0x04001B68 RID: 7016
		private readonly Type RequestedType;
	}
}
