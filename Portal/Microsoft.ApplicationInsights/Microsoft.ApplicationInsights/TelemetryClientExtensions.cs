using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights
{
	// Token: 0x02000025 RID: 37
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class TelemetryClientExtensions
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00007876 File Offset: 0x00005A76
		public static IOperationHolder<T> StartOperation<T>(this TelemetryClient telemetryClient, string operationName) where T : OperationTelemetry, new()
		{
			return telemetryClient.StartOperation(operationName, null, null);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007884 File Offset: 0x00005A84
		public static IOperationHolder<T> StartOperation<T>(this TelemetryClient telemetryClient, string operationName, string operationId, string parentOperationId = null) where T : OperationTelemetry, new()
		{
			if (telemetryClient == null)
			{
				throw new ArgumentNullException("telemetryClient");
			}
			T t = new T();
			if (string.IsNullOrEmpty(t.Name) && !string.IsNullOrEmpty(operationName))
			{
				t.Name = operationName;
			}
			if (string.IsNullOrEmpty(t.Context.Operation.Id) && !string.IsNullOrEmpty(operationId))
			{
				t.Context.Operation.Id = operationId;
			}
			if (string.IsNullOrEmpty(t.Context.Operation.ParentId) && !string.IsNullOrEmpty(parentOperationId))
			{
				t.Context.Operation.ParentId = parentOperationId;
			}
			return telemetryClient.StartOperation(t);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007948 File Offset: 0x00005B48
		public static IOperationHolder<T> StartOperation<T>(this TelemetryClient telemetryClient, T operationTelemetry) where T : OperationTelemetry
		{
			if (telemetryClient == null)
			{
				throw new ArgumentNullException("telemetryClient");
			}
			if (operationTelemetry == null)
			{
				throw new ArgumentNullException("operationTelemetry");
			}
			telemetryClient.Initialize(operationTelemetry);
			OperationContext telemetryContext = operationTelemetry.Context.Operation;
			if (string.IsNullOrEmpty(operationTelemetry.Id))
			{
				operationTelemetry.GenerateOperationId();
			}
			if (string.IsNullOrEmpty(telemetryContext.Id))
			{
				telemetryContext.Id = operationTelemetry.Id;
			}
			if (string.IsNullOrEmpty(telemetryContext.Name))
			{
				telemetryContext.Name = operationTelemetry.Name;
			}
			bool flag = ActivityExtensions.TryRun(delegate
			{
				Activity activity = Activity.Current;
				Activity activity2 = new Activity("Microsoft.ApplicationInsights.OperationContext");
				string text = telemetryContext.Name;
				if (string.IsNullOrEmpty(text))
				{
					text = ((activity != null) ? activity.GetOperationName() : null);
				}
				if (!string.IsNullOrEmpty(text))
				{
					activity2.SetOperationName(text);
				}
				if (activity == null)
				{
					activity2.SetParentId(telemetryContext.Id);
				}
				activity2.Start();
				operationTelemetry.Id = activity2.Id;
			});
			OperationHolder<T> operationHolder = new OperationHolder<T>(telemetryClient, operationTelemetry);
			if (!flag)
			{
				operationHolder.ParentContext = CallContextHelpers.GetCurrentOperationContext();
			}
			operationTelemetry.Start();
			if (!flag)
			{
				CallContextHelpers.SaveOperationContext(new OperationContextForCallContext
				{
					ParentOperationId = operationTelemetry.Id,
					RootOperationId = operationTelemetry.Context.Operation.Id,
					RootOperationName = operationTelemetry.Context.Operation.Name
				});
			}
			return operationHolder;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007AD4 File Offset: 0x00005CD4
		public static void StopOperation<T>(this TelemetryClient telemetryClient, IOperationHolder<T> operation) where T : OperationTelemetry
		{
			if (telemetryClient == null)
			{
				throw new ArgumentNullException("telemetryClient");
			}
			if (operation == null)
			{
				CoreEventSource.Log.OperationIsNullWarning("Incorrect");
				return;
			}
			operation.Dispose();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007B00 File Offset: 0x00005D00
		public static IOperationHolder<T> StartOperation<T>(this TelemetryClient telemetryClient, Activity activity) where T : OperationTelemetry, new()
		{
			if (telemetryClient == null)
			{
				throw new ArgumentNullException("telemetryClient");
			}
			if (activity == null)
			{
				throw new ArgumentNullException("activity");
			}
			activity.Start();
			T t = TelemetryClientExtensions.ActivityToTelemetry<T>(activity);
			telemetryClient.Initialize(t);
			t.Start();
			return new OperationHolder<T>(telemetryClient, t);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007B58 File Offset: 0x00005D58
		private static T ActivityToTelemetry<T>(Activity activity) where T : OperationTelemetry, new()
		{
			T t = new T();
			t.Name = activity.OperationName;
			T t2 = t;
			OperationContext operation = t2.Context.Operation;
			operation.Name = activity.GetOperationName();
			operation.Id = activity.RootId;
			operation.ParentId = activity.ParentId;
			t2.Id = activity.Id;
			foreach (KeyValuePair<string, string> keyValuePair in activity.Baggage)
			{
				if (!t2.Properties.ContainsKey(keyValuePair.Key))
				{
					t2.Properties.Add(keyValuePair);
				}
			}
			foreach (KeyValuePair<string, string> keyValuePair2 in activity.Tags)
			{
				if (!t2.Properties.ContainsKey(keyValuePair2.Key))
				{
					t2.Properties.Add(keyValuePair2);
				}
			}
			return t2;
		}

		// Token: 0x0400009A RID: 154
		private const string ChildActivityName = "Microsoft.ApplicationInsights.OperationContext";
	}
}
