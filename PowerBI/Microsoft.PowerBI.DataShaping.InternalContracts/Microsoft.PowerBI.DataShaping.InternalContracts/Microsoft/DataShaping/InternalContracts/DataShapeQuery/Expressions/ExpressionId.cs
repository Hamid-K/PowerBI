using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000CB RID: 203
	internal struct ExpressionId : IEquatable<ExpressionId>, IStructuredToString
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x0000AE7D File Offset: 0x0000907D
		public ExpressionId(int value)
		{
			this.Value = value;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000AE86 File Offset: 0x00009086
		internal readonly int Value { get; }

		// Token: 0x06000537 RID: 1335 RVA: 0x0000AE90 File Offset: 0x00009090
		public override string ToString()
		{
			return "ExpressionId [" + this.Value.ToString() + "]";
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000AEBA File Offset: 0x000090BA
		public override bool Equals(object obj)
		{
			return obj is ExpressionId && this.Equals((ExpressionId)obj);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000AED2 File Offset: 0x000090D2
		public bool Equals(ExpressionId other)
		{
			return this.Value == other.Value;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000AEE3 File Offset: 0x000090E3
		public static bool operator ==(ExpressionId left, ExpressionId right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000AEED File Offset: 0x000090ED
		public static bool operator !=(ExpressionId left, ExpressionId right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000AEFC File Offset: 0x000090FC
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000AF18 File Offset: 0x00009118
		public void WriteTo(StructuredStringBuilder builder)
		{
			string text = builder.ExpressionStringBuilder.Write(this);
			builder.WriteValue<string>(text);
		}
	}
}
