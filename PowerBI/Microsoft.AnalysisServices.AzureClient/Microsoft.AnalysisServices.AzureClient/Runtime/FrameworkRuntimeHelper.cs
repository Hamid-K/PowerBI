using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.AnalysisServices.AzureClient.Utilities;
using Microsoft.Win32;

namespace Microsoft.AnalysisServices.AzureClient.Runtime
{
	// Token: 0x0200003D RID: 61
	internal static class FrameworkRuntimeHelper
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008FE9 File Offset: 0x000071E9
		public static bool IsNetCoreDomain
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return FrameworkRuntimeHelper.isNetCoreDomain;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008FF0 File Offset: 0x000071F0
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

		// Token: 0x060001D9 RID: 473 RVA: 0x00009024 File Offset: 0x00007224
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsReferencedAssembly(AssemblyName assemblyRef, string name, Version version = null, string culture = null, string publicKeyToken = null)
		{
			return FrameworkRuntimeHelper.IsReferencedAssemblyImpl(assemblyRef, name, version, culture, publicKeyToken);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009034 File Offset: 0x00007234
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

		// Token: 0x060001DB RID: 475 RVA: 0x0000905C File Offset: 0x0000725C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAssemblyLoaded(string name, Version version = null, string culture = null, string publicKeyToken = null)
		{
			Assembly assembly;
			return FrameworkRuntimeHelper.IsAssemblyLoadedImpl(name, version, culture, publicKeyToken, out assembly);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00009074 File Offset: 0x00007274
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRuntimeVersion(out Version version)
		{
			version = FrameworkRuntimeHelper.runtimeVersion;
			return version != null;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009088 File Offset: 0x00007288
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

		// Token: 0x060001DE RID: 478 RVA: 0x00009114 File Offset: 0x00007314
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

		// Token: 0x060001DF RID: 479 RVA: 0x000091B0 File Offset: 0x000073B0
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

		// Token: 0x060001E0 RID: 480 RVA: 0x00009340 File Offset: 0x00007540
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

		// Token: 0x060001E1 RID: 481 RVA: 0x00009388 File Offset: 0x00007588
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

		// Token: 0x060001E2 RID: 482 RVA: 0x00009448 File Offset: 0x00007648
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

		// Token: 0x060001E3 RID: 483 RVA: 0x00009560 File Offset: 0x00007760
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

		// Token: 0x040000F0 RID: 240
		private const string AssemblyNamePart_Version = "Version";

		// Token: 0x040000F1 RID: 241
		private const string AssemblyNamePart_Culture = "Culture";

		// Token: 0x040000F2 RID: 242
		private const string AssemblyNamePart_PublicKeyToken = "PublicKeyToken";

		// Token: 0x040000F3 RID: 243
		private const string AssemblyNutralCulture = "neutral";

		// Token: 0x040000F4 RID: 244
		private const char AssemblyNameDelimeter = ',';

		// Token: 0x040000F5 RID: 245
		private const char AssemblyNamePartDelimeter = '=';

		// Token: 0x040000F6 RID: 246
		private const string NetCoreRuntimeAssemblyName = "System.Private.CoreLib";

		// Token: 0x040000F7 RID: 247
		private const string NetCoreRuntimePublicKeyToken = "7cec85d7bea7798e";

		// Token: 0x040000F8 RID: 248
		private const string NetFxRuntimeAssemblyName = "mscorlib";

		// Token: 0x040000F9 RID: 249
		private const string NetFxRuntimePublicKeyToken = "b77a5c561934e089";

		// Token: 0x040000FA RID: 250
		private static bool isNetCoreDomain = FrameworkRuntimeHelper.IsReferencedAssemblyImpl(typeof(object).Assembly.GetName(), "System.Private.CoreLib", null, null, "7cec85d7bea7798e");

		// Token: 0x040000FB RID: 251
		private static bool isNetFxLoaded;

		// Token: 0x040000FC RID: 252
		private static Version runtimeVersion = FrameworkRuntimeHelper.GetRuntimeVersionImpl();
	}
}
