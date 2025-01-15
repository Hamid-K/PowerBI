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
	// Token: 0x0200002B RID: 43
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class DuplicateStatefulPoolObjectReturnException : GatewayPipelineException
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A2E1 File Offset: 0x000084E1
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000A2E9 File Offset: 0x000084E9
		public string PoolObjectKey
		{
			get
			{
				return this.m_poolObjectKey;
			}
			protected set
			{
				this.m_poolObjectKey = value;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000A2F2 File Offset: 0x000084F2
		public DuplicateStatefulPoolObjectReturnException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A306 File Offset: 0x00008506
		public DuplicateStatefulPoolObjectReturnException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A31C File Offset: 0x0000851C
		public DuplicateStatefulPoolObjectReturnException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A33F File Offset: 0x0000853F
		public DuplicateStatefulPoolObjectReturnException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A364 File Offset: 0x00008564
		protected DuplicateStatefulPoolObjectReturnException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DuplicateStatefulPoolObjectReturnException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.PoolObjectKey = (string)info.GetValue("DuplicateStatefulPoolObjectReturnException_PoolObjectKey", typeof(string));
			}
			catch (SerializationException)
			{
				this.PoolObjectKey = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A400 File Offset: 0x00008600
		public DuplicateStatefulPoolObjectReturnException(string poolObjectKey, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.PoolObjectKey = poolObjectKey;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A42A File Offset: 0x0000862A
		public DuplicateStatefulPoolObjectReturnException(string poolObjectKey, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.PoolObjectKey = poolObjectKey;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A456 File Offset: 0x00008656
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_DuplicateStatefulPoolObjectReturnError";
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A46D File Offset: 0x0000866D
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A478 File Offset: 0x00008678
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DuplicateStatefulPoolObjectReturnException))
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

		// Token: 0x0600021A RID: 538 RVA: 0x0000A52C File Offset: 0x0000872C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DuplicateStatefulPoolObjectReturnException_creationMessage", this.creationMessage, typeof(string));
			if (this.PoolObjectKey != null)
			{
				info.AddValue("DuplicateStatefulPoolObjectReturnException_PoolObjectKey", this.PoolObjectKey, typeof(string));
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A58C File Offset: 0x0000878C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Stateful pool object '{0}' was already returned.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.PoolObjectKey != null) ? this.PoolObjectKey.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.PoolObjectKey != null) ? this.PoolObjectKey.ToString().MarkAsCustomerContent() : string.Empty) : ((this.PoolObjectKey != null) ? this.PoolObjectKey.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000A60B File Offset: 0x0000880B
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

		// Token: 0x0600021D RID: 541 RVA: 0x0000A628 File Offset: 0x00008828
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A648 File Offset: 0x00008848
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "PoolObjectKey={0}", (this.PoolObjectKey != null) ? this.PoolObjectKey.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "PoolObjectKey={0}", (this.PoolObjectKey != null) ? this.PoolObjectKey.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "PoolObjectKey={0}", (this.PoolObjectKey != null) ? this.PoolObjectKey.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A710 File Offset: 0x00008910
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A719 File Offset: 0x00008919
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A722 File Offset: 0x00008922
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A72C File Offset: 0x0000892C
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

		// Token: 0x06000223 RID: 547 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A8FC File Offset: 0x00008AFC
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

		// Token: 0x06000225 RID: 549 RVA: 0x0000A980 File Offset: 0x00008B80
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400024E RID: 590
		private string creationMessage;

		// Token: 0x0400024F RID: 591
		private string m_poolObjectKey;
	}
}
