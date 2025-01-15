using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.AnalysisServices.AdomdClient.Utilities;
using Microsoft.Win32;

namespace Microsoft.AnalysisServices.AdomdClient.Runtime
{
	// Token: 0x0200015A RID: 346
	internal static class FrameworkRuntimeHelper
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0003A4F1 File Offset: 0x000386F1
		public static bool IsNetCoreDomain
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return FrameworkRuntimeHelper.isNetCoreDomain;
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003A4F8 File Offset: 0x000386F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNetFxLoaded()
		{
			Assembly assembly;
			if (!FrameworkRuntimeHelper.isNetFxLoaded && FrameworkRuntimeHelper.IsAssemblyLoadedImpl("mscorlib", null, null, "b77a5c561934e089", out assembly))
			{
				FrameworkRuntimeHelper.isNetFxLoaded = true;
			}
			return FrameworkRuntimeHelper.isNetFxLoaded;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003A52C File Offset: 0x0003872C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsReferencedAssembly(AssemblyName assemblyRef, string name, Version version = null, string culture = null, string publicKeyToken = null)
		{
			return FrameworkRuntimeHelper.IsReferencedAssemblyImpl(assemblyRef, name, version, culture, publicKeyToken);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003A53C File Offset: 0x0003873C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsReferencedAssembly(AssemblyName assemblyRef, string fullName)
		{
			string text;
			Version version;
			string text2;
			string text3;
			FrameworkRuntimeHelper.ParseAssemblyName(fullName, out text, out version, out text2, out text3);
			return FrameworkRuntimeHelper.IsReferencedAssemblyImpl(assemblyRef, text, version, text2, text3);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003A564 File Offset: 0x00038764
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAssemblyLoaded(string name, Version version = null, string culture = null, string publicKeyToken = null)
		{
			Assembly assembly;
			return FrameworkRuntimeHelper.IsAssemblyLoadedImpl(name, version, culture, publicKeyToken, out assembly);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003A57C File Offset: 0x0003877C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRuntimeVersion(out Version version)
		{
			version = FrameworkRuntimeHelper.runtimeVersion;
			return version != null;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003A590 File Offset: 0x00038790
		public static bool TryLoadEmbeddedAssembly(Assembly hostAssembly, string dllName, out Assembly assembly)
		{
			string text = hostAssembly.GetManifestResourceNames().FirstOrDefault((string name) => name.EndsWith(dllName, StringComparison.InvariantCultureIgnoreCase));
			if (string.IsNullOrEmpty(text))
			{
				assembly = null;
				return false;
			}
			bool flag;
			using (Stream manifestResourceStream = hostAssembly.GetManifestResourceStream(text))
			{
				byte[] array = new byte[manifestResourceStream.Length];
				int num;
				StreamHelper.TryFillBuffer(manifestResourceStream, array, 0, array.Length, out num);
				assembly = Assembly.Load(array);
				flag = true;
			}
			return flag;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003A61C File Offset: 0x0003881C
		public static bool IsFatalException(Exception ex)
		{
			while (ex != null)
			{
				if ((ex is OutOfMemoryException && !(ex is InsufficientMemoryException)) || ex is ThreadAbortException)
				{
					return true;
				}
				if (ex is TypeInitializationException || ex is TargetInvocationException)
				{
					ex = ex.InnerException;
				}
				else
				{
					if (ex is AggregateException)
					{
						using (IEnumerator<Exception> enumerator = ((AggregateException)ex).InnerExceptions.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (FrameworkRuntimeHelper.IsFatalException(enumerator.Current))
								{
									return true;
								}
							}
							break;
						}
						continue;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003A6B8 File Offset: 0x000388B8
		internal static void ParseAssemblyName(string fullName, out string name, out Version version, out string culture, out string publicKeyToken)
		{
			version = null;
			culture = null;
			publicKeyToken = null;
			int num = fullName.IndexOf(',');
			if (num == -1)
			{
				name = fullName.Trim();
				return;
			}
			name = fullName.Substring(0, num).Trim();
			num++;
			for (;;)
			{
				if (num >= fullName.Length || !char.IsWhiteSpace(fullName, num))
				{
					if (num != fullName.Length)
					{
						int num2 = num;
						while (num < fullName.Length && fullName[num] != '=' && fullName[num] != ',')
						{
							num++;
						}
						if (num != fullName.Length)
						{
							if (fullName[num] == ',')
							{
								num++;
							}
							else
							{
								int i = num;
								num++;
								while (i > num2)
								{
									if (!char.IsWhiteSpace(fullName, i - 1))
									{
										break;
									}
									i--;
								}
								while (num < fullName.Length && char.IsWhiteSpace(fullName, num))
								{
									num++;
								}
								if (num != fullName.Length)
								{
									int num3 = num;
									while (num < fullName.Length && fullName[num] != ',')
									{
										num++;
									}
									int num4 = num;
									num++;
									while (num4 > num3 && char.IsWhiteSpace(fullName, num4 - 1))
									{
										num4--;
									}
									string text = fullName.Substring(num2, i - num2);
									string text2 = fullName.Substring(num3, num4 - num3);
									if (string.Compare(text, "Version", StringComparison.InvariantCultureIgnoreCase) == 0)
									{
										version = new Version(text2);
									}
									else if (string.Compare(text, "Culture", StringComparison.InvariantCultureIgnoreCase) == 0)
									{
										culture = text2;
									}
									else if (string.Compare(text, "PublicKeyToken", StringComparison.InvariantCultureIgnoreCase) == 0)
									{
										publicKeyToken = text2;
									}
								}
							}
						}
					}
					if (num >= fullName.Length)
					{
						break;
					}
				}
				else
				{
					num++;
				}
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003A848 File Offset: 0x00038A48
		private static bool IsAssemblyLoadedImpl(string name, Version version, string culture, string publicKeyToken, out Assembly assembly)
		{
			foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (FrameworkRuntimeHelper.IsReferencedAssemblyImpl(assembly2.GetName(), name, version, culture, publicKeyToken))
				{
					assembly = assembly2;
					return true;
				}
			}
			assembly = null;
			return false;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003A890 File Offset: 0x00038A90
		private static bool IsReferencedAssemblyImpl(AssemblyName assemblyRef, string name, Version version, string culture, string publicKeyToken)
		{
			if (string.Compare(name, assemblyRef.Name, StringComparison.Ordinal) != 0)
			{
				return false;
			}
			if (version != null && !version.Equals(assemblyRef.Version))
			{
				return false;
			}
			if (!string.IsNullOrEmpty(culture) && string.Compare(culture, "neutral", StringComparison.InvariantCultureIgnoreCase) != 0 && string.Compare(culture, assemblyRef.CultureName, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(publicKeyToken))
			{
				byte[] publicKeyToken2 = assemblyRef.GetPublicKeyToken();
				if (publicKeyToken2 == null || publicKeyToken2.Length == 0)
				{
					return false;
				}
				StringBuilder stringBuilder = new StringBuilder(publicKeyToken2.Length * 2 + 1);
				for (int i = 0; i < publicKeyToken2.Length; i++)
				{
					stringBuilder.Append(publicKeyToken2[i].ToString("x2"));
				}
				if (string.Compare(publicKeyToken, stringBuilder.ToString(), StringComparison.OrdinalIgnoreCase) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003A950 File Offset: 0x00038B50
		private static Version GetRuntimeVersionImpl()
		{
			if (FrameworkRuntimeHelper.isNetCoreDomain)
			{
				return Environment.Version;
			}
			string text;
			Version version;
			string text2;
			string text3;
			FrameworkRuntimeHelper.ParseAssemblyName(typeof(object).Assembly.FullName, out text, out version, out text2, out text3);
			if (version.Major == 4)
			{
				int netFx40RuntimeReleaseMarker = FrameworkRuntimeHelper.GetNetFx40RuntimeReleaseMarker();
				if (netFx40RuntimeReleaseMarker >= 533320)
				{
					return new Version(4, 8, 1);
				}
				if (netFx40RuntimeReleaseMarker >= 528040)
				{
					return new Version(4, 8);
				}
				if (netFx40RuntimeReleaseMarker >= 461808)
				{
					return new Version(4, 7, 2);
				}
				if (netFx40RuntimeReleaseMarker >= 461308)
				{
					return new Version(4, 7, 1);
				}
				if (netFx40RuntimeReleaseMarker >= 460798)
				{
					return new Version(4, 7);
				}
				if (netFx40RuntimeReleaseMarker >= 394802)
				{
					return new Version(4, 6, 2);
				}
				if (netFx40RuntimeReleaseMarker >= 394254)
				{
					return new Version(4, 6, 1);
				}
				if (netFx40RuntimeReleaseMarker >= 393295)
				{
					return new Version(4, 6);
				}
				if (netFx40RuntimeReleaseMarker >= 379893)
				{
					return new Version(4, 5, 2);
				}
				if (netFx40RuntimeReleaseMarker >= 378675)
				{
					return new Version(4, 5, 1);
				}
				if (netFx40RuntimeReleaseMarker >= 378389)
				{
					return new Version(4, 5);
				}
			}
			return Environment.Version;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003AA68 File Offset: 0x00038C68
		private static int GetNetFx40RuntimeReleaseMarker()
		{
			int num;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
				{
					if (registryKey == null)
					{
						num = -1;
					}
					else
					{
						object value = registryKey.GetValue("Release");
						if (value == null)
						{
							num = -1;
						}
						else
						{
							num = (int)value;
						}
					}
				}
			}
			catch (Exception)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x04000B2E RID: 2862
		private const string AssemblyNamePart_Version = "Version";

		// Token: 0x04000B2F RID: 2863
		private const string AssemblyNamePart_Culture = "Culture";

		// Token: 0x04000B30 RID: 2864
		private const string AssemblyNamePart_PublicKeyToken = "PublicKeyToken";

		// Token: 0x04000B31 RID: 2865
		private const string AssemblyNutralCulture = "neutral";

		// Token: 0x04000B32 RID: 2866
		private const char AssemblyNameDelimeter = ',';

		// Token: 0x04000B33 RID: 2867
		private const char AssemblyNamePartDelimeter = '=';

		// Token: 0x04000B34 RID: 2868
		private const string NetCoreRuntimeAssemblyName = "System.Private.CoreLib";

		// Token: 0x04000B35 RID: 2869
		private const string NetCoreRuntimePublicKeyToken = "7cec85d7bea7798e";

		// Token: 0x04000B36 RID: 2870
		private const string NetFxRuntimeAssemblyName = "mscorlib";

		// Token: 0x04000B37 RID: 2871
		private const string NetFxRuntimePublicKeyToken = "b77a5c561934e089";

		// Token: 0x04000B38 RID: 2872
		private static bool isNetCoreDomain = FrameworkRuntimeHelper.IsReferencedAssemblyImpl(typeof(object).Assembly.GetName(), "System.Private.CoreLib", null, null, "7cec85d7bea7798e");

		// Token: 0x04000B39 RID: 2873
		private static bool isNetFxLoaded;

		// Token: 0x04000B3A RID: 2874
		private static Version runtimeVersion = FrameworkRuntimeHelper.GetRuntimeVersionImpl();
	}
}
