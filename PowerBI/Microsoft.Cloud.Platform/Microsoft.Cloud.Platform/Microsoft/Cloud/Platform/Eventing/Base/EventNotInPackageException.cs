using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003D4 RID: 980
	[Serializable]
	public class EventNotInPackageException : MonitoredException
	{
		// Token: 0x06001E43 RID: 7747 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public EventNotInPackageException()
		{
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0000EB7D File Offset: 0x0000CD7D
		public EventNotInPackageException(string message)
			: base(message)
		{
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0000EB86 File Offset: 0x0000CD86
		public EventNotInPackageException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		protected EventNotInPackageException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x000721EA File Offset: 0x000703EA
		public EventNotInPackageException(EventIdentifier eid, Package package)
		{
			this.m_eid = eid;
			this.m_pmd = ((package != null) ? package.Metadata : null);
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0007220C File Offset: 0x0007040C
		public override string Message
		{
			get
			{
				if (this.m_pmd == null)
				{
					return string.Format(CultureInfo.CurrentCulture, "Event '{0}' cannot be located in any of the loaded packages. Please ensure that the assembly containing the package is loaded in the appdomain", new object[] { this.m_eid });
				}
				return string.Format(CultureInfo.CurrentCulture, "Event '{0}' is not a member of package '{1}'", new object[] { this.m_eid, this.m_pmd });
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00072268 File Offset: 0x00070468
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("package_metadata", this.m_pmd, typeof(PackageMetadata));
				info.AddValue("event_id", this.m_eid, typeof(EventIdentifier));
			}
		}

		// Token: 0x04000A64 RID: 2660
		private EventIdentifier m_eid;

		// Token: 0x04000A65 RID: 2661
		private PackageMetadata m_pmd;
	}
}
