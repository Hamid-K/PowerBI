using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000949 RID: 2377
	[Serializable]
	public class MqException : Exception
	{
		// Token: 0x060043CE RID: 17358 RVA: 0x000E4B72 File Offset: 0x000E2D72
		private MqException(object value, bool isCustomMqClientException)
		{
			this.ExceptionValue = value as Exception;
			this.IsCustomMqClientException = isCustomMqClientException;
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x000E4B8D File Offset: 0x000E2D8D
		protected MqException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ReasonCode = info.GetInt32("ReasonCode");
		}

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x060043D0 RID: 17360 RVA: 0x000E4BA8 File Offset: 0x000E2DA8
		// (set) Token: 0x060043D1 RID: 17361 RVA: 0x000E4BB0 File Offset: 0x000E2DB0
		public bool IsCustomMqClientException { get; private set; }

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x060043D2 RID: 17362 RVA: 0x000E4BB9 File Offset: 0x000E2DB9
		// (set) Token: 0x060043D3 RID: 17363 RVA: 0x000E4BC1 File Offset: 0x000E2DC1
		public Exception ExceptionValue { get; private set; }

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x060043D4 RID: 17364 RVA: 0x000E4BCA File Offset: 0x000E2DCA
		// (set) Token: 0x060043D5 RID: 17365 RVA: 0x000E4BD1 File Offset: 0x000E2DD1
		private static Type RealType { get; set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.CustomMqClientException");

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x060043D6 RID: 17366 RVA: 0x000E4BD9 File Offset: 0x000E2DD9
		// (set) Token: 0x060043D7 RID: 17367 RVA: 0x000E4BFB File Offset: 0x000E2DFB
		public int ReasonCode
		{
			get
			{
				if (!this.IsCustomMqClientException)
				{
					return 0;
				}
				return (int)MqException.ReasonCodeInfo.GetValue(this.ExceptionValue, null);
			}
			set
			{
				if (this.IsCustomMqClientException)
				{
					MqException.ReasonCodeInfo.SetValue(this.ExceptionValue, value, null);
				}
			}
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x060043D8 RID: 17368 RVA: 0x000E4C1C File Offset: 0x000E2E1C
		public override IDictionary Data
		{
			get
			{
				return this.ExceptionValue.Data;
			}
		}

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x060043D9 RID: 17369 RVA: 0x000E4C29 File Offset: 0x000E2E29
		// (set) Token: 0x060043DA RID: 17370 RVA: 0x000E4C36 File Offset: 0x000E2E36
		public override string HelpLink
		{
			get
			{
				return this.ExceptionValue.HelpLink;
			}
			set
			{
				this.ExceptionValue.HelpLink = value;
			}
		}

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x000E4C44 File Offset: 0x000E2E44
		public override string Message
		{
			get
			{
				return this.ExceptionValue.Message;
			}
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x060043DC RID: 17372 RVA: 0x000E4C51 File Offset: 0x000E2E51
		// (set) Token: 0x060043DD RID: 17373 RVA: 0x000E4C5E File Offset: 0x000E2E5E
		public override string Source
		{
			get
			{
				return this.ExceptionValue.Source;
			}
			set
			{
				this.ExceptionValue.Source = value;
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x060043DE RID: 17374 RVA: 0x000E4C6C File Offset: 0x000E2E6C
		public override string StackTrace
		{
			get
			{
				return this.ExceptionValue.StackTrace;
			}
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x000E4C79 File Offset: 0x000E2E79
		public override Exception GetBaseException()
		{
			return this.ExceptionValue.GetBaseException();
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x000E4C86 File Offset: 0x000E2E86
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ReasonCode", this.ReasonCode);
			base.GetObjectData(info, context);
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x000E4CA1 File Offset: 0x000E2EA1
		public override string ToString()
		{
			return this.ExceptionValue.ToString();
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x000E4CAE File Offset: 0x000E2EAE
		public static Exception New(Exception exception)
		{
			if (!SafeExceptions.IsSafeException(exception))
			{
				throw exception;
			}
			return new MqException(exception, exception.GetType() == MqException.RealType);
		}

		// Token: 0x040023C6 RID: 9158
		private const string reasonCodeName = "ReasonCode";

		// Token: 0x040023C7 RID: 9159
		private static readonly PropertyInfo ReasonCodeInfo = MqException.RealType.GetProperty("ReasonCode");
	}
}
