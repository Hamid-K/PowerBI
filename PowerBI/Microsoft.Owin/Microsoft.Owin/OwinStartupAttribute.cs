using System;

namespace Microsoft.Owin
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class OwinStartupAttribute : Attribute
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00002712 File Offset: 0x00000912
		public OwinStartupAttribute(Type startupType)
			: this(string.Empty, startupType, string.Empty)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002725 File Offset: 0x00000925
		public OwinStartupAttribute(string friendlyName, Type startupType)
			: this(friendlyName, startupType, string.Empty)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002734 File Offset: 0x00000934
		public OwinStartupAttribute(Type startupType, string methodName)
			: this(string.Empty, startupType, methodName)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002744 File Offset: 0x00000944
		public OwinStartupAttribute(string friendlyName, Type startupType, string methodName)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			if (startupType == null)
			{
				throw new ArgumentNullException("startupType");
			}
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			this.FriendlyName = friendlyName;
			this.StartupType = startupType;
			this.MethodName = methodName;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000279C File Offset: 0x0000099C
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000027A4 File Offset: 0x000009A4
		public string FriendlyName { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000027AD File Offset: 0x000009AD
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000027B5 File Offset: 0x000009B5
		public Type StartupType { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000027BE File Offset: 0x000009BE
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000027C6 File Offset: 0x000009C6
		public string MethodName { get; private set; }
	}
}
