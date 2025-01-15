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
	// Token: 0x02000084 RID: 132
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class MashupDataAccessCredentialException : MashupDataAccessException
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00029009 File Offset: 0x00027209
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00029011 File Offset: 0x00027211
		public string Reason
		{
			get
			{
				return this.m_reason;
			}
			protected set
			{
				this.m_reason = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0002901A File Offset: 0x0002721A
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x00029022 File Offset: 0x00027222
		public string DataSource
		{
			get
			{
				return this.m_dataSource;
			}
			protected set
			{
				this.m_dataSource = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0002902B File Offset: 0x0002722B
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x00029033 File Offset: 0x00027233
		public string DataSourceReference
		{
			get
			{
				return this.m_dataSourceReference;
			}
			protected set
			{
				this.m_dataSourceReference = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0002903C File Offset: 0x0002723C
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00029044 File Offset: 0x00027244
		public string DataSourceOrigin
		{
			get
			{
				return this.m_dataSourceOrigin;
			}
			protected set
			{
				this.m_dataSourceOrigin = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0002904D File Offset: 0x0002724D
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x00029055 File Offset: 0x00027255
		public string DataSourceReferenceOrigin
		{
			get
			{
				return this.m_dataSourceReferenceOrigin;
			}
			protected set
			{
				this.m_dataSourceReferenceOrigin = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0002905E File Offset: 0x0002725E
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x00029066 File Offset: 0x00027266
		public string InnerExceptionMessage
		{
			get
			{
				return this.m_innerExceptionMessage;
			}
			protected set
			{
				this.m_innerExceptionMessage = value;
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002906F File Offset: 0x0002726F
		public MashupDataAccessCredentialException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002909C File Offset: 0x0002729C
		public MashupDataAccessCredentialException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000290B2 File Offset: 0x000272B2
		public MashupDataAccessCredentialException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000290D5 File Offset: 0x000272D5
		public MashupDataAccessCredentialException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000290FC File Offset: 0x000272FC
		protected MashupDataAccessCredentialException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MashupDataAccessCredentialException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Reason = (string)info.GetValue("MashupDataAccessCredentialException_Reason", typeof(string));
			}
			catch (SerializationException)
			{
				this.Reason = null;
			}
			try
			{
				this.DataSource = (string)info.GetValue("MashupDataAccessCredentialException_DataSource", typeof(string));
			}
			catch (SerializationException)
			{
				this.DataSource = null;
			}
			try
			{
				this.DataSourceReference = (string)info.GetValue("MashupDataAccessCredentialException_DataSourceReference", typeof(string));
			}
			catch (SerializationException)
			{
				this.DataSourceReference = null;
			}
			try
			{
				this.DataSourceOrigin = (string)info.GetValue("MashupDataAccessCredentialException_DataSourceOrigin", typeof(string));
			}
			catch (SerializationException)
			{
				this.DataSourceOrigin = null;
			}
			try
			{
				this.DataSourceReferenceOrigin = (string)info.GetValue("MashupDataAccessCredentialException_DataSourceReferenceOrigin", typeof(string));
			}
			catch (SerializationException)
			{
				this.DataSourceReferenceOrigin = null;
			}
			try
			{
				this.InnerExceptionMessage = (string)info.GetValue("MashupDataAccessCredentialException_InnerExceptionMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerExceptionMessage = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000292B8 File Offset: 0x000274B8
		public MashupDataAccessCredentialException(string reason, string dataSource, string dataSourceReference, string dataSourceOrigin, string dataSourceReferenceOrigin, string innerExceptionMessage, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.Reason = reason;
			this.DataSource = dataSource;
			this.DataSourceReference = dataSourceReference;
			this.DataSourceOrigin = dataSourceOrigin;
			this.DataSourceReferenceOrigin = dataSourceReferenceOrigin;
			this.InnerExceptionMessage = innerExceptionMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00029308 File Offset: 0x00027508
		public MashupDataAccessCredentialException(string reason, string dataSource, string dataSourceReference, string dataSourceOrigin, string dataSourceReferenceOrigin, string innerExceptionMessage, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Reason = reason;
			this.DataSource = dataSource;
			this.DataSourceReference = dataSourceReference;
			this.DataSourceOrigin = dataSourceOrigin;
			this.DataSourceReferenceOrigin = dataSourceReferenceOrigin;
			this.InnerExceptionMessage = innerExceptionMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00029368 File Offset: 0x00027568
		public MashupDataAccessCredentialException(string reason, string dataSource, string dataSourceReference, string dataSourceOrigin, string dataSourceReferenceOrigin, string innerExceptionMessage, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Reason = reason;
			this.DataSource = dataSource;
			this.DataSourceReference = dataSourceReference;
			this.DataSourceOrigin = dataSourceOrigin;
			this.DataSourceReferenceOrigin = dataSourceReferenceOrigin;
			this.InnerExceptionMessage = innerExceptionMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000293C8 File Offset: 0x000275C8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidConnectionCredentials";
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000293DF File Offset: 0x000275DF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000293E7 File Offset: 0x000275E7
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000293EC File Offset: 0x000275EC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MashupDataAccessCredentialException))
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

		// Token: 0x060009E3 RID: 2531 RVA: 0x00029500 File Offset: 0x00027700
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MashupDataAccessCredentialException_creationMessage", this.creationMessage, typeof(string));
			if (this.Reason != null)
			{
				info.AddValue("MashupDataAccessCredentialException_Reason", this.Reason, typeof(string));
			}
			if (this.DataSource != null)
			{
				info.AddValue("MashupDataAccessCredentialException_DataSource", this.DataSource, typeof(string));
			}
			if (this.DataSourceReference != null)
			{
				info.AddValue("MashupDataAccessCredentialException_DataSourceReference", this.DataSourceReference, typeof(string));
			}
			if (this.DataSourceOrigin != null)
			{
				info.AddValue("MashupDataAccessCredentialException_DataSourceOrigin", this.DataSourceOrigin, typeof(string));
			}
			if (this.DataSourceReferenceOrigin != null)
			{
				info.AddValue("MashupDataAccessCredentialException_DataSourceReferenceOrigin", this.DataSourceReferenceOrigin, typeof(string));
			}
			if (this.InnerExceptionMessage != null)
			{
				info.AddValue("MashupDataAccessCredentialException_InnerExceptionMessage", this.InnerExceptionMessage, typeof(string));
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00029610 File Offset: 0x00027810
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Missing or invalid credentials for data source. Reason = '{0}', DataSource = '{1}', DataSourceReference = '{2}', DataSourceOrigin = '{3}', DataSourceReferenceOrigin = '{4}', InnerExceptionMessage = '{5}'.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((this.Reason != null) ? this.Reason.ToString() : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.DataSource != null) ? this.DataSource.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DataSource != null) ? this.DataSource.ToString().MarkAsCustomerContent() : string.Empty) : ((this.DataSource != null) ? this.DataSource.ToString().MarkAsCustomerContent() : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.DataSourceReference != null) ? this.DataSourceReference.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DataSourceReference != null) ? this.DataSourceReference.ToString().MarkAsCustomerContent() : string.Empty) : ((this.DataSourceReference != null) ? this.DataSourceReference.ToString().MarkAsCustomerContent() : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.DataSourceOrigin != null) ? this.DataSourceOrigin.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DataSourceOrigin != null) ? this.DataSourceOrigin.ToString().MarkAsCustomerContent() : string.Empty) : ((this.DataSourceOrigin != null) ? this.DataSourceOrigin.ToString().MarkAsCustomerContent() : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.DataSourceReferenceOrigin != null) ? this.DataSourceReferenceOrigin.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DataSourceReferenceOrigin != null) ? this.DataSourceReferenceOrigin.ToString().MarkAsCustomerContent() : string.Empty) : ((this.DataSourceReferenceOrigin != null) ? this.DataSourceReferenceOrigin.ToString().MarkAsCustomerContent() : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.InnerExceptionMessage != null) ? this.InnerExceptionMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.InnerExceptionMessage != null) ? this.InnerExceptionMessage.ToString().MarkAsCustomerContent() : string.Empty) : ((this.InnerExceptionMessage != null) ? this.InnerExceptionMessage.ToString().MarkAsCustomerContent() : string.Empty))
			});
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002988C File Offset: 0x00027A8C
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

		// Token: 0x060009E6 RID: 2534 RVA: 0x000298A9 File Offset: 0x00027AA9
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000298C8 File Offset: 0x00027AC8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DataSource={0}", (this.DataSource != null) ? this.DataSource.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DataSource={0}", (this.DataSource != null) ? this.DataSource.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DataSource={0}", (this.DataSource != null) ? this.DataSource.ToString().MarkAsCustomerContent() : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DataSourceReference={0}", (this.DataSourceReference != null) ? this.DataSourceReference.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DataSourceReference={0}", (this.DataSourceReference != null) ? this.DataSourceReference.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DataSourceReference={0}", (this.DataSourceReference != null) ? this.DataSourceReference.ToString().MarkAsCustomerContent() : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DataSourceOrigin={0}", (this.DataSourceOrigin != null) ? this.DataSourceOrigin.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DataSourceOrigin={0}", (this.DataSourceOrigin != null) ? this.DataSourceOrigin.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DataSourceOrigin={0}", (this.DataSourceOrigin != null) ? this.DataSourceOrigin.ToString().MarkAsCustomerContent() : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DataSourceReferenceOrigin={0}", (this.DataSourceReferenceOrigin != null) ? this.DataSourceReferenceOrigin.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DataSourceReferenceOrigin={0}", (this.DataSourceReferenceOrigin != null) ? this.DataSourceReferenceOrigin.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DataSourceReferenceOrigin={0}", (this.DataSourceReferenceOrigin != null) ? this.DataSourceReferenceOrigin.ToString().MarkAsCustomerContent() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerExceptionMessage={0}", (this.InnerExceptionMessage != null) ? this.InnerExceptionMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerExceptionMessage={0}", (this.InnerExceptionMessage != null) ? this.InnerExceptionMessage.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "InnerExceptionMessage={0}", (this.InnerExceptionMessage != null) ? this.InnerExceptionMessage.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00029CE2 File Offset: 0x00027EE2
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00029CEB File Offset: 0x00027EEB
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00029CF4 File Offset: 0x00027EF4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00029D00 File Offset: 0x00027F00
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

		// Token: 0x060009EC RID: 2540 RVA: 0x00029EC4 File Offset: 0x000280C4
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00029ED0 File Offset: 0x000280D0
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

		// Token: 0x060009EE RID: 2542 RVA: 0x00029F54 File Offset: 0x00028154
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002CA RID: 714
		private string creationMessage;

		// Token: 0x040002CB RID: 715
		private string m_reason;

		// Token: 0x040002CC RID: 716
		private string m_dataSource;

		// Token: 0x040002CD RID: 717
		private string m_dataSourceReference;

		// Token: 0x040002CE RID: 718
		private string m_dataSourceOrigin;

		// Token: 0x040002CF RID: 719
		private string m_dataSourceReferenceOrigin;

		// Token: 0x040002D0 RID: 720
		private string m_innerExceptionMessage;
	}
}
