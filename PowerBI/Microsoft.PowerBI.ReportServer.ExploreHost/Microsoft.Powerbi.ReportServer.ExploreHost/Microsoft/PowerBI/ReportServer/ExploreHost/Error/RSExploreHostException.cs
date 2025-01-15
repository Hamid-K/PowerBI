using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.PowerBI.ReportServer.ExploreHost.Error
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class RSExploreHostException : Exception
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003F44 File Offset: 0x00002144
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00003F4C File Offset: 0x0000214C
		public string Code { get; private set; }

		// Token: 0x060000E3 RID: 227 RVA: 0x00003F55 File Offset: 0x00002155
		public RSExploreHostException(string code, string message, Exception inner)
			: base(message, inner)
		{
			this.Code = code;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003F66 File Offset: 0x00002166
		protected RSExploreHostException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Code = info.GetString("Code");
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003F81 File Offset: 0x00002181
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Code", this.Code);
			base.GetObjectData(info, context);
		}
	}
}
