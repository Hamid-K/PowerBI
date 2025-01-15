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
	// Token: 0x02000083 RID: 131
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class FileLoadDataAccessException : DataAccessException
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x00028ACD File Offset: 0x00026CCD
		public FileLoadDataAccessException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00028ADC File Offset: 0x00026CDC
		public FileLoadDataAccessException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00028AF2 File Offset: 0x00026CF2
		public FileLoadDataAccessException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00028B15 File Offset: 0x00026D15
		public FileLoadDataAccessException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00028B3C File Offset: 0x00026D3C
		protected FileLoadDataAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FileLoadDataAccessException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00028BA0 File Offset: 0x00026DA0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_FileLoadDataAccessError";
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00028BB7 File Offset: 0x00026DB7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00028BBF File Offset: 0x00026DBF
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00028BC4 File Offset: 0x00026DC4
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FileLoadDataAccessException))
			{
				TraceSourceBase<DiagnosticsTraceSource>.Tracer.TraceError("Exception object created [IsBenign={0}]: {1}: {2}; ErrorShortName: {3}", new object[]
				{
					this.IsBenign(),
					type,
					this.GetMarkedUpMessage(),
					this.ErrorShortName
				});
				IList<PowerBIErrorDetail> errorDetails = this.GetErrorDetails();
				if (errorDetails != null && errorDetails.Count > 0)
				{
					for (int i = 0; i < errorDetails.Count; i++)
					{
						PowerBIErrorDetail powerBIErrorDetail = errorDetails[i];
						TraceSourceBase<DiagnosticsTraceSource>.Tracer.TraceError("Exception data: {0} = {1}", new object[]
						{
							powerBIErrorDetail.NameCode,
							powerBIErrorDetail.Value.ResourceValue.MarkAsCustomerContent()
						});
					}
				}
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

		// Token: 0x060009BF RID: 2495 RVA: 0x00028CD7 File Offset: 0x00026ED7
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FileLoadDataAccessException_creationMessage", this.creationMessage, typeof(string));
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00028D07 File Offset: 0x00026F07
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "FileLoadDataAccessException encountered while accessing the target data source.", Array.Empty<object>());
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00028D1D File Offset: 0x00026F1D
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

		// Token: 0x060009C2 RID: 2498 RVA: 0x00028D3A File Offset: 0x00026F3A
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00028D57 File Offset: 0x00026F57
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00028D66 File Offset: 0x00026F66
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00028D6F File Offset: 0x00026F6F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00028D78 File Offset: 0x00026F78
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00028D84 File Offset: 0x00026F84
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

		// Token: 0x060009C8 RID: 2504 RVA: 0x00028F48 File Offset: 0x00027148
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00028F54 File Offset: 0x00027154
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

		// Token: 0x060009CA RID: 2506 RVA: 0x00028FD8 File Offset: 0x000271D8
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002C9 RID: 713
		private string creationMessage;
	}
}
