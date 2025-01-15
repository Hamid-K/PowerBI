using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200160D RID: 5645
	public class SerializationApplyingModule : DelegatingModule
	{
		// Token: 0x06008DEF RID: 36335 RVA: 0x001DA79A File Offset: 0x001D899A
		public SerializationApplyingModule(Module module)
			: base(module)
		{
		}

		// Token: 0x06008DF0 RID: 36336 RVA: 0x001DA7A4 File Offset: 0x001D89A4
		public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
		{
			RecordValue recordValue = base.Link(environment, hostEnvironment);
			Value value;
			if (recordValue.TryGetValue("Shared", out value) && value.IsRecord)
			{
				recordValue = RecordValue.Combine(ListValue.New(new Value[]
				{
					recordValue,
					RecordValue.New(DelegatingModule.SharedKeys, new Value[] { SerializationApplyingModule.ApplySerialization(value.AsRecord) })
				}));
			}
			return recordValue;
		}

		// Token: 0x06008DF1 RID: 36337 RVA: 0x001DA808 File Offset: 0x001D8A08
		private static RecordValue ApplySerialization(RecordValue exports)
		{
			return RecordValue.New(exports.Keys, (int i) => SerializationApplyingModule.ApplySerialization(exports.Keys[i], exports[i]));
		}

		// Token: 0x06008DF2 RID: 36338 RVA: 0x001DA83E File Offset: 0x001D8A3E
		private static Value ApplySerialization(string name, Value value)
		{
			if (!value.IsFunction || value.Expression != null)
			{
				return value;
			}
			return new SerializationApplyingModule.SerializableFunctionValue(name, value.AsFunction);
		}

		// Token: 0x0200160E RID: 5646
		private sealed class SerializableFunctionValue : DelegatingFunctionValue
		{
			// Token: 0x06008DF3 RID: 36339 RVA: 0x001DA85E File Offset: 0x001D8A5E
			public SerializableFunctionValue(string name, FunctionValue function)
				: this(name, function, null)
			{
			}

			// Token: 0x06008DF4 RID: 36340 RVA: 0x001DA869 File Offset: 0x001D8A69
			private SerializableFunctionValue(string name, FunctionValue function, IExpression expression)
				: base(function)
			{
				this.name = name;
				this.expression = expression;
			}

			// Token: 0x17002545 RID: 9541
			// (get) Token: 0x06008DF5 RID: 36341 RVA: 0x001DA880 File Offset: 0x001D8A80
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = base.Function.Expression ?? new LibraryIdentifierExpression(this.name);
					}
					return this.expression;
				}
			}

			// Token: 0x06008DF6 RID: 36342 RVA: 0x001DA8B0 File Offset: 0x001D8AB0
			protected override FunctionValue Wrap(FunctionValue function)
			{
				return new SerializationApplyingModule.SerializableFunctionValue(this.name, function, this.expression);
			}

			// Token: 0x04004D33 RID: 19763
			private readonly string name;

			// Token: 0x04004D34 RID: 19764
			private IExpression expression;
		}
	}
}
