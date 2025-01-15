using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Eventing.Etw
{
	// Token: 0x020003DC RID: 988
	[Serializable]
	public class EtwException : Win32Exception
	{
		// Token: 0x06001E6B RID: 7787 RVA: 0x000729E3 File Offset: 0x00070BE3
		public EtwException()
		{
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x000729EB File Offset: 0x00070BEB
		public EtwException(string message)
			: base(message)
		{
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000729F4 File Offset: 0x00070BF4
		public EtwException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000729FE File Offset: 0x00070BFE
		protected EtwException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00072A08 File Offset: 0x00070C08
		public EtwException(int rc, string nativeFunction)
			: base(rc)
		{
			this.m_nativeFunction = nativeFunction;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x00072A18 File Offset: 0x00070C18
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("native_function", this.m_nativeFunction, typeof(string));
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x00072A40 File Offset: 0x00070C40
		public override string Message
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, "{0} code: {1}, native function: {2}", new object[] { base.Message, base.NativeErrorCode, this.m_nativeFunction });
			}
		}

		// Token: 0x04000AB1 RID: 2737
		private string m_nativeFunction;
	}
}
