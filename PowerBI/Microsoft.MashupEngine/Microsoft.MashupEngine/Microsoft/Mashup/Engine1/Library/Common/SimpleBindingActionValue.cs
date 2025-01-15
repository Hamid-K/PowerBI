using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001126 RID: 4390
	internal class SimpleBindingActionValue : ActionValue
	{
		// Token: 0x060072C3 RID: 29379 RVA: 0x0018A5B8 File Offset: 0x001887B8
		public SimpleBindingActionValue(Func<FunctionValue, ActionValue> bind)
		{
			this.bind = bind;
		}

		// Token: 0x1700201E RID: 8222
		// (get) Token: 0x060072C4 RID: 29380 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanBind
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060072C5 RID: 29381 RVA: 0x0018A5C8 File Offset: 0x001887C8
		public override bool TryBind(FunctionValue binding, out ActionValue action)
		{
			FunctionValue functionValue;
			FunctionValue functionValue2;
			if (SimpleActionBinding.TryGetSimpleActionBinding(binding, out functionValue, out functionValue2))
			{
				action = this.bind(functionValue).Bind(functionValue2);
				return true;
			}
			action = null;
			return false;
		}

		// Token: 0x060072C6 RID: 29382 RVA: 0x0018A5FB File Offset: 0x001887FB
		public override Value Execute()
		{
			return this.bind(SimpleActionBinding.ReturnResult).Execute();
		}

		// Token: 0x04003F44 RID: 16196
		private readonly Func<FunctionValue, ActionValue> bind;
	}
}
