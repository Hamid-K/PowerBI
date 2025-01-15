using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.DataShaping.Common.Json;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000D1 RID: 209
	internal class ExpressionStringBuilder : ExpressionNodeVisitor<ExpressionNode>, IExpressionStringBuilder
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x0000B97C File Offset: 0x00009B7C
		internal ExpressionStringBuilder()
			: base(false)
		{
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000B988 File Offset: 0x00009B88
		public virtual string Write(ExpressionId expressionId)
		{
			return expressionId.Value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000B9A9 File Offset: 0x00009BA9
		public string Write(IExpressionNode node)
		{
			return this.Write((ExpressionNode)node);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		public string Write(ExpressionNode node)
		{
			this.m_writer = new StringWriter(new StringBuilder(), CultureInfo.InvariantCulture);
			this.Visit(node);
			this.m_writer.Flush();
			this.m_writer.Close();
			return this.m_writer.ToString();
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		public override ExpressionNode Visit(BinaryOperatorExpressionNode node)
		{
			this.m_writer.Write('(');
			this.Visit(node.Left);
			this.Visit(node.OperatorKind);
			this.Visit(node.Right);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000BA48 File Offset: 0x00009C48
		private void Visit(BinaryOperatorKind kind)
		{
			switch (kind)
			{
			case BinaryOperatorKind.Add:
				this.m_writer.Write(" + ");
				return;
			case BinaryOperatorKind.And:
				this.m_writer.Write(" && ");
				return;
			case BinaryOperatorKind.Divide:
				this.m_writer.Write(" // ");
				return;
			case BinaryOperatorKind.Equal:
				this.m_writer.Write(" == ");
				return;
			case BinaryOperatorKind.GreaterThan:
				this.m_writer.Write(" > ");
				return;
			case BinaryOperatorKind.GreaterThanOrEqual:
				this.m_writer.Write(" >= ");
				return;
			case BinaryOperatorKind.LessThan:
				this.m_writer.Write(" < ");
				return;
			case BinaryOperatorKind.LessThanOrEqual:
				this.m_writer.Write(" <= ");
				return;
			case BinaryOperatorKind.Multiply:
				this.m_writer.Write(" * ");
				return;
			case BinaryOperatorKind.Or:
				this.m_writer.Write(" || ");
				return;
			case BinaryOperatorKind.Subtract:
				this.m_writer.Write(" - ");
				return;
			default:
				throw new NotSupportedException("Unsupported binary operator kind: " + kind.ToString());
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000BB63 File Offset: 0x00009D63
		public override ExpressionNode Visit(EntitySetExpressionNode node)
		{
			this.m_writer.Write(node.Container);
			this.m_writer.Write('.');
			this.m_writer.Write(node.Name);
			return node;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000BB98 File Offset: 0x00009D98
		public override ExpressionNode Visit(LiteralExpressionNode node)
		{
			object value = node.Value.Value;
			if (value == null)
			{
				JsonValueUtils.WriteString(this.m_writer, null);
			}
			else
			{
				TypeCode typeCode = Type.GetTypeCode(value.GetType());
				if (typeCode != TypeCode.Object)
				{
					if (typeCode != TypeCode.Boolean)
					{
						switch (typeCode)
						{
						case TypeCode.Int16:
							JsonValueUtils.WriteInt(this.m_writer, (int)((short)value));
							return node;
						case TypeCode.Int32:
							JsonValueUtils.WriteInt(this.m_writer, (int)value);
							return node;
						case TypeCode.Int64:
						case TypeCode.Double:
						case TypeCode.Decimal:
						case TypeCode.DateTime:
						case TypeCode.String:
							goto IL_00AA;
						}
						throw new InvalidOperationException("Unsupported type: " + typeCode.ToString());
					}
					JsonValueUtils.WriteBoolean(this.m_writer, (bool)value);
					return node;
				}
				IL_00AA:
				this.m_writer.Write(JsonValueUtils.ToTypeEncodedString(value));
			}
			return node;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000BC80 File Offset: 0x00009E80
		public override ExpressionNode Visit(StructureReferenceExpressionNode node)
		{
			this.m_writer.Write('[');
			this.WriteId(node.TargetId);
			this.m_writer.Write(']');
			return node;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000BCA9 File Offset: 0x00009EA9
		public override ExpressionNode Visit(PropertyExpressionNode node)
		{
			this.WritePropertyReference(node.EntitySet.Container, node.EntitySet.Name, node.Name);
			return node;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		private void WritePropertyReference(string containerName, string entitySetName, string propertyName)
		{
			this.m_writer.Write(containerName);
			this.m_writer.Write(".");
			this.m_writer.Write(ExpressionStringBuilder.EncodeIdentifierOrStringLiteral(entitySetName));
			this.m_writer.Write('/');
			this.m_writer.Write(ExpressionStringBuilder.EncodeIdentifierOrStringLiteral(propertyName));
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000BD28 File Offset: 0x00009F28
		public static string EncodeIdentifierOrStringLiteral(string value)
		{
			if (ExpressionTextUtils.IsValidIdentifier(value))
			{
				return value;
			}
			return PrimitiveValueUtils.ConvertToPrimitiveLiteral(value);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000BD3C File Offset: 0x00009F3C
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			this.m_writer.Write(node.Descriptor.Name);
			this.m_writer.Write('(');
			int count = node.Arguments.Count;
			for (int i = 0; i < count; i++)
			{
				this.Visit(node.Arguments[i]);
				if (i < count - 1)
				{
					this.m_writer.Write(", ");
				}
			}
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000BDBB File Offset: 0x00009FBB
		public override ExpressionNode Visit(DataTransformTableColumnReferenceExpressionNode node)
		{
			this.Visit(node.Table);
			this.m_writer.Write('/');
			this.Visit(node.Column);
			return node;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000BDE5 File Offset: 0x00009FE5
		public override ExpressionNode Visit(DaxTextExpressionNode node)
		{
			this.m_writer.Write("Dax(");
			this.m_writer.Write(ExpressionStringBuilder.EncodeIdentifierOrStringLiteral(node.Text));
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000BE1B File Offset: 0x0000A01B
		public override ExpressionNode Visit(VisualCalculationExpressionNode node)
		{
			this.m_writer.Write("VisualCalculation");
			this.m_writer.Write("(");
			this.Visit(node.Expression);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000BE58 File Offset: 0x0000A058
		public override ExpressionNode Visit(QueryParameterReferenceExpressionNode node)
		{
			this.m_writer.Write("@");
			this.m_writer.Write(ExpressionStringBuilder.EncodeIdentifierOrStringLiteral(node.Name));
			return node;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000BE81 File Offset: 0x0000A081
		private string GetId(IIdentifiable identifiable)
		{
			if (identifiable == null || identifiable.Id == null || identifiable.Id.Value == null)
			{
				return string.Empty;
			}
			return identifiable.Id.Value;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		private void WriteId(IIdentifiable identifiable)
		{
			if (identifiable == null)
			{
				this.m_writer.Write(string.Empty);
				return;
			}
			this.WriteId(identifiable.Id);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		private void WriteId(Identifier id)
		{
			if (id == null || id.Value == null)
			{
				this.m_writer.Write(string.Empty);
				return;
			}
			this.m_writer.Write(id.Value);
		}

		// Token: 0x04000265 RID: 613
		protected StringWriter m_writer;
	}
}
