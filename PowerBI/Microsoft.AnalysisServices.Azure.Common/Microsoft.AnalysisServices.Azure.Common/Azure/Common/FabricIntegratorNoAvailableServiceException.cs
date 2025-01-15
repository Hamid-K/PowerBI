using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000FC RID: 252
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorNoAvailableServiceException : MonitoredException
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x0002D5EC File Offset: 0x0002B7EC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0002D5F4 File Offset: 0x0002B7F4
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0002D5FC File Offset: 0x0002B7FC
		public string DatabaseId
		{
			get
			{
				return this.m_databaseId;
			}
			protected set
			{
				this.m_databaseId = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0002D605 File Offset: 0x0002B805
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0002D60D File Offset: 0x0002B80D
		public string DatabaseType
		{
			get
			{
				return this.m_databaseType;
			}
			protected set
			{
				this.m_databaseType = value;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002D616 File Offset: 0x0002B816
		public FabricIntegratorNoAvailableServiceException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002D62F File Offset: 0x0002B82F
		public FabricIntegratorNoAvailableServiceException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002D646 File Offset: 0x0002B846
		public FabricIntegratorNoAvailableServiceException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002D664 File Offset: 0x0002B864
		protected FabricIntegratorNoAvailableServiceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorNoAvailableServiceException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseId = (string)info.GetValue("FabricIntegratorNoAvailableServiceException_DatabaseId", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseId = null;
			}
			try
			{
				this.DatabaseType = (string)info.GetValue("FabricIntegratorNoAvailableServiceException_DatabaseType", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseType = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorNoAvailableServiceException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002D774 File Offset: 0x0002B974
		public FabricIntegratorNoAvailableServiceException(string databaseId, string databaseType)
		{
			this.DatabaseId = databaseId;
			this.DatabaseType = databaseType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002D791 File Offset: 0x0002B991
		public FabricIntegratorNoAvailableServiceException(string databaseId, string databaseType, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.DatabaseType = databaseType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002D7B6 File Offset: 0x0002B9B6
		public FabricIntegratorNoAvailableServiceException(string databaseId, string databaseType, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.DatabaseType = databaseType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002D7E4 File Offset: 0x0002B9E4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002D81C File Offset: 0x0002BA1C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorNoAvailableServiceException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorNoAvailableServiceException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseId != null)
			{
				info.AddValue("FabricIntegratorNoAvailableServiceException_DatabaseId", this.DatabaseId, typeof(string));
			}
			if (this.DatabaseType != null)
			{
				info.AddValue("FabricIntegratorNoAvailableServiceException_DatabaseType", this.DatabaseType, typeof(string));
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002D8C0 File Offset: 0x0002BAC0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Fabric Integrator failed to bind a database '{0}' because there is no available service of type '{1}'.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : ((this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseType != null) ? this.DatabaseType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseType != null) ? this.DatabaseType.MarkIfInternal() : string.Empty) : ((this.DatabaseType != null) ? this.DatabaseType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0002D99A File Offset: 0x0002BB9A
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

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseType={0}", (this.DatabaseType != null) ? this.DatabaseType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseType={0}", (this.DatabaseType != null) ? this.DatabaseType.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseType={0}", (this.DatabaseType != null) ? this.DatabaseType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002DB24 File Offset: 0x0002BD24
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002DB2D File Offset: 0x0002BD2D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002DB36 File Offset: 0x0002BD36
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002DB24 File Offset: 0x0002BD24
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002DB40 File Offset: 0x0002BD40
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

		// Token: 0x04000318 RID: 792
		private string creationMessage;

		// Token: 0x04000319 RID: 793
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400031A RID: 794
		private string m_databaseId;

		// Token: 0x0400031B RID: 795
		private string m_databaseType;
	}
}
