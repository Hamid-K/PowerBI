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
	// Token: 0x0200007E RID: 126
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class ConnectionNotOpenException : GatewayPipelineException
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00027044 File Offset: 0x00025244
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0002704C File Offset: 0x0002524C
		public string ConnectionState
		{
			get
			{
				return this.m_connectionState;
			}
			protected set
			{
				this.m_connectionState = value;
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00027055 File Offset: 0x00025255
		public ConnectionNotOpenException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00027069 File Offset: 0x00025269
		public ConnectionNotOpenException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002707F File Offset: 0x0002527F
		public ConnectionNotOpenException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000270A2 File Offset: 0x000252A2
		public ConnectionNotOpenException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000270C8 File Offset: 0x000252C8
		protected ConnectionNotOpenException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ConnectionNotOpenException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ConnectionState = (string)info.GetValue("ConnectionNotOpenException_ConnectionState", typeof(string));
			}
			catch (SerializationException)
			{
				this.ConnectionState = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00027164 File Offset: 0x00025364
		public ConnectionNotOpenException(string connectionState, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConnectionState = connectionState;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002718E File Offset: 0x0002538E
		public ConnectionNotOpenException(string connectionState, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConnectionState = connectionState;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x000271BA File Offset: 0x000253BA
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_ConnectionNotOpenError";
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000271D1 File Offset: 0x000253D1
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000271DC File Offset: 0x000253DC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ConnectionNotOpenException))
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

		// Token: 0x06000958 RID: 2392 RVA: 0x00027290 File Offset: 0x00025490
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ConnectionNotOpenException_creationMessage", this.creationMessage, typeof(string));
			if (this.ConnectionState != null)
			{
				info.AddValue("ConnectionNotOpenException_ConnectionState", this.ConnectionState, typeof(string));
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000272F0 File Offset: 0x000254F0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Cannot use a connection that is not in state 'open'. Current connection state: {0}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : ((this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty)));
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00027365 File Offset: 0x00025565
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

		// Token: 0x0600095B RID: 2395 RVA: 0x00027382 File Offset: 0x00025582
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000273A0 File Offset: 0x000255A0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ConnectionState={0}", (this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ConnectionState={0}", (this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ConnectionState={0}", (this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty)));
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002745E File Offset: 0x0002565E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00027467 File Offset: 0x00025667
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00027470 File Offset: 0x00025670
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002747C File Offset: 0x0002567C
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

		// Token: 0x06000961 RID: 2401 RVA: 0x00027640 File Offset: 0x00025840
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002764C File Offset: 0x0002584C
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

		// Token: 0x06000963 RID: 2403 RVA: 0x000276D0 File Offset: 0x000258D0
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002C3 RID: 707
		private string creationMessage;

		// Token: 0x040002C4 RID: 708
		private string m_connectionState;
	}
}
