using System;
using System.IO;
using System.Reflection;
using System.Security;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x0200010B RID: 267
	internal static class AssemblyHelpers
	{
		// Token: 0x06000E67 RID: 3687 RVA: 0x00023BF4 File Offset: 0x00021DF4
		public static Assembly LoadFromPath(string assemblyFileName, string baseDirectory = null)
		{
			string text = ((baseDirectory == null) ? assemblyFileName : Path.Combine(baseDirectory, assemblyFileName));
			InternalLogger.Info<string>("Loading assembly file: {0}", text);
			return Assembly.LoadFrom(text);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00023C20 File Offset: 0x00021E20
		public static Assembly LoadFromName(string assemblyName)
		{
			InternalLogger.Info<string>("Loading assembly: {0}", assemblyName);
			return Assembly.Load(assemblyName);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00023C34 File Offset: 0x00021E34
		public static string GetAssemblyFileLocation(Assembly assembly)
		{
			string text = string.Empty;
			string text2;
			try
			{
				if (assembly == null)
				{
					text2 = string.Empty;
				}
				else
				{
					text = assembly.FullName;
					Uri uri;
					if (!Uri.TryCreate(assembly.CodeBase, UriKind.RelativeOrAbsolute, out uri))
					{
						InternalLogger.Warn<string, string>("Ignoring assembly location because code base is unknown: '{0}' ({1})", assembly.CodeBase, text);
						text2 = string.Empty;
					}
					else
					{
						string directoryName = Path.GetDirectoryName(uri.LocalPath);
						if (string.IsNullOrEmpty(directoryName))
						{
							InternalLogger.Warn<string, string>("Ignoring assembly location because it is not a valid directory: '{0}' ({1})", uri.LocalPath, text);
							text2 = string.Empty;
						}
						else
						{
							DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
							if (!directoryInfo.Exists)
							{
								InternalLogger.Warn<string, string>("Ignoring assembly location because directory doesn't exists: '{0}' ({1})", directoryName, text);
								text2 = string.Empty;
							}
							else
							{
								InternalLogger.Debug<string, string>("Found assembly location directory: '{0}' ({1})", directoryInfo.FullName, text);
								text2 = directoryInfo.FullName;
							}
						}
					}
				}
			}
			catch (PlatformNotSupportedException ex)
			{
				InternalLogger.Warn(ex, "Ignoring assembly location because assembly lookup is not supported: {0}", new object[] { text });
				if (ex.MustBeRethrown())
				{
					throw;
				}
				text2 = string.Empty;
			}
			catch (SecurityException ex2)
			{
				InternalLogger.Warn(ex2, "Ignoring assembly location because assembly lookup is not allowed: {0}", new object[] { text });
				if (ex2.MustBeRethrown())
				{
					throw;
				}
				text2 = string.Empty;
			}
			catch (UnauthorizedAccessException ex3)
			{
				InternalLogger.Warn(ex3, "Ignoring assembly location because assembly lookup is not allowed: {0}", new object[] { text });
				if (ex3.MustBeRethrown())
				{
					throw;
				}
				text2 = string.Empty;
			}
			return text2;
		}
	}
}
