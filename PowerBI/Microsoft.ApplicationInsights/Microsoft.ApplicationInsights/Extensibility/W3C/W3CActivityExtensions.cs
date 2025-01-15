using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Microsoft.ApplicationInsights.Extensibility.W3C
{
	// Token: 0x02000061 RID: 97
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class W3CActivityExtensions
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000DD74 File Offset: 0x0000BF74
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static Activity GenerateW3CContext(this Activity activity)
		{
			activity.SetVersion("00");
			activity.SetSampled("02");
			activity.SetSpanId(W3CUtilities.GenerateSpanId());
			activity.SetTraceId((activity.RootId != null && W3CActivityExtensions.TraceIdRegex.IsMatch(activity.RootId)) ? activity.RootId : W3CUtilities.GenerateTraceId());
			return activity;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool IsW3CActivity(this Activity activity)
		{
			if (activity != null)
			{
				return activity.Tags.Any((KeyValuePair<string, string> t) => t.Key == "w3c_traceId");
			}
			return false;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000DE04 File Offset: 0x0000C004
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static Activity UpdateContextOnActivity(this Activity activity)
		{
			if (activity != null)
			{
				if (!activity.Tags.Any((KeyValuePair<string, string> t) => t.Key == "w3c_traceId"))
				{
					activity.Parent.UpdateContextOnActivity();
					return activity.UpdateContextFromParent();
				}
			}
			return activity;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000DE54 File Offset: 0x0000C054
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string GetTraceparent(this Activity activity)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			foreach (KeyValuePair<string, string> keyValuePair in activity.Tags)
			{
				string key = keyValuePair.Key;
				if (!(key == "w3c_traceId"))
				{
					if (!(key == "w3c_spanId"))
					{
						if (!(key == "w3c_version"))
						{
							if (key == "w3c_sampled")
							{
								text4 = keyValuePair.Value;
							}
						}
						else
						{
							text = keyValuePair.Value;
						}
					}
					else
					{
						text3 = keyValuePair.Value;
					}
				}
				else
				{
					text2 = keyValuePair.Value;
				}
			}
			if (text2 == null || text3 == null || text == null || text4 == null)
			{
				return null;
			}
			return string.Join("-", new string[] { text, text2, text3, text4 });
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000DF40 File Offset: 0x0000C140
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void SetTraceparent(this Activity activity, string value)
		{
			if (activity.IsW3CActivity())
			{
				return;
			}
			activity.SetVersion("00");
			string text = null;
			string text2 = null;
			string text3 = null;
			bool flag = false;
			string[] array = ((value != null) ? value.Split(new char[] { '-' }) : null);
			if (array != null && array.Length == 4)
			{
				text = array[1];
				text2 = array[2];
				text3 = array[3];
				flag = W3CActivityExtensions.TraceIdRegex.IsMatch(text) && W3CActivityExtensions.SpanIdRegex.IsMatch(text2);
			}
			if (flag)
			{
				byte b;
				byte.TryParse(text3, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out b);
				if ((b & 1) == 1)
				{
					activity.SetSampled("03");
				}
				else
				{
					activity.SetSampled("02");
				}
				activity.SetParentSpanId(text2);
				activity.SetSpanId(W3CUtilities.GenerateSpanId());
				activity.SetTraceId(text);
			}
			else
			{
				activity.SetSampled("02");
				activity.SetSpanId(W3CUtilities.GenerateSpanId());
				activity.SetTraceId(W3CUtilities.GenerateTraceId());
			}
			if (activity.Id == null)
			{
				activity.SetParentId(string.Concat(new string[]
				{
					"|",
					activity.GetTraceId(),
					".",
					activity.GetParentSpanId(),
					"."
				}));
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000E070 File Offset: 0x0000C270
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string GetTracestate(this Activity activity)
		{
			return activity.Tags.FirstOrDefault((KeyValuePair<string, string> t) => t.Key == "w3c_tracestate").Value;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000E0AF File Offset: 0x0000C2AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void SetTracestate(this Activity activity, string value)
		{
			activity.AddTag("w3c_tracestate", value);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string GetTraceId(this Activity activity)
		{
			return activity.Tags.FirstOrDefault((KeyValuePair<string, string> t) => t.Key == "w3c_traceId").Value;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000E100 File Offset: 0x0000C300
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string GetSpanId(this Activity activity)
		{
			return activity.Tags.FirstOrDefault((KeyValuePair<string, string> t) => t.Key == "w3c_spanId").Value;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000E140 File Offset: 0x0000C340
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string GetParentSpanId(this Activity activity)
		{
			return activity.Tags.FirstOrDefault((KeyValuePair<string, string> t) => t.Key == "w3c_parentSpanId").Value;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000E180 File Offset: 0x0000C380
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "This method has different code for Net45/NetCore")]
		public static void UpdateTelemetry(this Activity activity, ITelemetry telemetry, bool forceUpdate)
		{
			if (activity == null)
			{
				return;
			}
			activity.UpdateContextOnActivity();
			OperationTelemetry operationTelemetry = telemetry as OperationTelemetry;
			bool flag = operationTelemetry != null;
			if (flag)
			{
				DependencyTelemetry dependencyTelemetry;
				flag &= (dependencyTelemetry = operationTelemetry as DependencyTelemetry) == null || !(dependencyTelemetry.Type == "SQL") || !dependencyTelemetry.Context.GetInternalContext().SdkVersion.StartsWith("rdddsc", StringComparison.Ordinal);
			}
			string text = null;
			string text2 = null;
			foreach (KeyValuePair<string, string> keyValuePair in activity.Tags)
			{
				string key = keyValuePair.Key;
				if (!(key == "w3c_traceId"))
				{
					if (!(key == "w3c_spanId"))
					{
						if (!(key == "w3c_parentSpanId"))
						{
							if (key == "w3c_tracestate")
							{
								OperationTelemetry operationTelemetry2;
								if ((operationTelemetry2 = telemetry as OperationTelemetry) != null)
								{
									operationTelemetry2.Properties["w3c_tracestate"] = keyValuePair.Value;
								}
							}
						}
						else
						{
							text2 = keyValuePair.Value;
						}
					}
					else
					{
						text = keyValuePair.Value;
					}
				}
				else
				{
					telemetry.Context.Operation.Id = keyValuePair.Value;
				}
			}
			if (flag)
			{
				if (!forceUpdate && W3CActivityExtensions.IsValidTelemetryId(operationTelemetry.Id, telemetry.Context.Operation.Id))
				{
					return;
				}
				operationTelemetry.Id = W3CActivityExtensions.FormatRequestId(telemetry.Context.Operation.Id, text);
				if (text2 != null)
				{
					telemetry.Context.Operation.ParentId = W3CActivityExtensions.FormatRequestId(telemetry.Context.Operation.Id, text2);
				}
			}
			else
			{
				telemetry.Context.Operation.ParentId = W3CActivityExtensions.FormatRequestId(telemetry.Context.Operation.Id, text);
			}
			if (operationTelemetry != null)
			{
				if (operationTelemetry.Context.Operation.Id != activity.RootId)
				{
					operationTelemetry.Properties["ai_legacyRootId"] = activity.RootId;
				}
				if (operationTelemetry.Id != activity.Id)
				{
					operationTelemetry.Properties["ai_legacyRequestId"] = activity.Id;
				}
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal static void SetParentSpanId(this Activity activity, string value)
		{
			activity.AddTag("w3c_parentSpanId", value);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000E3C7 File Offset: 0x0000C5C7
		private static void SetTraceId(this Activity activity, string value)
		{
			activity.AddTag("w3c_traceId", value);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000E3D6 File Offset: 0x0000C5D6
		private static void SetSpanId(this Activity activity, string value)
		{
			activity.AddTag("w3c_spanId", value);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000E3E5 File Offset: 0x0000C5E5
		private static void SetVersion(this Activity activity, string value)
		{
			activity.AddTag("w3c_version", value);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		private static void SetSampled(this Activity activity, string value)
		{
			activity.AddTag("w3c_sampled", value);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000E404 File Offset: 0x0000C604
		private static Activity UpdateContextFromParent(this Activity activity)
		{
			if (activity != null)
			{
				if (activity.Tags.All((KeyValuePair<string, string> t) => t.Key != "w3c_traceId"))
				{
					if (activity.Parent == null)
					{
						activity.GenerateW3CContext();
					}
					else
					{
						foreach (KeyValuePair<string, string> keyValuePair in activity.Parent.Tags)
						{
							string key = keyValuePair.Key;
							if (!(key == "w3c_traceId"))
							{
								if (!(key == "w3c_spanId"))
								{
									if (!(key == "w3c_version"))
									{
										if (!(key == "w3c_sampled"))
										{
											if (key == "w3c_tracestate")
											{
												activity.SetTracestate(keyValuePair.Value);
											}
										}
										else
										{
											activity.SetSampled(keyValuePair.Value);
										}
									}
									else
									{
										activity.SetVersion(keyValuePair.Value);
									}
								}
								else
								{
									activity.SetParentSpanId(keyValuePair.Value);
									activity.SetSpanId(W3CUtilities.GenerateSpanId());
								}
							}
							else
							{
								activity.SetTraceId(keyValuePair.Value);
							}
						}
					}
				}
			}
			return activity;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000E540 File Offset: 0x0000C740
		private static bool IsValidTelemetryId(string id, string operationId)
		{
			return id.Length == 51 && id[0] == '|' && id[33] == '.' && id.IndexOf('.', 34) == 50 && id.IndexOf(operationId, 1, 33, StringComparison.Ordinal) == 1;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000E58C File Offset: 0x0000C78C
		private static string FormatRequestId(string traceId, string spanId)
		{
			return string.Concat(new string[] { "|", traceId, ".", spanId, "." });
		}

		// Token: 0x04000140 RID: 320
		private const string RddDiagnosticSourcePrefix = "rdddsc";

		// Token: 0x04000141 RID: 321
		private const string SqlRemoteDependencyType = "SQL";

		// Token: 0x04000142 RID: 322
		private static readonly Regex TraceIdRegex = new Regex("^[a-f0-9]{32}$", RegexOptions.Compiled);

		// Token: 0x04000143 RID: 323
		private static readonly Regex SpanIdRegex = new Regex("^[a-f0-9]{16}$", RegexOptions.Compiled);
	}
}
