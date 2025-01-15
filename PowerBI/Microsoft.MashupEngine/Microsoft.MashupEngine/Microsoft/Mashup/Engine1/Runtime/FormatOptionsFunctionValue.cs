using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001318 RID: 4888
	public abstract class FormatOptionsFunctionValue<TResult, T0, T1> : CultureSpecificFunctionValue2<Value, Value, Value> where TResult : Value where T0 : Value where T1 : Value
	{
		// Token: 0x0600811A RID: 33050 RVA: 0x001B7E08 File Offset: 0x001B6008
		protected FormatOptionsFunctionValue(IEngineHost engineHost, TypeValue dataType, ConversionDirection direction, string param0Name)
			: base(engineHost, null, (direction == ConversionDirection.FromText) ? dataType.Nullable : NullableTypeValue.Text, 1, param0Name, (direction == ConversionDirection.ToText) ? dataType.Nullable : NullableTypeValue.Text, "options", TypeValue.Any)
		{
			this.dataType = dataType.Nullable;
			this.engineHost = engineHost;
			this.validatedEntries = new ValidationCache<string>();
		}

		// Token: 0x0600811B RID: 33051 RVA: 0x001B7E6C File Offset: 0x001B606C
		public override Value TypedInvoke(Value param0, Value options)
		{
			if (param0.IsNull)
			{
				return Value.Null;
			}
			if (!param0.Is(this.Type0))
			{
				throw ValueException.InvalidArguments(this, new Value[] { param0, options });
			}
			T0 t = param0.As<T0>(this.Type0);
			if (options.IsRecord)
			{
				return this.ProcessOptions(t, options.AsRecord);
			}
			return this.TypedInvokeNoOptions(t, options);
		}

		// Token: 0x0600811C RID: 33052 RVA: 0x001B7ED8 File Offset: 0x001B60D8
		private Value ProcessOptions(T0 text, RecordValue options)
		{
			Value format;
			Value value;
			DateTimeFormat.UnpackFormatOptions(options, out format, out value);
			if (format.IsNull)
			{
				return this.TypedInvokeNoOptions(text, value);
			}
			this.validatedEntries.Validate(format.AsString, delegate
			{
				DateTimeFormat.ValidateFormat(this.dataType, format.AsString);
			});
			return this.Convert(text, format.AsText, value);
		}

		// Token: 0x0600811D RID: 33053
		protected abstract Value TypedInvokeNoOptions(T0 param0, Value param1);

		// Token: 0x0600811E RID: 33054
		protected abstract Value Convert(T0 param0, TextValue format, Value culture);

		// Token: 0x04004671 RID: 18033
		private readonly TypeValue dataType;

		// Token: 0x04004672 RID: 18034
		protected readonly IEngineHost engineHost;

		// Token: 0x04004673 RID: 18035
		private readonly ValidationCache<string> validatedEntries;
	}
}
