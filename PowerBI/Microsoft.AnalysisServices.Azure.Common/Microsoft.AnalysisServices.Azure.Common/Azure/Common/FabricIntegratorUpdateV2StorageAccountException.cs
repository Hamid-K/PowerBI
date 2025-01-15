using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000110 RID: 272
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorUpdateV2StorageAccountException : MonitoredException
	{
		// Token: 0x06000DAF RID: 3503 RVA: 0x000356B4 File Offset: 0x000338B4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x000356BC File Offset: 0x000338BC
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x000356C4 File Offset: 0x000338C4
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

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x000356CD File Offset: 0x000338CD
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x000356D5 File Offset: 0x000338D5
		public string StorageAccountName
		{
			get
			{
				return this.m_storageAccountName;
			}
			protected set
			{
				this.m_storageAccountName = value;
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000356DE File Offset: 0x000338DE
		public FabricIntegratorUpdateV2StorageAccountException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000356F7 File Offset: 0x000338F7
		public FabricIntegratorUpdateV2StorageAccountException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003570E File Offset: 0x0003390E
		public FabricIntegratorUpdateV2StorageAccountException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003572C File Offset: 0x0003392C
		protected FabricIntegratorUpdateV2StorageAccountException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorUpdateV2StorageAccountException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("FabricIntegratorUpdateV2StorageAccountException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.StorageAccountName = (string)info.GetValue("FabricIntegratorUpdateV2StorageAccountException_StorageAccountName", typeof(string));
			}
			catch (SerializationException)
			{
				this.StorageAccountName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorUpdateV2StorageAccountException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003583C File Offset: 0x00033A3C
		public FabricIntegratorUpdateV2StorageAccountException(string databaseName, string storageAccountName)
		{
			this.DatabaseName = databaseName;
			this.StorageAccountName = storageAccountName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00035859 File Offset: 0x00033A59
		public FabricIntegratorUpdateV2StorageAccountException(string databaseName, string storageAccountName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.StorageAccountName = storageAccountName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003587E File Offset: 0x00033A7E
		public FabricIntegratorUpdateV2StorageAccountException(string databaseName, string storageAccountName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.StorageAccountName = storageAccountName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000358AC File Offset: 0x00033AAC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000358E3 File Offset: 0x00033AE3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000358EC File Offset: 0x00033AEC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FabricIntegratorUpdateV2StorageAccountException))
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

		// Token: 0x06000DBE RID: 3518 RVA: 0x000359BC File Offset: 0x00033BBC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorUpdateV2StorageAccountException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorUpdateV2StorageAccountException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("FabricIntegratorUpdateV2StorageAccountException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.StorageAccountName != null)
			{
				info.AddValue("FabricIntegratorUpdateV2StorageAccountException_StorageAccountName", this.StorageAccountName, typeof(string));
			}
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00035A60 File Offset: 0x00033C60
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Updating v2 storage account name for database {0} to storage account {1} failed", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.StorageAccountName != null) ? this.StorageAccountName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.StorageAccountName != null) ? this.StorageAccountName.MarkIfInternal() : string.Empty) : ((this.StorageAccountName != null) ? this.StorageAccountName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00035B3A File Offset: 0x00033D3A
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

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00035B58 File Offset: 0x00033D58
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "StorageAccountName={0}", (this.StorageAccountName != null) ? this.StorageAccountName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "StorageAccountName={0}", (this.StorageAccountName != null) ? this.StorageAccountName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "StorageAccountName={0}", (this.StorageAccountName != null) ? this.StorageAccountName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00035CC4 File Offset: 0x00033EC4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00035CCD File Offset: 0x00033ECD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00035CD6 File Offset: 0x00033ED6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00035CC4 File Offset: 0x00033EC4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00035CE0 File Offset: 0x00033EE0
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

		// Token: 0x04000358 RID: 856
		private string creationMessage;

		// Token: 0x04000359 RID: 857
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400035A RID: 858
		private string m_databaseName;

		// Token: 0x0400035B RID: 859
		private string m_storageAccountName;
	}
}
