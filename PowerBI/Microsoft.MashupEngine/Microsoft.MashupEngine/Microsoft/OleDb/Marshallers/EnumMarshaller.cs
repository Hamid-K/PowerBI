using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FCE RID: 8142
	public class EnumMarshaller<T> : IMarshaller<T>, IMarshaller where T : struct
	{
		// Token: 0x0600C6F7 RID: 50935 RVA: 0x0027A41C File Offset: 0x0027861C
		public EnumMarshaller()
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(int), "intValue");
			UnaryExpression unaryExpression = Expression.Convert(parameterExpression, typeof(T));
			this.fromInt = Expression.Lambda<Func<int, T>>(unaryExpression, new ParameterExpression[] { parameterExpression }).Compile();
			parameterExpression = Expression.Parameter(typeof(T), "TValue");
			unaryExpression = Expression.Convert(parameterExpression, typeof(int));
			this.toInt = Expression.Lambda<Func<T, int>>(unaryExpression, new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x0600C6F8 RID: 50936 RVA: 0x0027A4B4 File Offset: 0x002786B4
		public T GetManaged(IntPtr native)
		{
			int num = Marshal.ReadInt32(native);
			return this.fromInt(num);
		}

		// Token: 0x0600C6F9 RID: 50937 RVA: 0x0027A4D4 File Offset: 0x002786D4
		public void GetNative(T managed, IntPtr native)
		{
			Marshal.WriteInt32(native, this.toInt(managed));
		}

		// Token: 0x17003033 RID: 12339
		// (get) Token: 0x0600C6FA RID: 50938 RVA: 0x0000244F File Offset: 0x0000064F
		public int NativeSizeInBytes
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17003034 RID: 12340
		// (get) Token: 0x0600C6FB RID: 50939 RVA: 0x0000240C File Offset: 0x0000060C
		public VARTYPE Type
		{
			get
			{
				return VARTYPE.I4;
			}
		}

		// Token: 0x0600C6FC RID: 50940 RVA: 0x0000336E File Offset: 0x0000156E
		public void Cleanup(IntPtr native)
		{
		}

		// Token: 0x04006588 RID: 25992
		private readonly Func<int, T> fromInt;

		// Token: 0x04006589 RID: 25993
		private readonly Func<T, int> toInt;
	}
}
