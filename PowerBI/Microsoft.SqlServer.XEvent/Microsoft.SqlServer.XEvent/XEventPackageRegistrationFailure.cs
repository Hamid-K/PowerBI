using System;
using System.Runtime.Serialization;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public class XEventPackageRegistrationFailure : Exception
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00003C74 File Offset: 0x00003C74
		protected XEventPackageRegistrationFailure(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00004FC0 File Offset: 0x00004FC0
		public XEventPackageRegistrationFailure(string packageName, int major, int minor, int state)
			: base(XEventPackageRegistrationFailure.MakeMessage(packageName, major, minor, state))
		{
			this.m_packageName = packageName;
			this.m_major = major;
			this.m_minor = minor;
			this.m_state = state;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00003C20 File Offset: 0x00003C20
		public XEventPackageRegistrationFailure(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00003C04 File Offset: 0x00003C04
		public XEventPackageRegistrationFailure(string message)
			: base(message)
		{
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00003BE8 File Offset: 0x00003BE8
		public XEventPackageRegistrationFailure()
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00003E28 File Offset: 0x00003E28
		public virtual string PackageName
		{
			get
			{
				return this.m_packageName;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00003E44 File Offset: 0x00003E44
		public virtual int Major
		{
			get
			{
				return this.m_major;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00003E60 File Offset: 0x00003E60
		public virtual int Minor
		{
			get
			{
				return this.m_minor;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00003E7C File Offset: 0x00003E7C
		public virtual int State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005000 File Offset: 0x00005000
		public override string ToString()
		{
			if (this.m_packageName != null)
			{
				return XEventPackageRegistrationFailure.MakeMessage(this.m_packageName, this.m_major, this.m_minor, this.m_state);
			}
			return this.Message;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00003E98 File Offset: 0x00003E98
		protected static string MakeMessage(string packageName, int major, int minor, int state)
		{
			int num;
			if (major != 0 && minor != 0)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			if ((byte)num != 0)
			{
				object[] array = new object[] { packageName, major, minor, state };
				return string.Format(ResourcesMgr.GetString("PackageRegistrationFailureExceptionExtended"), array);
			}
			return string.Format(ResourcesMgr.GetString("PackageRegistrationFailureException"), packageName);
		}

		// Token: 0x0400014B RID: 331
		protected string m_packageName;

		// Token: 0x0400014C RID: 332
		protected int m_major;

		// Token: 0x0400014D RID: 333
		protected int m_minor;

		// Token: 0x0400014E RID: 334
		protected int m_state;
	}
}
