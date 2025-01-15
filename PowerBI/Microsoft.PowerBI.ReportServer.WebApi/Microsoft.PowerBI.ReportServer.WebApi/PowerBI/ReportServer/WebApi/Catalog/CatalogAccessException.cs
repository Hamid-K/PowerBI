using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.PowerBI.ReportServer.WebApi.Catalog
{
	// Token: 0x0200003E RID: 62
	public class CatalogAccessException : Exception
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000071D6 File Offset: 0x000053D6
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000071DE File Offset: 0x000053DE
		public CatalogAccessExceptionErrorCode ErrorCode { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000071E7 File Offset: 0x000053E7
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000071EF File Offset: 0x000053EF
		public HttpStatusCode HttpCode { get; private set; }

		// Token: 0x06000110 RID: 272 RVA: 0x000071F8 File Offset: 0x000053F8
		public CatalogAccessException(string message, CatalogAccessExceptionErrorCode errorCode, Exception innerException)
			: base(message, innerException)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007209 File Offset: 0x00005409
		public CatalogAccessException(string message, CatalogAccessExceptionErrorCode errorCode)
			: base(message)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007219 File Offset: 0x00005419
		public CatalogAccessException(string message, CatalogAccessExceptionErrorCode errorCode, HttpStatusCode code)
			: base(message)
		{
			this.ErrorCode = errorCode;
			this.HttpCode = code;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007230 File Offset: 0x00005430
		public CatalogAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ErrorCode = (CatalogAccessExceptionErrorCode)info.GetValue("ErrorCode", typeof(CatalogAccessExceptionErrorCode));
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000725A File Offset: 0x0000545A
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ErrorCode", this.ErrorCode, typeof(CatalogAccessExceptionErrorCode));
			base.GetObjectData(info, context);
		}
	}
}
