using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x0200005A RID: 90
	public class OperationCorrelationTelemetryInitializer : ITelemetryInitializer
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000CFD8 File Offset: 0x0000B1D8
		public void Initialize(ITelemetry telemetryItem)
		{
			OperationContext itemContext = telemetryItem.Context.Operation;
			ISupportProperties telemetryProp = telemetryItem as ISupportProperties;
			if (!ActivityExtensions.TryRun(delegate
			{
				Activity activity = Activity.Current;
				if (activity != null)
				{
					if (string.IsNullOrEmpty(itemContext.Id))
					{
						itemContext.Id = activity.RootId;
						if (string.IsNullOrEmpty(itemContext.ParentId))
						{
							itemContext.ParentId = activity.Id;
						}
						foreach (KeyValuePair<string, string> keyValuePair2 in activity.Baggage)
						{
							if (telemetryProp != null && !telemetryProp.Properties.ContainsKey(keyValuePair2.Key))
							{
								telemetryProp.Properties.Add(keyValuePair2);
							}
						}
					}
					string operationName = activity.GetOperationName();
					if (string.IsNullOrEmpty(itemContext.Name) && !string.IsNullOrEmpty(operationName))
					{
						itemContext.Name = operationName;
					}
				}
			}) && (string.IsNullOrEmpty(itemContext.ParentId) || string.IsNullOrEmpty(itemContext.Id) || string.IsNullOrEmpty(itemContext.Name)))
			{
				OperationContextForCallContext currentOperationContext = CallContextHelpers.GetCurrentOperationContext();
				if (currentOperationContext != null)
				{
					if (string.IsNullOrEmpty(itemContext.ParentId) && !string.IsNullOrEmpty(currentOperationContext.ParentOperationId))
					{
						itemContext.ParentId = currentOperationContext.ParentOperationId;
					}
					if (string.IsNullOrEmpty(itemContext.Id) && !string.IsNullOrEmpty(currentOperationContext.RootOperationId))
					{
						itemContext.Id = currentOperationContext.RootOperationId;
					}
					if (string.IsNullOrEmpty(itemContext.Name) && !string.IsNullOrEmpty(currentOperationContext.RootOperationName))
					{
						itemContext.Name = currentOperationContext.RootOperationName;
					}
					if (currentOperationContext.CorrelationContext != null)
					{
						foreach (KeyValuePair<string, string> keyValuePair in currentOperationContext.CorrelationContext)
						{
							if (telemetryProp != null && !telemetryProp.Properties.ContainsKey(keyValuePair.Key))
							{
								telemetryProp.Properties.Add(keyValuePair);
							}
						}
					}
				}
			}
		}
	}
}
