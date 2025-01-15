using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000050 RID: 80
	internal class DynamicProxy<T>
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x00013DF6 File Offset: 0x00011FF6
		public virtual IEnumerable<string> GetDynamicMemberNames(T instance)
		{
			return CollectionUtils.ArrayEmpty<string>();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00013DFD File Offset: 0x00011FFD
		public virtual bool TryBinaryOperation(T instance, BinaryOperationBinder binder, object arg, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00013E04 File Offset: 0x00012004
		public virtual bool TryConvert(T instance, ConvertBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00013E0A File Offset: 0x0001200A
		public virtual bool TryCreateInstance(T instance, CreateInstanceBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00013E11 File Offset: 0x00012011
		public virtual bool TryDeleteIndex(T instance, DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013E14 File Offset: 0x00012014
		public virtual bool TryDeleteMember(T instance, DeleteMemberBinder binder)
		{
			return false;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013E17 File Offset: 0x00012017
		public virtual bool TryGetIndex(T instance, GetIndexBinder binder, object[] indexes, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013E1E File Offset: 0x0001201E
		public virtual bool TryGetMember(T instance, GetMemberBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00013E24 File Offset: 0x00012024
		public virtual bool TryInvoke(T instance, InvokeBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00013E2B File Offset: 0x0001202B
		public virtual bool TryInvokeMember(T instance, InvokeMemberBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00013E32 File Offset: 0x00012032
		public virtual bool TrySetIndex(T instance, SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00013E35 File Offset: 0x00012035
		public virtual bool TrySetMember(T instance, SetMemberBinder binder, object value)
		{
			return false;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00013E38 File Offset: 0x00012038
		public virtual bool TryUnaryOperation(T instance, UnaryOperationBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}
	}
}
