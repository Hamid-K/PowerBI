using System;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D4 RID: 212
	[LayoutRenderer("machinename")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class MachineNameLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x00020F18 File Offset: 0x0001F118
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			try
			{
				this._machineName = EnvironmentHelper.GetMachineName();
				if (string.IsNullOrEmpty(this._machineName))
				{
					InternalLogger.Info("MachineName is not available.");
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error getting machine name.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
				this._machineName = string.Empty;
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00020F80 File Offset: 0x0001F180
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this._machineName);
		}

		// Token: 0x04000340 RID: 832
		private string _machineName;
	}
}
