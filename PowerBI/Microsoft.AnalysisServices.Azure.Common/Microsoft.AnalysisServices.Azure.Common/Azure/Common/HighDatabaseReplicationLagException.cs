using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Storage.Database;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000111 RID: 273
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class HighDatabaseReplicationLagException : DatabaseException
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x00035ECC File Offset: 0x000340CC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00035ED4 File Offset: 0x000340D4
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00035EDC File Offset: 0x000340DC
		public string LagInSec
		{
			get
			{
				return this.m_lagInSec;
			}
			protected set
			{
				this.m_lagInSec = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00035EE5 File Offset: 0x000340E5
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x00035EED File Offset: 0x000340ED
		public string PrimaryServerName
		{
			get
			{
				return this.m_primaryServerName;
			}
			protected set
			{
				this.m_primaryServerName = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00035EF6 File Offset: 0x000340F6
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x00035EFE File Offset: 0x000340FE
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

		// Token: 0x06000DCE RID: 3534 RVA: 0x00035F07 File Offset: 0x00034107
		public HighDatabaseReplicationLagException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00035F25 File Offset: 0x00034125
		public HighDatabaseReplicationLagException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00035F3C File Offset: 0x0003413C
		public HighDatabaseReplicationLagException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00035F5C File Offset: 0x0003415C
		protected HighDatabaseReplicationLagException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("HighDatabaseReplicationLagException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.LagInSec = (string)info.GetValue("HighDatabaseReplicationLagException_LagInSec", typeof(string));
			}
			catch (SerializationException)
			{
				this.LagInSec = null;
			}
			try
			{
				this.PrimaryServerName = (string)info.GetValue("HighDatabaseReplicationLagException_PrimaryServerName", typeof(string));
			}
			catch (SerializationException)
			{
				this.PrimaryServerName = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("HighDatabaseReplicationLagException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("HighDatabaseReplicationLagException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000360A4 File Offset: 0x000342A4
		public HighDatabaseReplicationLagException(string lagInSec, string primaryServerName, string databaseName)
		{
			this.LagInSec = lagInSec;
			this.PrimaryServerName = primaryServerName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000360C8 File Offset: 0x000342C8
		public HighDatabaseReplicationLagException(string lagInSec, string primaryServerName, string databaseName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.LagInSec = lagInSec;
			this.PrimaryServerName = primaryServerName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000360F6 File Offset: 0x000342F6
		public HighDatabaseReplicationLagException(string lagInSec, string primaryServerName, string databaseName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.LagInSec = lagInSec;
			this.PrimaryServerName = primaryServerName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003612C File Offset: 0x0003432C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00036163 File Offset: 0x00034363
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003616C File Offset: 0x0003436C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(HighDatabaseReplicationLagException))
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

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003623C File Offset: 0x0003443C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("HighDatabaseReplicationLagException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("HighDatabaseReplicationLagException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.LagInSec != null)
			{
				info.AddValue("HighDatabaseReplicationLagException_LagInSec", this.LagInSec, typeof(string));
			}
			if (this.PrimaryServerName != null)
			{
				info.AddValue("HighDatabaseReplicationLagException_PrimaryServerName", this.PrimaryServerName, typeof(string));
			}
			if (this.DatabaseName != null)
			{
				info.AddValue("HighDatabaseReplicationLagException_DatabaseName", this.DatabaseName, typeof(string));
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00036300 File Offset: 0x00034500
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Database replication lag is '{0}'s which is too high. Primary server: {1}, database: {2}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.LagInSec != null) ? this.LagInSec.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.LagInSec != null) ? this.LagInSec.MarkIfInternal() : string.Empty) : ((this.LagInSec != null) ? this.LagInSec.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.PrimaryServerName != null) ? this.PrimaryServerName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.PrimaryServerName != null) ? this.PrimaryServerName.MarkIfInternal() : string.Empty) : ((this.PrimaryServerName != null) ? this.PrimaryServerName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00036439 File Offset: 0x00034639
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

		// Token: 0x06000DDB RID: 3547 RVA: 0x00036458 File Offset: 0x00034658
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "LagInSec={0}", (this.LagInSec != null) ? this.LagInSec.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "LagInSec={0}", (this.LagInSec != null) ? this.LagInSec.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "LagInSec={0}", (this.LagInSec != null) ? this.LagInSec.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "PrimaryServerName={0}", (this.PrimaryServerName != null) ? this.PrimaryServerName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "PrimaryServerName={0}", (this.PrimaryServerName != null) ? this.PrimaryServerName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "PrimaryServerName={0}", (this.PrimaryServerName != null) ? this.PrimaryServerName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003666C File Offset: 0x0003486C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00036675 File Offset: 0x00034875
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003667E File Offset: 0x0003487E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003666C File Offset: 0x0003486C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00036688 File Offset: 0x00034888
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

		// Token: 0x0400035C RID: 860
		private string creationMessage;

		// Token: 0x0400035D RID: 861
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400035E RID: 862
		private string m_lagInSec;

		// Token: 0x0400035F RID: 863
		private string m_primaryServerName;

		// Token: 0x04000360 RID: 864
		private string m_databaseName;
	}
}
