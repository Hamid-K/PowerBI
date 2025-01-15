using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000CA RID: 202
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DeleteDatabaseNotUnboundException : StateManagerBaseException
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x000196AC File Offset: 0x000178AC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000196B4 File Offset: 0x000178B4
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x000196BC File Offset: 0x000178BC
		public string Database
		{
			get
			{
				return this.m_database;
			}
			protected set
			{
				this.m_database = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x000196C5 File Offset: 0x000178C5
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x000196CD File Offset: 0x000178CD
		public string Service
		{
			get
			{
				return this.m_service;
			}
			protected set
			{
				this.m_service = value;
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000196D6 File Offset: 0x000178D6
		public DeleteDatabaseNotUnboundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000196EF File Offset: 0x000178EF
		public DeleteDatabaseNotUnboundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00019706 File Offset: 0x00017906
		public DeleteDatabaseNotUnboundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00019724 File Offset: 0x00017924
		protected DeleteDatabaseNotUnboundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DeleteDatabaseNotUnboundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Database = (string)info.GetValue("DeleteDatabaseNotUnboundException_Database", typeof(string));
			}
			catch (SerializationException)
			{
				this.Database = null;
			}
			try
			{
				this.Service = (string)info.GetValue("DeleteDatabaseNotUnboundException_Service", typeof(string));
			}
			catch (SerializationException)
			{
				this.Service = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DeleteDatabaseNotUnboundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00019834 File Offset: 0x00017A34
		public DeleteDatabaseNotUnboundException(string database, string service)
		{
			this.Database = database;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00019851 File Offset: 0x00017A51
		public DeleteDatabaseNotUnboundException(string database, string service, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Database = database;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00019876 File Offset: 0x00017A76
		public DeleteDatabaseNotUnboundException(string database, string service, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Database = database;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000198A4 File Offset: 0x00017AA4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000198DC File Offset: 0x00017ADC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DeleteDatabaseNotUnboundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DeleteDatabaseNotUnboundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Database != null)
			{
				info.AddValue("DeleteDatabaseNotUnboundException_Database", this.Database, typeof(string));
			}
			if (this.Service != null)
			{
				info.AddValue("DeleteDatabaseNotUnboundException_Service", this.Service, typeof(string));
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00019980 File Offset: 0x00017B80
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to delete database {0} because the database is not unbound from service '{1}'", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : ((this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : ((this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00019A5A File Offset: 0x00017C5A
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

		// Token: 0x060007FC RID: 2044 RVA: 0x00019A78 File Offset: 0x00017C78
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00019BE4 File Offset: 0x00017DE4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00019BED File Offset: 0x00017DED
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00019BF6 File Offset: 0x00017DF6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00019BE4 File Offset: 0x00017DE4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00019C00 File Offset: 0x00017E00
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

		// Token: 0x0400026F RID: 623
		private string creationMessage;

		// Token: 0x04000270 RID: 624
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000271 RID: 625
		private string m_database;

		// Token: 0x04000272 RID: 626
		private string m_service;
	}
}
