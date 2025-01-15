using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000016 RID: 22
	internal abstract class ProviderContext
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000056A8 File Offset: 0x000038A8
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005714 File Offset: 0x00003914
		public static string BinariesFolderPath
		{
			get
			{
				if (ProviderContext.binariesFolderPath == null)
				{
					object obj = ProviderContext.staticSyncRoot;
					lock (obj)
					{
						if (ProviderContext.binariesFolderPath == null)
						{
							ProviderContext.binariesFolderPath = Path.GetDirectoryName(typeof(ProviderContext).Assembly.Location);
						}
					}
				}
				return ProviderContext.binariesFolderPath;
			}
			set
			{
				object obj = ProviderContext.staticSyncRoot;
				lock (obj)
				{
					ProviderContext.binariesFolderPath = value;
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005754 File Offset: 0x00003954
		public static string ContainerExecutableFolderPath
		{
			get
			{
				return ProviderContext.BinariesFolderPath;
			}
		}

		// Token: 0x060000ED RID: 237
		public abstract Exception CreateCanceledException();

		// Token: 0x060000EE RID: 238
		public abstract Exception CreateMashupKindException(string message);

		// Token: 0x060000EF RID: 239
		public abstract Exception CreateMashupKindException(string message, Exception innerException);

		// Token: 0x060000F0 RID: 240
		public abstract Exception CreateInternalKindException(string message, Exception innerException);

		// Token: 0x060000F1 RID: 241
		public abstract Exception CreateExpressKindException(string message);

		// Token: 0x060000F2 RID: 242
		public abstract Exception CreateExpressKindException(string message, Exception innerException);

		// Token: 0x060000F3 RID: 243
		public abstract Exception CreateDeadlockException(string message);

		// Token: 0x060000F4 RID: 244
		public abstract Exception CreateHostingKindException(string message, string reason);

		// Token: 0x060000F5 RID: 245
		public abstract Exception CreateValueKindException(ValueException2 valueException);

		// Token: 0x060000F6 RID: 246
		public abstract Exception CreateVersionKindException(string message);

		// Token: 0x060000F7 RID: 247
		public abstract Exception CreateCredentialKindException(string message, string reason, IResource resource, IResource resourceOrigin);

		// Token: 0x060000F8 RID: 248
		public abstract Exception CreateCredentialKindException(string message, string reason, string dataSourceReference, string dataSourceReferenceOrigin);

		// Token: 0x060000F9 RID: 249
		public abstract Exception CreateCredentialKindException(string message, string reason, IResource dataSource);

		// Token: 0x060000FA RID: 250
		public abstract Exception CreatePermissionKindException(string message, IResource resource, string kind, string value, IDictionary<string, object> properties);

		// Token: 0x060000FB RID: 251
		public abstract Exception CreatePrivacyKindException(string message, Exception innerException);

		// Token: 0x060000FC RID: 252
		public abstract Exception CreatePrivacyKindException(string message);

		// Token: 0x060000FD RID: 253
		public abstract Exception CreatePrivacySettingKindException(string message, IEnumerable<IResource> resources, Exception innerException);

		// Token: 0x060000FE RID: 254
		public abstract Exception CreatePrivacyEnforcementKindException(string message, IEnumerable<IResource> resources, Exception innerException);

		// Token: 0x060000FF RID: 255
		public abstract bool NeedTranslate(Exception exception);

		// Token: 0x06000100 RID: 256
		public abstract ValueException2 GetValueException(Exception exception);

		// Token: 0x04000094 RID: 148
		private static readonly object staticSyncRoot = new object();

		// Token: 0x04000095 RID: 149
		private static string binariesFolderPath;
	}
}
