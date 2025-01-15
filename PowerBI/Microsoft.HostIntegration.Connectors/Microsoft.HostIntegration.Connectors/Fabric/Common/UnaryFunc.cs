using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000404 RID: 1028
	internal abstract class UnaryFunc : StandaloneFunc
	{
		// Token: 0x060023EC RID: 9196
		protected abstract object UnaryInvoke(object arg);

		// Token: 0x060023ED RID: 9197 RVA: 0x0006E2F7 File Offset: 0x0006C4F7
		public override object Invoke(object[] args)
		{
			if (args.Length != 1)
			{
				throw new ArgumentException("Invalid argument for UnaryFunc: " + base.GetType());
			}
			return this.UnaryInvoke(args[0]);
		}
	}
}
