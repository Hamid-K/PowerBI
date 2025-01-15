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
	// Token: 0x0200002D RID: 45
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class PipelineJsonSerializationException : GatewayPipelineException
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000AE89 File Offset: 0x00009089
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000AE91 File Offset: 0x00009091
		public string TypeFullName
		{
			get
			{
				return this.m_typeFullName;
			}
			protected set
			{
				this.m_typeFullName = value;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000AE9A File Offset: 0x0000909A
		public PipelineJsonSerializationException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000AEAE File Offset: 0x000090AE
		public PipelineJsonSerializationException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000AEC4 File Offset: 0x000090C4
		public PipelineJsonSerializationException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000AEE7 File Offset: 0x000090E7
		public PipelineJsonSerializationException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000AF10 File Offset: 0x00009110
		protected PipelineJsonSerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("PipelineJsonSerializationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.TypeFullName = (string)info.GetValue("PipelineJsonSerializationException_TypeFullName", typeof(string));
			}
			catch (SerializationException)
			{
				this.TypeFullName = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000AFAC File Offset: 0x000091AC
		public PipelineJsonSerializationException(string typeFullName, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.TypeFullName = typeFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000AFD6 File Offset: 0x000091D6
		public PipelineJsonSerializationException(string typeFullName, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.TypeFullName = typeFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B007 File Offset: 0x00009207
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_PipelineJsonSerializationError";
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B01E File Offset: 0x0000921E
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B028 File Offset: 0x00009228
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(PipelineJsonSerializationException))
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

		// Token: 0x06000246 RID: 582 RVA: 0x0000B0DC File Offset: 0x000092DC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("PipelineJsonSerializationException_creationMessage", this.creationMessage, typeof(string));
			if (this.TypeFullName != null)
			{
				info.AddValue("PipelineJsonSerializationException_TypeFullName", this.TypeFullName, typeof(string));
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B13C File Offset: 0x0000933C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Serialization failed for '{0}'.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty) : ((this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty)));
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000B1B1 File Offset: 0x000093B1
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

		// Token: 0x06000249 RID: 585 RVA: 0x0000B1CE File Offset: 0x000093CE
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B1EC File Offset: 0x000093EC
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "TypeFullName={0}", (this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "TypeFullName={0}", (this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "TypeFullName={0}", (this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty)));
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B2AA File Offset: 0x000094AA
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B2B3 File Offset: 0x000094B3
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B2BC File Offset: 0x000094BC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B2C8 File Offset: 0x000094C8
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

		// Token: 0x0600024F RID: 591 RVA: 0x0000B48C File Offset: 0x0000968C
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B498 File Offset: 0x00009698
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

		// Token: 0x06000251 RID: 593 RVA: 0x0000B51C File Offset: 0x0000971C
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000251 RID: 593
		private string creationMessage;

		// Token: 0x04000252 RID: 594
		private string m_typeFullName;
	}
}
