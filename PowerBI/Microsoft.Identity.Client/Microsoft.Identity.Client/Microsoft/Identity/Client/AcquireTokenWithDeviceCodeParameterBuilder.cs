using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000125 RID: 293
	public sealed class AcquireTokenWithDeviceCodeParameterBuilder : AbstractPublicClientAcquireTokenParameterBuilder<AcquireTokenWithDeviceCodeParameterBuilder>
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00038083 File Offset: 0x00036283
		private AcquireTokenWithDeviceCodeParameters Parameters { get; } = new AcquireTokenWithDeviceCodeParameters();

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003808B File Offset: 0x0003628B
		internal AcquireTokenWithDeviceCodeParameterBuilder(IPublicClientApplicationExecutor publicClientApplicationExecutor)
			: base(publicClientApplicationExecutor)
		{
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003809F File Offset: 0x0003629F
		internal static AcquireTokenWithDeviceCodeParameterBuilder Create(IPublicClientApplicationExecutor publicClientApplicationExecutor, IEnumerable<string> scopes, Func<DeviceCodeResult, Task> deviceCodeResultCallback)
		{
			return new AcquireTokenWithDeviceCodeParameterBuilder(publicClientApplicationExecutor).WithScopes(scopes).WithDeviceCodeResultCallback(deviceCodeResultCallback);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x000380B3 File Offset: 0x000362B3
		public AcquireTokenWithDeviceCodeParameterBuilder WithDeviceCodeResultCallback(Func<DeviceCodeResult, Task> deviceCodeResultCallback)
		{
			this.Parameters.DeviceCodeResultCallback = deviceCodeResultCallback;
			return this;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000380C2 File Offset: 0x000362C2
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			return base.PublicClientApplicationExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000380DC File Offset: 0x000362DC
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			return ApiEvent.ApiIds.AcquireTokenByDeviceCode;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x000380E3 File Offset: 0x000362E3
		protected override void Validate()
		{
			base.Validate();
			if (this.Parameters.DeviceCodeResultCallback == null)
			{
				throw new ArgumentNullException("DeviceCodeResultCallback", "A deviceCodeResultCallback must be provided for Device Code authentication to work properly");
			}
		}
	}
}
