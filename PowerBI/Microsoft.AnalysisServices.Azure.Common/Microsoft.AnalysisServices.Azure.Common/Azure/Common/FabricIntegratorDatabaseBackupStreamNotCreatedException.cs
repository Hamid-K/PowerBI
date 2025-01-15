using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000F8 RID: 248
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorDatabaseBackupStreamNotCreatedException : MonitoredException
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002BB5C File Offset: 0x00029D5C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0002BB64 File Offset: 0x00029D64
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x0002BB6C File Offset: 0x00029D6C
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

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002BB75 File Offset: 0x00029D75
		public FabricIntegratorDatabaseBackupStreamNotCreatedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002BB89 File Offset: 0x00029D89
		public FabricIntegratorDatabaseBackupStreamNotCreatedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002BBA0 File Offset: 0x00029DA0
		public FabricIntegratorDatabaseBackupStreamNotCreatedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002BBC0 File Offset: 0x00029DC0
		protected FabricIntegratorDatabaseBackupStreamNotCreatedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorDatabaseBackupStreamNotCreatedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseId = (string)info.GetValue("FabricIntegratorDatabaseBackupStreamNotCreatedException_DatabaseId", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseId = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorDatabaseBackupStreamNotCreatedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002BC94 File Offset: 0x00029E94
		public FabricIntegratorDatabaseBackupStreamNotCreatedException(string databaseId, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002BCB2 File Offset: 0x00029EB2
		public FabricIntegratorDatabaseBackupStreamNotCreatedException(string databaseId, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002BCD8 File Offset: 0x00029ED8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002BD0F File Offset: 0x00029F0F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002BD18 File Offset: 0x00029F18
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FabricIntegratorDatabaseBackupStreamNotCreatedException))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ANCommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002BDE8 File Offset: 0x00029FE8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorDatabaseBackupStreamNotCreatedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorDatabaseBackupStreamNotCreatedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseId != null)
			{
				info.AddValue("FabricIntegratorDatabaseBackupStreamNotCreatedException_DatabaseId", this.DatabaseId, typeof(string));
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002BE68 File Offset: 0x0002A068
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Fabric Host failed to create a stream for database: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : ((this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002BEE3 File Offset: 0x0002A0E3
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

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002BF00 File Offset: 0x0002A100
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002BFC4 File Offset: 0x0002A1C4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002BFCD File Offset: 0x0002A1CD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002BFD6 File Offset: 0x0002A1D6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002BFC4 File Offset: 0x0002A1C4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002BFE0 File Offset: 0x0002A1E0
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

		// Token: 0x0400030B RID: 779
		private string creationMessage;

		// Token: 0x0400030C RID: 780
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400030D RID: 781
		private string m_databaseId;
	}
}
