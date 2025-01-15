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
	// Token: 0x0200006B RID: 107
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class AdomdDataAccessConnectionException : AdomdDataAccessException
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00020851 File Offset: 0x0001EA51
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00020859 File Offset: 0x0001EA59
		public int ConnectionExceptionCause
		{
			get
			{
				return this.m_connectionExceptionCause;
			}
			protected set
			{
				this.m_connectionExceptionCause = value;
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00020862 File Offset: 0x0001EA62
		public AdomdDataAccessConnectionException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<int>();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00020876 File Offset: 0x0001EA76
		public AdomdDataAccessConnectionException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0002088C File Offset: 0x0001EA8C
		public AdomdDataAccessConnectionException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000208AF File Offset: 0x0001EAAF
		public AdomdDataAccessConnectionException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000208D4 File Offset: 0x0001EAD4
		protected AdomdDataAccessConnectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AdomdDataAccessConnectionException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConnectionExceptionCause = (int)info.GetValue("AdomdDataAccessConnectionException_ConnectionExceptionCause", typeof(int));
			this.ConstructorInternal(true);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00020958 File Offset: 0x0001EB58
		public AdomdDataAccessConnectionException(int connectionExceptionCause, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConnectionExceptionCause = connectionExceptionCause;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00020975 File Offset: 0x0001EB75
		public AdomdDataAccessConnectionException(int connectionExceptionCause, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConnectionExceptionCause = connectionExceptionCause;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0002099F File Offset: 0x0001EB9F
		public AdomdDataAccessConnectionException(int connectionExceptionCause, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConnectionExceptionCause = connectionExceptionCause;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000209CB File Offset: 0x0001EBCB
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000209D4 File Offset: 0x0001EBD4
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000209DC File Offset: 0x0001EBDC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(AdomdDataAccessConnectionException))
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

		// Token: 0x060007AF RID: 1967 RVA: 0x00020A90 File Offset: 0x0001EC90
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AdomdDataAccessConnectionException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("AdomdDataAccessConnectionException_ConnectionExceptionCause", this.ConnectionExceptionCause, typeof(int));
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00020AEB File Offset: 0x0001ECEB
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "AdomdException encountered while connecting to the target data source.", Array.Empty<object>());
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00020B01 File Offset: 0x0001ED01
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

		// Token: 0x060007B2 RID: 1970 RVA: 0x00020B1E File Offset: 0x0001ED1E
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00020B3C File Offset: 0x0001ED3C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ConnectionExceptionCause={0}", this.ConnectionExceptionCause.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ConnectionExceptionCause={0}", this.ConnectionExceptionCause.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "ConnectionExceptionCause={0}", this.ConnectionExceptionCause.ToString(CultureInfo.InvariantCulture))));
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00020BE5 File Offset: 0x0001EDE5
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00020BEE File Offset: 0x0001EDEE
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00020BF7 File Offset: 0x0001EDF7
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00020C00 File Offset: 0x0001EE00
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

		// Token: 0x060007B8 RID: 1976 RVA: 0x00020DC4 File Offset: 0x0001EFC4
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00020DD0 File Offset: 0x0001EFD0
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

		// Token: 0x060007BA RID: 1978 RVA: 0x00020E54 File Offset: 0x0001F054
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002A8 RID: 680
		private string creationMessage;

		// Token: 0x040002A9 RID: 681
		private int m_connectionExceptionCause;
	}
}
