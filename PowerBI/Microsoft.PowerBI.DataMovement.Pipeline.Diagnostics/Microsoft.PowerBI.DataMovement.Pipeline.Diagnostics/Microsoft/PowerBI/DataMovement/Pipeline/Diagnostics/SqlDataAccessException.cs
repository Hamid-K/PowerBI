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
	// Token: 0x02000054 RID: 84
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class SqlDataAccessException : DataAccessException
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00018C05 File Offset: 0x00016E05
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00018C0D File Offset: 0x00016E0D
		public byte ErrorClass
		{
			get
			{
				return this.m_errorClass;
			}
			protected set
			{
				this.m_errorClass = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00018C16 File Offset: 0x00016E16
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x00018C1E File Offset: 0x00016E1E
		public int ErrorNumber
		{
			get
			{
				return this.m_errorNumber;
			}
			protected set
			{
				this.m_errorNumber = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00018C27 File Offset: 0x00016E27
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00018C2F File Offset: 0x00016E2F
		public byte ErrorState
		{
			get
			{
				return this.m_errorState;
			}
			protected set
			{
				this.m_errorState = value;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00018C38 File Offset: 0x00016E38
		public SqlDataAccessException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<byte>();
			GatewayExceptionUtils.CompileCheck.IsValidValueField<int>();
			GatewayExceptionUtils.CompileCheck.IsValidValueField<byte>();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00018C56 File Offset: 0x00016E56
		public SqlDataAccessException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00018C6C File Offset: 0x00016E6C
		public SqlDataAccessException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00018C8F File Offset: 0x00016E8F
		public SqlDataAccessException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018CB4 File Offset: 0x00016EB4
		protected SqlDataAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("SqlDataAccessException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ErrorClass = (byte)info.GetValue("SqlDataAccessException_ErrorClass", typeof(byte));
			this.ErrorNumber = (int)info.GetValue("SqlDataAccessException_ErrorNumber", typeof(int));
			this.ErrorState = (byte)info.GetValue("SqlDataAccessException_ErrorState", typeof(byte));
			this.ConstructorInternal(true);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00018D78 File Offset: 0x00016F78
		public SqlDataAccessException(byte errorClass, int errorNumber, byte errorState, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ErrorClass = errorClass;
			this.ErrorNumber = errorNumber;
			this.ErrorState = errorState;
			this.ConstructorInternal(false);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00018DA4 File Offset: 0x00016FA4
		public SqlDataAccessException(byte errorClass, int errorNumber, byte errorState, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ErrorClass = errorClass;
			this.ErrorNumber = errorNumber;
			this.ErrorState = errorState;
			this.ConstructorInternal(false);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00018DDF File Offset: 0x00016FDF
		public SqlDataAccessException(byte errorClass, int errorNumber, byte errorState, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ErrorClass = errorClass;
			this.ErrorNumber = errorNumber;
			this.ErrorState = errorState;
			this.ConstructorInternal(false);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00018E1C File Offset: 0x0001701C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00018E25 File Offset: 0x00017025
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00018E30 File Offset: 0x00017030
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(SqlDataAccessException))
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

		// Token: 0x060005BB RID: 1467 RVA: 0x00018EE4 File Offset: 0x000170E4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("SqlDataAccessException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("SqlDataAccessException_ErrorClass", this.ErrorClass, typeof(byte));
			info.AddValue("SqlDataAccessException_ErrorNumber", this.ErrorNumber, typeof(int));
			info.AddValue("SqlDataAccessException_ErrorState", this.ErrorState, typeof(byte));
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00018F7F File Offset: 0x0001717F
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "SqlException encountered while accessing the target data source", Array.Empty<object>());
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00018F95 File Offset: 0x00017195
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

		// Token: 0x060005BE RID: 1470 RVA: 0x00018FB2 File Offset: 0x000171B2
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018FD0 File Offset: 0x000171D0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ErrorClass={0}", this.ErrorClass.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ErrorClass={0}", this.ErrorClass.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "ErrorClass={0}", this.ErrorClass.ToString(CultureInfo.InvariantCulture))));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ErrorNumber={0}", this.ErrorNumber.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ErrorNumber={0}", this.ErrorNumber.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "ErrorNumber={0}", this.ErrorNumber.ToString(CultureInfo.InvariantCulture))));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ErrorState={0}", this.ErrorState.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ErrorState={0}", this.ErrorState.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "ErrorState={0}", this.ErrorState.ToString(CultureInfo.InvariantCulture))));
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00019193 File Offset: 0x00017393
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001919C File Offset: 0x0001739C
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000191A5 File Offset: 0x000173A5
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000191B0 File Offset: 0x000173B0
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

		// Token: 0x060005C4 RID: 1476 RVA: 0x00019374 File Offset: 0x00017574
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00019380 File Offset: 0x00017580
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

		// Token: 0x060005C6 RID: 1478 RVA: 0x00019404 File Offset: 0x00017604
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000289 RID: 649
		private string creationMessage;

		// Token: 0x0400028A RID: 650
		private byte m_errorClass;

		// Token: 0x0400028B RID: 651
		private int m_errorNumber;

		// Token: 0x0400028C RID: 652
		private byte m_errorState;
	}
}
