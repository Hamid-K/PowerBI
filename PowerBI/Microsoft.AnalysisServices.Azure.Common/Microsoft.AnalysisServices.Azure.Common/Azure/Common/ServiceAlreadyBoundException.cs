using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000C3 RID: 195
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ServiceAlreadyBoundException : ServiceDatabaseMappingOperationFailed
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00016654 File Offset: 0x00014854
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001665C File Offset: 0x0001485C
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x00016664 File Offset: 0x00014864
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

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001666D File Offset: 0x0001486D
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x00016675 File Offset: 0x00014875
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

		// Token: 0x06000756 RID: 1878 RVA: 0x0001667E File Offset: 0x0001487E
		public ServiceAlreadyBoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00016697 File Offset: 0x00014897
		public ServiceAlreadyBoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000166AE File Offset: 0x000148AE
		public ServiceAlreadyBoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000166CC File Offset: 0x000148CC
		protected ServiceAlreadyBoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ServiceAlreadyBoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Service = (string)info.GetValue("ServiceAlreadyBoundException_Service", typeof(string));
			}
			catch (SerializationException)
			{
				this.Service = null;
			}
			try
			{
				this.Database = (string)info.GetValue("ServiceAlreadyBoundException_Database", typeof(string));
			}
			catch (SerializationException)
			{
				this.Database = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ServiceAlreadyBoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000167DC File Offset: 0x000149DC
		public ServiceAlreadyBoundException(string service, string database)
		{
			this.Service = service;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000167F9 File Offset: 0x000149F9
		public ServiceAlreadyBoundException(string service, string database, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Service = service;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001681E File Offset: 0x00014A1E
		public ServiceAlreadyBoundException(string service, string database, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Service = service;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001684C File Offset: 0x00014A4C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00016884 File Offset: 0x00014A84
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ServiceAlreadyBoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ServiceAlreadyBoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Service != null)
			{
				info.AddValue("ServiceAlreadyBoundException_Service", this.Service, typeof(string));
			}
			if (this.Database != null)
			{
				info.AddValue("ServiceAlreadyBoundException_Database", this.Database, typeof(string));
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00016928 File Offset: 0x00014B28
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to create mapping between {0} and {1} because the service has already bound to other database", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : ((this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : ((this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00016A02 File Offset: 0x00014C02
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

		// Token: 0x06000762 RID: 1890 RVA: 0x00016A20 File Offset: 0x00014C20
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00016B8C File Offset: 0x00014D8C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00016B95 File Offset: 0x00014D95
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00016B9E File Offset: 0x00014D9E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00016B8C File Offset: 0x00014D8C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00016BA8 File Offset: 0x00014DA8
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

		// Token: 0x04000257 RID: 599
		private string creationMessage;

		// Token: 0x04000258 RID: 600
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000259 RID: 601
		private string m_service;

		// Token: 0x0400025A RID: 602
		private string m_database;
	}
}
