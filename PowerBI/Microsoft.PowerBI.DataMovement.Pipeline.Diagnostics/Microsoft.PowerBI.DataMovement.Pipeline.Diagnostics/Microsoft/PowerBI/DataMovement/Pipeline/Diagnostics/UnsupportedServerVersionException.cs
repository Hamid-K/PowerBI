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
	// Token: 0x0200007D RID: 125
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class UnsupportedServerVersionException : DataAccessException
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00026849 File Offset: 0x00024A49
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x00026851 File Offset: 0x00024A51
		public string ServerVersion
		{
			get
			{
				return this.m_serverVersion;
			}
			protected set
			{
				this.m_serverVersion = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0002685A File Offset: 0x00024A5A
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x00026862 File Offset: 0x00024A62
		public string MinSupportedVersion
		{
			get
			{
				return this.m_minSupportedVersion;
			}
			protected set
			{
				this.m_minSupportedVersion = value;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0002686B File Offset: 0x00024A6B
		public UnsupportedServerVersionException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00026884 File Offset: 0x00024A84
		public UnsupportedServerVersionException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0002689A File Offset: 0x00024A9A
		public UnsupportedServerVersionException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000268BD File Offset: 0x00024ABD
		public UnsupportedServerVersionException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000268E4 File Offset: 0x00024AE4
		protected UnsupportedServerVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("UnsupportedServerVersionException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServerVersion = (string)info.GetValue("UnsupportedServerVersionException_ServerVersion", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServerVersion = null;
			}
			try
			{
				this.MinSupportedVersion = (string)info.GetValue("UnsupportedServerVersionException_MinSupportedVersion", typeof(string));
			}
			catch (SerializationException)
			{
				this.MinSupportedVersion = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000269BC File Offset: 0x00024BBC
		public UnsupportedServerVersionException(string serverVersion, string minSupportedVersion, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ServerVersion = serverVersion;
			this.MinSupportedVersion = minSupportedVersion;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000269E0 File Offset: 0x00024BE0
		public UnsupportedServerVersionException(string serverVersion, string minSupportedVersion, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ServerVersion = serverVersion;
			this.MinSupportedVersion = minSupportedVersion;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00026A12 File Offset: 0x00024C12
		public UnsupportedServerVersionException(string serverVersion, string minSupportedVersion, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ServerVersion = serverVersion;
			this.MinSupportedVersion = minSupportedVersion;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00026A46 File Offset: 0x00024C46
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_UnsupportedServerVersion";
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00026A5D File Offset: 0x00024C5D
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00026A65 File Offset: 0x00024C65
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00026A68 File Offset: 0x00024C68
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(UnsupportedServerVersionException))
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

		// Token: 0x06000940 RID: 2368 RVA: 0x00026B1C File Offset: 0x00024D1C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("UnsupportedServerVersionException_creationMessage", this.creationMessage, typeof(string));
			if (this.ServerVersion != null)
			{
				info.AddValue("UnsupportedServerVersionException_ServerVersion", this.ServerVersion, typeof(string));
			}
			if (this.MinSupportedVersion != null)
			{
				info.AddValue("UnsupportedServerVersionException_MinSupportedVersion", this.MinSupportedVersion, typeof(string));
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00026B9D File Offset: 0x00024D9D
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The target server version is not supported.", Array.Empty<object>());
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00026BB3 File Offset: 0x00024DB3
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

		// Token: 0x06000943 RID: 2371 RVA: 0x00026BD0 File Offset: 0x00024DD0
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00026BF0 File Offset: 0x00024DF0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServerVersion={0}", (this.ServerVersion != null) ? this.ServerVersion.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServerVersion={0}", (this.ServerVersion != null) ? this.ServerVersion.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServerVersion={0}", (this.ServerVersion != null) ? this.ServerVersion.ToString() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "MinSupportedVersion={0}", (this.MinSupportedVersion != null) ? this.MinSupportedVersion.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "MinSupportedVersion={0}", (this.MinSupportedVersion != null) ? this.MinSupportedVersion.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "MinSupportedVersion={0}", (this.MinSupportedVersion != null) ? this.MinSupportedVersion.ToString() : string.Empty)));
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00026D50 File Offset: 0x00024F50
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00026D59 File Offset: 0x00024F59
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00026D62 File Offset: 0x00024F62
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00026D6C File Offset: 0x00024F6C
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

		// Token: 0x06000949 RID: 2377 RVA: 0x00026F30 File Offset: 0x00025130
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00026F3C File Offset: 0x0002513C
		internal override IDictionary<string, string> GetClientErrorParameters(bool includeInner)
		{
			IDictionary<string, string> clientErrorParameters = base.GetClientErrorParameters(true);
			clientErrorParameters["ServerVersion"] = this.ServerVersion.ToString().RemovePrivateAndInternalMarkup();
			clientErrorParameters["MinSupportedVersion"] = this.MinSupportedVersion.ToString().RemovePrivateAndInternalMarkup();
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

		// Token: 0x0600094B RID: 2379 RVA: 0x00026FF8 File Offset: 0x000251F8
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			GatewayPipelineException ex = base.InnerException as GatewayPipelineException;
			if (ex != null)
			{
				list.AddRange(ex.GetErrorDetails());
			}
			return list;
		}

		// Token: 0x040002C0 RID: 704
		private string creationMessage;

		// Token: 0x040002C1 RID: 705
		private string m_serverVersion;

		// Token: 0x040002C2 RID: 706
		private string m_minSupportedVersion;
	}
}
