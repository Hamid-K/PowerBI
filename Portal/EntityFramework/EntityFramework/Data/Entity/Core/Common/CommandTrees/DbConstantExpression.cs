using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006AD RID: 1709
	public class DbConstantExpression : DbExpression
	{
		// Token: 0x0600500B RID: 20491 RVA: 0x00121781 File Offset: 0x0011F981
		internal DbConstantExpression()
		{
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x0012178C File Offset: 0x0011F98C
		internal DbConstantExpression(TypeUsage resultType, object value)
			: base(DbExpressionKind.Constant, resultType, true)
		{
			PrimitiveType primitiveType;
			this._shouldCloneValue = TypeHelpers.TryGetEdmType<PrimitiveType>(resultType, out primitiveType) && primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Binary;
			if (this._shouldCloneValue)
			{
				this._value = ((byte[])value).Clone();
				return;
			}
			this._value = value;
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x001217DF File Offset: 0x0011F9DF
		internal object GetValue()
		{
			return this._value;
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x0600500E RID: 20494 RVA: 0x001217E7 File Offset: 0x0011F9E7
		public virtual object Value
		{
			get
			{
				if (this._shouldCloneValue)
				{
					return ((byte[])this._value).Clone();
				}
				return this._value;
			}
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x00121808 File Offset: 0x0011FA08
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0012181D File Offset: 0x0011FA1D
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001D46 RID: 7494
		private readonly bool _shouldCloneValue;

		// Token: 0x04001D47 RID: 7495
		private readonly object _value;
	}
}
