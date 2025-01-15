using System;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using JetBrains.Annotations;
using NLog.Common;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x0200015E RID: 350
	[SecuritySafeCritical]
	internal abstract class BaseMutexFileAppender : BaseFileAppender
	{
		// Token: 0x06001083 RID: 4227 RVA: 0x0002AEF8 File Offset: 0x000290F8
		protected BaseMutexFileAppender(string fileName, ICreateFileParameters createParameters)
			: base(fileName, createParameters)
		{
			if (createParameters.IsArchivingEnabled)
			{
				if (PlatformDetector.SupportsSharableMutex)
				{
					this.ArchiveMutex = this.CreateArchiveMutex();
					return;
				}
				InternalLogger.Debug("Mutex for file archive not supported");
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0002AF28 File Offset: 0x00029128
		// (set) Token: 0x06001085 RID: 4229 RVA: 0x0002AF30 File Offset: 0x00029130
		[CanBeNull]
		public Mutex ArchiveMutex { get; private set; }

		// Token: 0x06001086 RID: 4230 RVA: 0x0002AF3C File Offset: 0x0002913C
		private Mutex CreateArchiveMutex()
		{
			Mutex mutex;
			try
			{
				mutex = this.CreateSharableMutex("FileArchiveLock");
			}
			catch (Exception ex)
			{
				if (ex is SecurityException || ex is UnauthorizedAccessException || ex is NotSupportedException || ex is NotImplementedException || ex is PlatformNotSupportedException)
				{
					InternalLogger.Warn(ex, "Failed to create global archive mutex: {0}", new object[] { base.FileName });
					mutex = new Mutex();
				}
				else
				{
					InternalLogger.Error(ex, "Failed to create global archive mutex: {0}", new object[] { base.FileName });
					if (ex.MustBeRethrown())
					{
						throw;
					}
					mutex = new Mutex();
				}
			}
			return mutex;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0002AFE0 File Offset: 0x000291E0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				Mutex archiveMutex = this.ArchiveMutex;
				if (archiveMutex == null)
				{
					return;
				}
				archiveMutex.Close();
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0002AFFC File Offset: 0x000291FC
		protected Mutex CreateSharableMutex(string mutexNamePrefix)
		{
			if (!PlatformDetector.SupportsSharableMutex)
			{
				throw new NotSupportedException("Creating Mutex not supported");
			}
			return BaseMutexFileAppender.ForceCreateSharableMutex(this.GetMutexName(mutexNamePrefix));
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0002B01C File Offset: 0x0002921C
		internal static Mutex ForceCreateSharableMutex(string name)
		{
			MutexSecurity mutexSecurity = new MutexSecurity();
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
			mutexSecurity.AddAccessRule(new MutexAccessRule(securityIdentifier, MutexRights.FullControl, AccessControlType.Allow));
			bool flag;
			return new Mutex(false, name, out flag, mutexSecurity);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0002B054 File Offset: 0x00029254
		private string GetMutexName(string mutexNamePrefix)
		{
			string text = Path.GetFullPath(base.FileName).ToLowerInvariant();
			text = text.Replace('\\', '_');
			text = text.Replace('/', '_');
			string text2 = string.Format("Global\\NLog-File{0}-{1}", mutexNamePrefix, text);
			if (text2.Length <= 260)
			{
				return text2;
			}
			string text3;
			using (MD5 md = MD5.Create())
			{
				text3 = Convert.ToBase64String(md.ComputeHash(Encoding.UTF8.GetBytes(text)));
			}
			text2 = string.Format("Global\\NLog-File{0}-{1}", mutexNamePrefix, text3);
			int num = text.Length - (260 - text2.Length);
			return text2 + text.Substring(num);
		}
	}
}
