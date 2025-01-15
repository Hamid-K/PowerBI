using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200037F RID: 895
	internal class OpenApiFunctionValue : NativeFunctionValueN
	{
		// Token: 0x06001F9A RID: 8090 RVA: 0x000521DD File Offset: 0x000503DD
		private OpenApiFunctionValue(OpenApiDocument document, OpenApiOperationDefinition operation, string operationPath)
			: this(document, operation, operationPath, new OpenApiFunctionValue.ParametersParser(operation.ParameterDefinitions))
		{
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000521F3 File Offset: 0x000503F3
		private OpenApiFunctionValue(OpenApiDocument document, OpenApiOperationDefinition operation, string operationPath, OpenApiFunctionValue.ParametersParser parser)
			: base(parser.RequiredParameterCount, parser.ParameterNames)
		{
			this.document = document;
			this.operation = operation;
			this.operationPath = operationPath;
			this.parameterTypes = parser.ParameterTypes;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0005222C File Offset: 0x0005042C
		public static FunctionValue New(OpenApiDocument document, OpenApiOperationDefinition operation, string operationPath)
		{
			FunctionValue functionValue;
			try
			{
				functionValue = new OpenApiFunctionValue(document, operation, operationPath);
			}
			catch (ValueException ex)
			{
				functionValue = new OpenApiFunctionValue.ExceptionFunctionValue(ex);
			}
			return functionValue;
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x00052260 File Offset: 0x00050460
		public override RecordValue MetaValue
		{
			get
			{
				return this.operation.GetMetadata();
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0005226D File Offset: 0x0005046D
		protected override TypeValue ParamType(int index)
		{
			return this.parameterTypes[index];
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00052278 File Offset: 0x00050478
		protected override Value InvokeN(Value[] args)
		{
			Keys keys = this.Type.AsFunctionType.Parameters.Keys;
			return new OpenApiRequest(this.document, this.operationPath, this.operation, keys, args).GetResponse().GetValue();
		}

		// Token: 0x04000BE6 RID: 3046
		private readonly OpenApiDocument document;

		// Token: 0x04000BE7 RID: 3047
		private readonly OpenApiOperationDefinition operation;

		// Token: 0x04000BE8 RID: 3048
		private readonly string operationPath;

		// Token: 0x04000BE9 RID: 3049
		private readonly TypeValue[] parameterTypes;

		// Token: 0x02000380 RID: 896
		private class ParametersParser
		{
			// Token: 0x06001FA0 RID: 8096 RVA: 0x000522C0 File Offset: 0x000504C0
			public ParametersParser(IList<OpenApiParameterDefinition> parameters)
			{
				this.requiredParameterCount = 0;
				List<string> list = new List<string>();
				List<TypeValue> list2 = new List<TypeValue>();
				List<string> list3 = new List<string>();
				List<TypeValue> list4 = new List<TypeValue>();
				if (parameters != null)
				{
					foreach (OpenApiParameterDefinition openApiParameterDefinition in parameters)
					{
						TypeValue type = openApiParameterDefinition.PartialSchema.Type;
						if (openApiParameterDefinition.Required != null)
						{
							bool? required = openApiParameterDefinition.Required;
							bool flag = true;
							if ((required.GetValueOrDefault() == flag) & (required != null))
							{
								this.requiredParameterCount++;
								list.Add(openApiParameterDefinition.Name);
								list2.Add(type);
								continue;
							}
						}
						list3.Add(openApiParameterDefinition.Name);
						list4.Add(type);
					}
				}
				this.paramterNames = list.Concat(list3).ToArray<string>();
				this.paramterTypes = list2.Concat(list4).ToArray<TypeValue>();
			}

			// Token: 0x17000DDD RID: 3549
			// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x000523D8 File Offset: 0x000505D8
			public int RequiredParameterCount
			{
				get
				{
					return this.requiredParameterCount;
				}
			}

			// Token: 0x17000DDE RID: 3550
			// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x000523E0 File Offset: 0x000505E0
			public string[] ParameterNames
			{
				get
				{
					return this.paramterNames;
				}
			}

			// Token: 0x17000DDF RID: 3551
			// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x000523E8 File Offset: 0x000505E8
			public TypeValue[] ParameterTypes
			{
				get
				{
					return this.paramterTypes;
				}
			}

			// Token: 0x04000BEA RID: 3050
			private int requiredParameterCount;

			// Token: 0x04000BEB RID: 3051
			private string[] paramterNames;

			// Token: 0x04000BEC RID: 3052
			private TypeValue[] paramterTypes;
		}

		// Token: 0x02000381 RID: 897
		private class ExceptionFunctionValue : NativeFunctionValueN
		{
			// Token: 0x06001FA4 RID: 8100 RVA: 0x000523F0 File Offset: 0x000505F0
			public ExceptionFunctionValue(ValueException exception)
				: base(0, Array.Empty<string>())
			{
				this.exception = exception;
			}

			// Token: 0x06001FA5 RID: 8101 RVA: 0x00052405 File Offset: 0x00050605
			protected override Value InvokeN(Value[] args)
			{
				throw this.exception;
			}

			// Token: 0x04000BED RID: 3053
			private readonly ValueException exception;
		}
	}
}
