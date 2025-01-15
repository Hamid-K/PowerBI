using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000405 RID: 1029
	internal abstract class BinaryFunc : StandaloneFunc
	{
		// Token: 0x060023EF RID: 9199
		protected abstract object InvokeBinary(object arg1, object arg2);

		// Token: 0x060023F0 RID: 9200 RVA: 0x0006E326 File Offset: 0x0006C526
		public override object Invoke(object[] args)
		{
			if (args.Length != 2)
			{
				throw new ArgumentException("Invalid argument for BinaryFunc: " + base.GetType());
			}
			return this.InvokeBinary(args[0], args[1]);
		}
	}
}
