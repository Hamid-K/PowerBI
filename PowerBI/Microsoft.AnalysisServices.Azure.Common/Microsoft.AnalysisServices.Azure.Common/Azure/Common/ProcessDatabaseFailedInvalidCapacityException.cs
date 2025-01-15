using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000F5 RID: 245
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ProcessDatabaseFailedInvalidCapacityException : MonitoredException
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x0002A734 File Offset: 0x00028934
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002A73C File Offset: 0x0002893C
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0002A744 File Offset: 0x00028944
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
			protected set
			{
				this.m_databaseName = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002A74D File Offset: 0x0002894D
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x0002A755 File Offset: 0x00028955
		public string MwcErrorCode
		{
			get
			{
				return this.m_mwcErrorCode;
			}
			protected set
			{
				this.m_mwcErrorCode = value;
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002A75E File Offset: 0x0002895E
		public ProcessDatabaseFailedInvalidCapacityException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002A777 File Offset: 0x00028977
		public ProcessDatabaseFailedInvalidCapacityException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002A78E File Offset: 0x0002898E
		public ProcessDatabaseFailedInvalidCapacityException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002A7AC File Offset: 0x000289AC
		protected ProcessDatabaseFailedInvalidCapacityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ProcessDatabaseFailedInvalidCapacityException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("ProcessDatabaseFailedInvalidCapacityException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.MwcErrorCode = (string)info.GetValue("ProcessDatabaseFailedInvalidCapacityException_MwcErrorCode", typeof(string));
			}
			catch (SerializationException)
			{
				this.MwcErrorCode = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ProcessDatabaseFailedInvalidCapacityException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002A8BC File Offset: 0x00028ABC
		public ProcessDatabaseFailedInvalidCapacityException(string databaseName, string mwcErrorCode)
		{
			this.DatabaseName = databaseName;
			this.MwcErrorCode = mwcErrorCode;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002A8D9 File Offset: 0x00028AD9
		public ProcessDatabaseFailedInvalidCapacityException(string databaseName, string mwcErrorCode, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.MwcErrorCode = mwcErrorCode;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002A8FE File Offset: 0x00028AFE
		public ProcessDatabaseFailedInvalidCapacityException(string databaseName, string mwcErrorCode, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.MwcErrorCode = mwcErrorCode;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002A92C File Offset: 0x00028B2C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			this.exceptionCulprit = ExceptionCulprit.User;
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002A96C File Offset: 0x00028B6C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ProcessDatabaseFailedInvalidCapacityException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ProcessDatabaseFailedInvalidCapacityException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("ProcessDatabaseFailedInvalidCapacityException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.MwcErrorCode != null)
			{
				info.AddValue("ProcessDatabaseFailedInvalidCapacityException_MwcErrorCode", this.MwcErrorCode, typeof(string));
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002AA10 File Offset: 0x00028C10
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "ProcessDatabase for database {0} failed due to invalid capacity with MWC error code: {1}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.MwcErrorCode != null) ? this.MwcErrorCode.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.MwcErrorCode != null) ? this.MwcErrorCode.MarkIfInternal() : string.Empty) : ((this.MwcErrorCode != null) ? this.MwcErrorCode.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0002AAEA File Offset: 0x00028CEA
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

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002AB08 File Offset: 0x00028D08
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "MwcErrorCode={0}", (this.MwcErrorCode != null) ? this.MwcErrorCode.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "MwcErrorCode={0}", (this.MwcErrorCode != null) ? this.MwcErrorCode.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "MwcErrorCode={0}", (this.MwcErrorCode != null) ? this.MwcErrorCode.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002AC74 File Offset: 0x00028E74
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002AC7D File Offset: 0x00028E7D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002AC86 File Offset: 0x00028E86
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002AC74 File Offset: 0x00028E74
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002AC90 File Offset: 0x00028E90
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
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
			text3 = text3 + Environment.NewLine + "ExceptionCulprit=" + this.exceptionCulprit.ToString();
			if (base.InnerException != null)
			{
				try
				{
					ExceptionsTemplateHelper.IncrementMagicLevel();
					IMonitoredError monitoredError = base.InnerException as MonitoredException;
					string text4;
					if (markupKind != PrivateInformationMarkupKind.None)
					{
						if (markupKind != PrivateInformationMarkupKind.Internal)
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfPrivate() : monitoredError.ToPrivateString());
							text4 = text4.ObfuscatePrivateValue(true);
						}
						else
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfInternal() : monitoredError.ToInternalString());
						}
					}
					else
					{
						text4 = ((monitoredError == null) ? base.InnerException.ToString() : monitoredError.ToOriginalString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---",
						Environment.NewLine,
						"  (",
						text,
						".StackTrace:)"
					});
				}
				finally
				{
					ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x04000301 RID: 769
		private string creationMessage;

		// Token: 0x04000302 RID: 770
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000303 RID: 771
		private string m_databaseName;

		// Token: 0x04000304 RID: 772
		private string m_mwcErrorCode;
	}
}
