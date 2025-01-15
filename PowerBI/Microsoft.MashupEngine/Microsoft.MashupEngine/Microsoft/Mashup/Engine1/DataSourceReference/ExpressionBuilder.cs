using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018DD RID: 6365
	internal sealed class ExpressionBuilder
	{
		// Token: 0x0600A24A RID: 41546 RVA: 0x0021A5E3 File Offset: 0x002187E3
		public IExpression List(params object[] values)
		{
			return new ListExpressionSyntaxNode(values.Select(new Func<object, IExpression>(this.ToExpression)).ToArray<IExpression>());
		}

		// Token: 0x0600A24B RID: 41547 RVA: 0x0021A601 File Offset: 0x00218801
		public IExpression Invoke(string function, params object[] values)
		{
			return this.Invoke(function, values.Length, values);
		}

		// Token: 0x0600A24C RID: 41548 RVA: 0x0021A610 File Offset: 0x00218810
		public IExpression Invoke(string function, int minCount, params object[] values)
		{
			List<IExpression> list = values.Select(new Func<object, IExpression>(this.ToExpression)).ToList<IExpression>();
			while (list.Count > minCount)
			{
				IExpression expression = list[list.Count - 1];
				if (expression.Kind != ExpressionKind.Constant || !((IConstantExpression)expression).Value.IsNull)
				{
					break;
				}
				list.RemoveAt(list.Count - 1);
			}
			return new InvocationExpressionSyntaxNodeN(this.Identifier(function), list.ToArray());
		}

		// Token: 0x0600A24D RID: 41549 RVA: 0x0021A68C File Offset: 0x0021888C
		public VariableInitializer Declare(string key, object value, bool include = true)
		{
			if (!include)
			{
				return default(VariableInitializer);
			}
			return new VariableInitializer(Microsoft.Mashup.Engine.Interface.Identifier.New(key), this.ToExpression(value));
		}

		// Token: 0x0600A24E RID: 41550 RVA: 0x0021A6B8 File Offset: 0x002188B8
		public IExpression Record(params VariableInitializer[] members)
		{
			if (members.All((VariableInitializer m) => m.Value == null || m.Value.Kind == ExpressionKind.Constant))
			{
				return ConstantExpressionSyntaxNode.New(RecordValue.New((from m in members
					where m.Value != null
					select new NamedValue(m.Name, ((IConstantExpression)m.Value).Value)).ToArray<NamedValue>()));
			}
			return new RecordExpressionSyntaxNode(members.Where((VariableInitializer i) => i.Name != null).ToList<VariableInitializer>());
		}

		// Token: 0x0600A24F RID: 41551 RVA: 0x0021A774 File Offset: 0x00218974
		public IExpression Let(params VariableInitializer[] members)
		{
			Identifier name = members.Where((VariableInitializer i) => i.Name != null).Last<VariableInitializer>().Name;
			return new LetExpressionSyntaxNode(members.Where((VariableInitializer i) => i.Name != null).ToList<VariableInitializer>(), new ExclusiveIdentifierExpressionSyntaxNode(name));
		}

		// Token: 0x0600A250 RID: 41552 RVA: 0x0021A7E9 File Offset: 0x002189E9
		public IExpression Index(object value, object index)
		{
			return new RequiredElementAccessExpressionSyntaxNode(this.ToExpression(value), this.ToExpression(index));
		}

		// Token: 0x0600A251 RID: 41553 RVA: 0x0021A7FE File Offset: 0x002189FE
		public IExpression Member(object value, string member)
		{
			return new RequiredFieldAccessExpressionSyntaxNode(this.ToExpression(value), Microsoft.Mashup.Engine.Interface.Identifier.New(member));
		}

		// Token: 0x0600A252 RID: 41554 RVA: 0x0021A812 File Offset: 0x00218A12
		public IExpression Each(object value)
		{
			return new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, this.ToExpression(value));
		}

		// Token: 0x0600A253 RID: 41555 RVA: 0x0021A825 File Offset: 0x00218A25
		public IExpression Equals(object left, object right)
		{
			return BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, this.ToExpression(left), this.ToExpression(right), TokenRange.Null);
		}

		// Token: 0x0600A254 RID: 41556 RVA: 0x0021A840 File Offset: 0x00218A40
		public IExpression And(params IExpression[] values)
		{
			IExpression expression = null;
			for (int i = values.Length - 1; i >= 0; i--)
			{
				IExpression expression2 = values[i];
				if (expression2 != null)
				{
					if (expression == null)
					{
						expression = expression2;
					}
					else
					{
						expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.And, expression2, expression, TokenRange.Null);
					}
				}
			}
			return expression ?? ConstantExpressionSyntaxNode.True;
		}

		// Token: 0x0600A255 RID: 41557 RVA: 0x0021A886 File Offset: 0x00218A86
		public IExpression Identifier(string identifier)
		{
			return new ExclusiveIdentifierExpressionSyntaxNode(Microsoft.Mashup.Engine.Interface.Identifier.New(identifier));
		}

		// Token: 0x0600A256 RID: 41558 RVA: 0x0021A894 File Offset: 0x00218A94
		public IExpression Navigate(IExpression source, string key, object value, string data)
		{
			return this.Member(this.Index(source, this.Record(new VariableInitializer[] { this.Declare(key, value, true) })), data);
		}

		// Token: 0x0600A257 RID: 41559 RVA: 0x0021A8CC File Offset: 0x00218ACC
		public IExpression Navigate(IExpression source, string key1, object value1, string key2, object value2, string data)
		{
			return this.Member(this.Index(source, this.Record(new VariableInitializer[]
			{
				this.Declare(key1, value1, true),
				this.Declare(key2, value2, true)
			})), data);
		}

		// Token: 0x0600A258 RID: 41560 RVA: 0x0021A916 File Offset: 0x00218B16
		public string ToSource(IExpression expression)
		{
			return new ExpressionToMVisitor(Engine.Instance, LibraryHelper.StandardLibrary).Visit(expression);
		}

		// Token: 0x0600A259 RID: 41561 RVA: 0x0021A930 File Offset: 0x00218B30
		private IExpression ToExpression(object value)
		{
			if (value is Identifier)
			{
				return new ExclusiveIdentifierExpressionSyntaxNode((Identifier)value);
			}
			if (value is IExpression)
			{
				return (IExpression)value;
			}
			if (value is RecordValue)
			{
				return this.AddEnums((RecordValue)value);
			}
			if (value is Value)
			{
				return ConstantExpressionSyntaxNode.New((Value)value);
			}
			if (value is string)
			{
				return ConstantExpressionSyntaxNode.New((string)value);
			}
			if (value == null)
			{
				return ConstantExpressionSyntaxNode.Null;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600A25A RID: 41562 RVA: 0x0021A9AC File Offset: 0x00218BAC
		private IExpression AddEnums(RecordValue record)
		{
			RecordBuilder recordBuilder = default(RecordBuilder);
			bool flag = false;
			RecordValue fields = record.Type.AsRecordType.Fields;
			for (int i = 0; i < record.Count; i++)
			{
				Value value = record[i];
				TypeValue asType = fields[i]["Type"].AsType;
				Value value2;
				if ((asType.TryGetMetaField("Documentation.AllowedValues", out value2) || asType.NonNullable.TryGetMetaField("Documentation.AllowedValues", out value2)) && value2.IsList)
				{
					foreach (IValueReference valueReference in value2.AsList)
					{
						Value value3;
						if (valueReference.Value.Equals(value) && valueReference.Value.TryGetMetaField("Documentation.Name", out value3) && value3.IsText)
						{
							if (!flag)
							{
								recordBuilder = new RecordBuilder(record.Count);
								for (int j = 0; j < i; j++)
								{
									recordBuilder.Add(record.Keys[j], record[j], fields[j]["Type"].AsType);
								}
								flag = true;
							}
							value = valueReference.Value;
						}
					}
				}
				if (flag)
				{
					recordBuilder.Add(record.Keys[i], value, asType);
				}
			}
			return ConstantExpressionSyntaxNode.New(flag ? recordBuilder.ToRecord() : record);
		}

		// Token: 0x040054BC RID: 21692
		public static readonly ExpressionBuilder Instance = new ExpressionBuilder();
	}
}
