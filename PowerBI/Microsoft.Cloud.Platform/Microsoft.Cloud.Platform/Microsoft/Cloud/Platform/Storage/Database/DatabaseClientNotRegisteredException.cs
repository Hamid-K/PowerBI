using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000057 RID: 87
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DatabaseClientNotRegisteredException : DatabaseException
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00007558 File Offset: 0x00005758
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00007560 File Offset: 0x00005760
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00007568 File Offset: 0x00005768
		public string Client
		{
			get
			{
				return this.m_client;
			}
			protected set
			{
				this.m_client = value;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007571 File Offset: 0x00005771
		public DatabaseClientNotRegisteredException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007585 File Offset: 0x00005785
		public DatabaseClientNotRegisteredException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000759C File Offset: 0x0000579C
		public DatabaseClientNotRegisteredException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000075BC File Offset: 0x000057BC
		protected DatabaseClientNotRegisteredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DatabaseClientNotRegisteredException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Client = (string)info.GetValue("DatabaseClientNotRegisteredException_Client", typeof(string));
			}
			catch (SerializationException)
			{
				this.Client = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DatabaseClientNotRegisteredException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007690 File Offset: 0x00005890
		public DatabaseClientNotRegisteredException(string client, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Client = client;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000076AE File Offset: 0x000058AE
		public DatabaseClientNotRegisteredException(string client, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Client = client;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000076D4 File Offset: 0x000058D4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000770B File Offset: 0x0000590B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007714 File Offset: 0x00005914
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DatabaseClientNotRegisteredException))
			{
				TraceSourceBase<StorageTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<StorageTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<StorageTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000077E4 File Offset: 0x000059E4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DatabaseClientNotRegisteredException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DatabaseClientNotRegisteredException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Client != null)
			{
				info.AddValue("DatabaseClientNotRegisteredException_Client", this.Client, typeof(string));
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007864 File Offset: 0x00005A64
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Database client interface {0} is not registered", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.Client != null) ? this.Client.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Client != null) ? this.Client.MarkIfInternal() : string.Empty) : ((this.Client != null) ? this.Client.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000078E8 File Offset: 0x00005AE8
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

		// Token: 0x06000231 RID: 561 RVA: 0x00007908 File Offset: 0x00005B08
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Client={0}", new object[] { (this.Client != null) ? this.Client.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Client={0}", new object[] { (this.Client != null) ? this.Client.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Client={0}", new object[] { (this.Client != null) ? this.Client.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000079E7 File Offset: 0x00005BE7
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000079F0 File Offset: 0x00005BF0
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000079F9 File Offset: 0x00005BF9
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000079E7 File Offset: 0x00005BE7
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007A04 File Offset: 0x00005C04
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

		// Token: 0x040000E6 RID: 230
		private string creationMessage;

		// Token: 0x040000E7 RID: 231
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040000E8 RID: 232
		private string m_client;
	}
}
