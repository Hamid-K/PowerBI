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
	// Token: 0x020000C8 RID: 200
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class InvalidServiceBusBindingException : GatewayPipelineException
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00042E39 File Offset: 0x00041039
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x00042E41 File Offset: 0x00041041
		public string BindingName
		{
			get
			{
				return this.m_bindingName;
			}
			protected set
			{
				this.m_bindingName = value;
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00042E4A File Offset: 0x0004104A
		public InvalidServiceBusBindingException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00042E5E File Offset: 0x0004105E
		public InvalidServiceBusBindingException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00042E74 File Offset: 0x00041074
		public InvalidServiceBusBindingException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00042E97 File Offset: 0x00041097
		public InvalidServiceBusBindingException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00042EBC File Offset: 0x000410BC
		protected InvalidServiceBusBindingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidServiceBusBindingException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.BindingName = (string)info.GetValue("InvalidServiceBusBindingException_BindingName", typeof(string));
			}
			catch (SerializationException)
			{
				this.BindingName = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00042F58 File Offset: 0x00041158
		public InvalidServiceBusBindingException(string bindingName, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.BindingName = bindingName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00042F82 File Offset: 0x00041182
		public InvalidServiceBusBindingException(string bindingName, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.BindingName = bindingName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00042FAE File Offset: 0x000411AE
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidServiceBusBindingError";
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00042FC5 File Offset: 0x000411C5
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00042FD0 File Offset: 0x000411D0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidServiceBusBindingException))
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

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00043084 File Offset: 0x00041284
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidServiceBusBindingException_creationMessage", this.creationMessage, typeof(string));
			if (this.BindingName != null)
			{
				info.AddValue("InvalidServiceBusBindingException_BindingName", this.BindingName, typeof(string));
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000430E4 File Offset: 0x000412E4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Error resolving the service bus binding with name {0}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.BindingName != null) ? this.BindingName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.BindingName != null) ? this.BindingName.ToString() : string.Empty) : ((this.BindingName != null) ? this.BindingName.ToString() : string.Empty)));
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00043159 File Offset: 0x00041359
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

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00043176 File Offset: 0x00041376
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00043194 File Offset: 0x00041394
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "BindingName={0}", (this.BindingName != null) ? this.BindingName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "BindingName={0}", (this.BindingName != null) ? this.BindingName.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "BindingName={0}", (this.BindingName != null) ? this.BindingName.ToString() : string.Empty)));
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00043252 File Offset: 0x00041452
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0004325B File Offset: 0x0004145B
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00043264 File Offset: 0x00041464
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00043270 File Offset: 0x00041470
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

		// Token: 0x06000FFD RID: 4093 RVA: 0x00043434 File Offset: 0x00041634
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00043440 File Offset: 0x00041640
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

		// Token: 0x06000FFF RID: 4095 RVA: 0x000434C4 File Offset: 0x000416C4
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400033A RID: 826
		private string creationMessage;

		// Token: 0x0400033B RID: 827
		private string m_bindingName;
	}
}
