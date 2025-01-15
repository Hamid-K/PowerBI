using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000C6 RID: 198
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DatabaseNotBoundException : ServiceDatabaseMappingOperationFailed
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x00017B44 File Offset: 0x00015D44
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00017B4C File Offset: 0x00015D4C
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x00017B54 File Offset: 0x00015D54
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

		// Token: 0x06000797 RID: 1943 RVA: 0x00017B5D File Offset: 0x00015D5D
		public DatabaseNotBoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00017B71 File Offset: 0x00015D71
		public DatabaseNotBoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00017B88 File Offset: 0x00015D88
		public DatabaseNotBoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00017BA8 File Offset: 0x00015DA8
		protected DatabaseNotBoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DatabaseNotBoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Database = (string)info.GetValue("DatabaseNotBoundException_Database", typeof(string));
			}
			catch (SerializationException)
			{
				this.Database = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DatabaseNotBoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00017C7C File Offset: 0x00015E7C
		public DatabaseNotBoundException(string database, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00017C9A File Offset: 0x00015E9A
		public DatabaseNotBoundException(string database, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Database = database;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00017CC0 File Offset: 0x00015EC0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00017CF7 File Offset: 0x00015EF7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00017D00 File Offset: 0x00015F00
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DatabaseNotBoundException))
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

		// Token: 0x060007A0 RID: 1952 RVA: 0x00017DD0 File Offset: 0x00015FD0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DatabaseNotBoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DatabaseNotBoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Database != null)
			{
				info.AddValue("DatabaseNotBoundException_Database", this.Database, typeof(string));
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00017E50 File Offset: 0x00016050
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Database {0} is not bound to any service", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : ((this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00017ECB File Offset: 0x000160CB
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

		// Token: 0x060007A3 RID: 1955 RVA: 0x00017EE8 File Offset: 0x000160E8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00017FAC File Offset: 0x000161AC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00017FB5 File Offset: 0x000161B5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00017FBE File Offset: 0x000161BE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00017FAC File Offset: 0x000161AC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00017FC8 File Offset: 0x000161C8
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

		// Token: 0x04000262 RID: 610
		private string creationMessage;

		// Token: 0x04000263 RID: 611
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000264 RID: 612
		private string m_database;
	}
}
