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
	// Token: 0x0200002E RID: 46
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class PipelineJsonDeserializationException : GatewayPipelineException
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000B54D File Offset: 0x0000974D
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000B555 File Offset: 0x00009755
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

		// Token: 0x06000254 RID: 596 RVA: 0x0000B55E File Offset: 0x0000975E
		public PipelineJsonDeserializationException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B572 File Offset: 0x00009772
		public PipelineJsonDeserializationException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B588 File Offset: 0x00009788
		public PipelineJsonDeserializationException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B5AB File Offset: 0x000097AB
		public PipelineJsonDeserializationException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B5D4 File Offset: 0x000097D4
		protected PipelineJsonDeserializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("PipelineJsonDeserializationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.TypeFullName = (string)info.GetValue("PipelineJsonDeserializationException_TypeFullName", typeof(string));
			}
			catch (SerializationException)
			{
				this.TypeFullName = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B670 File Offset: 0x00009870
		public PipelineJsonDeserializationException(string typeFullName, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.TypeFullName = typeFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B69A File Offset: 0x0000989A
		public PipelineJsonDeserializationException(string typeFullName, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.TypeFullName = typeFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B6CB File Offset: 0x000098CB
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_PipelineJsonDeserializationError";
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B6E2 File Offset: 0x000098E2
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B6EA File Offset: 0x000098EA
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B6F0 File Offset: 0x000098F0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(PipelineJsonDeserializationException))
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

		// Token: 0x0600025F RID: 607 RVA: 0x0000B7A4 File Offset: 0x000099A4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("PipelineJsonDeserializationException_creationMessage", this.creationMessage, typeof(string));
			if (this.TypeFullName != null)
			{
				info.AddValue("PipelineJsonDeserializationException_TypeFullName", this.TypeFullName, typeof(string));
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B804 File Offset: 0x00009A04
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Deserialization failed for '{0}'.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty) : ((this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty)));
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000B879 File Offset: 0x00009A79
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

		// Token: 0x06000262 RID: 610 RVA: 0x0000B896 File Offset: 0x00009A96
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "TypeFullName={0}", (this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "TypeFullName={0}", (this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "TypeFullName={0}", (this.TypeFullName != null) ? this.TypeFullName.ToString() : string.Empty)));
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B972 File Offset: 0x00009B72
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B97B File Offset: 0x00009B7B
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B984 File Offset: 0x00009B84
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B990 File Offset: 0x00009B90
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

		// Token: 0x06000268 RID: 616 RVA: 0x0000BB54 File Offset: 0x00009D54
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BB60 File Offset: 0x00009D60
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

		// Token: 0x0600026A RID: 618 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000253 RID: 595
		private string creationMessage;

		// Token: 0x04000254 RID: 596
		private string m_typeFullName;
	}
}
