using System;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001123 RID: 4387
	internal sealed class ReturnTypedTableFromCountFunctionValue : NativeFunctionValue1
	{
		// Token: 0x060072B6 RID: 29366 RVA: 0x0018A28C File Offset: 0x0018848C
		public ReturnTypedTableFromCountFunctionValue(TableTypeValue type)
			: base("count")
		{
			this.type = type;
		}

		// Token: 0x060072B7 RID: 29367 RVA: 0x0018A2A0 File Offset: 0x001884A0
		public override Value Invoke(Value count)
		{
			return ActionModule.Action.Return.Invoke(new CountAndTypeTableValue(count.AsNumber.AsInteger64, this.type));
		}

		// Token: 0x04003F3D RID: 16189
		private readonly TableTypeValue type;
	}
}
