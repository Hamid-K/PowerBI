using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001670 RID: 5744
	public abstract class TimeSpecificFunctionValue0<TResult> : NativeFunctionValue0<TResult> where TResult : Value
	{
		// Token: 0x06009143 RID: 37187 RVA: 0x001E2D4B File Offset: 0x001E0F4B
		protected TimeSpecificFunctionValue0(IEngineHost engineHost, TypeValue returnType)
			: base(returnType)
		{
			this.engineHost = engineHost;
			this.currentTimeService = engineHost.QueryService<ICurrentTimeService>();
			this.timeZone = engineHost.QueryService<ITimeZoneService>().DefaultTimeZone;
		}

		// Token: 0x170025FE RID: 9726
		// (get) Token: 0x06009144 RID: 37188 RVA: 0x0005DED2 File Offset: 0x0005C0D2
		public override IFunctionIdentity FunctionIdentity
		{
			get
			{
				return new FunctionTypeIdentity(base.GetType());
			}
		}

		// Token: 0x170025FF RID: 9727
		// (get) Token: 0x06009145 RID: 37189 RVA: 0x001E2D78 File Offset: 0x001E0F78
		protected IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x17002600 RID: 9728
		// (get) Token: 0x06009146 RID: 37190 RVA: 0x001E2D80 File Offset: 0x001E0F80
		protected DateTime FixedDateTimeUtcNow
		{
			get
			{
				return this.FixedDateTimeOffsetUtcNow.DateTime;
			}
		}

		// Token: 0x17002601 RID: 9729
		// (get) Token: 0x06009147 RID: 37191 RVA: 0x001E2D9B File Offset: 0x001E0F9B
		protected DateTimeOffset FixedDateTimeOffsetUtcNow
		{
			get
			{
				return this.currentTimeService.FixedUtcNow;
			}
		}

		// Token: 0x17002602 RID: 9730
		// (get) Token: 0x06009148 RID: 37192 RVA: 0x001E2DB0 File Offset: 0x001E0FB0
		protected DateTime FixedDateTimeLocalNow
		{
			get
			{
				return this.FixedDateTimeOffsetLocalNow.LocalDateTime;
			}
		}

		// Token: 0x17002603 RID: 9731
		// (get) Token: 0x06009149 RID: 37193 RVA: 0x001E2DCB File Offset: 0x001E0FCB
		protected DateTimeOffset FixedDateTimeOffsetLocalNow
		{
			get
			{
				return this.currentTimeService.FixedUtcNow.AdjustForTimeZone(this.timeZone);
			}
		}

		// Token: 0x17002604 RID: 9732
		// (get) Token: 0x0600914A RID: 37194 RVA: 0x001E2DE8 File Offset: 0x001E0FE8
		protected DateTime DateTimeUtcNow
		{
			get
			{
				return this.DateTimeOffsetUtcNow.DateTime;
			}
		}

		// Token: 0x17002605 RID: 9733
		// (get) Token: 0x0600914B RID: 37195 RVA: 0x001E2E03 File Offset: 0x001E1003
		protected DateTimeOffset DateTimeOffsetUtcNow
		{
			get
			{
				return this.currentTimeService.UtcNow;
			}
		}

		// Token: 0x17002606 RID: 9734
		// (get) Token: 0x0600914C RID: 37196 RVA: 0x001E2E18 File Offset: 0x001E1018
		protected DateTime DateTimeLocalNow
		{
			get
			{
				return this.DateTimeOffsetLocalNow.LocalDateTime;
			}
		}

		// Token: 0x17002607 RID: 9735
		// (get) Token: 0x0600914D RID: 37197 RVA: 0x001E2E33 File Offset: 0x001E1033
		protected DateTimeOffset DateTimeOffsetLocalNow
		{
			get
			{
				return this.currentTimeService.UtcNow.AdjustForTimeZone(this.timeZone);
			}
		}

		// Token: 0x04004E03 RID: 19971
		private readonly IEngineHost engineHost;

		// Token: 0x04004E04 RID: 19972
		private readonly ICurrentTimeService currentTimeService;

		// Token: 0x04004E05 RID: 19973
		private readonly ITimeZone timeZone;
	}
}
