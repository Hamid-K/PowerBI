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
	// Token: 0x0200002A RID: 42
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class StatefulPoolObjectExpiredException : GatewayPipelineException
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00009C11 File Offset: 0x00007E11
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00009C19 File Offset: 0x00007E19
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

		// Token: 0x060001F8 RID: 504 RVA: 0x00009C22 File Offset: 0x00007E22
		public StatefulPoolObjectExpiredException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009C36 File Offset: 0x00007E36
		public StatefulPoolObjectExpiredException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009C4C File Offset: 0x00007E4C
		public StatefulPoolObjectExpiredException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009C6F File Offset: 0x00007E6F
		public StatefulPoolObjectExpiredException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009C94 File Offset: 0x00007E94
		protected StatefulPoolObjectExpiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("StatefulPoolObjectExpiredException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.PoolObjectKey = (string)info.GetValue("StatefulPoolObjectExpiredException_PoolObjectKey", typeof(string));
			}
			catch (SerializationException)
			{
				this.PoolObjectKey = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009D30 File Offset: 0x00007F30
		public StatefulPoolObjectExpiredException(string poolObjectKey, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.PoolObjectKey = poolObjectKey;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009D5A File Offset: 0x00007F5A
		public StatefulPoolObjectExpiredException(string poolObjectKey, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.PoolObjectKey = poolObjectKey;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009D86 File Offset: 0x00007F86
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_StatefulPoolObjectExpiredError";
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009D9D File Offset: 0x00007F9D
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009DA8 File Offset: 0x00007FA8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(StatefulPoolObjectExpiredException))
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

		// Token: 0x06000202 RID: 514 RVA: 0x00009E5C File Offset: 0x0000805C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("StatefulPoolObjectExpiredException_creationMessage", this.creationMessage, typeof(string));
			if (this.PoolObjectKey != null)
			{
				info.AddValue("StatefulPoolObjectExpiredException_PoolObjectKey", this.PoolObjectKey, typeof(string));
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009EBC File Offset: 0x000080BC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Stateful pool object '{0}' expired.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.PoolObjectKey != null) ? this.PoolObjectKey.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.PoolObjectKey != null) ? this.PoolObjectKey.ToString().MarkAsCustomerContent() : string.Empty) : ((this.PoolObjectKey != null) ? this.PoolObjectKey.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00009F3B File Offset: 0x0000813B
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

		// Token: 0x06000205 RID: 517 RVA: 0x00009F58 File Offset: 0x00008158
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009F78 File Offset: 0x00008178
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "PoolObjectKey={0}", (this.PoolObjectKey != null) ? this.PoolObjectKey.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "PoolObjectKey={0}", (this.PoolObjectKey != null) ? this.PoolObjectKey.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "PoolObjectKey={0}", (this.PoolObjectKey != null) ? this.PoolObjectKey.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A040 File Offset: 0x00008240
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A049 File Offset: 0x00008249
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A052 File Offset: 0x00008252
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A05C File Offset: 0x0000825C
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

		// Token: 0x0600020B RID: 523 RVA: 0x0000A220 File Offset: 0x00008420
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A22C File Offset: 0x0000842C
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

		// Token: 0x0600020D RID: 525 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400024C RID: 588
		private string creationMessage;

		// Token: 0x0400024D RID: 589
		private string m_poolObjectKey;
	}
}
