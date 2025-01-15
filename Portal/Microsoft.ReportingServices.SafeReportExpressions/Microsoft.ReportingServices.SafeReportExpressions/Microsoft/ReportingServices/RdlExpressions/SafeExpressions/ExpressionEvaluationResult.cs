using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200000E RID: 14
	internal readonly struct ExpressionEvaluationResult
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002433 File Offset: 0x00000633
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000243B File Offset: 0x0000063B
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002443 File Offset: 0x00000643
		public ExpressionEvaluationResult(object value)
		{
			this._value = value;
			if (value == null)
			{
				this._type = typeof(object);
				return;
			}
			this._type = value.GetType();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000246C File Offset: 0x0000066C
		public ExpressionEvaluationResult(object value, Type type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000247C File Offset: 0x0000067C
		public static ExpressionEvaluationResult CreateNull()
		{
			return new ExpressionEvaluationResult(null);
		}

		// Token: 0x0400000A RID: 10
		private readonly object _value;

		// Token: 0x0400000B RID: 11
		private readonly Type _type;
	}
}
