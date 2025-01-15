using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003D3 RID: 979
	[Serializable]
	public class PackageRegisteredException : MonitoredException
	{
		// Token: 0x06001E3C RID: 7740 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public PackageRegisteredException()
		{
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0000EB7D File Offset: 0x0000CD7D
		public PackageRegisteredException(string message)
			: base(message)
		{
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x0000EB86 File Offset: 0x0000CD86
		public PackageRegisteredException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		protected PackageRegisteredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00072189 File Offset: 0x00070389
		public PackageRegisteredException(IPackage package)
		{
			this.m_package = package;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x00072198 File Offset: 0x00070398
		public override string Message
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, "Package '{0}' is already registered. This is an internal coding error", new object[] { this.m_package.Metadata });
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000721BD File Offset: 0x000703BD
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("package_metadata", this.m_package.Metadata, typeof(PackageMetadata));
			}
		}

		// Token: 0x04000A63 RID: 2659
		private IPackage m_package;
	}
}
