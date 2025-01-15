using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x0200000F RID: 15
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public abstract class GatewayPipelineException : Exception, IContainsPrivateInformation
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000024C6 File Offset: 0x000006C6
		public void MarkAsBenign()
		{
			this.m_benignOverride = true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000024CF File Offset: 0x000006CF
		[NullableContext(1)]
		public void SetGatewayVersion(string version)
		{
			if (this.GatewayVersion == null)
			{
				this.GatewayVersion = version;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000024E0 File Offset: 0x000006E0
		public virtual bool IsBenign()
		{
			return this.m_benignOverride;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000024E8 File Offset: 0x000006E8
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000024F0 File Offset: 0x000006F0
		public string GatewayPipelineErrorCode
		{
			get
			{
				return this.m_gatewayPipelineErrorCode;
			}
			protected set
			{
				this.m_gatewayPipelineErrorCode = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000024F9 File Offset: 0x000006F9
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002501 File Offset: 0x00000701
		public string GatewayVersion
		{
			get
			{
				return this.m_gatewayVersion;
			}
			protected set
			{
				this.m_gatewayVersion = value;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000250A File Offset: 0x0000070A
		public GatewayPipelineException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002523 File Offset: 0x00000723
		public GatewayPipelineException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002539 File Offset: 0x00000739
		public GatewayPipelineException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message)
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002557 File Offset: 0x00000757
		public GatewayPipelineException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000257C File Offset: 0x0000077C
		protected GatewayPipelineException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("GatewayPipelineException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.creationErrorDetails = (PowerBIErrorDetail[])info.GetValue("GatewayPipelineException_creationErrorDetails", typeof(PowerBIErrorDetail[]));
			}
			catch (SerializationException)
			{
				this.creationErrorDetails = null;
			}
			try
			{
				this.GatewayPipelineErrorCode = (string)info.GetValue("GatewayPipelineException_GatewayPipelineErrorCode", typeof(string));
			}
			catch (SerializationException)
			{
				this.GatewayPipelineErrorCode = null;
			}
			try
			{
				this.GatewayVersion = (string)info.GetValue("GatewayPipelineException_GatewayVersion", typeof(string));
			}
			catch (SerializationException)
			{
				this.GatewayVersion = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000268C File Offset: 0x0000088C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				this.GatewayPipelineErrorCode = ((base.InnerException is GatewayPipelineException) ? ((GatewayPipelineException)base.InnerException).GatewayPipelineErrorCode : "DM_GWPipeline_UnknownError");
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000026C2 File Offset: 0x000008C2
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000026CC File Offset: 0x000008CC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(GatewayPipelineException))
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

		// Token: 0x06000048 RID: 72 RVA: 0x00002780 File Offset: 0x00000980
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("GatewayPipelineException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("GatewayPipelineException_creationErrorDetails", this.creationErrorDetails, typeof(PowerBIErrorDetail[]));
			if (this.GatewayPipelineErrorCode != null)
			{
				info.AddValue("GatewayPipelineException_GatewayPipelineErrorCode", this.GatewayPipelineErrorCode, typeof(string));
			}
			if (this.GatewayVersion != null)
			{
				info.AddValue("GatewayPipelineException_GatewayVersion", this.GatewayVersion, typeof(string));
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000281C File Offset: 0x00000A1C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A Gateway pipeline exception occurred", Array.Empty<object>());
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002832 File Offset: 0x00000A32
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

		// Token: 0x0600004B RID: 75 RVA: 0x0000284F File Offset: 0x00000A4F
		internal virtual string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000286C File Offset: 0x00000A6C
		public virtual string ErrorShortName
		{
			get
			{
				if (this.m_errorShortName == null)
				{
					string[] array = new string[16];
					array[0] = this.ExceptionErrorShortName;
					Exception ex = base.InnerException;
					int num = 1;
					while (num < 16 && ex != null)
					{
						array[num] = GatewayExceptionUtils.InnerExceptionCreator.GetExceptionErrorShortName(ex);
						ex = ex.InnerException;
						num++;
					}
					this.m_errorShortName = string.Join("/", array, 0, num);
				}
				return this.m_errorShortName;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000028D2 File Offset: 0x00000AD2
		protected virtual string ExceptionClassName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000028E0 File Offset: 0x00000AE0
		public string ExceptionErrorShortName
		{
			get
			{
				if (this.m_exceptionErrorShortName == null)
				{
					List<string> list = new List<string>();
					string exceptionClassName = this.ExceptionClassName;
					list.Add(exceptionClassName);
					this.PopulateExceptionErrorShortNameComponents(list);
					this.m_exceptionErrorShortName = GatewayExceptionUtils.InnerExceptionCreator.BuildExceptionErrorShortName(list);
				}
				return this.m_exceptionErrorShortName;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002924 File Offset: 0x00000B24
		protected virtual void PopulateExceptionErrorShortNameComponents(List<string> components)
		{
			if (this.creationErrorDetails == null)
			{
				return;
			}
			for (int i = 0; i < this.creationErrorDetails.Length; i++)
			{
				PowerBIErrorDetail powerBIErrorDetail = this.creationErrorDetails[i];
				if (powerBIErrorDetail.Value != null && powerBIErrorDetail.Value.ResourceValue != null)
				{
					string nameCode = powerBIErrorDetail.NameCode;
					if (!(nameCode == "DM_ErrorDetailNameCode_UnderlyingHResult"))
					{
						if (!(nameCode == "DM_ErrorDetailNameCode_UnderlyingErrorCode"))
						{
							if (nameCode == "DM_ErrorDetailNameCode_UnderlyingNativeErrorCode")
							{
								components.Add("NativeErrorCode");
								components.Add(powerBIErrorDetail.Value.ResourceValue);
							}
						}
						else
						{
							components.Add("ErrorCode");
							components.Add(powerBIErrorDetail.Value.ResourceValue);
						}
					}
					else
					{
						components.Add("HResult");
						components.Add(powerBIErrorDetail.Value.ResourceValue);
					}
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A00 File Offset: 0x00000C00
		protected virtual string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "GatewayPipelineErrorCode={0}", (this.GatewayPipelineErrorCode != null) ? this.GatewayPipelineErrorCode.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "GatewayPipelineErrorCode={0}", (this.GatewayPipelineErrorCode != null) ? this.GatewayPipelineErrorCode.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "GatewayPipelineErrorCode={0}", (this.GatewayPipelineErrorCode != null) ? this.GatewayPipelineErrorCode.ToString() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "GatewayVersion={0}", (this.GatewayVersion != null) ? this.GatewayVersion.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "GatewayVersion={0}", (this.GatewayVersion != null) ? this.GatewayVersion.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "GatewayVersion={0}", (this.GatewayVersion != null) ? this.GatewayVersion.ToString() : string.Empty)));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B58 File Offset: 0x00000D58
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B61 File Offset: 0x00000D61
		public virtual string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B6A File Offset: 0x00000D6A
		public virtual string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B74 File Offset: 0x00000D74
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

		// Token: 0x06000055 RID: 85 RVA: 0x00002D38 File Offset: 0x00000F38
		public virtual IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D44 File Offset: 0x00000F44
		internal virtual IDictionary<string, string> GetClientErrorParameters(bool includeInner)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (includeInner)
			{
				GatewayPipelineException ex = base.InnerException as GatewayPipelineException;
				if (ex != null)
				{
					IDictionary<string, string> clientErrorParameters = ex.GetClientErrorParameters();
					foreach (string text in clientErrorParameters.Keys)
					{
						if (!dictionary.ContainsKey(text))
						{
							dictionary[text] = clientErrorParameters[text];
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public virtual IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000032 RID: 50
		private bool m_benignOverride;

		// Token: 0x04000033 RID: 51
		private string creationMessage;

		// Token: 0x04000034 RID: 52
		private const int c_maxErrorShortNameDepth = 16;

		// Token: 0x04000035 RID: 53
		private string m_errorShortName;

		// Token: 0x04000036 RID: 54
		private string m_exceptionErrorShortName;

		// Token: 0x04000037 RID: 55
		protected PowerBIErrorDetail[] creationErrorDetails;

		// Token: 0x04000038 RID: 56
		private string m_gatewayPipelineErrorCode;

		// Token: 0x04000039 RID: 57
		private string m_gatewayVersion;
	}
}
