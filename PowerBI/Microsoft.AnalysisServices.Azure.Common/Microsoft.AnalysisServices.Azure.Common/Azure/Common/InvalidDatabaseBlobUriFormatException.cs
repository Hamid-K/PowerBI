using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000CB RID: 203
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class InvalidDatabaseBlobUriFormatException : StateManagerBaseException
	{
		// Token: 0x06000802 RID: 2050 RVA: 0x00019DEC File Offset: 0x00017FEC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00019DF4 File Offset: 0x00017FF4
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00019DFC File Offset: 0x00017FFC
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

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00019E05 File Offset: 0x00018005
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x00019E0D File Offset: 0x0001800D
		public string PassedUri
		{
			get
			{
				return this.m_passedUri;
			}
			protected set
			{
				this.m_passedUri = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00019E16 File Offset: 0x00018016
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x00019E1E File Offset: 0x0001801E
		public string AllowedUriFormat
		{
			get
			{
				return this.m_allowedUriFormat;
			}
			protected set
			{
				this.m_allowedUriFormat = value;
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00019E27 File Offset: 0x00018027
		public InvalidDatabaseBlobUriFormatException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00019E45 File Offset: 0x00018045
		public InvalidDatabaseBlobUriFormatException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00019E5C File Offset: 0x0001805C
		public InvalidDatabaseBlobUriFormatException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00019E7C File Offset: 0x0001807C
		protected InvalidDatabaseBlobUriFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidDatabaseBlobUriFormatException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Database = (string)info.GetValue("InvalidDatabaseBlobUriFormatException_Database", typeof(string));
			}
			catch (SerializationException)
			{
				this.Database = null;
			}
			try
			{
				this.PassedUri = (string)info.GetValue("InvalidDatabaseBlobUriFormatException_PassedUri", typeof(string));
			}
			catch (SerializationException)
			{
				this.PassedUri = null;
			}
			try
			{
				this.AllowedUriFormat = (string)info.GetValue("InvalidDatabaseBlobUriFormatException_AllowedUriFormat", typeof(string));
			}
			catch (SerializationException)
			{
				this.AllowedUriFormat = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("InvalidDatabaseBlobUriFormatException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00019FC4 File Offset: 0x000181C4
		public InvalidDatabaseBlobUriFormatException(string database, string passedUri, string allowedUriFormat)
		{
			this.Database = database;
			this.PassedUri = passedUri;
			this.AllowedUriFormat = allowedUriFormat;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00019FE8 File Offset: 0x000181E8
		public InvalidDatabaseBlobUriFormatException(string database, string passedUri, string allowedUriFormat, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Database = database;
			this.PassedUri = passedUri;
			this.AllowedUriFormat = allowedUriFormat;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001A016 File Offset: 0x00018216
		public InvalidDatabaseBlobUriFormatException(string database, string passedUri, string allowedUriFormat, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Database = database;
			this.PassedUri = passedUri;
			this.AllowedUriFormat = allowedUriFormat;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001A04C File Offset: 0x0001824C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001A083 File Offset: 0x00018283
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001A08C File Offset: 0x0001828C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidDatabaseBlobUriFormatException))
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

		// Token: 0x06000813 RID: 2067 RVA: 0x0001A15C File Offset: 0x0001835C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidDatabaseBlobUriFormatException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InvalidDatabaseBlobUriFormatException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Database != null)
			{
				info.AddValue("InvalidDatabaseBlobUriFormatException_Database", this.Database, typeof(string));
			}
			if (this.PassedUri != null)
			{
				info.AddValue("InvalidDatabaseBlobUriFormatException_PassedUri", this.PassedUri, typeof(string));
			}
			if (this.AllowedUriFormat != null)
			{
				info.AddValue("InvalidDatabaseBlobUriFormatException_AllowedUriFormat", this.AllowedUriFormat, typeof(string));
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001A220 File Offset: 0x00018420
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Database {0} blob URI is in invalid format: {1}, allowed format: {2}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : ((this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.PassedUri != null) ? this.PassedUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.PassedUri != null) ? this.PassedUri.MarkIfInternal() : string.Empty) : ((this.PassedUri != null) ? this.PassedUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.AllowedUriFormat != null) ? this.AllowedUriFormat.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.AllowedUriFormat != null) ? this.AllowedUriFormat.MarkIfInternal() : string.Empty) : ((this.AllowedUriFormat != null) ? this.AllowedUriFormat.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001A359 File Offset: 0x00018559
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

		// Token: 0x06000816 RID: 2070 RVA: 0x0001A378 File Offset: 0x00018578
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "PassedUri={0}", (this.PassedUri != null) ? this.PassedUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "PassedUri={0}", (this.PassedUri != null) ? this.PassedUri.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "PassedUri={0}", (this.PassedUri != null) ? this.PassedUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AllowedUriFormat={0}", (this.AllowedUriFormat != null) ? this.AllowedUriFormat.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AllowedUriFormat={0}", (this.AllowedUriFormat != null) ? this.AllowedUriFormat.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "AllowedUriFormat={0}", (this.AllowedUriFormat != null) ? this.AllowedUriFormat.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001A58C File Offset: 0x0001878C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001A595 File Offset: 0x00018795
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001A59E File Offset: 0x0001879E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001A58C File Offset: 0x0001878C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001A5A8 File Offset: 0x000187A8
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

		// Token: 0x04000273 RID: 627
		private string creationMessage;

		// Token: 0x04000274 RID: 628
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000275 RID: 629
		private string m_database;

		// Token: 0x04000276 RID: 630
		private string m_passedUri;

		// Token: 0x04000277 RID: 631
		private string m_allowedUriFormat;
	}
}
