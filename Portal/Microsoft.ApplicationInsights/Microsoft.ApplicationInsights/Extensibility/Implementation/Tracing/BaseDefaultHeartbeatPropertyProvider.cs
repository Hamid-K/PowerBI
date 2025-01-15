using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000A7 RID: 167
	internal class BaseDefaultHeartbeatPropertyProvider : IHeartbeatDefaultPayloadProvider
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x000151FF File Offset: 0x000133FF
		public string Name
		{
			get
			{
				return "Base";
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00015206 File Offset: 0x00013406
		public bool IsKeyword(string keyword)
		{
			return this.DefaultFields.Contains(keyword, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001521C File Offset: 0x0001341C
		public Task<bool> SetDefaultPayload(IEnumerable<string> disabledFields, IHeartbeatProvider provider)
		{
			bool flag = false;
			foreach (string text in this.DefaultFields.Except(disabledFields))
			{
				try
				{
					if (!(text == "runtimeFramework"))
					{
						if (!(text == "baseSdkTargetFramework"))
						{
							if (!(text == "osType"))
							{
								if (!(text == "processSessionId"))
								{
									provider.AddHeartbeatProperty(text, true, "UNDEFINED", true);
								}
								else
								{
									provider.AddHeartbeatProperty(text, true, BaseDefaultHeartbeatPropertyProvider.GetProcessSessionId(), true);
									flag = true;
								}
							}
							else
							{
								provider.AddHeartbeatProperty(text, true, BaseDefaultHeartbeatPropertyProvider.GetRuntimeOsType(), true);
								flag = true;
							}
						}
						else
						{
							provider.AddHeartbeatProperty(text, true, BaseDefaultHeartbeatPropertyProvider.GetBaseSdkTargetFramework(), true);
							flag = true;
						}
					}
					else
					{
						provider.AddHeartbeatProperty(text, true, BaseDefaultHeartbeatPropertyProvider.GetRuntimeFrameworkVer(), true);
						flag = true;
					}
				}
				catch (Exception ex)
				{
					CoreEventSource.Log.FailedToObtainDefaultHeartbeatProperty(text, ex.ToString(), "Incorrect");
				}
			}
			return Task.FromResult<bool>(flag);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00015330 File Offset: 0x00013530
		private static string GetRuntimeFrameworkVer()
		{
			AssemblyFileVersionAttribute assemblyFileVersionAttribute = typeof(object).GetTypeInfo().Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute)).Cast<AssemblyFileVersionAttribute>().FirstOrDefault<AssemblyFileVersionAttribute>();
			if (assemblyFileVersionAttribute == null)
			{
				return "undefined";
			}
			return assemblyFileVersionAttribute.Version;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001537A File Offset: 0x0001357A
		private static string GetBaseSdkTargetFramework()
		{
			return "net45";
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00015384 File Offset: 0x00013584
		private static string GetRuntimeOsType()
		{
			return Environment.OSVersion.Platform.ToString();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000153A9 File Offset: 0x000135A9
		private static string GetProcessSessionId()
		{
			if (BaseDefaultHeartbeatPropertyProvider.uniqueProcessSessionId == null)
			{
				BaseDefaultHeartbeatPropertyProvider.uniqueProcessSessionId = new Guid?(Guid.NewGuid());
			}
			return BaseDefaultHeartbeatPropertyProvider.uniqueProcessSessionId.ToString();
		}

		// Token: 0x04000205 RID: 517
		internal readonly List<string> DefaultFields = new List<string> { "runtimeFramework", "baseSdkTargetFramework", "osType", "processSessionId" };

		// Token: 0x04000206 RID: 518
		private static Guid? uniqueProcessSessionId;
	}
}
