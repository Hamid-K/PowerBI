using System;
using System.Data.Entity.Core;
using System.Security;
using System.Threading;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200007D RID: 125
	internal static class ExceptionExtensions
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		public static bool IsCatchableExceptionType(this Exception e)
		{
			Type type = e.GetType();
			return type != typeof(StackOverflowException) && type != typeof(OutOfMemoryException) && type != typeof(ThreadAbortException) && type != typeof(NullReferenceException) && type != typeof(AccessViolationException) && !typeof(SecurityException).IsAssignableFrom(type);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public static bool IsCatchableEntityExceptionType(this Exception e)
		{
			Type type = e.GetType();
			return e.IsCatchableExceptionType() && type != typeof(EntityCommandExecutionException) && type != typeof(EntityCommandCompilationException) && type != typeof(EntitySqlException);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000FD42 File Offset: 0x0000DF42
		public static bool RequiresContext(this Exception e)
		{
			return e.IsCatchableExceptionType() && !(e is UpdateException) && !(e is ProviderIncompatibleException);
		}
	}
}
