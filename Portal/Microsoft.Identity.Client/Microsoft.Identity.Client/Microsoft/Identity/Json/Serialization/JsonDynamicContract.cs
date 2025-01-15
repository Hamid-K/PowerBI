using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200008D RID: 141
	internal class JsonDynamicContract : JsonContainerContract
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001CC5A File Offset: 0x0001AE5A
		public JsonPropertyCollection Properties { get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001CC62 File Offset: 0x0001AE62
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x0001CC6A File Offset: 0x0001AE6A
		[Nullable(new byte[] { 2, 0, 0 })]
		public Func<string, string> PropertyNameResolver
		{
			[return: Nullable(new byte[] { 2, 0, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0, 0 })]
			set;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001CC73 File Offset: 0x0001AE73
		private static CallSite<Func<CallSite, object, object>> CreateCallSiteGetter(string name)
		{
			return CallSite<Func<CallSite, object, object>>.Create(new NoThrowGetBinderMember((GetMemberBinder)DynamicUtils.BinderWrapper.GetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001CC94 File Offset: 0x0001AE94
		[return: Nullable(new byte[] { 0, 0, 0, 0, 2, 0 })]
		private static CallSite<Func<CallSite, object, object, object>> CreateCallSiteSetter(string name)
		{
			return CallSite<Func<CallSite, object, object, object>>.Create(new NoThrowSetBinderMember((SetMemberBinder)DynamicUtils.BinderWrapper.SetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001CCB8 File Offset: 0x0001AEB8
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

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001CD30 File Offset: 0x0001AF30
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

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001CD74 File Offset: 0x0001AF74
		internal bool TrySetMember(IDynamicMetaObjectProvider dynamicProvider, string name, [Nullable(2)] object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object, object>> callSite = this._callSiteSetters.Get(name);
			return callSite.Target(callSite, dynamicProvider, value) != NoThrowExpressionVisitor.ErrorResult;
		}

		// Token: 0x04000273 RID: 627
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>> _callSiteGetters;

		// Token: 0x04000274 RID: 628
		[Nullable(new byte[] { 0, 0, 0, 0, 0, 0, 2, 0 })]
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>> _callSiteSetters;

		// Token: 0x0200037C RID: 892
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F4C RID: 3916
			public static Func<string, CallSite<Func<CallSite, object, object>>> <0>__CreateCallSiteGetter;

			// Token: 0x04000F4D RID: 3917
			[Nullable(new byte[] { 0, 0, 0, 0, 0, 0, 2, 0 })]
			public static Func<string, CallSite<Func<CallSite, object, object, object>>> <1>__CreateCallSiteSetter;
		}
	}
}
