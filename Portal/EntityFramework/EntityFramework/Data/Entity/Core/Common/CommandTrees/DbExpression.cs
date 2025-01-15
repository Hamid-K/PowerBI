using System;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Globalization;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006B5 RID: 1717
	public abstract class DbExpression
	{
		// Token: 0x0600502D RID: 20525 RVA: 0x001219FA File Offset: 0x0011FBFA
		internal DbExpression()
		{
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x00121A04 File Offset: 0x0011FC04
		internal DbExpression(DbExpressionKind kind, TypeUsage type, bool forceNullable = true)
		{
			DbExpression.CheckExpressionKind(kind);
			this._kind = kind;
			if (forceNullable && !TypeSemantics.IsNullable(type))
			{
				type = type.ShallowCopy(new FacetValues
				{
					Nullable = new bool?(true)
				});
			}
			this._type = type;
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x00121A54 File Offset: 0x0011FC54
		public virtual TypeUsage ResultType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x00121A5C File Offset: 0x0011FC5C
		public virtual DbExpressionKind ExpressionKind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x06005031 RID: 20529
		public abstract void Accept(DbExpressionVisitor visitor);

		// Token: 0x06005032 RID: 20530
		public abstract TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor);

		// Token: 0x06005033 RID: 20531 RVA: 0x00121A64 File Offset: 0x0011FC64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x00121A6D File Offset: 0x0011FC6D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x00121A75 File Offset: 0x0011FC75
		public static DbExpression FromBinary(byte[] value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Binary);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x00121A87 File Offset: 0x0011FC87
		public static implicit operator DbExpression(byte[] value)
		{
			return DbExpression.FromBinary(value);
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x00121A8F File Offset: 0x0011FC8F
		public static DbExpression FromBoolean(bool? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Boolean);
			}
			if (!value.Value)
			{
				return DbExpressionBuilder.False;
			}
			return DbExpressionBuilder.True;
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x00121AB5 File Offset: 0x0011FCB5
		public static implicit operator DbExpression(bool? value)
		{
			return DbExpression.FromBoolean(value);
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x00121ABD File Offset: 0x0011FCBD
		public static DbExpression FromByte(byte? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Byte);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x00121AE0 File Offset: 0x0011FCE0
		public static implicit operator DbExpression(byte? value)
		{
			return DbExpression.FromByte(value);
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x00121AE8 File Offset: 0x0011FCE8
		public static DbExpression FromDateTime(DateTime? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.DateTime);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x00121B0B File Offset: 0x0011FD0B
		public static implicit operator DbExpression(DateTime? value)
		{
			return DbExpression.FromDateTime(value);
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x00121B13 File Offset: 0x0011FD13
		public static DbExpression FromDateTimeOffset(DateTimeOffset? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.DateTimeOffset);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x00121B37 File Offset: 0x0011FD37
		public static implicit operator DbExpression(DateTimeOffset? value)
		{
			return DbExpression.FromDateTimeOffset(value);
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x00121B3F File Offset: 0x0011FD3F
		public static DbExpression FromDecimal(decimal? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Decimal);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x00121B62 File Offset: 0x0011FD62
		public static implicit operator DbExpression(decimal? value)
		{
			return DbExpression.FromDecimal(value);
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x00121B6A File Offset: 0x0011FD6A
		public static DbExpression FromDouble(double? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Double);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x00121B8D File Offset: 0x0011FD8D
		public static implicit operator DbExpression(double? value)
		{
			return DbExpression.FromDouble(value);
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x00121B95 File Offset: 0x0011FD95
		public static DbExpression FromGeography(DbGeography value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Geography);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x00121BA8 File Offset: 0x0011FDA8
		public static implicit operator DbExpression(DbGeography value)
		{
			return DbExpression.FromGeography(value);
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x00121BB0 File Offset: 0x0011FDB0
		public static DbExpression FromGeometry(DbGeometry value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Geometry);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x00121BC3 File Offset: 0x0011FDC3
		public static implicit operator DbExpression(DbGeometry value)
		{
			return DbExpression.FromGeometry(value);
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x00121BCB File Offset: 0x0011FDCB
		public static DbExpression FromGuid(Guid? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Guid);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x00121BEE File Offset: 0x0011FDEE
		public static implicit operator DbExpression(Guid? value)
		{
			return DbExpression.FromGuid(value);
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x00121BF6 File Offset: 0x0011FDF6
		public static DbExpression FromInt16(short? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Int16);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x00121C1A File Offset: 0x0011FE1A
		public static implicit operator DbExpression(short? value)
		{
			return DbExpression.FromInt16(value);
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x00121C22 File Offset: 0x0011FE22
		public static DbExpression FromInt32(int? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Int32);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x00121C46 File Offset: 0x0011FE46
		public static implicit operator DbExpression(int? value)
		{
			return DbExpression.FromInt32(value);
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x00121C4E File Offset: 0x0011FE4E
		public static DbExpression FromInt64(long? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Int64);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x00121C72 File Offset: 0x0011FE72
		public static implicit operator DbExpression(long? value)
		{
			return DbExpression.FromInt64(value);
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x00121C7A File Offset: 0x0011FE7A
		public static DbExpression FromSingle(float? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Single);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x00121C9D File Offset: 0x0011FE9D
		public static implicit operator DbExpression(float? value)
		{
			return DbExpression.FromSingle(value);
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x00121CA5 File Offset: 0x0011FEA5
		public static DbExpression FromString(string value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.String);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x00121CB8 File Offset: 0x0011FEB8
		public static implicit operator DbExpression(string value)
		{
			return DbExpression.FromString(value);
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x00121CC0 File Offset: 0x0011FEC0
		internal static void CheckExpressionKind(DbExpressionKind kind)
		{
			if (kind < DbExpressionKind.All || DbExpressionKindHelper.Last < kind)
			{
				string name = typeof(DbExpressionKind).Name;
				int num = (int)kind;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name, num.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x04001D4B RID: 7499
		private readonly TypeUsage _type;

		// Token: 0x04001D4C RID: 7500
		private readonly DbExpressionKind _kind;
	}
}
