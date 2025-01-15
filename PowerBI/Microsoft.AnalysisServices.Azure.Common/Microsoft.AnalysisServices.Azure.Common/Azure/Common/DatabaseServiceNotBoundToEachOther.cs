using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000C9 RID: 201
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DatabaseServiceNotBoundToEachOther : StateManagerBaseException
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x00018E94 File Offset: 0x00017094
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00018E9C File Offset: 0x0001709C
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00018EA4 File Offset: 0x000170A4
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

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00018EAD File Offset: 0x000170AD
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x00018EB5 File Offset: 0x000170B5
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

		// Token: 0x060007D8 RID: 2008 RVA: 0x00018EBE File Offset: 0x000170BE
		public DatabaseServiceNotBoundToEachOther()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00018ED7 File Offset: 0x000170D7
		public DatabaseServiceNotBoundToEachOther(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00018EEE File Offset: 0x000170EE
		public DatabaseServiceNotBoundToEachOther(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00018F0C File Offset: 0x0001710C
		protected DatabaseServiceNotBoundToEachOther(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DatabaseServiceNotBoundToEachOther_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Database = (string)info.GetValue("DatabaseServiceNotBoundToEachOther_Database", typeof(string));
			}
			catch (SerializationException)
			{
				this.Database = null;
			}
			try
			{
				this.Service = (string)info.GetValue("DatabaseServiceNotBoundToEachOther_Service", typeof(string));
			}
			catch (SerializationException)
			{
				this.Service = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DatabaseServiceNotBoundToEachOther_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001901C File Offset: 0x0001721C
		public DatabaseServiceNotBoundToEachOther(string database, string service)
		{
			this.Database = database;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00019039 File Offset: 0x00017239
		public DatabaseServiceNotBoundToEachOther(string database, string service, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Database = database;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001905E File Offset: 0x0001725E
		public DatabaseServiceNotBoundToEachOther(string database, string service, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Database = database;
			this.Service = service;
			this.ConstructorInternal(false);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001908C File Offset: 0x0001728C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000190C3 File Offset: 0x000172C3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000190CC File Offset: 0x000172CC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DatabaseServiceNotBoundToEachOther))
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

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001919C File Offset: 0x0001739C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DatabaseServiceNotBoundToEachOther_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DatabaseServiceNotBoundToEachOther_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Database != null)
			{
				info.AddValue("DatabaseServiceNotBoundToEachOther_Database", this.Database, typeof(string));
			}
			if (this.Service != null)
			{
				info.AddValue("DatabaseServiceNotBoundToEachOther_Service", this.Service, typeof(string));
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00019240 File Offset: 0x00017440
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Database {0} is not bound to Service '{1}' is not found in persistent storage", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : ((this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : ((this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001931A File Offset: 0x0001751A
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

		// Token: 0x060007E5 RID: 2021 RVA: 0x00019338 File Offset: 0x00017538
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Database={0}", (this.Database != null) ? this.Database.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Service={0}", (this.Service != null) ? this.Service.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000194A4 File Offset: 0x000176A4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000194AD File Offset: 0x000176AD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000194B6 File Offset: 0x000176B6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000194A4 File Offset: 0x000176A4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000194C0 File Offset: 0x000176C0
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

		// Token: 0x0400026B RID: 619
		private string creationMessage;

		// Token: 0x0400026C RID: 620
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400026D RID: 621
		private string m_database;

		// Token: 0x0400026E RID: 622
		private string m_service;
	}
}
