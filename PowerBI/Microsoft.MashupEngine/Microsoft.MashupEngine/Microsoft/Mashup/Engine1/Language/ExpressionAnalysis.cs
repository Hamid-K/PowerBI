using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200172A RID: 5930
	internal class ExpressionAnalysis
	{
		// Token: 0x060096C6 RID: 38598 RVA: 0x001F4248 File Offset: 0x001F2448
		public static bool IsPlaceholder(Value value)
		{
			long num;
			return value.IsBinary && value.AsBinary.TryGetLength(out num) && num == 0L;
		}

		// Token: 0x060096C7 RID: 38599 RVA: 0x001F4274 File Offset: 0x001F2474
		public static Value[] GetArgumentValues(IExpression expression)
		{
			if (expression.Kind == ExpressionKind.Invocation)
			{
				return ((IInvocationExpression)expression).Arguments.Select((IExpression arg) => ExpressionAnalysis.GetValue(arg)).ToArray<Value>();
			}
			return null;
		}

		// Token: 0x060096C8 RID: 38600 RVA: 0x001F42C0 File Offset: 0x001F24C0
		public static Value[] GetArgumentValues(IExpression expression, out ListValue navigationSteps)
		{
			List<IValueReference> list = new List<IValueReference>();
			bool flag = false;
			while (!flag)
			{
				ExpressionKind kind = expression.Kind;
				if (kind != ExpressionKind.ElementAccess)
				{
					if (kind != ExpressionKind.FieldAccess)
					{
						if (kind == ExpressionKind.Invocation)
						{
							navigationSteps = ListValue.New(list);
							return ExpressionAnalysis.GetArgumentValues(expression);
						}
						flag = true;
					}
					else
					{
						IFieldAccessExpression fieldAccessExpression = (IFieldAccessExpression)expression;
						list.Insert(0, TextValue.New(fieldAccessExpression.MemberName));
						expression = fieldAccessExpression.Expression;
					}
				}
				else
				{
					IElementAccessExpression elementAccessExpression = (IElementAccessExpression)expression;
					Value value = ExpressionAnalysis.GetValue(elementAccessExpression.Key);
					if (value.IsNumber || value.IsRecord)
					{
						list.Insert(0, value);
						expression = elementAccessExpression.Collection;
					}
					else
					{
						flag = true;
					}
				}
			}
			navigationSteps = null;
			return null;
		}

		// Token: 0x060096C9 RID: 38601 RVA: 0x001F4374 File Offset: 0x001F2574
		public static RecordValue GetRecord(IExpression expression)
		{
			Value value = ExpressionAnalysis.GetValue(expression);
			if (value.IsNull)
			{
				value = RecordValue.Empty;
			}
			if (!value.IsRecord)
			{
				return null;
			}
			return value.AsRecord;
		}

		// Token: 0x060096CA RID: 38602 RVA: 0x001F43A8 File Offset: 0x001F25A8
		public static Value GetValue(IExpression expression)
		{
			Value value;
			if (expression.TryGetConstant(out value))
			{
				return value;
			}
			return ExpressionAnalysis.GetRecord(expression, (IExpression expr) => ExpressionAnalysis.GetValue(expr)) ?? ExpressionAnalysis.placeholder;
		}

		// Token: 0x060096CB RID: 38603 RVA: 0x001F43F0 File Offset: 0x001F25F0
		public static RecordValue RemovePlaceholders(RecordValue record, out Keys unknownKeys)
		{
			KeysBuilder keysBuilder = new KeysBuilder(record.Count);
			RecordBuilder recordBuilder = new RecordBuilder(record.Count);
			for (int i = 0; i < record.Count; i++)
			{
				string text = record.Keys[i];
				Value value = record[i];
				if (ExpressionAnalysis.IsPlaceholder(value))
				{
					keysBuilder.Add(text);
				}
				else
				{
					if (value.IsRecord)
					{
						value = ExpressionAnalysis.RemovePlaceholders(value.AsRecord, out unknownKeys);
					}
					recordBuilder.Add(text, value, value.Type);
				}
			}
			unknownKeys = keysBuilder.ToKeys();
			return recordBuilder.ToRecord();
		}

		// Token: 0x060096CC RID: 38604 RVA: 0x001F448C File Offset: 0x001F268C
		public static RecordValue GetAttributeValue(IRecordExpression attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			RecordBuilder recordBuilder = new RecordBuilder(attribute.Members.Count);
			for (int i = 0; i < attribute.Members.Count; i++)
			{
				if (attribute.Members[i].Name != null)
				{
					Value attributeValue = ExpressionAnalysis.GetAttributeValue(attribute.Members[i].Value);
					if (attributeValue != null)
					{
						recordBuilder.Add(attribute.Members[i].Name, attributeValue, attributeValue.Type);
					}
				}
			}
			RecordValue recordValue = recordBuilder.ToRecord();
			if (recordValue.Count != 0)
			{
				return recordValue;
			}
			return null;
		}

		// Token: 0x060096CD RID: 38605 RVA: 0x001F4540 File Offset: 0x001F2740
		private static ListValue GetAttributeValue(IListExpression attribute)
		{
			List<IValueReference> list = new List<IValueReference>(attribute.Members.Count);
			foreach (IExpression expression in attribute.Members)
			{
				Value attributeValue = ExpressionAnalysis.GetAttributeValue(expression);
				if (attributeValue != null)
				{
					list.Add(attributeValue);
				}
			}
			if (list.Count != 0)
			{
				return ListValue.New(list);
			}
			return null;
		}

		// Token: 0x060096CE RID: 38606 RVA: 0x001F45B8 File Offset: 0x001F27B8
		private static Value GetAttributeValue(IExpression attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			ExpressionKind kind = attribute.Kind;
			if (kind == ExpressionKind.Constant)
			{
				return ((IConstantExpression)attribute).Value;
			}
			if (kind == ExpressionKind.List)
			{
				return ExpressionAnalysis.GetAttributeValue((IListExpression)attribute);
			}
			if (kind != ExpressionKind.Record)
			{
				return null;
			}
			return ExpressionAnalysis.GetAttributeValue((IRecordExpression)attribute);
		}

		// Token: 0x060096CF RID: 38607 RVA: 0x001F4608 File Offset: 0x001F2808
		private static Value GetRecord(IExpression node, Func<IExpression, Value> converter)
		{
			node = ConstantFoldingVisitor2.Fold(node);
			Value value;
			if (node.TryGetConstant(out value) && (value.IsNull || value.IsRecord))
			{
				return value;
			}
			IRecordExpression recordExpression = node as IRecordExpression;
			if (recordExpression != null)
			{
				RecordBuilder recordBuilder = new RecordBuilder(recordExpression.Members.Count);
				for (int i = 0; i < recordExpression.Members.Count; i++)
				{
					Value value2 = converter(recordExpression.Members[i].Value);
					if (value2 != null)
					{
						recordBuilder.Add(recordExpression.Members[i].Name.Name, value2, value2.Type);
					}
				}
				return recordBuilder.ToRecord();
			}
			return null;
		}

		// Token: 0x04005022 RID: 20514
		private static readonly Value placeholder = BinaryValue.New(new byte[0]);
	}
}
