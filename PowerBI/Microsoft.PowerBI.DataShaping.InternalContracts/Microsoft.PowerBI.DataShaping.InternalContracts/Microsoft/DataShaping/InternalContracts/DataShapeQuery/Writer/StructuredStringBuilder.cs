using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer
{
	// Token: 0x020000BD RID: 189
	internal sealed class StructuredStringBuilder
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x00007E44 File Offset: 0x00006044
		internal StructuredStringBuilder(IExpressionStringBuilder expressionStringBuilder, int baseIndentLevel = 0, bool omitItemsWithDefaultValues = false)
		{
			this.m_builder = new StringBuilder();
			this.m_objectStates = new Stack<StructuredStringBuilder.ObjectState>();
			this.m_expressionStringBuilder = expressionStringBuilder;
			this.m_omitItemsWithDefaultValues = omitItemsWithDefaultValues;
			this.m_indentLevel = baseIndentLevel;
			this.UpdateIndent();
			if (baseIndentLevel > 0)
			{
				this.m_builder.Append(this.m_indent);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00007E9E File Offset: 0x0000609E
		public IExpressionStringBuilder ExpressionStringBuilder
		{
			get
			{
				return this.m_expressionStringBuilder;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00007EA6 File Offset: 0x000060A6
		public void BeginObject(string name)
		{
			this.m_builder.Append(name);
			this.IncreaseIndent();
			this.m_objectStates.Push(new StructuredStringBuilder.ObjectState());
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00007ECB File Offset: 0x000060CB
		public void EndObject()
		{
			this.DecreaseIndent();
			this.m_objectStates.Pop();
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00007EDF File Offset: 0x000060DF
		public void WriteProperty<T>(string name, T value, bool forceWriteDefaultValue = false)
		{
			if (!forceWriteDefaultValue && this.ShouldOmitDefaultValue<T>(value))
			{
				return;
			}
			this.WritePropertyName(name);
			this.WriteValue<T>(value);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00007EFC File Offset: 0x000060FC
		public void WriteProperty<T>(string name, Candidate<T> value)
		{
			if (this.ShouldOmitDefaultValue<T>(value))
			{
				return;
			}
			this.WritePropertyName(name);
			this.WriteValue<T>(value);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00007F16 File Offset: 0x00006116
		public void WriteAttribute<T>(string name, T value, bool forceWriteDefaultValue = false, bool allowComplexValues = false)
		{
			this.CheckCanWriteAttribute();
			if (!forceWriteDefaultValue && this.ShouldOmitDefaultValue<T>(value))
			{
				return;
			}
			this.WriteAttributeName(name);
			this.WriteValue<T>(value, !allowComplexValues);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00007F3E File Offset: 0x0000613E
		public void WriteAttribute<T>(string name, Candidate<T> value)
		{
			this.CheckCanWriteAttribute();
			if (this.ShouldOmitDefaultValue<T>(value))
			{
				return;
			}
			this.WriteAttributeName(name);
			this.WriteValue<T>(value, true);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00007F5F File Offset: 0x0000615F
		public void WriteValue<T>(Candidate<T> value)
		{
			this.WriteValue<T>(value, false);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00007F69 File Offset: 0x00006169
		private void WriteValue<T>(Candidate<T> value, bool isAttribute)
		{
			if (value == null)
			{
				this.WriteNullValue();
				return;
			}
			if (value.IsValid)
			{
				this.WriteValue<T>(value.Value, isAttribute);
				return;
			}
			this.WriteValue<string>("<invalid>", isAttribute);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00007F9D File Offset: 0x0000619D
		public void WriteValue<T>(T? value) where T : struct
		{
			if (value == null)
			{
				this.WriteNullValue();
				return;
			}
			this.WriteValue<T>(value.Value);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00007FBC File Offset: 0x000061BC
		public void WriteValue<T>(T value)
		{
			this.WriteValue<T>(value, false);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00007FC8 File Offset: 0x000061C8
		private void WriteValue<T>(T value, bool isAttribute)
		{
			if (value == null)
			{
				this.WriteNullValue();
				return;
			}
			if (value is ExpressionId)
			{
				this.CheckInvalidTypeForAttribute<T>(isAttribute);
				this.Write((ExpressionId)((object)value));
				return;
			}
			if (value is IEnumerable<ExpressionId>)
			{
				this.CheckInvalidTypeForAttribute<T>(isAttribute);
				this.Write((IEnumerable<ExpressionId>)((object)value));
				return;
			}
			if (value is IExpressionNode)
			{
				this.CheckInvalidTypeForAttribute<T>(isAttribute);
				this.Write((IExpressionNode)((object)value));
				return;
			}
			if (value is IEnumerable<IExpressionNode>)
			{
				this.CheckInvalidTypeForAttribute<T>(isAttribute);
				this.Write((IEnumerable<IExpressionNode>)((object)value));
				return;
			}
			if (value is Identifier)
			{
				this.m_builder.Append(((Identifier)((object)value)).Value);
				return;
			}
			if (value is IStructuredToString)
			{
				this.CheckInvalidTypeForAttribute<T>(isAttribute);
				this.Write((IStructuredToString)((object)value));
				return;
			}
			if (value is IEnumerable<IStructuredToString>)
			{
				this.CheckInvalidTypeForAttribute<T>(isAttribute);
				this.Write((IEnumerable<IStructuredToString>)((object)value));
				return;
			}
			if (value is IEnumerable<IEnumerable<IStructuredToString>>)
			{
				this.CheckInvalidTypeForAttribute<T>(isAttribute);
				this.Write((IEnumerable<IEnumerable<IStructuredToString>>)((object)value));
				return;
			}
			if (value is IIdentifiable)
			{
				this.Write((IIdentifiable)((object)value));
				return;
			}
			if (value is IEnumerable<IIdentifiable>)
			{
				this.CheckInvalidTypeForAttribute<T>(isAttribute);
				this.Write((IEnumerable<IIdentifiable>)((object)value));
				return;
			}
			if (value is IEnumerable<string>)
			{
				this.CheckInvalidTypeForAttribute<T>(isAttribute);
				this.Write((IEnumerable<string>)((object)value));
				return;
			}
			this.m_builder.Append(Convert.ToString(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000819F File Offset: 0x0000639F
		private void CheckInvalidTypeForAttribute<T>(bool isAttribute)
		{
			if (isAttribute)
			{
				throw new InvalidOperationException(StringUtil.FormatInvariant("The type: {0} cannot be used in an attribute.", new object[] { typeof(T).Name }));
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000081CC File Offset: 0x000063CC
		private void CheckCanWriteAttribute()
		{
			if (this.ActiveObject.HasProperty)
			{
				throw new InvalidOperationException("Attributes must appear before all properties on an object");
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000081E6 File Offset: 0x000063E6
		private bool ShouldOmitDefaultValue<T>(Candidate<T> value)
		{
			return this.m_omitItemsWithDefaultValues && (value == null || (value.IsValid && this.ShouldOmitDefaultValue<T>(value.Value)));
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00008214 File Offset: 0x00006414
		private bool ShouldOmitDefaultValue<T>(T value)
		{
			if (!this.m_omitItemsWithDefaultValues)
			{
				return false;
			}
			if (value is Enum)
			{
				return false;
			}
			if (typeof(T) == typeof(ExpressionId))
			{
				return false;
			}
			IEnumerable enumerable = value as IEnumerable;
			return (enumerable != null && !enumerable.GetEnumerator().MoveNext()) || EqualityComparer<T>.Default.Equals(value, default(T));
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00008289 File Offset: 0x00006489
		public void WriteNullValue()
		{
			this.m_builder.Append("<null>");
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000829C File Offset: 0x0000649C
		private void Write(ExpressionId value)
		{
			string text = this.m_expressionStringBuilder.Write(value);
			this.AppendExpressionString(text);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000082C0 File Offset: 0x000064C0
		private void AppendExpressionString(string exprStr)
		{
			string text = exprStr.Replace("\n", "\n" + this.m_indent);
			this.m_builder.Append(text);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000082F8 File Offset: 0x000064F8
		private void Write(IExpressionNode value)
		{
			string text = this.m_expressionStringBuilder.Write(value);
			this.AppendExpressionString(text);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00008319 File Offset: 0x00006519
		private void Write(IStructuredToString value)
		{
			if (value is Expression || value is ExpressionId)
			{
				value.WriteTo(this);
				return;
			}
			this.IncreaseIndent();
			this.NewLine();
			value.WriteTo(this);
			this.DecreaseIndent();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000834C File Offset: 0x0000654C
		private void Write(IIdentifiable value)
		{
			this.m_builder.Append(StructuredStringBuilder.GetString(value));
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00008360 File Offset: 0x00006560
		private void Write(IEnumerable<IIdentifiable> value)
		{
			Func<IIdentifiable, string> func;
			if ((func = StructuredStringBuilder.<>O.<0>__GetString) == null)
			{
				func = (StructuredStringBuilder.<>O.<0>__GetString = new Func<IIdentifiable, string>(StructuredStringBuilder.GetString));
			}
			this.Write(value.Select(func));
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00008389 File Offset: 0x00006589
		private static string GetString(IIdentifiable v)
		{
			if (!(v.Id == null))
			{
				return v.Id.Value;
			}
			return "<null>";
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000083AC File Offset: 0x000065AC
		private void Write(IEnumerable<string> value)
		{
			this.m_builder.Append("[");
			bool flag = false;
			foreach (string text in value)
			{
				if (flag)
				{
					this.m_builder.Append(", ");
				}
				this.m_builder.Append(text);
				flag = true;
			}
			this.m_builder.Append("]");
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00008434 File Offset: 0x00006634
		private void Write(IEnumerable<IStructuredToString> collection)
		{
			this.m_builder.Append("[");
			this.IncreaseIndent();
			foreach (IStructuredToString structuredToString in collection)
			{
				this.NewLine();
				if (structuredToString == null)
				{
					this.m_builder.Append("<null>");
				}
				else
				{
					structuredToString.WriteTo(this);
				}
			}
			this.m_builder.Append("]");
			this.DecreaseIndent();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000084C8 File Offset: 0x000066C8
		private void Write(IEnumerable<IEnumerable<IStructuredToString>> collection)
		{
			this.m_builder.Append("[");
			this.IncreaseIndent();
			foreach (IEnumerable<IStructuredToString> enumerable in collection)
			{
				this.NewLine();
				this.Write(enumerable);
			}
			this.m_builder.Append("]");
			this.DecreaseIndent();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00008544 File Offset: 0x00006744
		private void Write(IEnumerable<ExpressionId> collection)
		{
			this.m_builder.Append("[");
			this.IncreaseIndent();
			foreach (ExpressionId expressionId in collection)
			{
				this.NewLine();
				this.Write(expressionId);
			}
			this.m_builder.Append("]");
			this.DecreaseIndent();
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000085C0 File Offset: 0x000067C0
		private void Write(IEnumerable<IExpressionNode> collection)
		{
			this.m_builder.Append("[");
			this.IncreaseIndent();
			foreach (IExpressionNode expressionNode in collection)
			{
				this.NewLine();
				this.Write(expressionNode);
			}
			this.m_builder.Append("]");
			this.DecreaseIndent();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000863C File Offset: 0x0000683C
		private void WritePropertyName(string name)
		{
			this.NewLine();
			this.m_builder.Append(name).Append(": ");
			this.ActiveObject.HasProperty = true;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00008668 File Offset: 0x00006868
		private void WriteAttributeName(string name)
		{
			StructuredStringBuilder.ObjectState activeObject = this.ActiveObject;
			if (!activeObject.HasAttribute)
			{
				this.m_builder.Append(":: ");
			}
			else
			{
				this.m_builder.Append(" | ");
			}
			this.m_builder.Append(name).Append(": ");
			activeObject.HasAttribute = true;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000086C4 File Offset: 0x000068C4
		private void IncreaseIndent()
		{
			this.m_indentLevel++;
			this.UpdateIndent();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000086DA File Offset: 0x000068DA
		private void DecreaseIndent()
		{
			this.m_indentLevel--;
			this.UpdateIndent();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000086F0 File Offset: 0x000068F0
		private void UpdateIndent()
		{
			this.m_indent = new string(' ', this.m_indentLevel * 2);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00008707 File Offset: 0x00006907
		private void NewLine()
		{
			this.m_builder.AppendLine().Append(this.m_indent);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00008720 File Offset: 0x00006920
		public override string ToString()
		{
			return this.m_builder.ToString();
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000872D File Offset: 0x0000692D
		private StructuredStringBuilder.ObjectState ActiveObject
		{
			get
			{
				return this.m_objectStates.Peek();
			}
		}

		// Token: 0x04000215 RID: 533
		internal const string NullValueString = "<null>";

		// Token: 0x04000216 RID: 534
		internal const string InvalidValueString = "<invalid>";

		// Token: 0x04000217 RID: 535
		private readonly StringBuilder m_builder;

		// Token: 0x04000218 RID: 536
		private readonly Stack<StructuredStringBuilder.ObjectState> m_objectStates;

		// Token: 0x04000219 RID: 537
		private readonly IExpressionStringBuilder m_expressionStringBuilder;

		// Token: 0x0400021A RID: 538
		private readonly bool m_omitItemsWithDefaultValues;

		// Token: 0x0400021B RID: 539
		private int m_indentLevel;

		// Token: 0x0400021C RID: 540
		private string m_indent;

		// Token: 0x02000146 RID: 326
		private sealed class ObjectState
		{
			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001042C File Offset: 0x0000E62C
			// (set) Token: 0x06000886 RID: 2182 RVA: 0x00010434 File Offset: 0x0000E634
			internal bool HasProperty { get; set; }

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000887 RID: 2183 RVA: 0x0001043D File Offset: 0x0000E63D
			// (set) Token: 0x06000888 RID: 2184 RVA: 0x00010445 File Offset: 0x0000E645
			internal bool HasAttribute { get; set; }
		}

		// Token: 0x02000147 RID: 327
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000377 RID: 887
			public static Func<IIdentifiable, string> <0>__GetString;
		}
	}
}
