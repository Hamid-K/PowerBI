using System;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Shims.Interprocess;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A24 RID: 6692
	public class MipService
	{
		// Token: 0x0600A94C RID: 43340 RVA: 0x002302F8 File Offset: 0x0022E4F8
		public static void Initialize(string mipDirectory, string applicationId)
		{
			if (MipService.mipDirectory != null)
			{
				return;
			}
			MipService.mipDirectory = mipDirectory;
			MipService.applicationId = applicationId;
			Assembly.LoadFrom(Path.Combine(mipDirectory, "Microsoft.InformationProtection.dll"));
			MipService.serviceType = Assembly.LoadFrom(Path.Combine(mipDirectory, "Microsoft.Mashup.InformationProtection.dll")).GetType("Microsoft.Mashup.InformationProtection.MipService");
			MipService.serviceType.GetMethod("Initialize").Invoke(null, new object[]
			{
				mipDirectory,
				applicationId,
				new Func<string>(MipService.CreateTempDirectory)
			});
			ParameterExpression parameterExpression;
			MipService.serviceCtor = Expression.Lambda<Func<IEngineHost, IMipService>>(Expression.New(MipService.serviceType.GetConstructor(new Type[] { typeof(IEngineHost) }), new Expression[] { parameterExpression }), new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x0600A94D RID: 43341 RVA: 0x002303D8 File Offset: 0x0022E5D8
		public static bool TryAddService(IEngineHost engineHost, MutableEngineHost services)
		{
			MipService.EnsureInitialized();
			if (MipService.serviceCtor == null)
			{
				return false;
			}
			IMipService mipService = MipService.serviceCtor(engineHost);
			services.Add(new SimpleEngineHost<IMipService>(mipService));
			services.Add(new SimpleEngineHost<IMipConfigService>(mipService as IMipConfigService));
			return true;
		}

		// Token: 0x0600A94E RID: 43342 RVA: 0x00230420 File Offset: 0x0022E620
		private static void EnsureInitialized()
		{
			if (!MipService.initialized)
			{
				object obj = MipService.syncRoot;
				lock (obj)
				{
					if (!MipService.initialized)
					{
						if (FxVersionDetector.FxVersion >= ClrVersion.Net45)
						{
							string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
							if (!MipService.TryDirectory(Path.Combine(directoryName, "InformationProtection")))
							{
								MipService.TryDirectory(directoryName);
							}
						}
						MipService.initialized = true;
					}
				}
			}
		}

		// Token: 0x0600A94F RID: 43343 RVA: 0x002304A0 File Offset: 0x0022E6A0
		private static bool TryDirectory(string directory)
		{
			if (Directory.Exists(directory) && File.Exists(Path.Combine(directory, "Microsoft.InformationProtection.dll")) && File.Exists(Path.Combine(directory, "Microsoft.Mashup.InformationProtection.dll")))
			{
				MipService.Initialize(directory, "00000000-0000-0000-0000-000000000000");
				return true;
			}
			return false;
		}

		// Token: 0x0600A950 RID: 43344 RVA: 0x002304DC File Offset: 0x0022E6DC
		private static string CreateTempDirectory()
		{
			if (MipService.tempDirectory != null)
			{
				return MipService.tempDirectory;
			}
			for (int i = 0; i < 10000; i++)
			{
				string text = "MashupMipDirectory" + i.ToString(CultureInfo.InvariantCulture);
				Mutex mutex = null;
				try
				{
					bool flag;
					mutex = MutexFactory.Create(true, text, out flag);
					if (flag)
					{
						string directoryName = Path.Combine(Path.GetTempPath(), text);
						FileSystemAccessHelper.IgnoringAccessExceptions("MipService/CreateTempDirectory", delegate
						{
							Directory.Delete(directoryName, true);
						}, null);
						Directory.CreateDirectory(directoryName);
						MipService.tempDirectory = directoryName;
						MipService.tempDirectoryMutex = mutex;
						mutex = null;
						return MipService.tempDirectory;
					}
				}
				catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
				{
				}
				finally
				{
					if (mutex != null)
					{
						mutex.Close();
					}
				}
			}
			throw new InvalidOperationException("Unable to create temporary directory for MIP SDK");
		}

		// Token: 0x04005816 RID: 22550
		private const string mipAssembly = "Microsoft.InformationProtection.dll";

		// Token: 0x04005817 RID: 22551
		private const string mashupMipAssembly = "Microsoft.Mashup.InformationProtection.dll";

		// Token: 0x04005818 RID: 22552
		private static object syncRoot = new object();

		// Token: 0x04005819 RID: 22553
		private static bool initialized;

		// Token: 0x0400581A RID: 22554
		private static string mipDirectory;

		// Token: 0x0400581B RID: 22555
		private static string applicationId;

		// Token: 0x0400581C RID: 22556
		private static string tempDirectory;

		// Token: 0x0400581D RID: 22557
		private static Mutex tempDirectoryMutex;

		// Token: 0x0400581E RID: 22558
		private static Type serviceType;

		// Token: 0x0400581F RID: 22559
		private static Func<IEngineHost, IMipService> serviceCtor;
	}
}
