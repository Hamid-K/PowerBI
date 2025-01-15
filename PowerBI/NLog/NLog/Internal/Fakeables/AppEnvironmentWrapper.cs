using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace NLog.Internal.Fakeables
{
	// Token: 0x02000169 RID: 361
	internal class AppEnvironmentWrapper : IAppEnvironment, IFileSystem
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0002C6A7 File Offset: 0x0002A8A7
		public string AppDomainBaseDirectory
		{
			get
			{
				return LogFactory.CurrentAppDomain.BaseDirectory;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0002C6B3 File Offset: 0x0002A8B3
		public string AppDomainConfigurationFile
		{
			get
			{
				return LogFactory.CurrentAppDomain.ConfigurationFile;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0002C6BF File Offset: 0x0002A8BF
		public string CurrentProcessFilePath
		{
			get
			{
				return ProcessIDHelper.Instance.CurrentProcessFilePath;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0002C6CB File Offset: 0x0002A8CB
		public string EntryAssemblyLocation
		{
			get
			{
				return AssemblyHelpers.GetAssemblyFileLocation(Assembly.GetEntryAssembly());
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x0002C6D7 File Offset: 0x0002A8D7
		public string EntryAssemblyFileName
		{
			get
			{
				return Path.GetFileName(Assembly.GetEntryAssembly().Location ?? string.Empty);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0002C6F1 File Offset: 0x0002A8F1
		public IEnumerable<string> PrivateBinPath
		{
			get
			{
				return LogFactory.CurrentAppDomain.PrivateBinPath;
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0002C6FD File Offset: 0x0002A8FD
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0002C705 File Offset: 0x0002A905
		public XmlReader LoadXmlFile(string path)
		{
			return XmlReader.Create(path);
		}
	}
}
