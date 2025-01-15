using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x0200006C RID: 108
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class AdomdDataAccessErrorResponseException : AdomdDataAccessException
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00020E85 File Offset: 0x0001F085
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x00020E8D File Offset: 0x0001F08D
		public int ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
			protected set
			{
				this.m_errorCode = value;
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00020E96 File Offset: 0x0001F096
		public AdomdDataAccessErrorResponseException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<int>();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00020EAA File Offset: 0x0001F0AA
		public AdomdDataAccessErrorResponseException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		public AdomdDataAccessErrorResponseException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00020EE3 File Offset: 0x0001F0E3
		public AdomdDataAccessErrorResponseException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00020F08 File Offset: 0x0001F108
		protected AdomdDataAccessErrorResponseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AdomdDataAccessErrorResponseException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ErrorCode = (int)info.GetValue("AdomdDataAccessErrorResponseException_ErrorCode", typeof(int));
			this.ConstructorInternal(true);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00020F8C File Offset: 0x0001F18C
		public AdomdDataAccessErrorResponseException(int errorCode, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ErrorCode = errorCode;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00020FA9 File Offset: 0x0001F1A9
		public AdomdDataAccessErrorResponseException(int errorCode, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ErrorCode = errorCode;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00020FD3 File Offset: 0x0001F1D3
		public AdomdDataAccessErrorResponseException(int errorCode, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ErrorCode = errorCode;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00020FFF File Offset: 0x0001F1FF
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00021008 File Offset: 0x0001F208
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00021010 File Offset: 0x0001F210
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00021014 File Offset: 0x0001F214
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(AdomdDataAccessErrorResponseException))
			{
				TraceSourceBase<DiagnosticsTraceSource>.Tracer.TraceError("Exception object created [IsBenign={0}]: {1}: {2}; ErrorShortName: {3}", new object[]
				{
					this.IsBenign(),
					type,
					this.GetMarkedUpMessage(),
					this.ErrorShortName
				});
				bool flag = base.InnerException != null && base.InnerException is GatewayPipelineException;
				if (TraceSourceBase<DiagnosticsTraceSource>.Tracer.ShouldTrace(PipelineTraceVerbosity.Error) && (base.InnerException == null || !flag))
				{
					TraceSourceBase<DiagnosticsTraceSource>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000210C8 File Offset: 0x0001F2C8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AdomdDataAccessErrorResponseException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("AdomdDataAccessErrorResponseException_ErrorCode", this.ErrorCode, typeof(int));
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00021123 File Offset: 0x0001F323
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "AdomdException encountered while accessing the target data source.", Array.Empty<object>());
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00021139 File Offset: 0x0001F339
		public override string Message
		{
			get
			{
				if (!string.IsNullOrEmpty(this.creationMessage))
				{
					return this.creationMessage;
				}
				return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.None);
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00021156 File Offset: 0x0001F356
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00021174 File Offset: 0x0001F374
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ErrorCode={0}", this.ErrorCode.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ErrorCode={0}", this.ErrorCode.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "ErrorCode={0}", this.ErrorCode.ToString(CultureInfo.InvariantCulture))));
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002121D File Offset: 0x0001F41D
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00021226 File Offset: 0x0001F426
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0002122F File Offset: 0x0001F42F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00021238 File Offset: 0x0001F438
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + GatewayExceptionUtils.ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
			string text2 = this.CreateMessageFromTemplate(markupKind);
			string text3 = text + ": ";
			if (string.IsNullOrEmpty(this.creationMessage))
			{
				text3 += text2;
			}
			else
			{
				if (markupKind == PrivateInformationMarkupKind.Private || markupKind == PrivateInformationMarkupKind.Internal)
				{
					text3 += this.creationMessage.ObfuscatePrivateValue(true);
				}
				else
				{
					text3 += this.creationMessage;
				}
				if (!string.Equals(this.creationMessage, text2))
				{
					text3 = text3 + Environment.NewLine + "  TemplateMessage: " + text2;
				}
			}
			text3 += this.GetPropertiesString(markupKind);
			if (base.InnerException != null)
			{
				try
				{
					GatewayExceptionUtils.ExceptionsTemplateHelper.IncrementMagicLevel();
					IContainsPrivateInformation containsPrivateInformation = base.InnerException as IContainsPrivateInformation;
					string text4;
					if (markupKind == PrivateInformationMarkupKind.None)
					{
						text4 = ((containsPrivateInformation == null) ? base.InnerException.ToString() : containsPrivateInformation.ToOriginalString());
					}
					else
					{
						text4 = ((containsPrivateInformation == null) ? (GatewayExceptionUtils.InnerExceptionStringCreator.CreateInnerExceptionStack(base.InnerException) + base.InnerException.ToString().MarkAsCustomerContent()) : containsPrivateInformation.ToPrivateString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---"
					});
				}
				finally
				{
					GatewayExceptionUtils.ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = string.Concat(new string[]
				{
					text3,
					Environment.NewLine,
					"  (",
					text,
					".StackTrace:)"
				});
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000213FC File Offset: 0x0001F5FC
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00021408 File Offset: 0x0001F608
		internal override IDictionary<string, string> GetClientErrorParameters(bool includeInner)
		{
			IDictionary<string, string> clientErrorParameters = base.GetClientErrorParameters(true);
			if (includeInner)
			{
				GatewayPipelineException ex = base.InnerException as GatewayPipelineException;
				if (ex != null)
				{
					IDictionary<string, string> clientErrorParameters2 = ex.GetClientErrorParameters();
					foreach (string text in clientErrorParameters2.Keys)
					{
						if (!clientErrorParameters.ContainsKey(text))
						{
							clientErrorParameters[text] = clientErrorParameters2[text];
						}
					}
				}
			}
			return clientErrorParameters;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002148C File Offset: 0x0001F68C
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002AA RID: 682
		private string creationMessage;

		// Token: 0x040002AB RID: 683
		private int m_errorCode;
	}
}
