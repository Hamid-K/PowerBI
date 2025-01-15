using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200131A RID: 4890
	public abstract class FormatOptionsFunctionValue<TResult, T0, T1, T2> : CultureSpecificFunctionValue3<Value, Value, Value, T2> where TResult : Value where T0 : Value where T2 : Value
	{
		// Token: 0x06008121 RID: 33057 RVA: 0x001B7F68 File Offset: 0x001B6168
		public FormatOptionsFunctionValue(IEngineHost engineHost, ConversionDirection direction, string param0Name, TypeValue dataType, string arg2Name, TypeValue arg2Type)
			: base(engineHost, null, (direction == ConversionDirection.FromText) ? dataType.Nullable : NullableTypeValue.Text.Nullable, 1, param0Name, (direction == ConversionDirection.ToText) ? dataType.Nullable : NullableTypeValue.Text, "options", TypeValue.Any, arg2Name, arg2Type)
		{
			this.dataType = dataType;
			this.engineHost = engineHost;
			this.validatedEntries = new ValidationCache<Value>();
		}

		// Token: 0x06008122 RID: 33058 RVA: 0x001B7FD0 File Offset: 0x001B61D0
		public override Value TypedInvoke(Value param0, Value options, T2 param2)
		{
			if (param0.IsNull)
			{
				return Value.Null;
			}
			if (param0.Is(base.Type0))
			{
				T0 t = param0.As<T0>(base.Type0);
				if (options.IsRecord)
				{
					return this.ProcessOptions(t, options.AsRecord, param2);
				}
				if (options is T1)
				{
					return this.TypedInvokeNoOptions(t, options, param2);
				}
			}
			throw ValueException.InvalidArguments(this, new Value[] { param0, options });
		}

		// Token: 0x06008123 RID: 33059 RVA: 0x001B8048 File Offset: 0x001B6248
		private Value ProcessOptions(T0 param0, RecordValue options, T2 param2)
		{
			Value format;
			Value value;
			DateTimeFormat.UnpackFormatOptions(options, out format, out value);
			if (format.IsNull)
			{
				return this.TypedInvokeNoOptions(param0, Value.Null, param2);
			}
			this.validatedEntries.Validate(options, delegate
			{
				DateTimeFormat.ValidateFormat(this.dataType, format.AsString);
			});
			return this.Convert(param0, format.AsString, Culture.ResolveCulture(this.engineHost, value ?? Value.Null));
		}

		// Token: 0x06008124 RID: 33060
		protected abstract TResult TypedInvokeNoOptions(T0 param0, Value param1, T2 param2);

		// Token: 0x06008125 RID: 33061
		protected abstract TResult Convert(T0 arg, string format, ICulture culture);

		// Token: 0x04004676 RID: 18038
		private readonly TypeValue dataType;

		// Token: 0x04004677 RID: 18039
		protected readonly IEngineHost engineHost;

		// Token: 0x04004678 RID: 18040
		private readonly ValidationCache<Value> validatedEntries;
	}
}
