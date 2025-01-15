using System;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D3F RID: 3391
	internal class ReplaceDimensionsCubeValue : WrappingCubeValue
	{
		// Token: 0x06005B2C RID: 23340 RVA: 0x0013EA63 File Offset: 0x0013CC63
		public ReplaceDimensionsCubeValue(CubeValue cube, FunctionValue getDimensions)
			: base(cube)
		{
			this.getDimensions = getDimensions;
		}

		// Token: 0x17001AF8 RID: 6904
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x0013EA73 File Offset: 0x0013CC73
		public override TableValue Dimensions
		{
			get
			{
				if (this.dimensions == null)
				{
					this.dimensions = this.getDimensions.Invoke(base.Cube).AsTable;
				}
				return this.dimensions;
			}
		}

		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x06005B2E RID: 23342 RVA: 0x0013EAA0 File Offset: 0x0013CCA0
		public override TableValue Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					Value value = ListValue.New(new Value[]
					{
						TextValue.New("Data"),
						new ReplaceDimensionsCubeValue.ReplaceParameterFunctionValue(this)
					});
					this.parameters = TableModule.Table.TransformColumns.Invoke(base.Cube.Parameters, ListValue.New(new Value[] { value })).AsTable;
				}
				return this.parameters;
			}
		}

		// Token: 0x06005B2F RID: 23343 RVA: 0x0013EB0C File Offset: 0x0013CD0C
		protected override CubeValue New(CubeValue cube)
		{
			return new ReplaceDimensionsCubeValue(cube, this.getDimensions);
		}

		// Token: 0x040032D9 RID: 13017
		private readonly FunctionValue getDimensions;

		// Token: 0x040032DA RID: 13018
		private TableValue dimensions;

		// Token: 0x040032DB RID: 13019
		private TableValue parameters;

		// Token: 0x02000D40 RID: 3392
		private sealed class ReplaceParameterFunctionValue : NativeFunctionValue1<FunctionValue, FunctionValue>
		{
			// Token: 0x06005B30 RID: 23344 RVA: 0x0013EB1A File Offset: 0x0013CD1A
			public ReplaceParameterFunctionValue(ReplaceDimensionsCubeValue cube)
				: base(TypeValue.Function, 1, "parameter", TypeValue.Function)
			{
				this.cube = cube;
			}

			// Token: 0x06005B31 RID: 23345 RVA: 0x0013EB39 File Offset: 0x0013CD39
			public override FunctionValue TypedInvoke(FunctionValue parameter)
			{
				return this.TypedInvoke(parameter.AsParameter());
			}

			// Token: 0x06005B32 RID: 23346 RVA: 0x0013EB48 File Offset: 0x0013CD48
			private FunctionValue TypedInvoke(ParameterValue parameter)
			{
				FunctionTypeValue asFunctionType = parameter.Type.AsFunctionType;
				string[] array = new string[asFunctionType.ParameterCount];
				TypeValue[] array2 = new TypeValue[asFunctionType.ParameterCount];
				for (int i = 0; i < asFunctionType.ParameterCount; i++)
				{
					array[i] = asFunctionType.ParameterName(i);
					array2[i] = asFunctionType.ParameterType(i);
				}
				return new ParameterValue(this.cube, parameter.Parameter, asFunctionType.Min, array, array2);
			}

			// Token: 0x040032DC RID: 13020
			private readonly ReplaceDimensionsCubeValue cube;
		}
	}
}
