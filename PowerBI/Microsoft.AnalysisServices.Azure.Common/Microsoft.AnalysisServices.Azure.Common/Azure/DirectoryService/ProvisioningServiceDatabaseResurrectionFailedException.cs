using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.DirectoryService
{
	// Token: 0x02000025 RID: 37
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ProvisioningServiceDatabaseResurrectionFailedException : DirectoryServiceException
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000C62C File Offset: 0x0000A82C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000C634 File Offset: 0x0000A834
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000C63C File Offset: 0x0000A83C
		public string DatabaseFullName
		{
			get
			{
				return this.m_databaseFullName;
			}
			protected set
			{
				this.m_databaseFullName = value;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000C645 File Offset: 0x0000A845
		public ProvisioningServiceDatabaseResurrectionFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000C659 File Offset: 0x0000A859
		public ProvisioningServiceDatabaseResurrectionFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000C670 File Offset: 0x0000A870
		public ProvisioningServiceDatabaseResurrectionFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000C690 File Offset: 0x0000A890
		protected ProvisioningServiceDatabaseResurrectionFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ProvisioningServiceDatabaseResurrectionFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseFullName = (string)info.GetValue("ProvisioningServiceDatabaseResurrectionFailedException_DatabaseFullName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseFullName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ProvisioningServiceDatabaseResurrectionFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000C764 File Offset: 0x0000A964
		public ProvisioningServiceDatabaseResurrectionFailedException(string databaseFullName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseFullName = databaseFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000C782 File Offset: 0x0000A982
		public ProvisioningServiceDatabaseResurrectionFailedException(string databaseFullName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseFullName = databaseFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000C7DF File Offset: 0x0000A9DF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ProvisioningServiceDatabaseResurrectionFailedException))
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

		// Token: 0x0600026B RID: 619 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ProvisioningServiceDatabaseResurrectionFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ProvisioningServiceDatabaseResurrectionFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseFullName != null)
			{
				info.AddValue("ProvisioningServiceDatabaseResurrectionFailedException_DatabaseFullName", this.DatabaseFullName, typeof(string));
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000C938 File Offset: 0x0000AB38
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Fail to resurrect database '{0}'", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseFullName != null) ? this.DatabaseFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfInternal() : string.Empty) : ((this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000C9B3 File Offset: 0x0000ABB3
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

		// Token: 0x0600026E RID: 622 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000CA94 File Offset: 0x0000AC94
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000CA9D File Offset: 0x0000AC9D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000CAA6 File Offset: 0x0000ACA6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000CA94 File Offset: 0x0000AC94
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
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

		// Token: 0x04000073 RID: 115
		private string creationMessage;

		// Token: 0x04000074 RID: 116
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000075 RID: 117
		private string m_databaseFullName;
	}
}
