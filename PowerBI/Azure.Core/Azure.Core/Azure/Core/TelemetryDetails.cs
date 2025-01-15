using System;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x02000065 RID: 101
	[NullableContext(1)]
	[Nullable(0)]
	public class TelemetryDetails
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000A184 File Offset: 0x00008384
		public Assembly Assembly { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000A18C File Offset: 0x0000838C
		[Nullable(2)]
		public string ApplicationId
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000A194 File Offset: 0x00008394
		public TelemetryDetails(Assembly assembly, [Nullable(2)] string applicationId = null)
			: this(assembly, applicationId, new RuntimeInformationWrapper())
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000A1A4 File Offset: 0x000083A4
		[NullableContext(2)]
		internal TelemetryDetails([Nullable(1)] Assembly assembly, string applicationId = null, RuntimeInformationWrapper runtimeInformation = null)
		{
			Argument.AssertNotNull<Assembly>(assembly, "assembly");
			if (applicationId != null && applicationId.Length > 24)
			{
				throw new ArgumentOutOfRangeException("applicationId", string.Format("{0} must be shorter than {1} characters", "applicationId", 25));
			}
			this.Assembly = assembly;
			this.ApplicationId = applicationId;
			this._userAgent = TelemetryDetails.GenerateUserAgentString(assembly, applicationId, runtimeInformation);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000A20C File Offset: 0x0000840C
		public void Apply(HttpMessage message)
		{
			message.SetProperty(typeof(UserAgentValueKey), this.ToString());
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000A224 File Offset: 0x00008424
		internal static string GenerateUserAgentString(Assembly clientAssembly, [Nullable(2)] string applicationId = null, [Nullable(2)] RuntimeInformationWrapper runtimeInformation = null)
		{
			AssemblyInformationalVersionAttribute customAttribute = clientAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
			if (customAttribute == null)
			{
				throw new InvalidOperationException("AssemblyInformationalVersionAttribute is required on client SDK assembly '" + clientAssembly.FullName + "'.");
			}
			string text = customAttribute.InformationalVersion;
			string text2 = clientAssembly.GetName().Name;
			if (text2.StartsWith("Azure.", StringComparison.Ordinal))
			{
				text2 = text2.Substring("Azure.".Length);
			}
			int num = text.IndexOfOrdinal('+');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			if (runtimeInformation == null)
			{
				runtimeInformation = new RuntimeInformationWrapper();
			}
			string text3 = TelemetryDetails.EscapeProductInformation(string.Concat(new string[] { "(", runtimeInformation.FrameworkDescription, "; ", runtimeInformation.OSDescription, ")" }));
			if (applicationId == null)
			{
				return string.Concat(new string[] { "azsdk-net-", text2, "/", text, " ", text3 });
			}
			return string.Concat(new string[] { applicationId, " azsdk-net-", text2, "/", text, " ", text3 });
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000A347 File Offset: 0x00008547
		public override string ToString()
		{
			return this._userAgent;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000A350 File Offset: 0x00008550
		private static string EscapeProductInformation(string productInfo)
		{
			bool flag = false;
			try
			{
				ProductInfoHeaderValue productInfoHeaderValue;
				flag = ProductInfoHeaderValue.TryParse(productInfo, ref productInfoHeaderValue);
			}
			catch (Exception)
			{
			}
			if (flag)
			{
				return productInfo;
			}
			StringBuilder stringBuilder = new StringBuilder(productInfo.Length + 2);
			stringBuilder.Append('(');
			int i = 1;
			while (i < productInfo.Length - 1)
			{
				char c = productInfo[i];
				if (c == ')' || c == '(')
				{
					stringBuilder.Append('\\');
					goto IL_00AF;
				}
				if (c != '\\')
				{
					goto IL_00AF;
				}
				if (i + 1 >= productInfo.Length - 1)
				{
					stringBuilder.Append('\\');
					goto IL_00AF;
				}
				char c2 = productInfo[i + 1];
				if (c2 != '\\' && c2 != '(' && c2 != ')')
				{
					stringBuilder.Append('\\');
					goto IL_00AF;
				}
				stringBuilder.Append(c);
				stringBuilder.Append(c2);
				i++;
				IL_00B8:
				i++;
				continue;
				IL_00AF:
				stringBuilder.Append(c);
				goto IL_00B8;
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x04000170 RID: 368
		private const int MaxApplicationIdLength = 24;

		// Token: 0x04000171 RID: 369
		private readonly string _userAgent;
	}
}
