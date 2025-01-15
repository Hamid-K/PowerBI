using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003D1 RID: 977
	[Serializable]
	public class PackageNotFoundException : MonitoredException
	{
		// Token: 0x06001E30 RID: 7728 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public PackageNotFoundException()
		{
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0000EB7D File Offset: 0x0000CD7D
		public PackageNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0000EB86 File Offset: 0x0000CD86
		public PackageNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		protected PackageNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0007210B File Offset: 0x0007030B
		public PackageNotFoundException(Guid packageId)
		{
			this.m_packageId = packageId;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0007211A File Offset: 0x0007031A
		public PackageNotFoundException(Guid packageId, Exception inner)
			: base(packageId.ToString(), inner)
		{
			this.m_packageId = packageId;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x00072137 File Offset: 0x00070337
		public override string Message
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, "Package with id '{0}' is not registered with the package manager", new object[] { this.m_packageId });
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0007215C File Offset: 0x0007035C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("package_id", this.m_packageId, typeof(Guid));
			}
		}

		// Token: 0x04000A62 RID: 2658
		private Guid m_packageId;
	}
}
