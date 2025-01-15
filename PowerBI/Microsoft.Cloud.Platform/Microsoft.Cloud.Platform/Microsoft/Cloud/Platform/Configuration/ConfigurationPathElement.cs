using System;
using System.Linq;
using System.Reflection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x0200042D RID: 1069
	public sealed class ConfigurationPathElement
	{
		// Token: 0x060020F9 RID: 8441 RVA: 0x0007C2D0 File Offset: 0x0007A4D0
		public ConfigurationPathElement(ConfigurationPathElement parent, object item, string path, bool loadToFile, bool loadToParent)
			: this(parent, item, path, loadToFile, loadToParent, false)
		{
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0007C2E0 File Offset: 0x0007A4E0
		public ConfigurationPathElement(ConfigurationPathElement parent, object item, string path, bool loadToFile, bool loadToParent, bool encrypted)
			: this(parent, item, path, loadToFile, loadToParent, encrypted, false)
		{
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0007C2F4 File Offset: 0x0007A4F4
		public ConfigurationPathElement(ConfigurationPathElement parent, object item, string path, bool loadToFile, bool loadToParent, bool encrypted, bool isValidClientId)
			: this(parent, item, path, loadToFile, loadToParent, encrypted, isValidClientId, false)
		{
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x0007C314 File Offset: 0x0007A514
		public ConfigurationPathElement(ConfigurationPathElement parent, object item, string path, bool loadToFile, bool loadToParent, bool encrypted, bool isValidClientId, bool onlyLowercase)
		{
			this.Path = path;
			this.Item = item;
			this.Parent = parent;
			this.LoadToFile = loadToFile;
			this.LoadToParent = loadToParent;
			this.Encrypted = encrypted;
			this.IsValidClientId = isValidClientId;
			this.OnlyLowercase = onlyLowercase;
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x0007C364 File Offset: 0x0007A564
		public string Name
		{
			get
			{
				return this.Path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last<string>();
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x0007C382 File Offset: 0x0007A582
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x0007C38A File Offset: 0x0007A58A
		public string Path { get; private set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x0007C393 File Offset: 0x0007A593
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x0007C39B File Offset: 0x0007A59B
		public ConfigurationPathElement Parent { get; private set; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x0007C3A4 File Offset: 0x0007A5A4
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x0007C3AC File Offset: 0x0007A5AC
		public object Item { get; private set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x0007C3B5 File Offset: 0x0007A5B5
		public ConfigurationPathElement Root
		{
			get
			{
				if (this.Parent != null)
				{
					return this.Parent.Root;
				}
				return this;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x0007C3CC File Offset: 0x0007A5CC
		// (set) Token: 0x06002106 RID: 8454 RVA: 0x0007C3D4 File Offset: 0x0007A5D4
		public bool LoadToFile { get; private set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x0007C3DD File Offset: 0x0007A5DD
		// (set) Token: 0x06002108 RID: 8456 RVA: 0x0007C3E5 File Offset: 0x0007A5E5
		public bool LoadToParent { get; private set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x0007C3EE File Offset: 0x0007A5EE
		// (set) Token: 0x0600210A RID: 8458 RVA: 0x0007C3F6 File Offset: 0x0007A5F6
		public bool Encrypted { get; private set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x0007C3FF File Offset: 0x0007A5FF
		// (set) Token: 0x0600210C RID: 8460 RVA: 0x0007C407 File Offset: 0x0007A607
		public bool IsValidClientId { get; private set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x0007C410 File Offset: 0x0007A610
		// (set) Token: 0x0600210E RID: 8462 RVA: 0x0007C418 File Offset: 0x0007A618
		public bool OnlyLowercase { get; private set; }

		// Token: 0x0600210F RID: 8463 RVA: 0x0007C421 File Offset: 0x0007A621
		public override string ToString()
		{
			return "<{0}, '{1}'>".FormatWithInvariantCulture(new object[] { this.Item, this.Path });
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x0007C445 File Offset: 0x0007A645
		public PropertyInfo GetProperty()
		{
			return ConfigurationPropertyAttribute.GetConfigurationProperties(this.Parent.Item).Single((PropertyInfo p) => p.Name.Equals(this.Name, StringComparison.Ordinal));
		}
	}
}
