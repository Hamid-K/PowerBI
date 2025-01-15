using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016DC RID: 5852
	internal static class ViewExceptions
	{
		// Token: 0x060094D5 RID: 38101 RVA: 0x001EB6D5 File Offset: 0x001E98D5
		public static RecordValue Mark(RecordValue errorRecord)
		{
			return Library.Record.TransformFields.Invoke(errorRecord, ViewExceptions.transformMessage).AsRecord;
		}

		// Token: 0x060094D6 RID: 38102 RVA: 0x001EB6EC File Offset: 0x001E98EC
		public static bool IsMarked(ValueException e)
		{
			Value value;
			return e.Value.TryGetValue("Reason", out value) && value.Type.TryGetMetaField(ViewExceptions.markerMeta.Keys[0], out value) && value.IsLogical && value.AsBoolean;
		}

		// Token: 0x04004F21 RID: 20257
		private static readonly RecordValue markerMeta = RecordValue.New(Keys.New(Guid.NewGuid().ToString()), new Value[] { LogicalValue.True });

		// Token: 0x04004F22 RID: 20258
		private static readonly ListValue transformMessage = ListValue.New(new Value[]
		{
			TextValue.New("Reason"),
			new ViewExceptions.ApplyMarkerFunctionValue()
		});

		// Token: 0x020016DD RID: 5853
		private sealed class ApplyMarkerFunctionValue : NativeFunctionValue1<Value, Value>
		{
			// Token: 0x060094D8 RID: 38104 RVA: 0x0007E42B File Offset: 0x0007C62B
			public ApplyMarkerFunctionValue()
				: base(TypeValue.Any, "value", TypeValue.Any)
			{
			}

			// Token: 0x060094D9 RID: 38105 RVA: 0x001EB79E File Offset: 0x001E999E
			public override Value TypedInvoke(Value value)
			{
				return value.ReplaceType(BinaryOperator.AddMeta.Invoke(value.Type, ViewExceptions.markerMeta).AsType);
			}
		}
	}
}
