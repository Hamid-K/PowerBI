using System;
using System.Reflection;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004B7 RID: 1207
	public class MethodReference<TDelegate> : MethodReference
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0005177B File Offset: 0x0004F97B
		public TDelegate Invoke { get; }

		// Token: 0x06001B0C RID: 6924 RVA: 0x00051783 File Offset: 0x0004F983
		internal MethodReference(MethodInfo methodInfo, TDelegate @delegate, bool handleParams)
			: base(methodInfo, handleParams)
		{
			this.Invoke = @delegate;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00051794 File Offset: 0x0004F994
		public static implicit operator MethodInfo(MethodReference<TDelegate> mref)
		{
			MethodInfo methodInfo;
			if ((methodInfo = mref._methodInfo) == null)
			{
				Delegate @delegate = mref.Invoke as Delegate;
				if (@delegate == null)
				{
					return null;
				}
				methodInfo = @delegate.GetMethodInfo();
			}
			return methodInfo;
		}
	}
}
