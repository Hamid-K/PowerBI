using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001F1 RID: 497
	public static class ExtendedAssembly
	{
		// Token: 0x06000D14 RID: 3348 RVA: 0x0002DF65 File Offset: 0x0002C165
		public static Assembly GetExecutingAssembly([NotNull] Type typeInExecutingAssembly)
		{
			Ensure.ArgNotNull<Type>(typeInExecutingAssembly, "typeInExecutingAssembly");
			return typeInExecutingAssembly.Assembly;
		}
	}
}
