using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000670 RID: 1648
	internal sealed class ValueExpression : ExpressionResolution
	{
		// Token: 0x06004EEB RID: 20203 RVA: 0x0011F52A File Offset: 0x0011D72A
		internal ValueExpression(DbExpression value)
			: base(ExpressionResolutionClass.Value)
		{
			this.Value = value;
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06004EEC RID: 20204 RVA: 0x0011F53A File Offset: 0x0011D73A
		internal override string ExpressionClassName
		{
			get
			{
				return ValueExpression.ValueClassName;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x0011F541 File Offset: 0x0011D741
		internal static string ValueClassName
		{
			get
			{
				return Strings.LocalizedValueExpression;
			}
		}

		// Token: 0x04001C86 RID: 7302
		internal readonly DbExpression Value;
	}
}
