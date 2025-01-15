using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D0F RID: 3343
	public sealed class CubeParametersModule : Module
	{
		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x06005A5A RID: 23130 RVA: 0x0013BB80 File Offset: 0x00139D80
		public override string Name
		{
			get
			{
				return "CubeParameters";
			}
		}

		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x06005A5B RID: 23131 RVA: 0x0013BB87 File Offset: 0x00139D87
		public override Keys ExportKeys
		{
			get
			{
				if (CubeParametersModule.exportKeys == null)
				{
					CubeParametersModule.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Cube.Parameters";
						}
						if (index != 1)
						{
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
						return "Cube.ApplyParameter";
					});
				}
				return CubeParametersModule.exportKeys;
			}
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x0013BBBF File Offset: 0x00139DBF
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return CubeParametersModule.Cube.Parameters;
				}
				if (index != 1)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				return CubeParametersModule.Cube.ApplyParameter;
			});
		}

		// Token: 0x0400328C RID: 12940
		private static Keys exportKeys;

		// Token: 0x02000D10 RID: 3344
		private enum Exports
		{
			// Token: 0x0400328E RID: 12942
			Cube_Parameters,
			// Token: 0x0400328F RID: 12943
			Cube_ApplyParameter,
			// Token: 0x04003290 RID: 12944
			Count
		}

		// Token: 0x02000D11 RID: 3345
		public static class Cube
		{
			// Token: 0x04003291 RID: 12945
			public static readonly FunctionValue Parameters = new CubeParametersModule.Cube.ParametersFunctionValue();

			// Token: 0x04003292 RID: 12946
			public static readonly FunctionValue ApplyParameter = new CubeParametersModule.Cube.ApplyParameterFunctionValue();

			// Token: 0x02000D12 RID: 3346
			private class ParametersFunctionValue : NativeFunctionValue1<TableValue, TableValue>
			{
				// Token: 0x06005A5F RID: 23135 RVA: 0x0013B00D File Offset: 0x0013920D
				public ParametersFunctionValue()
					: base(TypeValue.Table, "cube", TypeValue.Table)
				{
				}

				// Token: 0x06005A60 RID: 23136 RVA: 0x0013BC01 File Offset: 0x00139E01
				public override TableValue TypedInvoke(TableValue cube)
				{
					return cube.AsCube.Parameters;
				}
			}

			// Token: 0x02000D13 RID: 3347
			private class ApplyParameterFunctionValue : NativeFunctionValue3<TableValue, TableValue, Value, Value>
			{
				// Token: 0x06005A61 RID: 23137 RVA: 0x0013BC10 File Offset: 0x00139E10
				public ApplyParameterFunctionValue()
					: base(TypeValue.Table, 2, "cube", TypeValue.Table, "parameter", TypeValue.Any, "arguments", TypeValue.List.Nullable)
				{
				}

				// Token: 0x06005A62 RID: 23138 RVA: 0x0013BC4C File Offset: 0x00139E4C
				public override TableValue TypedInvoke(TableValue cube, Value parameter, Value arguments)
				{
					CubeValue asCube = cube.AsCube;
					FunctionValue parameterValue = this.GetParameterValue(asCube, parameter);
					Value[] array = (arguments.IsNull ? EmptyArray<Value>.Instance : arguments.AsList.ToArray());
					return parameterValue.Invoke(array).AsTable;
				}

				// Token: 0x06005A63 RID: 23139 RVA: 0x0013BC90 File Offset: 0x00139E90
				private FunctionValue GetParameterValue(CubeValue cubeValue, Value parameter)
				{
					if (parameter.Kind == ValueKind.Text)
					{
						RecordValue recordValue = RecordValue.New(CubeParametersModule.Cube.ApplyParameterFunctionValue.indexKeys, new Value[] { parameter });
						return cubeValue.Parameters[recordValue]["Data"].AsFunction;
					}
					FunctionValue asFunction = parameter.AsFunction;
					if (parameter.AsFunction is ParameterValue)
					{
						return asFunction;
					}
					throw ValueException.NewExpressionError<Message0>(Strings.Cube_InvalidParameterFunction, RecordValue.New(CubeParametersModule.Cube.ApplyParameterFunctionValue.errorKeys, new Value[] { parameter.AsFunction }), null);
				}

				// Token: 0x04003293 RID: 12947
				private static readonly Keys indexKeys = Keys.New("Id");

				// Token: 0x04003294 RID: 12948
				private static readonly Keys errorKeys = Keys.New("Function");
			}
		}
	}
}
