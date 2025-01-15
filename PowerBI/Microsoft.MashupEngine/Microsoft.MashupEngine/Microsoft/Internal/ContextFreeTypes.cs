using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Internal
{
	// Token: 0x0200018E RID: 398
	internal static class ContextFreeTypes
	{
		// Token: 0x0400049E RID: 1182
		public static readonly HashSet<Type> Types = new HashSet<Type>
		{
			typeof(bool),
			typeof(DateTime),
			typeof(string),
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(double),
			typeof(decimal),
			typeof(DBNull),
			typeof(byte),
			typeof(float),
			typeof(sbyte),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(TimeSpan),
			typeof(DateTimeOffset),
			typeof(Guid),
			typeof(byte[]),
			typeof(Type),
			typeof(DataTable)
		};
	}
}
