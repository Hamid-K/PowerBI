using System;
using System.Reflection;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006F8 RID: 1784
	public class OperationContext : ApplicationInsightsTypeImplement<OperationContext>
	{
		// Token: 0x060038C0 RID: 14528 RVA: 0x000BE503 File Offset: 0x000BC703
		static OperationContext()
		{
			ApplicationInsightsTypeImplement<OperationContext>.Type = TelemetryClient.Assembly.GetType("Microsoft.ApplicationInsights.Extensibility.Implementation.OperationContext");
			OperationContext._idInfo = ApplicationInsightsTypeImplement<OperationContext>.Type.GetProperty("Id");
			OperationContext._parentIdInfo = ApplicationInsightsTypeImplement<OperationContext>.Type.GetProperty("ParentId");
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000BE541 File Offset: 0x000BC741
		internal OperationContext(object value)
		{
			if (value != null)
			{
				base.Value = value.GetType().GetProperty("Operation").GetValue(value);
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x000BE568 File Offset: 0x000BC768
		// (set) Token: 0x060038C3 RID: 14531 RVA: 0x000BE57F File Offset: 0x000BC77F
		public string Id
		{
			get
			{
				return OperationContext._idInfo.GetValue(base.Value) as string;
			}
			set
			{
				OperationContext._idInfo.SetValue(base.Value, value);
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x000BE592 File Offset: 0x000BC792
		// (set) Token: 0x060038C5 RID: 14533 RVA: 0x000BE5A9 File Offset: 0x000BC7A9
		public string ParentId
		{
			get
			{
				return OperationContext._parentIdInfo.GetValue(base.Value) as string;
			}
			set
			{
				OperationContext._parentIdInfo.SetValue(base.Value, value);
			}
		}

		// Token: 0x040020F0 RID: 8432
		private static PropertyInfo _idInfo;

		// Token: 0x040020F1 RID: 8433
		private static PropertyInfo _parentIdInfo;
	}
}
