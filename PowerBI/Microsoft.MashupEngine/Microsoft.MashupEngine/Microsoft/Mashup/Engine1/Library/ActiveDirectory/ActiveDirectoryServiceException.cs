using System;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FF4 RID: 4084
	[Serializable]
	public class ActiveDirectoryServiceException : Exception
	{
		// Token: 0x06006B20 RID: 27424 RVA: 0x00171198 File Offset: 0x0016F398
		public ActiveDirectoryServiceException(COMException innerException)
			: base(innerException.Message, innerException)
		{
			this.ErrorCode = innerException.ErrorCode;
			DirectoryServicesCOMException ex = innerException as DirectoryServicesCOMException;
			if (ex != null)
			{
				this.ExtendedErrorCode = new int?(ex.ExtendedError);
			}
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x001711D9 File Offset: 0x0016F3D9
		public ActiveDirectoryServiceException(int errorCode, int? extendedErrorCode)
		{
			this.ErrorCode = errorCode;
			this.ExtendedErrorCode = extendedErrorCode;
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x001711EF File Offset: 0x0016F3EF
		protected ActiveDirectoryServiceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ErrorCode = info.GetInt32("ErrorCode");
			if (info.GetBoolean("HasExtendedErrorCode"))
			{
				this.ExtendedErrorCode = new int?(info.GetInt32("ExtendedErrorCode"));
			}
		}

		// Token: 0x17001EA7 RID: 7847
		// (get) Token: 0x06006B23 RID: 27427 RVA: 0x0017122D File Offset: 0x0016F42D
		// (set) Token: 0x06006B24 RID: 27428 RVA: 0x00171235 File Offset: 0x0016F435
		public int ErrorCode { get; private set; }

		// Token: 0x17001EA8 RID: 7848
		// (get) Token: 0x06006B25 RID: 27429 RVA: 0x0017123E File Offset: 0x0016F43E
		// (set) Token: 0x06006B26 RID: 27430 RVA: 0x00171246 File Offset: 0x0016F446
		public int? ExtendedErrorCode { get; private set; }

		// Token: 0x06006B27 RID: 27431 RVA: 0x00171250 File Offset: 0x0016F450
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ErrorCode", this.ErrorCode);
			info.AddValue("HasExtendedErrorCode", this.ExtendedErrorCode != null);
			if (this.ExtendedErrorCode != null)
			{
				info.AddValue("ExtendedErrorCode", this.ExtendedErrorCode.Value);
			}
			base.GetObjectData(info, context);
		}

		// Token: 0x04003B92 RID: 15250
		private const string errorCodeName = "ErrorCode";

		// Token: 0x04003B93 RID: 15251
		private const string hasExtendedErrorCodeName = "HasExtendedErrorCode";

		// Token: 0x04003B94 RID: 15252
		private const string extendedErrorCodeName = "ExtendedErrorCode";
	}
}
