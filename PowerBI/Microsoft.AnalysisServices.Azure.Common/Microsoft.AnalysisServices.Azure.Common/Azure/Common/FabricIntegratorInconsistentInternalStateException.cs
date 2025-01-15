using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000FE RID: 254
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorInconsistentInternalStateException : MonitoredException
	{
		// Token: 0x06000C3B RID: 3131 RVA: 0x0002E46C File Offset: 0x0002C66C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0002E474 File Offset: 0x0002C674
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x0002E47C File Offset: 0x0002C67C
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

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0002E485 File Offset: 0x0002C685
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x0002E48D File Offset: 0x0002C68D
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

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0002E496 File Offset: 0x0002C696
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x0002E49E File Offset: 0x0002C69E
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

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0002E4A7 File Offset: 0x0002C6A7
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x0002E4AF File Offset: 0x0002C6AF
		public string LocalDatabaseId
		{
			get
			{
				return this.m_localDatabaseId;
			}
			protected set
			{
				this.m_localDatabaseId = value;
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002E4B8 File Offset: 0x0002C6B8
		public FabricIntegratorInconsistentInternalStateException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002E4DB File Offset: 0x0002C6DB
		public FabricIntegratorInconsistentInternalStateException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002E4F2 File Offset: 0x0002C6F2
		public FabricIntegratorInconsistentInternalStateException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002E510 File Offset: 0x0002C710
		protected FabricIntegratorInconsistentInternalStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorInconsistentInternalStateException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Reason = (string)info.GetValue("FabricIntegratorInconsistentInternalStateException_Reason", typeof(string));
			}
			catch (SerializationException)
			{
				this.Reason = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("FabricIntegratorInconsistentInternalStateException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.ServiceUri = (string)info.GetValue("FabricIntegratorInconsistentInternalStateException_ServiceUri", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceUri = null;
			}
			try
			{
				this.LocalDatabaseId = (string)info.GetValue("FabricIntegratorInconsistentInternalStateException_LocalDatabaseId", typeof(string));
			}
			catch (SerializationException)
			{
				this.LocalDatabaseId = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorInconsistentInternalStateException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002E690 File Offset: 0x0002C890
		public FabricIntegratorInconsistentInternalStateException(string reason, string databaseName, string serviceUri, string localDatabaseId)
		{
			this.Reason = reason;
			this.DatabaseName = databaseName;
			this.ServiceUri = serviceUri;
			this.LocalDatabaseId = localDatabaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002E6BC File Offset: 0x0002C8BC
		public FabricIntegratorInconsistentInternalStateException(string reason, string databaseName, string serviceUri, string localDatabaseId, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Reason = reason;
			this.DatabaseName = databaseName;
			this.ServiceUri = serviceUri;
			this.LocalDatabaseId = localDatabaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002E6F2 File Offset: 0x0002C8F2
		public FabricIntegratorInconsistentInternalStateException(string reason, string databaseName, string serviceUri, string localDatabaseId, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Reason = reason;
			this.DatabaseName = databaseName;
			this.ServiceUri = serviceUri;
			this.LocalDatabaseId = localDatabaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002E730 File Offset: 0x0002C930
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002E768 File Offset: 0x0002C968
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorInconsistentInternalStateException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorInconsistentInternalStateException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Reason != null)
			{
				info.AddValue("FabricIntegratorInconsistentInternalStateException_Reason", this.Reason, typeof(string));
			}
			if (this.DatabaseName != null)
			{
				info.AddValue("FabricIntegratorInconsistentInternalStateException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.ServiceUri != null)
			{
				info.AddValue("FabricIntegratorInconsistentInternalStateException_ServiceUri", this.ServiceUri, typeof(string));
			}
			if (this.LocalDatabaseId != null)
			{
				info.AddValue("FabricIntegratorInconsistentInternalStateException_LocalDatabaseId", this.LocalDatabaseId, typeof(string));
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002E850 File Offset: 0x0002CA50
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Fabric Integrator detects inconsistent internal state: Reason={0}, DatbaseMoniker={1}, Service={2}, LocalDatabaseId={3}.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Reason != null) ? this.Reason.MarkIfInternal() : string.Empty) : ((this.Reason != null) ? this.Reason.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceUri != null) ? this.ServiceUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceUri != null) ? this.ServiceUri.MarkIfInternal() : string.Empty) : ((this.ServiceUri != null) ? this.ServiceUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.LocalDatabaseId != null) ? this.LocalDatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.LocalDatabaseId != null) ? this.LocalDatabaseId.MarkIfInternal() : string.Empty) : ((this.LocalDatabaseId != null) ? this.LocalDatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0002E9FA File Offset: 0x0002CBFA
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

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002EA18 File Offset: 0x0002CC18
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "LocalDatabaseId={0}", (this.LocalDatabaseId != null) ? this.LocalDatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "LocalDatabaseId={0}", (this.LocalDatabaseId != null) ? this.LocalDatabaseId.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "LocalDatabaseId={0}", (this.LocalDatabaseId != null) ? this.LocalDatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002ECD4 File Offset: 0x0002CED4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002ECDD File Offset: 0x0002CEDD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002ECE6 File Offset: 0x0002CEE6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002ECD4 File Offset: 0x0002CED4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002ECF0 File Offset: 0x0002CEF0
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

		// Token: 0x04000320 RID: 800
		private string creationMessage;

		// Token: 0x04000321 RID: 801
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000322 RID: 802
		private string m_reason;

		// Token: 0x04000323 RID: 803
		private string m_databaseName;

		// Token: 0x04000324 RID: 804
		private string m_serviceUri;

		// Token: 0x04000325 RID: 805
		private string m_localDatabaseId;
	}
}
