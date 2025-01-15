using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001110 RID: 4368
	internal abstract class QueryResultFunctionValue : FunctionValue, IQueryResultValue
	{
		// Token: 0x06007247 RID: 29255 RVA: 0x001894DD File Offset: 0x001876DD
		protected QueryResultFunctionValue(EnvironmentBase environment, Identifier identifier, IEngineHost host, FunctionTypeValue type)
		{
			this.environment = environment;
			this.host = host;
			this.identifier = identifier;
			this.type = type;
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x06007248 RID: 29256 RVA: 0x00189502 File Offset: 0x00187702
		public EnvironmentBase Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x06007249 RID: 29257 RVA: 0x0018950A File Offset: 0x0018770A
		public string FunctionName
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x0600724A RID: 29258 RVA: 0x00189517 File Offset: 0x00187717
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x0600724B RID: 29259 RVA: 0x0018951F File Offset: 0x0018771F
		public Identifier Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x0600724C RID: 29260 RVA: 0x00189527 File Offset: 0x00187727
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600724D RID: 29261 RVA: 0x0018952F File Offset: 0x0018772F
		public static QueryResultFunctionValue CreateFunction(EnvironmentBase environment, Identifier identifier, IEngineHost host, FunctionTypeValue functionType)
		{
			return new QueryResultFunctionValue.AnyResultFunctionValue(environment, identifier, host, functionType);
		}

		// Token: 0x0600724E RID: 29262 RVA: 0x0018953A File Offset: 0x0018773A
		public static QueryResultFunctionValue CreateProcedure(DbEnvironment environment, Identifier identifier, IEngineHost host, FunctionTypeValue functionType)
		{
			return new QueryResultFunctionValue.ActionResultProcedureValue(environment, identifier, host, functionType);
		}

		// Token: 0x0600724F RID: 29263 RVA: 0x00189545 File Offset: 0x00187745
		public sealed override Value Invoke()
		{
			return this.Invoke(new Value[0]);
		}

		// Token: 0x06007250 RID: 29264 RVA: 0x00189553 File Offset: 0x00187753
		public sealed override Value Invoke(Value arg0)
		{
			return this.Invoke(new Value[] { arg0 });
		}

		// Token: 0x06007251 RID: 29265 RVA: 0x00189565 File Offset: 0x00187765
		public sealed override Value Invoke(Value arg0, Value arg1)
		{
			return this.Invoke(new Value[] { arg0, arg1 });
		}

		// Token: 0x06007252 RID: 29266 RVA: 0x0018957B File Offset: 0x0018777B
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2 });
		}

		// Token: 0x06007253 RID: 29267 RVA: 0x00189595 File Offset: 0x00187795
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2, arg3 });
		}

		// Token: 0x06007254 RID: 29268 RVA: 0x001895B4 File Offset: 0x001877B4
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2, arg3, arg4 });
		}

		// Token: 0x06007255 RID: 29269 RVA: 0x001895D8 File Offset: 0x001877D8
		public sealed override Value Invoke(params Value[] args)
		{
			FunctionTypeValue asFunctionType = this.Type.AsFunctionType;
			if (args.Length < asFunctionType.Min || args.Length > asFunctionType.Parameters.Count)
			{
				throw ValueException.InvalidArguments(this, args);
			}
			if (args.Length != asFunctionType.Parameters.Count)
			{
				Value[] array = new Value[asFunctionType.Parameters.Count];
				Array.Copy(args, array, args.Length);
				for (int i = args.Length; i < asFunctionType.Parameters.Count; i++)
				{
					array[i] = Value.Null;
				}
				args = array;
			}
			Value[] array2 = new Value[args.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = args[j].As<Value>(asFunctionType.Parameters[j].AsType);
			}
			return this.InvokeN(array2);
		}

		// Token: 0x06007256 RID: 29270
		protected abstract Value InvokeN(Value[] args);

		// Token: 0x06007257 RID: 29271 RVA: 0x001896A4 File Offset: 0x001878A4
		public static bool IsExternalFunctionExpressionInvocation(IExpression expression)
		{
			if (expression.Kind == ExpressionKind.Invocation)
			{
				IInvocationExpression invocationExpression = (IInvocationExpression)expression;
				if (invocationExpression.Function.Kind == ExpressionKind.Constant)
				{
					return ((IConstantExpression)invocationExpression.Function).Value is QueryResultFunctionValue.RuntimeFunctionValue;
				}
			}
			return false;
		}

		// Token: 0x04003F0E RID: 16142
		private static readonly QueryProcessor externalQueryProcessor = new ExternalQueryProcessor();

		// Token: 0x04003F0F RID: 16143
		private readonly EnvironmentBase environment;

		// Token: 0x04003F10 RID: 16144
		private readonly IEngineHost host;

		// Token: 0x04003F11 RID: 16145
		private readonly Identifier identifier;

		// Token: 0x04003F12 RID: 16146
		private readonly FunctionTypeValue type;

		// Token: 0x02001111 RID: 4369
		private sealed class RuntimeFunctionValue : QueryResultFunctionValue
		{
			// Token: 0x06007259 RID: 29273 RVA: 0x001896F5 File Offset: 0x001878F5
			public RuntimeFunctionValue(EnvironmentBase environment, Identifier identifier, IEngineHost host, FunctionTypeValue type)
				: base(environment, identifier, host, type)
			{
			}

			// Token: 0x0600725A RID: 29274 RVA: 0x0000EE09 File Offset: 0x0000D009
			protected override Value InvokeN(Value[] args)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02001112 RID: 4370
		private sealed class AnyResultFunctionValue : QueryResultFunctionValue
		{
			// Token: 0x0600725B RID: 29275 RVA: 0x00189702 File Offset: 0x00187902
			public AnyResultFunctionValue(EnvironmentBase environment, Identifier identifier, IEngineHost host, FunctionTypeValue functionType)
				: base(environment, identifier, host, functionType)
			{
				this.runtimeFunction = new QueryResultFunctionValue.RuntimeFunctionValue(environment, identifier, host, this.type);
			}

			// Token: 0x0600725C RID: 29276 RVA: 0x00189724 File Offset: 0x00187924
			protected override Value InvokeN(Value[] args)
			{
				IExpression expression = LanguageLibrary.Query.CreateInvokeQuery(this.runtimeFunction, args);
				IExpression expression2;
				if (!QueryResultFunctionValue.externalQueryProcessor.TryFold(null, expression, out expression2))
				{
					throw new InvalidOperationException();
				}
				return ((IConstantExpression)expression2).Value.As<Value>(this.type.ReturnType);
			}

			// Token: 0x04003F13 RID: 16147
			private readonly Value runtimeFunction;
		}

		// Token: 0x02001113 RID: 4371
		private sealed class RuntimeProcedureValue : QueryResultFunctionValue
		{
			// Token: 0x0600725D RID: 29277 RVA: 0x001896F5 File Offset: 0x001878F5
			public RuntimeProcedureValue(DbEnvironment environment, Identifier identifier, IEngineHost host, FunctionTypeValue type)
				: base(environment, identifier, host, type)
			{
			}

			// Token: 0x0600725E RID: 29278 RVA: 0x0000EE09 File Offset: 0x0000D009
			protected override Value InvokeN(Value[] args)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02001114 RID: 4372
		private sealed class ActionResultProcedureValue : QueryResultFunctionValue
		{
			// Token: 0x0600725F RID: 29279 RVA: 0x0018976F File Offset: 0x0018796F
			public ActionResultProcedureValue(DbEnvironment environment, Identifier identifier, IEngineHost host, FunctionTypeValue functionType)
				: base(environment, identifier, host, functionType)
			{
				this.runtimeFunction = new QueryResultFunctionValue.RuntimeProcedureValue(environment, identifier, host, this.type);
			}

			// Token: 0x06007260 RID: 29280 RVA: 0x00189790 File Offset: 0x00187990
			protected override Value InvokeN(Value[] args)
			{
				IExpression expression = LanguageLibrary.Query.CreateInvokeQuery(this.runtimeFunction, args);
				this.environment.VerifyActionPermitted();
				return this.environment.CompileStatement(this.environment.CreateCatalogTableValue(this.host).Query, expression, "StoredProcedure");
			}

			// Token: 0x04003F14 RID: 16148
			private readonly Value runtimeFunction;
		}
	}
}
