using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001325 RID: 4901
	public abstract class FunctionValue : StructureValue, IFunctionValue, IValue, IFunctionIdentity, IEquatable<IFunctionIdentity>
	{
		// Token: 0x06008175 RID: 33141 RVA: 0x001B85BF File Offset: 0x001B67BF
		public static FunctionValue New(Func<Value> getValue)
		{
			return new FunctionValue.GetValueFunctionValue(getValue);
		}

		// Token: 0x06008176 RID: 33142 RVA: 0x001B85C7 File Offset: 0x001B67C7
		private static FunctionValue New(FunctionValue value, RecordValue meta, TypeValue type)
		{
			if (meta.IsEmpty && value.Type == type)
			{
				return value;
			}
			return new FunctionValue.MetaTypeFunctionValue(value, meta, type);
		}

		// Token: 0x17002306 RID: 8966
		// (get) Token: 0x06008177 RID: 33143 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Function;
			}
		}

		// Token: 0x17002307 RID: 8967
		// (get) Token: 0x06008178 RID: 33144 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsFunction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002308 RID: 8968
		// (get) Token: 0x06008179 RID: 33145 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override FunctionValue AsFunction
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600817A RID: 33146 RVA: 0x001B85E4 File Offset: 0x001B67E4
		public virtual bool TryGetCultureCase(out CultureInfo cultureInfo, out bool ignoreCase)
		{
			cultureInfo = null;
			ignoreCase = false;
			return false;
		}

		// Token: 0x0600817B RID: 33147 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetEqualityComparer(out IEqualityComparer<Value> comparer)
		{
			comparer = null;
			return false;
		}

		// Token: 0x0600817C RID: 33148 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetBinaryFormat(out IBinaryFormat binaryFormat)
		{
			binaryFormat = null;
			return false;
		}

		// Token: 0x17002309 RID: 8969
		// (get) Token: 0x0600817D RID: 33149 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public virtual IFunctionIdentity FunctionIdentity
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700230A RID: 8970
		// (get) Token: 0x0600817E RID: 33150 RVA: 0x001B85ED File Offset: 0x001B67ED
		IFunctionExpression IFunctionValue.FunctionExpression
		{
			get
			{
				return this.Expression as IFunctionExpression;
			}
		}

		// Token: 0x0600817F RID: 33151 RVA: 0x001B85FA File Offset: 0x001B67FA
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsFunction && this.FunctionIdentity.Equals(value.AsFunction.FunctionIdentity);
		}

		// Token: 0x06008180 RID: 33152 RVA: 0x001B861C File Offset: 0x001B681C
		public override int GetHashCode(_ValueComparer comparer)
		{
			return this.FunctionIdentity.GetHashCode();
		}

		// Token: 0x06008181 RID: 33153 RVA: 0x00002E92 File Offset: 0x00001092
		bool IEquatable<IFunctionIdentity>.Equals(IFunctionIdentity functionIdentity)
		{
			return this == functionIdentity;
		}

		// Token: 0x06008182 RID: 33154 RVA: 0x001B8629 File Offset: 0x001B6829
		int IFunctionIdentity.GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this.FunctionIdentity);
		}

		// Token: 0x06008183 RID: 33155 RVA: 0x001B8636 File Offset: 0x001B6836
		public virtual Value Invoke()
		{
			throw ValueException.InvalidArguments(this, Array.Empty<Value>());
		}

		// Token: 0x06008184 RID: 33156 RVA: 0x001B8643 File Offset: 0x001B6843
		public virtual Value Invoke(Value arg0)
		{
			throw ValueException.InvalidArguments(this, new Value[] { arg0 });
		}

		// Token: 0x06008185 RID: 33157 RVA: 0x001B8655 File Offset: 0x001B6855
		public virtual Value Invoke(Value arg0, Value arg1)
		{
			throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1 });
		}

		// Token: 0x06008186 RID: 33158 RVA: 0x001B866B File Offset: 0x001B686B
		public virtual Value Invoke(Value arg0, Value arg1, Value arg2)
		{
			throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1, arg2 });
		}

		// Token: 0x06008187 RID: 33159 RVA: 0x001B8685 File Offset: 0x001B6885
		public virtual Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
		{
			throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1, arg2, arg3 });
		}

		// Token: 0x06008188 RID: 33160 RVA: 0x001B86A4 File Offset: 0x001B68A4
		public virtual Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
		{
			throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1, arg2, arg3, arg4 });
		}

		// Token: 0x06008189 RID: 33161 RVA: 0x001B86C8 File Offset: 0x001B68C8
		public virtual Value Invoke(params Value[] args)
		{
			switch (args.Length)
			{
			case 0:
				return this.Invoke();
			case 1:
				return this.Invoke(args[0]);
			case 2:
				return this.Invoke(args[0], args[1]);
			case 3:
				return this.Invoke(args[0], args[1], args[2]);
			case 4:
				return this.Invoke(args[0], args[1], args[2], args[3]);
			case 5:
				return this.Invoke(args[0], args[1], args[2], args[3], args[4]);
			default:
				throw ValueException.InvalidArguments(this, args);
			}
		}

		// Token: 0x1700230B RID: 8971
		// (get) Token: 0x0600818A RID: 33162 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x0600818B RID: 33163 RVA: 0x001B8757 File Offset: 0x001B6957
		public override Value NewMeta(RecordValue metaValue)
		{
			return FunctionValue.New(this, metaValue, this.Type);
		}

		// Token: 0x1700230C RID: 8972
		// (get) Token: 0x0600818C RID: 33164 RVA: 0x001B8766 File Offset: 0x001B6966
		public override TypeValue Type
		{
			get
			{
				return TypeValue.Function;
			}
		}

		// Token: 0x0600818D RID: 33165 RVA: 0x001B8770 File Offset: 0x001B6970
		public override Value NewType(TypeValue type)
		{
			FunctionTypeValue asFunctionType = this.Type.AsFunctionType;
			FunctionTypeValue asFunctionType2 = type.AsFunctionType;
			if (asFunctionType.ParameterCount != asFunctionType2.ParameterCount || asFunctionType.Min != asFunctionType2.Min)
			{
				throw ValueException.CastTypeMismatch(this, type);
			}
			return FunctionValue.New(this, this.MetaValue, type);
		}

		// Token: 0x0600818E RID: 33166 RVA: 0x001B87C1 File Offset: 0x001B69C1
		public override string ToSource()
		{
			return "(...) => ...";
		}

		// Token: 0x0600818F RID: 33167 RVA: 0x001B87C8 File Offset: 0x001B69C8
		public override string ToString()
		{
			return "Function";
		}

		// Token: 0x06008190 RID: 33168 RVA: 0x001B87CF File Offset: 0x001B69CF
		public sealed override object ToOleDb(Type type)
		{
			return ValueMarshaller.ToOleDbString("[Function]", this, type);
		}

		// Token: 0x06008191 RID: 33169 RVA: 0x0000336E File Offset: 0x0000156E
		public override void TestConnection()
		{
		}

		// Token: 0x06008192 RID: 33170 RVA: 0x001B87DD File Offset: 0x001B69DD
		public virtual TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
		{
			return this.Type.AsFunctionType.ReturnType;
		}

		// Token: 0x06008193 RID: 33171 RVA: 0x001B87EF File Offset: 0x001B69EF
		public virtual FunctionValue Optimize()
		{
			return new FunctionValue.OptimizedFunctionValue(this);
		}

		// Token: 0x1700230D RID: 8973
		// (get) Token: 0x06008194 RID: 33172 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual string PrimaryResourceKind
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700230E RID: 8974
		// (get) Token: 0x06008195 RID: 33173 RVA: 0x001B87F7 File Offset: 0x001B69F7
		public bool IsResourceAccessFunction
		{
			get
			{
				return this.PrimaryResourceKind != null;
			}
		}

		// Token: 0x06008196 RID: 33174 RVA: 0x001B8802 File Offset: 0x001B6A02
		public virtual bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
		{
			location = null;
			foundOptions = null;
			unknownOptions = null;
			return false;
		}

		// Token: 0x06008197 RID: 33175 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryVectorizeFunction(out FunctionValue vectorFunction)
		{
			vectorFunction = null;
			return false;
		}

		// Token: 0x06008198 RID: 33176 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
		{
			accumulableFunction = null;
			return false;
		}

		// Token: 0x06008199 RID: 33177 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
		{
			accumulableChainingFunction = null;
			return false;
		}

		// Token: 0x0400468E RID: 18062
		private const string placeholder = "[Function]";

		// Token: 0x0400468F RID: 18063
		public static readonly TextValue Placeholder = TextValue.New("[Function]");

		// Token: 0x02001326 RID: 4902
		private sealed class OptimizedFunctionValue : DelegatingFunctionValue, IOptimizedValue
		{
			// Token: 0x0600819C RID: 33180 RVA: 0x001B7181 File Offset: 0x001B5381
			public OptimizedFunctionValue(FunctionValue function)
				: base(function)
			{
			}

			// Token: 0x0600819D RID: 33181 RVA: 0x001B8828 File Offset: 0x001B6A28
			protected override FunctionValue Wrap(FunctionValue function)
			{
				throw new InvalidOperationException("invalid call to OptimizedFunctionValue.Wrap(FunctionValue)");
			}

			// Token: 0x1700230F RID: 8975
			// (get) Token: 0x0600819E RID: 33182 RVA: 0x001B8834 File Offset: 0x001B6A34
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null && base.Function.Expression != null)
					{
						this.expression = NormalizationVisitor.Normalize(base.Function.Expression, true);
					}
					return this.expression;
				}
			}

			// Token: 0x0600819F RID: 33183 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override FunctionValue Optimize()
			{
				return this;
			}

			// Token: 0x04004690 RID: 18064
			private IExpression expression;
		}

		// Token: 0x02001327 RID: 4903
		private sealed class MetaTypeFunctionValue : DelegatingFunctionValue
		{
			// Token: 0x060081A0 RID: 33184 RVA: 0x001B8868 File Offset: 0x001B6A68
			public MetaTypeFunctionValue(FunctionValue function, RecordValue meta, TypeValue type)
				: base(function)
			{
				this.meta = meta;
				this.type = type;
			}

			// Token: 0x060081A1 RID: 33185 RVA: 0x001B887F File Offset: 0x001B6A7F
			protected override FunctionValue Wrap(FunctionValue function)
			{
				throw new InvalidOperationException("invalid call to MetaTypeFunctionValue.Wrap(FunctionValue)");
			}

			// Token: 0x17002310 RID: 8976
			// (get) Token: 0x060081A2 RID: 33186 RVA: 0x001B888B File Offset: 0x001B6A8B
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x060081A3 RID: 33187 RVA: 0x001B8893 File Offset: 0x001B6A93
			public override Value NewMeta(RecordValue metaValue)
			{
				return FunctionValue.New(base.Function, metaValue, this.Type);
			}

			// Token: 0x17002311 RID: 8977
			// (get) Token: 0x060081A4 RID: 33188 RVA: 0x001B88A7 File Offset: 0x001B6AA7
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x060081A5 RID: 33189 RVA: 0x001B88AF File Offset: 0x001B6AAF
			public override Value NewType(TypeValue type)
			{
				return base.Function.NewType(type).NewMeta(this.meta);
			}

			// Token: 0x04004691 RID: 18065
			private readonly RecordValue meta;

			// Token: 0x04004692 RID: 18066
			private readonly TypeValue type;
		}

		// Token: 0x02001328 RID: 4904
		private sealed class GetValueFunctionValue : NativeFunctionValue0<Value>
		{
			// Token: 0x060081A6 RID: 33190 RVA: 0x001B88C8 File Offset: 0x001B6AC8
			public GetValueFunctionValue(Func<Value> getValue)
				: base(TypeValue.Any)
			{
				this.getValue = getValue;
			}

			// Token: 0x060081A7 RID: 33191 RVA: 0x001B88DC File Offset: 0x001B6ADC
			public override Value TypedInvoke()
			{
				return this.getValue();
			}

			// Token: 0x04004693 RID: 18067
			private readonly Func<Value> getValue;
		}
	}
}
