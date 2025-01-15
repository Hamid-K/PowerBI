using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000C2 RID: 194
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DatabaseMappingAlreadyExistException : ServiceDatabaseMappingOperationFailed
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x00015F14 File Offset: 0x00014114
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00015F1C File Offset: 0x0001411C
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00015F24 File Offset: 0x00014124
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

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00015F2D File Offset: 0x0001412D
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00015F35 File Offset: 0x00014135
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

		// Token: 0x0600073F RID: 1855 RVA: 0x00015F3E File Offset: 0x0001413E
		public DatabaseMappingAlreadyExistException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00015F57 File Offset: 0x00014157
		public DatabaseMappingAlreadyExistException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00015F6E File Offset: 0x0001416E
		public DatabaseMappingAlreadyExistException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00015F8C File Offset: 0x0001418C
		protected DatabaseMappingAlreadyExistException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DatabaseMappingAlreadyExistException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Service = (string)info.GetValue("DatabaseMappingAlreadyExistException_Service", typeof(string));
			}
			catch (SerializationException)
			{
				this.Service = null;
			}
			try
			{
				this.Database = (string)info.GetValue("DatabaseMappingAlreadyExistException_Database", typeof(string));
			}
			catch (SerializationException)
			{
				this.Database = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DatabaseMappingAlreadyExistException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001609C File Offset: 0x0001429C
		public DatabaseMappingAlreadyExistException(string service, string database)
		{
			this.Service = service;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000160B9 File Offset: 0x000142B9
		public DatabaseMappingAlreadyExistException(string service, string database, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Service = service;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000160DE File Offset: 0x000142DE
		public DatabaseMappingAlreadyExistException(string service, string database, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Service = service;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001610C File Offset: 0x0001430C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00016144 File Offset: 0x00014344
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DatabaseMappingAlreadyExistException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DatabaseMappingAlreadyExistException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Service != null)
			{
				info.AddValue("DatabaseMappingAlreadyExistException_Service", this.Service, typeof(string));
			}
			if (this.Database != null)
			{
				info.AddValue("DatabaseMappingAlreadyExistException_Database", this.Database, typeof(string));
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000161E8 File Offset: 0x000143E8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to create mapping between {0} and {1} because the binding already exists", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : ((this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : ((this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x000162C2 File Offset: 0x000144C2
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

		// Token: 0x0600074B RID: 1867 RVA: 0x000162E0 File Offset: 0x000144E0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001644C File Offset: 0x0001464C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00016455 File Offset: 0x00014655
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001645E File Offset: 0x0001465E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001644C File Offset: 0x0001464C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00016468 File Offset: 0x00014668
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

		// Token: 0x04000253 RID: 595
		private string creationMessage;

		// Token: 0x04000254 RID: 596
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000255 RID: 597
		private string m_service;

		// Token: 0x04000256 RID: 598
		private string m_database;
	}
}
