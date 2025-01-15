using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D38 RID: 3384
	internal class MeasureValue : NativeFunctionValue1<Value, Value>
	{
		// Token: 0x06005AF9 RID: 23289 RVA: 0x0013DB82 File Offset: 0x0013BD82
		public MeasureValue(IdentifierCubeExpression measure, TypeValue returnType)
			: base(returnType, "context", TypeValue.Any)
		{
			this.measure = measure;
		}

		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x06005AFA RID: 23290 RVA: 0x0013DB9C File Offset: 0x0013BD9C
		public IdentifierCubeExpression Measure
		{
			get
			{
				return this.measure;
			}
		}

		// Token: 0x06005AFB RID: 23291 RVA: 0x0013B406 File Offset: 0x00139606
		public override Value TypedInvoke(Value context)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.Cube_QueryNotSupported, null, null);
		}

		// Token: 0x040032CB RID: 13003
		public static readonly MeasureValue Count = new MeasureValue(IdentifierCubeExpression.NewUnique(), TypeValue.Number);

		// Token: 0x040032CC RID: 13004
		private readonly IdentifierCubeExpression measure;
	}
}
