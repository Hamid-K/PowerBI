using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000FD RID: 253
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorUnableToFindBoundLocalDatabaseException : MonitoredException
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x0002DD2C File Offset: 0x0002BF2C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002DD34 File Offset: 0x0002BF34
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0002DD3C File Offset: 0x0002BF3C
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

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0002DD45 File Offset: 0x0002BF45
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0002DD4D File Offset: 0x0002BF4D
		public string ServiceUri
		{
			get
			{
				return this.m_serviceUri;
			}
			protected set
			{
				this.m_serviceUri = value;
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002DD56 File Offset: 0x0002BF56
		public FabricIntegratorUnableToFindBoundLocalDatabaseException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002DD6F File Offset: 0x0002BF6F
		public FabricIntegratorUnableToFindBoundLocalDatabaseException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002DD86 File Offset: 0x0002BF86
		public FabricIntegratorUnableToFindBoundLocalDatabaseException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002DDA4 File Offset: 0x0002BFA4
		protected FabricIntegratorUnableToFindBoundLocalDatabaseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorUnableToFindBoundLocalDatabaseException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseId = (string)info.GetValue("FabricIntegratorUnableToFindBoundLocalDatabaseException_DatabaseId", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseId = null;
			}
			try
			{
				this.ServiceUri = (string)info.GetValue("FabricIntegratorUnableToFindBoundLocalDatabaseException_ServiceUri", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceUri = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorUnableToFindBoundLocalDatabaseException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002DEB4 File Offset: 0x0002C0B4
		public FabricIntegratorUnableToFindBoundLocalDatabaseException(string databaseId, string serviceUri)
		{
			this.DatabaseId = databaseId;
			this.ServiceUri = serviceUri;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002DED1 File Offset: 0x0002C0D1
		public FabricIntegratorUnableToFindBoundLocalDatabaseException(string databaseId, string serviceUri, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ServiceUri = serviceUri;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002DEF6 File Offset: 0x0002C0F6
		public FabricIntegratorUnableToFindBoundLocalDatabaseException(string databaseId, string serviceUri, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ServiceUri = serviceUri;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002DF24 File Offset: 0x0002C124
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002DF5C File Offset: 0x0002C15C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorUnableToFindBoundLocalDatabaseException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorUnableToFindBoundLocalDatabaseException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseId != null)
			{
				info.AddValue("FabricIntegratorUnableToFindBoundLocalDatabaseException_DatabaseId", this.DatabaseId, typeof(string));
			}
			if (this.ServiceUri != null)
			{
				info.AddValue("FabricIntegratorUnableToFindBoundLocalDatabaseException_ServiceUri", this.ServiceUri, typeof(string));
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002E000 File Offset: 0x0002C200
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Fabric Integrator failed to find local database for bound database '{0}' and bound service {1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : ((this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceUri != null) ? this.ServiceUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceUri != null) ? this.ServiceUri.MarkIfInternal() : string.Empty) : ((this.ServiceUri != null) ? this.ServiceUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0002E0DA File Offset: 0x0002C2DA
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

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002E0F8 File Offset: 0x0002C2F8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002E264 File Offset: 0x0002C464
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002E26D File Offset: 0x0002C46D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002E276 File Offset: 0x0002C476
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002E264 File Offset: 0x0002C464
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002E280 File Offset: 0x0002C480
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

		// Token: 0x0400031C RID: 796
		private string creationMessage;

		// Token: 0x0400031D RID: 797
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400031E RID: 798
		private string m_databaseId;

		// Token: 0x0400031F RID: 799
		private string m_serviceUri;
	}
}
