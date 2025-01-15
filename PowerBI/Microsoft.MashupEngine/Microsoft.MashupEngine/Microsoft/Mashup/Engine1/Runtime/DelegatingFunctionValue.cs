using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012D6 RID: 4822
	public abstract class DelegatingFunctionValue : FunctionValue, IFunctionIdentity, IEquatable<IFunctionIdentity>
	{
		// Token: 0x06007F41 RID: 32577 RVA: 0x001B3E40 File Offset: 0x001B2040
		protected DelegatingFunctionValue(FunctionValue function)
		{
			this.function = function;
		}

		// Token: 0x06007F42 RID: 32578
		protected abstract FunctionValue Wrap(FunctionValue function);

		// Token: 0x17002274 RID: 8820
		// (get) Token: 0x06007F43 RID: 32579 RVA: 0x001B3E4F File Offset: 0x001B204F
		protected FunctionValue Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17002275 RID: 8821
		// (get) Token: 0x06007F44 RID: 32580 RVA: 0x001B3E57 File Offset: 0x001B2057
		public override IFunctionIdentity FunctionIdentity
		{
			get
			{
				return this.function.FunctionIdentity;
			}
		}

		// Token: 0x06007F45 RID: 32581 RVA: 0x001B3E64 File Offset: 0x001B2064
		bool IEquatable<IFunctionIdentity>.Equals(IFunctionIdentity functionIdentity)
		{
			throw new InvalidOperationException("invalid call to DelegatingFunctionValue.Equals(FunctionIdentity)");
		}

		// Token: 0x06007F46 RID: 32582 RVA: 0x001B3E70 File Offset: 0x001B2070
		int IFunctionIdentity.GetHashCode()
		{
			throw new InvalidOperationException("invalid call to DelegatingFunctionValue.GetHashCode()");
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x001B3E7C File Offset: 0x001B207C
		public override bool TryGetProcessor(out QueryProcessor processor)
		{
			return this.function.TryGetProcessor(out processor);
		}

		// Token: 0x17002276 RID: 8822
		// (get) Token: 0x06007F48 RID: 32584 RVA: 0x001B3E8A File Offset: 0x001B208A
		public override RecordValue MetaValue
		{
			get
			{
				return this.function.MetaValue;
			}
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x001B3E97 File Offset: 0x001B2097
		public override Value NewMeta(RecordValue metaValue)
		{
			return this.Wrap(this.function.NewMeta(metaValue).AsFunction);
		}

		// Token: 0x17002277 RID: 8823
		// (get) Token: 0x06007F4A RID: 32586 RVA: 0x001B3EB0 File Offset: 0x001B20B0
		public override TypeValue Type
		{
			get
			{
				return this.function.Type;
			}
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x001B3EBD File Offset: 0x001B20BD
		public override Value NewType(TypeValue type)
		{
			return this.Wrap(this.function.NewType(type).AsFunction);
		}

		// Token: 0x17002278 RID: 8824
		// (get) Token: 0x06007F4C RID: 32588 RVA: 0x001B3ED6 File Offset: 0x001B20D6
		public override IExpression Expression
		{
			get
			{
				return this.function.Expression;
			}
		}

		// Token: 0x06007F4D RID: 32589 RVA: 0x001B3EE3 File Offset: 0x001B20E3
		public override bool TryGetCultureCase(out CultureInfo cultureInfo, out bool ignoreCase)
		{
			return this.function.TryGetCultureCase(out cultureInfo, out ignoreCase);
		}

		// Token: 0x06007F4E RID: 32590 RVA: 0x001B3EF2 File Offset: 0x001B20F2
		public override bool TryGetEqualityComparer(out IEqualityComparer<Value> comparer)
		{
			return this.function.TryGetEqualityComparer(out comparer);
		}

		// Token: 0x06007F4F RID: 32591 RVA: 0x001B3F00 File Offset: 0x001B2100
		public override bool TryGetBinaryFormat(out IBinaryFormat binaryFormat)
		{
			return this.function.TryGetBinaryFormat(out binaryFormat);
		}

		// Token: 0x06007F50 RID: 32592 RVA: 0x001B3F0E File Offset: 0x001B210E
		public override Value Invoke()
		{
			return this.function.Invoke();
		}

		// Token: 0x06007F51 RID: 32593 RVA: 0x001B3F1B File Offset: 0x001B211B
		public override Value Invoke(Value arg0)
		{
			return this.function.Invoke(arg0);
		}

		// Token: 0x06007F52 RID: 32594 RVA: 0x001B3F29 File Offset: 0x001B2129
		public override Value Invoke(Value arg0, Value arg1)
		{
			return this.function.Invoke(arg0, arg1);
		}

		// Token: 0x06007F53 RID: 32595 RVA: 0x001B3F38 File Offset: 0x001B2138
		public override Value Invoke(Value arg0, Value arg1, Value arg2)
		{
			return this.function.Invoke(arg0, arg1, arg2);
		}

		// Token: 0x06007F54 RID: 32596 RVA: 0x001B3F48 File Offset: 0x001B2148
		public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
		{
			return this.function.Invoke(arg0, arg1, arg2, arg3);
		}

		// Token: 0x06007F55 RID: 32597 RVA: 0x001B3F5A File Offset: 0x001B215A
		public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
		{
			return this.function.Invoke(arg0, arg1, arg2, arg3, arg4);
		}

		// Token: 0x06007F56 RID: 32598 RVA: 0x001B3F6E File Offset: 0x001B216E
		public override Value Invoke(params Value[] args)
		{
			return this.function.Invoke(args);
		}

		// Token: 0x06007F57 RID: 32599 RVA: 0x001B3F7C File Offset: 0x001B217C
		public override string ToSource()
		{
			return this.function.ToSource();
		}

		// Token: 0x17002279 RID: 8825
		// (get) Token: 0x06007F58 RID: 32600 RVA: 0x001B3F89 File Offset: 0x001B2189
		public override string PrimaryResourceKind
		{
			get
			{
				return this.function.PrimaryResourceKind;
			}
		}

		// Token: 0x06007F59 RID: 32601 RVA: 0x001B3F96 File Offset: 0x001B2196
		public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
		{
			return this.function.TryGetLocation(expression, out location, out foundOptions, out unknownOptions);
		}

		// Token: 0x06007F5A RID: 32602 RVA: 0x001B3FA8 File Offset: 0x001B21A8
		public override bool TryVectorizeFunction(out FunctionValue vectorFunction)
		{
			return this.function.TryVectorizeFunction(out vectorFunction);
		}

		// Token: 0x06007F5B RID: 32603 RVA: 0x001B3FB6 File Offset: 0x001B21B6
		public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
		{
			return this.function.TryGetAccumulableFunction(out accumulableFunction);
		}

		// Token: 0x06007F5C RID: 32604 RVA: 0x001B3FC4 File Offset: 0x001B21C4
		public override bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
		{
			return this.function.TryGetAccumulableChainingFunction(out accumulableChainingFunction);
		}

		// Token: 0x06007F5D RID: 32605 RVA: 0x001B3FD2 File Offset: 0x001B21D2
		public override bool TryGetAs<T>(out T contract)
		{
			return this.function.TryGetAs<T>(out contract);
		}

		// Token: 0x06007F5E RID: 32606 RVA: 0x001B3FE0 File Offset: 0x001B21E0
		public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
		{
			return this.function.GetTypeflowReturnType(arguments, environment);
		}

		// Token: 0x040045A8 RID: 17832
		private readonly FunctionValue function;
	}
}
