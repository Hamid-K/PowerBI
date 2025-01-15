using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200008E RID: 142
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonDynamicContract : JsonContainerContract
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001D22E File Offset: 0x0001B42E
		public JsonPropertyCollection Properties { get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001D236 File Offset: 0x0001B436
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0001D23E File Offset: 0x0001B43E
		[Nullable(new byte[] { 2, 1, 1 })]
		public Func<string, string> PropertyNameResolver
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001D247 File Offset: 0x0001B447
		private static CallSite<Func<CallSite, object, object>> CreateCallSiteGetter(string name)
		{
			return CallSite<Func<CallSite, object, object>>.Create(new NoThrowGetBinderMember((GetMemberBinder)DynamicUtils.BinderWrapper.GetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001D268 File Offset: 0x0001B468
		[return: Nullable(new byte[] { 1, 1, 1, 1, 2, 1 })]
		private static CallSite<Func<CallSite, object, object, object>> CreateCallSiteSetter(string name)
		{
			return CallSite<Func<CallSite, object, object, object>>.Create(new NoThrowSetBinderMember((SetMemberBinder)DynamicUtils.BinderWrapper.SetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001D28C File Offset: 0x0001B48C
		public JsonDynamicContract(Type underlyingType)
		{
			Func<string, CallSite<Func<CallSite, object, object>>> func;
			if ((func = JsonDynamicContract.<>O.<0>__CreateCallSiteGetter) == null)
			{
				func = (JsonDynamicContract.<>O.<0>__CreateCallSiteGetter = new Func<string, CallSite<Func<CallSite, object, object>>>(JsonDynamicContract.CreateCallSiteGetter));
			}
			this._callSiteGetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>>(func);
			Func<string, CallSite<Func<CallSite, object, object, object>>> func2;
			if ((func2 = JsonDynamicContract.<>O.<1>__CreateCallSiteSetter) == null)
			{
				func2 = (JsonDynamicContract.<>O.<1>__CreateCallSiteSetter = new Func<string, CallSite<Func<CallSite, object, object, object>>>(JsonDynamicContract.CreateCallSiteSetter));
			}
			this._callSiteSetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>>(func2);
			base..ctor(underlyingType);
			this.ContractType = JsonContractType.Dynamic;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001D304 File Offset: 0x0001B504
		internal bool TryGetMember(IDynamicMetaObjectProvider dynamicProvider, string name, [Nullable(2)] out object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object>> callSite = this._callSiteGetters.Get(name);
			object obj = callSite.Target(callSite, dynamicProvider);
			if (obj != NoThrowExpressionVisitor.ErrorResult)
			{
				value = obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001D348 File Offset: 0x0001B548
		internal bool TrySetMember(IDynamicMetaObjectProvider dynamicProvider, string name, [Nullable(2)] object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object, object>> callSite = this._callSiteSetters.Get(name);
			return callSite.Target(callSite, dynamicProvider, value) != NoThrowExpressionVisitor.ErrorResult;
		}

		// Token: 0x0400028E RID: 654
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>> _callSiteGetters;

		// Token: 0x0400028F RID: 655
		[Nullable(new byte[] { 1, 1, 1, 1, 1, 1, 2, 1 })]
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>> _callSiteSetters;

		// Token: 0x0200022C RID: 556
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040009CE RID: 2510
			[Nullable(new byte[] { 0, 1, 1, 1, 1, 1, 1 })]
			public static Func<string, CallSite<Func<CallSite, object, object>>> <0>__CreateCallSiteGetter;

			// Token: 0x040009CF RID: 2511
			[Nullable(new byte[] { 0, 1, 1, 1, 1, 1, 2, 1 })]
			public static Func<string, CallSite<Func<CallSite, object, object, object>>> <1>__CreateCallSiteSetter;
		}
	}
}
