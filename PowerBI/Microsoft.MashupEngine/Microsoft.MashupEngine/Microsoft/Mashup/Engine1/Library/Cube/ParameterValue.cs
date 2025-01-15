using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D3A RID: 3386
	internal class ParameterValue : NativeFunctionValueN
	{
		// Token: 0x06005B00 RID: 23296 RVA: 0x0013DBE0 File Offset: 0x0013BDE0
		public ParameterValue(CubeValue cube, IdentifierCubeExpression identifier, int min, string[] parameters, TypeValue[] types)
			: base(min, parameters)
		{
			this.cube = cube;
			this.identifier = identifier;
			this.types = types;
		}

		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x06005B01 RID: 23297 RVA: 0x0013DC01 File Offset: 0x0013BE01
		public IdentifierCubeExpression Parameter
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06005B02 RID: 23298 RVA: 0x0013DC09 File Offset: 0x0013BE09
		public override bool TryGetAs<T>(out T contract)
		{
			if (typeof(T) == typeof(ParameterValue))
			{
				contract = (T)((object)this);
				return true;
			}
			return base.TryGetAs<T>(out contract);
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x0013DC3B File Offset: 0x0013BE3B
		protected override TypeValue ParamType(int index)
		{
			return this.types[index];
		}

		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x0013DC45 File Offset: 0x0013BE45
		protected override TypeValue ReturnType
		{
			get
			{
				return this.cube.Type;
			}
		}

		// Token: 0x06005B05 RID: 23301 RVA: 0x0013DC54 File Offset: 0x0013BE54
		protected override Value InvokeN(Value[] args)
		{
			Value[] array = new Value[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = args[i].As<Value>(this.types[i]);
			}
			return this.cube.ApplyParameter(this, array);
		}

		// Token: 0x040032CF RID: 13007
		private readonly CubeValue cube;

		// Token: 0x040032D0 RID: 13008
		private readonly IdentifierCubeExpression identifier;

		// Token: 0x040032D1 RID: 13009
		private readonly TypeValue[] types;
	}
}
