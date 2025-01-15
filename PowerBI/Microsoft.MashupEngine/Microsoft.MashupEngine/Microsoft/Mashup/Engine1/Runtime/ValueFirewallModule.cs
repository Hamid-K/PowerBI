using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016A1 RID: 5793
	internal class ValueFirewallModule : Module
	{
		// Token: 0x170026D5 RID: 9941
		// (get) Token: 0x06009350 RID: 37712 RVA: 0x001E6ED9 File Offset: 0x001E50D9
		public override string Name
		{
			get
			{
				return "Firewall";
			}
		}

		// Token: 0x170026D6 RID: 9942
		// (get) Token: 0x06009351 RID: 37713 RVA: 0x001E6EE0 File Offset: 0x001E50E0
		public override Keys ExportKeys
		{
			get
			{
				return ValueFirewallModule.keys;
			}
		}

		// Token: 0x06009352 RID: 37714 RVA: 0x001E6EE8 File Offset: 0x001E50E8
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(ValueFirewallModule.keys, (int index) => new ValueFirewallModule.FirewallFunctionValue(host));
		}

		// Token: 0x04004EA5 RID: 20133
		private static readonly Keys keys = Keys.New("Value.Firewall");

		// Token: 0x020016A2 RID: 5794
		private class FirewallFunctionValue : NativeFunctionValue1<Value, TextValue>
		{
			// Token: 0x06009355 RID: 37717 RVA: 0x001E6F29 File Offset: 0x001E5129
			public FirewallFunctionValue(IEngineHost host)
				: base(TypeValue.Any, "key", TypeValue.Text)
			{
				this.firewallService = host.QueryService<IFirewallService>();
			}

			// Token: 0x06009356 RID: 37718 RVA: 0x001E6F4C File Offset: 0x001E514C
			public override Value TypedInvoke(TextValue key)
			{
				return (Value)this.firewallService.GetValue(key.String);
			}

			// Token: 0x04004EA6 RID: 20134
			private IFirewallService firewallService;
		}
	}
}
