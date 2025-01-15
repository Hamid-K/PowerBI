using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationClasses.Communication;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000498 RID: 1176
	public static class CommunicationUtilities
	{
		// Token: 0x06002445 RID: 9285 RVA: 0x00082B85 File Offset: 0x00080D85
		public static string ConstructNamedPipeAddress(string fqdn, string serviceName)
		{
			return "net.pipe://{0}/{1}".FormatWithInvariantCulture(new object[] { fqdn, serviceName });
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00082B9F File Offset: 0x00080D9F
		public static string ConstructTcpAddress(string fqdn, int port)
		{
			return "net.tcp://{0}:{1}/".FormatWithInvariantCulture(new object[] { fqdn, port });
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00082BBE File Offset: 0x00080DBE
		public static string ConstructHttpAddress(string fqdn, int port, string serviceName)
		{
			return "http://{0}:{1}/{2}".FormatWithInvariantCulture(new object[] { fqdn, port, serviceName });
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00082BE1 File Offset: 0x00080DE1
		public static string ConstructHttpsAddress(string fqdn, int port, string serviceName)
		{
			return "https://{0}:{1}/{2}/".FormatWithInvariantCulture(new object[] { fqdn, port, serviceName });
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00082C04 File Offset: 0x00080E04
		[CanBeNull]
		public static string GetListeningProcessName(int port)
		{
			StreamReader standardOutput;
			using (Process process = new Process())
			{
				string text = "netstat";
				string text2 = "-a -o";
				process.StartInfo = new ProcessStartInfo(text, text2)
				{
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				};
				process.Start();
				standardOutput = process.StandardOutput;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (!standardOutput.EndOfStream)
			{
				stringBuilder.Append(standardOutput.ReadLine());
			}
			string text3 = stringBuilder.ToString();
			int num = text3.IndexOf(":" + port.ToString(CultureInfo.InvariantCulture) + " ", StringComparison.Ordinal);
			if (num < 0)
			{
				return null;
			}
			text3 = text3.Substring(num);
			string text4 = Regex.Match(text3, "[ \t]+[0-9]+[ \t]+").Value;
			text4 = text4.Trim();
			try
			{
				return Process.GetProcessById(int.Parse(text4, CultureInfo.InvariantCulture)).ProcessName;
			}
			catch (ArgumentException)
			{
			}
			return text4;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00082D1C File Offset: 0x00080F1C
		internal static string GetAllProcesses()
		{
			StreamReader standardOutput;
			using (Process process = new Process())
			{
				process.StartInfo = new ProcessStartInfo("tasklist")
				{
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				};
				process.Start();
				standardOutput = process.StandardOutput;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (!standardOutput.EndOfStream)
			{
				stringBuilder.Append(standardOutput.ReadLine());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00034065 File Offset: 0x00032265
		public static int LongToInt(long value)
		{
			if (value >= 2147483647L)
			{
				return int.MaxValue;
			}
			return (int)value;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x00082DA4 File Offset: 0x00080FA4
		public static void AddKnownTypesToEndPoint(ServiceEndpoint endpoint, IEnumerable<Type> knownTypesList)
		{
			foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
			{
				foreach (Type type in knownTypesList)
				{
					if (!operationDescription.KnownTypes.Contains(type))
					{
						operationDescription.KnownTypes.Add(type);
					}
				}
			}
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00082E3C File Offset: 0x0008103C
		public static Uri ConstructUri(BindingType bindingType, string securityMode, string host, int port, string uriPrefix)
		{
			switch (bindingType)
			{
			case BindingType.Tcp:
				return new Uri(CommunicationUtilities.ConstructTcpAddress(host, port));
			case BindingType.BasicHttp:
			case BindingType.WsHttp:
			case BindingType.WebHttp:
			case BindingType.HttpsWithSoap12:
			{
				SecurityMode securityMode2;
				ExtendedDiagnostics.EnsureOperation(Enum.TryParse<SecurityMode>(securityMode, out securityMode2), "Could not parse {0} as {1}".FormatWithInvariantCulture(new object[]
				{
					securityMode,
					typeof(SecurityMode)
				}));
				if (securityMode2 != SecurityMode.None)
				{
					return new Uri(CommunicationUtilities.ConstructHttpsAddress(host, port, uriPrefix));
				}
				return new Uri(CommunicationUtilities.ConstructHttpAddress(host, port, uriPrefix));
			}
			case BindingType.NamedPipe:
				return new Uri(CommunicationUtilities.ConstructNamedPipeAddress(host, uriPrefix));
			default:
				return null;
			}
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00082ED4 File Offset: 0x000810D4
		[CanBeNull]
		internal static Type GetTypeWithAttribute<T>(Type[] types, Func<T, Type, bool> validation) where T : Attribute
		{
			foreach (Type type in types)
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(T), false);
				if (customAttributes.Length != 0 && validation((T)((object)customAttributes[0]), type))
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00082F1F File Offset: 0x0008111F
		internal static Message CreateMessageCopy(Message message, XmlDictionaryReader body)
		{
			Message message2 = Message.CreateMessage(message.Version, message.Headers.Action, body);
			message2.Headers.CopyHeaderFrom(message, 0);
			message2.Properties.CopyProperties(message.Properties);
			return message2;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x00082F58 File Offset: 0x00081158
		public static Type GetKnownType(TypeIdentifier typeIdentifier)
		{
			string assemblyName = typeIdentifier.AssemblyName;
			string typeName = typeIdentifier.TypeName;
			Type type = null;
			try
			{
				type = DynamicLoader.Load(assemblyName, typeName, new Predicate<Type>(DynamicLoader.IsValidType), LoadOptions.Explicit);
			}
			catch (DynamicLoaderException ex)
			{
				throw new CommunicationFrameworkConfigurationException("Cannot create an instance of known type of type '{0}' from assembly '{1}'".FormatWithInvariantCulture(new object[] { typeName, assemblyName }), ex);
			}
			return type;
		}

		// Token: 0x04000CCE RID: 3278
		internal const string AllowAnyConsumer = "*";

		// Token: 0x04000CCF RID: 3279
		internal const string AllowKnownElement = "KnownElements";

		// Token: 0x04000CD0 RID: 3280
		public const int DefaultMaxDepth = 64;

		// Token: 0x04000CD1 RID: 3281
		public const int DefaultMaxStringContentLength = 65536;

		// Token: 0x04000CD2 RID: 3282
		public const long DefaultMaxMessageSize = 1048576L;
	}
}
