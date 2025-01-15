using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000068 RID: 104
	public sealed class DeviceContext
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0000E863 File Offset: 0x0000CA63
		internal DeviceContext(IDictionary<string, string> properties)
		{
			this.properties = properties;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000E872 File Offset: 0x0000CA72
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000E889 File Offset: 0x0000CA89
		public string Type
		{
			get
			{
				if (!string.IsNullOrEmpty(this.type))
				{
					return this.type;
				}
				return null;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000E892 File Offset: 0x0000CA92
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000E8A9 File Offset: 0x0000CAA9
		public string Id
		{
			get
			{
				if (!string.IsNullOrEmpty(this.id))
				{
					return this.id;
				}
				return null;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000E8B2 File Offset: 0x0000CAB2
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000E8C9 File Offset: 0x0000CAC9
		public string OperatingSystem
		{
			get
			{
				if (!string.IsNullOrEmpty(this.operatingSystem))
				{
					return this.operatingSystem;
				}
				return null;
			}
			set
			{
				this.operatingSystem = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000E8D2 File Offset: 0x0000CAD2
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000E8E9 File Offset: 0x0000CAE9
		public string OemName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.oemName))
				{
					return this.oemName;
				}
				return null;
			}
			set
			{
				this.oemName = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000E8F2 File Offset: 0x0000CAF2
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000E909 File Offset: 0x0000CB09
		public string Model
		{
			get
			{
				if (!string.IsNullOrEmpty(this.model))
				{
					return this.model;
				}
				return null;
			}
			set
			{
				this.model = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000E912 File Offset: 0x0000CB12
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000E924 File Offset: 0x0000CB24
		[Obsolete("Use custom properties.")]
		public string NetworkType
		{
			get
			{
				return this.properties.GetTagValueOrNull("ai.device.network");
			}
			set
			{
				this.properties.SetTagValueOrRemove("ai.device.network", value);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000E937 File Offset: 0x0000CB37
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000E949 File Offset: 0x0000CB49
		[Obsolete("Use custom properties.")]
		public string ScreenResolution
		{
			get
			{
				return this.properties.GetTagValueOrNull("ai.device.screenResolution");
			}
			set
			{
				this.properties.SetStringValueOrRemove("ai.device.screenResolution", value);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000E95C File Offset: 0x0000CB5C
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000E96E File Offset: 0x0000CB6E
		[Obsolete("Use custom properties.")]
		public string Language
		{
			get
			{
				return this.properties.GetTagValueOrNull("ai.device.language");
			}
			set
			{
				this.properties.SetStringValueOrRemove("ai.device.language", value);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000E984 File Offset: 0x0000CB84
		internal void UpdateTags(IDictionary<string, string> tags)
		{
			tags.UpdateTagValue(ContextTagKeys.Keys.DeviceType, this.Type);
			tags.UpdateTagValue(ContextTagKeys.Keys.DeviceId, this.Id);
			tags.UpdateTagValue(ContextTagKeys.Keys.DeviceOSVersion, this.OperatingSystem);
			tags.UpdateTagValue(ContextTagKeys.Keys.DeviceOEMName, this.OemName);
			tags.UpdateTagValue(ContextTagKeys.Keys.DeviceModel, this.Model);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000EA00 File Offset: 0x0000CC00
		internal void CopyTo(DeviceContext target)
		{
			Tags.CopyTagValue(this.Type, ref target.type);
			Tags.CopyTagValue(this.Id, ref target.id);
			Tags.CopyTagValue(this.OperatingSystem, ref target.operatingSystem);
			Tags.CopyTagValue(this.OemName, ref target.oemName);
			Tags.CopyTagValue(this.Model, ref target.model);
		}

		// Token: 0x04000155 RID: 341
		private readonly IDictionary<string, string> properties;

		// Token: 0x04000156 RID: 342
		private string type;

		// Token: 0x04000157 RID: 343
		private string id;

		// Token: 0x04000158 RID: 344
		private string operatingSystem;

		// Token: 0x04000159 RID: 345
		private string oemName;

		// Token: 0x0400015A RID: 346
		private string model;
	}
}
