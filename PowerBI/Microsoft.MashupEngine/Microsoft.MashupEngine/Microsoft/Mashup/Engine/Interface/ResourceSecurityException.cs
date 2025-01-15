using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	public abstract class ResourceSecurityException : RuntimeException
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x00005A75 File Offset: 0x00003C75
		protected ResourceSecurityException(IResource origin, IResource resource, string message, string userMessage, Exception innerException)
			: base(message, innerException)
		{
			this.resourceOrigin = origin;
			this.resource = resource;
			this.userMessage = userMessage;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00005A96 File Offset: 0x00003C96
		protected ResourceSecurityException(string dataSourceLocationOrigin, IResource origin, string dataSourceLocation, IResource resource, string message, string userMessage, Exception innerException)
			: this(origin, resource, message, userMessage, innerException)
		{
			this.dataSourceLocationOrigin = dataSourceLocationOrigin;
			this.dataSourceLocation = dataSourceLocation;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00005AB8 File Offset: 0x00003CB8
		protected ResourceSecurityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.resource = new ResourceSecurityException.SerializedResource(info.GetString("resourceKind"), info.GetString("resourcePath"), info.GetString("resourceNonNormalizedPath"));
			this.dataSourceLocation = info.GetString("dataSourceLocation");
			if (info.GetBoolean("hasOrigin"))
			{
				this.resourceOrigin = new ResourceSecurityException.SerializedResource(info.GetString("resourceOriginKind"), info.GetString("resourceOriginPath"), info.GetString("resourceOriginNonNormalizedPath"));
				this.dataSourceLocationOrigin = info.GetString("dataSourceLocationOrigin");
			}
			try
			{
				this.userMessage = (string)info.GetValue("userMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.userMessage = null;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00005B94 File Offset: 0x00003D94
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00005B9C File Offset: 0x00003D9C
		public IResource ResourceOrigin
		{
			get
			{
				return this.resourceOrigin;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public string DataSourceLocation
		{
			get
			{
				return this.dataSourceLocation;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00005BAC File Offset: 0x00003DAC
		public string DataSourceLocationOrigin
		{
			get
			{
				return this.dataSourceLocationOrigin;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public string UserMessage
		{
			get
			{
				return this.userMessage;
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00005BBC File Offset: 0x00003DBC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("resourceKind", this.resource.Kind);
			info.AddValue("resourcePath", this.resource.Path);
			info.AddValue("resourceNonNormalizedPath", this.resource.NonNormalizedPath);
			info.AddValue("dataSourceLocation", this.dataSourceLocation);
			info.AddValue("userMessage", this.userMessage);
			if (this.resourceOrigin == null)
			{
				info.AddValue("hasOrigin", false);
			}
			else
			{
				info.AddValue("hasOrigin", true);
				info.AddValue("resourceOriginKind", this.resourceOrigin.Kind);
				info.AddValue("resourceOriginPath", this.resourceOrigin.Path);
				info.AddValue("resourceOriginNonNormalizedPath", this.resourceOrigin.NonNormalizedPath);
				info.AddValue("dataSourceLocationOrigin", this.dataSourceLocation);
			}
			base.GetObjectData(info, context);
		}

		// Token: 0x0400027F RID: 639
		private const string ResourceKindKey = "resourceKind";

		// Token: 0x04000280 RID: 640
		private const string ResourcePathKey = "resourcePath";

		// Token: 0x04000281 RID: 641
		private const string ResourceNonNormalizedPathKey = "resourceNonNormalizedPath";

		// Token: 0x04000282 RID: 642
		private const string DataSourceLocationKey = "dataSourceLocation";

		// Token: 0x04000283 RID: 643
		private const string HasOriginKey = "hasOrigin";

		// Token: 0x04000284 RID: 644
		private const string ResourceOriginKindKey = "resourceOriginKind";

		// Token: 0x04000285 RID: 645
		private const string ResourceOriginPathKey = "resourceOriginPath";

		// Token: 0x04000286 RID: 646
		private const string ResourceOriginNonNormalizedPathKey = "resourceOriginNonNormalizedPath";

		// Token: 0x04000287 RID: 647
		private const string DataSourceLocationOriginKey = "dataSourceLocationOrigin";

		// Token: 0x04000288 RID: 648
		private const string UserMessageKey = "userMessage";

		// Token: 0x04000289 RID: 649
		private readonly IResource resource;

		// Token: 0x0400028A RID: 650
		private readonly string dataSourceLocation;

		// Token: 0x0400028B RID: 651
		private readonly IResource resourceOrigin;

		// Token: 0x0400028C RID: 652
		private readonly string dataSourceLocationOrigin;

		// Token: 0x0400028D RID: 653
		private readonly string userMessage;

		// Token: 0x02000108 RID: 264
		private class SerializedResource : IResource
		{
			// Token: 0x06000447 RID: 1095 RVA: 0x00005CAA File Offset: 0x00003EAA
			public SerializedResource(string kind, string path, string nonNormalizedPath)
			{
				this.kind = kind;
				this.path = path;
				this.nonNormalizedPath = nonNormalizedPath;
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000448 RID: 1096 RVA: 0x00005CC7 File Offset: 0x00003EC7
			public string Kind
			{
				get
				{
					return this.kind;
				}
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x06000449 RID: 1097 RVA: 0x00005CCF File Offset: 0x00003ECF
			public string Path
			{
				get
				{
					return this.path;
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x0600044A RID: 1098 RVA: 0x00005CD7 File Offset: 0x00003ED7
			public string NonNormalizedPath
			{
				get
				{
					return this.nonNormalizedPath;
				}
			}

			// Token: 0x0400028E RID: 654
			private readonly string kind;

			// Token: 0x0400028F RID: 655
			private readonly string path;

			// Token: 0x04000290 RID: 656
			private readonly string nonNormalizedPath;
		}
	}
}
