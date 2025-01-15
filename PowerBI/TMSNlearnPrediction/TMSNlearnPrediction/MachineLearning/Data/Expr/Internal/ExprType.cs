using System;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001A3 RID: 419
	internal struct ExprType
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x000320C3 File Offset: 0x000302C3
		public ExprType(ExprTypeKind kind)
		{
			this.Kind = kind;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x000320CC File Offset: 0x000302CC
		public bool IsValid
		{
			get
			{
				return this.Kind != ExprTypeKind.None;
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000320DC File Offset: 0x000302DC
		public Type GetSysType()
		{
			switch (this.Kind)
			{
			case ExprTypeKind.BL:
				return typeof(DvBool);
			case ExprTypeKind.I4:
				return typeof(DvInt4);
			case ExprTypeKind.I8:
				return typeof(DvInt8);
			case ExprTypeKind.R4:
				return typeof(float);
			case ExprTypeKind.R8:
				return typeof(double);
			case ExprTypeKind.TX:
				return typeof(DvText);
			default:
				return null;
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00032158 File Offset: 0x00030358
		public static ExprType ToExprType(Type type)
		{
			if (type == typeof(DvBool))
			{
				return ExprType.BL;
			}
			if (type == typeof(DvInt4))
			{
				return ExprType.I4;
			}
			if (type == typeof(DvInt8))
			{
				return ExprType.I8;
			}
			if (type == typeof(float))
			{
				return ExprType.R4;
			}
			if (type == typeof(double))
			{
				return ExprType.R8;
			}
			if (type == typeof(DvText))
			{
				return ExprType.TX;
			}
			return ExprType.Error;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000321FA File Offset: 0x000303FA
		public override string ToString()
		{
			return this.Kind.ToString();
		}

		// Token: 0x040004CC RID: 1228
		public static readonly ExprType None = new ExprType(ExprTypeKind.None);

		// Token: 0x040004CD RID: 1229
		public static readonly ExprType Error = new ExprType(ExprTypeKind.Error);

		// Token: 0x040004CE RID: 1230
		public static readonly ExprType I4 = new ExprType(ExprTypeKind.I4);

		// Token: 0x040004CF RID: 1231
		public static readonly ExprType I8 = new ExprType(ExprTypeKind.I8);

		// Token: 0x040004D0 RID: 1232
		public static readonly ExprType R4 = new ExprType(ExprTypeKind.R4);

		// Token: 0x040004D1 RID: 1233
		public static readonly ExprType R8 = new ExprType(ExprTypeKind.R8);

		// Token: 0x040004D2 RID: 1234
		public static readonly ExprType Float = new ExprType(ExprTypeKind.R4);

		// Token: 0x040004D3 RID: 1235
		public static readonly ExprType BL = new ExprType(ExprTypeKind.BL);

		// Token: 0x040004D4 RID: 1236
		public static readonly ExprType TX = new ExprType(ExprTypeKind.TX);

		// Token: 0x040004D5 RID: 1237
		public readonly ExprTypeKind Kind;
	}
}
